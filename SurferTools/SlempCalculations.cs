using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using Shared;
using Surfer;

namespace SurferTools
{
    public partial class SpecialCalculations
    {
        private const string LutumC = "L";
        private const string CaCo3C = "Ca";
        private const string Os = "Os";
        private const string Aftrek = "Af";

        /// <summary>
        /// Calculate Slemp
        /// </summary>
        /// <param name="outGrid"></param>
        /// <param name="gwt"></param>
        /// <returns></returns>
        public bool CalculateSlemp(string outGrid, string gwt)
        {
            var ontwatering = CalculateOntwatering(gwt);

            // Lutum, CaCO3, OS, pH, GWT

            var lutumCoderingLocation = BepaalLutumCodering();
            var caCo3CoderingLocation = BepaalCaCo3Codering();
            var osCoderingLocation = BepaalOsCodering();
            var aftrekLocation = BepaalAftrek(lutumCoderingLocation, ontwatering, caCo3CoderingLocation, osCoderingLocation);

            var gridMathInput = new List<IGridMathInput>
            {
                _surferApp.NewGridMathInput(lutumCoderingLocation, LutumC),
                _surferApp.NewGridMathInput(osCoderingLocation, Os),
                _surferApp.NewGridMathInput(aftrekLocation, Aftrek)
            };

            var formula = $"MIN(0.005*({LutumC}+{Os}),0.2+0.001*{LutumC})+MIN(5,6.5-{LutumC}*0.5)-{Aftrek}";

            DeleteFile(outGrid);
            _surferApp.GridMath3(formula, gridMathInput.ToArray(), outGrid);

            return File.Exists(outGrid);
        }

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        private string BepaalAftrek(string lutumCoderingLocation, int ontwateringCode, string caCo3CoderingLocation, string osCoderingLocation)
        {


            var gridMathInput = new List<IGridMathInput>
            {
                _surferApp.NewGridMathInput(lutumCoderingLocation, LutumC),
                _surferApp.NewGridMathInput(caCo3CoderingLocation, CaCo3C),
                _surferApp.NewGridMathInput(osCoderingLocation, Os)
            };

            var tmpFile = Path.Combine(Path.GetTempPath(), "Aftrek.grd");
            var formulaLutum1 = $"IF({LutumC}=1,IF({ontwateringCode}<3,0.5,0.25)";

            var aftrekDummy = (1.25 - ontwateringCode * 0.25).ToString(CultureInfo.InvariantCulture);
            var caCo3_os_sm3 = $"IF({Os}<3,{aftrekDummy}+1,IF({Os}=3,{aftrekDummy}+0.75,{aftrekDummy}))";
            var caCo3_os_eq3 = $"IF({Os}<3,{aftrekDummy}+0.75,IF({Os}=3,{aftrekDummy}+0.5,{aftrekDummy}))";
            var caCo3_os_gt3 = $"IF({Os}<3,{aftrekDummy}+0.5,IF({Os}=3,{aftrekDummy}+0.25,{aftrekDummy}))";
            var formulaLutum2 = $"IF({LutumC}=2,IF({CaCo3C}<3,{caCo3_os_sm3},IF({CaCo3C}=3,{caCo3_os_eq3},{caCo3_os_gt3}))";

            var formulaLutumElse = $"MIN(1.5,(2.5-{ontwateringCode}*0.5))+MAX(0,1-{CaCo3C}*0.25)+MAX(0,1-{Os}*0.25)";

            var formula = $"{formulaLutum1},{formulaLutum2},{formulaLutumElse}))";

            DeleteFile(tmpFile);
            _surferApp.GridMath3(formula, gridMathInput.ToArray(), tmpFile);

            return tmpFile;
        }

        private string BepaalLutumCodering()
        {
            var gridMathInput = new List<IGridMathInput>
            {
                _surferApp.NewGridMathInput(SurferService.GetFullPath(_workingFolder, _fieldName, FormulaConstants.Lutum), FormulaConstants.Lutum)
            };

            var tmpFile = Path.Combine(Path.GetTempPath(), "LutumCodering.grd");

            DeleteFile(tmpFile);
            _surferApp.GridMath3($"IF({FormulaConstants.Lutum} > 35,1,IF({FormulaConstants.Lutum} > 25,2,IF({FormulaConstants.Lutum} > 17,3,IF({FormulaConstants.Lutum} > 8,4,5))))", gridMathInput.ToArray(), tmpFile);

            return tmpFile;
        }

        private string BepaalCaCo3Codering()
        {
            var gridMathInput = new List<IGridMathInput>
            {
                _surferApp.NewGridMathInput(SurferService.GetFullPath(_workingFolder, _fieldName, FormulaConstants.Ph), FormulaConstants.Ph),
                _surferApp.NewGridMathInput(SurferService.GetFullPath(_workingFolder, _fieldName, FormulaConstants.CaCo3), FormulaConstants.CaCo3)
            };

            var tmpFile = Path.Combine(Path.GetTempPath(), "CaCo3Codering.grd");

            DeleteFile(tmpFile);
            _surferApp.GridMath3($"IF({FormulaConstants.Ph} < 5,1,IF({FormulaConstants.Ph} > 6,2,IF({FormulaConstants.Ph} > 7,3,IF({FormulaConstants.CaCo3} < 2,4,5))))", gridMathInput.ToArray(), tmpFile);

            return tmpFile;
        }

        private string BepaalOsCodering()
        {
            var gridMathInput = new List<IGridMathInput>
            {
                _surferApp.NewGridMathInput(SurferService.GetFullPath(_workingFolder, _fieldName, FormulaConstants.Os), FormulaConstants.Os)
            };

            var tmpFile = Path.Combine(Path.GetTempPath(), "OsCodering.grd");

            DeleteFile(tmpFile);
            _surferApp.GridMath3($"IF({FormulaConstants.Os} < 1.5,1,IF({FormulaConstants.Os} < 2,2,IF({FormulaConstants.Os} < 3,3,IF({FormulaConstants.Os} < 4,4,5))))", gridMathInput.ToArray(), tmpFile);

            return tmpFile;
        }

        private static int CalculateOntwatering(string gwt)
        {
            return gwt switch
            {
                "I" => 1,
                "II" => 1,
                "III+" => 1,
                "V+" => 1,
                "III-" => 2,
                "V-" => 3,
                "IV" => 3,
                "VI" => 4,
                _ => 5
            };
        }


    }
}
