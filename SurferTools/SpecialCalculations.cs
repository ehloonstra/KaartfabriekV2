using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Shared;
using Surfer;

namespace SurferTools
{
    /// <summary>
    /// Class for special more complicated soil map calculations
    /// </summary>
    public class SpecialCalculations
    {
        private readonly string _workingFolder;
        private readonly string _fieldName;
        private readonly IApplication5 _surferApp;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workingFolder"></param>
        /// <param name="fieldName"></param>
        /// <param name="surferApp"></param>
        public SpecialCalculations(string workingFolder, string fieldName, IApplication5 surferApp)
        {
            _workingFolder = workingFolder;
            _fieldName = fieldName;
            _surferApp = surferApp;
        }

        //public bool CalculateBodemclassificatie(string outGrid, bool eolisch)
        //{
        //    // Moerig, OS, Lutum, Zandfactie, CaCO3
        //    return false;
        //}

        /// <summary>
        /// Calculate Bulkdichtheid
        /// </summary>
        /// <param name="outGrid"></param>
        /// <returns></returns>
        public bool CalculateBulkdichtheid(string outGrid)
        {
            var tmpOs = AftoppenOs();

            // TODO: In VB6-code wordt ook Lutum soms afgetopt

            // OS, Lutum, Zandfactie, M50
            var gridMathInput = new List<IGridMathInput>
            {
                _surferApp.NewGridMathInput(tmpOs, FormulaConstants.Os),
                _surferApp.NewGridMathInput(SurferService.GetFullPath(_workingFolder, _fieldName, FormulaConstants.Lutum), FormulaConstants.Lutum),
                _surferApp.NewGridMathInput(SurferService.GetFullPath(_workingFolder, _fieldName, FormulaConstants.Zandfractie), FormulaConstants.Zandfractie),
                _surferApp.NewGridMathInput(SurferService.GetFullPath(_workingFolder, _fieldName, FormulaConstants.M50), FormulaConstants.M50)
            };

            // ReSharper disable once ConvertToConstant.Local
            var formuleKlei = $"1 / (0.6117 + (0.003601 * {FormulaConstants.Lutum}) + (0.002172 * ({FormulaConstants.Os} ^ 2)) + (0.01715 * ln({FormulaConstants.Os})))";
            // ReSharper disable once ConvertToConstant.Local
            var formuleZand =
                $"1 / (-7.58 + (0.01791 * {FormulaConstants.Os}) + (0.0326 * 1) - (0.00338 * {FormulaConstants.M50}) + (0.00003937 * ((100-{FormulaConstants.Zandfractie}) ^ 2)) + (157.7 * ({FormulaConstants.M50} ^ -1)) + (1.522 * ln({FormulaConstants.M50})))";

            var formula = $"IF({FormulaConstants.Lutum}>=8,{formuleKlei},{formuleZand})";
            DeleteFile(outGrid);
            _surferApp.GridMath3(formula, gridMathInput.ToArray(), outGrid);
            return File.Exists(outGrid);
        }

        /// <summary>
        /// Calculate the monsterdata grid
        /// </summary>
        /// <param name="outGrid"></param>
        /// <param name="inputGrid"></param>
        /// <returns></returns>
        public bool CalculateMonsterpunten(string outGrid, string inputGrid)
        {
            var tcGridLocation = SurferService.GetFullPath(_workingFolder, _fieldName, inputGrid);
            if (File.Exists(tcGridLocation))
            {
                File.Copy(tcGridLocation, outGrid, true);
            }

            return true;
        }

        public bool CalculateSlemp(string outGrid)
        {
            // Lutum, CaCO3, OS, pH, GWT
            return false;
        }

        /// <summary>
        /// Calculate Waterdoorlatendheid
        /// </summary>
        /// <param name="outGrid"></param>
        /// <returns></returns>
        public bool CalculateWaterdoorlatendheid(string outGrid)
        {
            // TODO: check op negatieve waarde:
            // Bij hele lage waarden lutum en leem (sportvelden)
            // gaat de berekening niet goed

            var tmpOs = AftoppenOs();

            // Formula is getting too long (> 256):
            var tmpKsSterKlei = CalculateWaterdoorlatendheidKsSter(tmpOs, true);
            var tmpKsSterZand = CalculateWaterdoorlatendheidKsSter(tmpOs, false);

            var gridMathInput = new List<IGridMathInput>
            {
                _surferApp.NewGridMathInput(SurferService.GetFullPath(_workingFolder, _fieldName, FormulaConstants.Lutum), FormulaConstants.Lutum),
                _surferApp.NewGridMathInput(tmpKsSterKlei, "A"),
                _surferApp.NewGridMathInput(tmpKsSterZand, "B")
            };

            // ReSharper disable once ConvertToConstant.Local
            var formula = $"IF({FormulaConstants.Lutum}>=8,Exp(A),Exp(B))";
            Debug.WriteLine("Lengte formule: " + formula.Length);

            DeleteFile(outGrid);
            _surferApp.GridMath3(formula, gridMathInput.ToArray(), outGrid);
            return File.Exists(outGrid);
        }

        private string CalculateWaterdoorlatendheidKsSter(string tmpOs, bool forKlei)
        {
            var gridMathInput = new List<IGridMathInput>
            {
                _surferApp.NewGridMathInput(tmpOs, FormulaConstants.Os),
                _surferApp.NewGridMathInput(SurferService.GetFullPath(_workingFolder, _fieldName, FormulaConstants.Lutum), FormulaConstants.Lutum),
                _surferApp.NewGridMathInput(SurferService.GetFullPath(_workingFolder, _fieldName, FormulaConstants.Zandfractie), FormulaConstants.Zandfractie),
                _surferApp.NewGridMathInput(SurferService.GetFullPath(_workingFolder, _fieldName, FormulaConstants.M50), FormulaConstants.M50),
                _surferApp.NewGridMathInput(SurferService.GetFullPath(_workingFolder, _fieldName, FormulaConstants.Bulkdichtheid), FormulaConstants.Bulkdichtheid)
            };

            string tmpFile, formula;
            if (forKlei)
            {
                tmpFile = Path.Combine(Path.GetTempPath(), "KsSterKlei.grd");
                formula = $"-42.6 + (8.71 * {FormulaConstants.Os}) + (61.9 * {FormulaConstants.Bulkdichtheid}) - (20.79 * ({FormulaConstants.Bulkdichtheid} ^ 2)) - (0.2107 * ({FormulaConstants.Os} ^ 2)) - (0.01622 * {FormulaConstants.Lutum} * {FormulaConstants.Os}) - (5.382 * {FormulaConstants.Bulkdichtheid} * {FormulaConstants.Os})";
            }
            else
            {
                tmpFile = Path.Combine(Path.GetTempPath(), "KsSterZand.grd");
                formula = $"45.8 - (14.34 * {FormulaConstants.Bulkdichtheid}) + (0.001481 * ((100-{FormulaConstants.Zandfractie}) ^ 2)) - (27.5 * ({FormulaConstants.Bulkdichtheid} ^ -1)) - (0.891 * Ln((100-{FormulaConstants.Zandfractie}))) - (0.34 * Ln({FormulaConstants.Os}))";
            }

            DeleteFile(tmpFile);
            _surferApp.GridMath3(formula, gridMathInput.ToArray(), tmpFile);

            return tmpFile;
        }

        /// <summary>
        /// Calculate the Waterretentie
        /// </summary>
        /// <param name="outGrid"></param>
        /// <returns></returns>
        public bool CalculateWaterretentie(string outGrid)
        {
            // TODO: check op negatieve waarde:
            // Bij hele lage waarden lutum en leem (sportvelden)
            // gaat de berekening niet goed

            var tmpOs = AftoppenOs();

            // BetaS
            var tmpBetaS = CalculateWaterretentieBetaS(tmpOs);

            // Alpha:
            var tmpAlpha = CalculateWaterretentieAlpha(tmpOs);

            // N
            var tmpN = CalculateWaterretentieN(tmpOs);

            // ThetaPfTweePuntNul
            var tmpThetaPfTweePuntNul = CalculateWaterretentieThetaPfTweePuntNul(tmpBetaS, tmpAlpha, tmpN);

            // ThetaPfVierPuntTwee
            var tmpThetaPfVierPuntTwee = CalculateWaterretentieThetaPfVierPuntTwee(tmpBetaS, tmpAlpha, tmpN);

            // Waterretentie:
            var gridMathInput = new List<IGridMathInput>
            {
                _surferApp.NewGridMathInput(tmpThetaPfTweePuntNul, "A"),
                _surferApp.NewGridMathInput(tmpThetaPfVierPuntTwee, "B")
            };

            const string formula = "(A-B)*100";
            DeleteFile(outGrid);
            _surferApp.GridMath3(formula, gridMathInput.ToArray(), outGrid);
            return File.Exists(outGrid);
        }

        /// <summary>
        /// Calculate Veldcapaciteit
        /// Veel overeenkomsten met Waterretentie
        /// </summary>
        /// <param name="outGrid"></param>
        /// <returns></returns>
        public bool CalculateVeldcapaciteit(string outGrid)
        {
            // Zie Waterretentie, ThetaPfTweePuntNul * 100

            // TODO: check op negatieve waarde:
            // Bij hele lage waarden lutum en leem (sportvelden)
            // gaat de berekening niet goed

            var tmpOs = AftoppenOs();

            // BetaS
            var tmpBetaS = CalculateWaterretentieBetaS(tmpOs);

            // Alpha:
            var tmpAlpha = CalculateWaterretentieAlpha(tmpOs);

            // N
            var tmpN = CalculateWaterretentieN(tmpOs);

            // ThetaPfTweePuntNul
            var tmpThetaPfTweePuntNul = CalculateWaterretentieThetaPfTweePuntNul(tmpBetaS, tmpAlpha, tmpN);

            // Waterretentie:
            var gridMathInput = new List<IGridMathInput>
            {
                _surferApp.NewGridMathInput(tmpThetaPfTweePuntNul, "A")
            };

            const string formula = "A*100";
            DeleteFile(outGrid);
            _surferApp.GridMath3(formula, gridMathInput.ToArray(), outGrid);
            return File.Exists(outGrid);
        }

        private string AftoppenOs()
        {
            // Bij OS > 15% aftoppen, omdat bij die waarden
            // de berekening niet werkt. OS > 15% = Veen:
            var gridMathInput = new List<IGridMathInput>
            {
                _surferApp.NewGridMathInput(SurferService.GetFullPath(_workingFolder, _fieldName, FormulaConstants.Os), FormulaConstants.Os)
            };
            var tmpOs = Path.Combine(Path.GetTempPath(), "OS15.grd");
            DeleteFile(tmpOs);
            _surferApp.GridMath3("IF(OS>=15,14.99,OS)", gridMathInput.ToArray(), tmpOs);
            if (!File.Exists(tmpOs))
                throw new Exception("Cannot create temp OS-15 grid.");

            return tmpOs;
        }

        private string CalculateWaterretentieBetaS(string tmpOs)
        {
            var gridMathInput = new List<IGridMathInput>
            {
                _surferApp.NewGridMathInput(tmpOs, FormulaConstants.Os),
                _surferApp.NewGridMathInput(SurferService.GetFullPath(_workingFolder, _fieldName, FormulaConstants.Lutum), FormulaConstants.Lutum),
                _surferApp.NewGridMathInput(SurferService.GetFullPath(_workingFolder, _fieldName, FormulaConstants.Zandfractie), FormulaConstants.Zandfractie),
                _surferApp.NewGridMathInput(SurferService.GetFullPath(_workingFolder, _fieldName, FormulaConstants.M50), FormulaConstants.M50),
                _surferApp.NewGridMathInput(SurferService.GetFullPath(_workingFolder, _fieldName, FormulaConstants.Bulkdichtheid), FormulaConstants.Bulkdichtheid)
            };

            var tmpFile = Path.Combine(Path.GetTempPath(), "BetaS.grd");
            DeleteFile(tmpFile);

            // BetaS
            // ReSharper disable once ConvertToConstant.Local
            var formuleKlei = $"0.6311 + (0.003383 * {FormulaConstants.Lutum}) - (0.09699 * ({FormulaConstants.Bulkdichtheid} ^ 2)) - (0.00204 * {FormulaConstants.Bulkdichtheid} * {FormulaConstants.Lutum})";
            // ReSharper disable once ConvertToConstant.Local
            var formuleZand = $"-35.7 - (0.1843 * {FormulaConstants.Bulkdichtheid}) - (0.03576 * {FormulaConstants.M50}) + (0.0000261 * ({FormulaConstants.M50} ^ 2)) - 0.0564 * ((100-{FormulaConstants.Zandfractie}) ^ -1) + 0.008 * ({FormulaConstants.Os} ^ -1) + 496 * ({FormulaConstants.M50} ^ -1) + 0.02244 * Ln({FormulaConstants.Os}) + 7.56 * Ln({FormulaConstants.M50})";

            var formula = $"IF({FormulaConstants.Lutum}>=8,{formuleKlei},{formuleZand})";

            _surferApp.GridMath3(formula, gridMathInput.ToArray(), tmpFile);
            if (!File.Exists(tmpFile))
                throw new Exception("Cannot create temp BetaS grid.");

            return tmpFile;
        }

        private string CalculateWaterretentieAlpha(string tmpOs)
        {
            var gridMathInput = new List<IGridMathInput>
            {
                _surferApp.NewGridMathInput(tmpOs, FormulaConstants.Os),
                _surferApp.NewGridMathInput(SurferService.GetFullPath(_workingFolder, _fieldName, FormulaConstants.Lutum), FormulaConstants.Lutum),
                _surferApp.NewGridMathInput(SurferService.GetFullPath(_workingFolder, _fieldName, FormulaConstants.Zandfractie), FormulaConstants.Zandfractie),
                _surferApp.NewGridMathInput(SurferService.GetFullPath(_workingFolder, _fieldName, FormulaConstants.M50), FormulaConstants.M50),
                _surferApp.NewGridMathInput(SurferService.GetFullPath(_workingFolder, _fieldName, FormulaConstants.Bulkdichtheid), FormulaConstants.Bulkdichtheid)
            };

            var tmpFile = Path.Combine(Path.GetTempPath(), "Alpha.grd");
            DeleteFile(tmpFile);

            // BetaS
            // ReSharper disable once ConvertToConstant.Local
            var formuleKlei = $"Exp(-19.13 + (0.812 * {FormulaConstants.Os}) + (23.4 * {FormulaConstants.Bulkdichtheid}) - (8.16 * ({FormulaConstants.Bulkdichtheid} ^ 2)) + (0.423 * ({FormulaConstants.Os} ^ -1)) + (2.388 * Ln({FormulaConstants.Os})) - (1.338 * {FormulaConstants.Bulkdichtheid} * {FormulaConstants.Os}))";
            // ReSharper disable once ConvertToConstant.Local
            var formuleZand = $"Exp(13.66 - (5.91 * {FormulaConstants.Bulkdichtheid}) - (0.172 * 1) + (0.003248 * {FormulaConstants.M50}) - (11.89 * ({FormulaConstants.Bulkdichtheid} ^ -1)) - (2.121 * ((100-{FormulaConstants.Zandfractie}) ^ -1)) - (0.3742 * Ln((100-{FormulaConstants.Zandfractie}))))";

            var formula = $"IF({FormulaConstants.Lutum}>=8,{formuleKlei},{formuleZand})";

            _surferApp.GridMath3(formula, gridMathInput.ToArray(), tmpFile);
            if (!File.Exists(tmpFile))
                throw new Exception("Cannot create temp Alpha grid.");

            return tmpFile;
        }

        private string CalculateWaterretentieN(string tmpOs)
        {
            var gridMathInput = new List<IGridMathInput>
            {
                _surferApp.NewGridMathInput(tmpOs, FormulaConstants.Os),
                _surferApp.NewGridMathInput(SurferService.GetFullPath(_workingFolder, _fieldName, FormulaConstants.Lutum), FormulaConstants.Lutum),
                _surferApp.NewGridMathInput(SurferService.GetFullPath(_workingFolder, _fieldName, FormulaConstants.Zandfractie), FormulaConstants.Zandfractie),
                _surferApp.NewGridMathInput(SurferService.GetFullPath(_workingFolder, _fieldName, FormulaConstants.M50), FormulaConstants.M50),
                _surferApp.NewGridMathInput(SurferService.GetFullPath(_workingFolder, _fieldName, FormulaConstants.Bulkdichtheid), FormulaConstants.Bulkdichtheid)
            };

            var tmpFile = Path.Combine(Path.GetTempPath(), "N.grd");
            DeleteFile(tmpFile);

            // BetaS
            // ReSharper disable once ConvertToConstant.Local
            var formuleKlei = $"Exp(-0.235 + (0.972 * ({FormulaConstants.Bulkdichtheid} ^ -1)) - (0.7743 * Ln({FormulaConstants.Lutum})) - (0.3154 * Ln({FormulaConstants.Os})) + (0.0678 * {FormulaConstants.Bulkdichtheid} * {FormulaConstants.Os})) + 1";
            // ReSharper disable once ConvertToConstant.Local
            var formuleZand = $"Exp(-1.057 + (0.1003 * {FormulaConstants.Os}) + (1.119 * {FormulaConstants.Bulkdichtheid}) + (0.000764 * ((100-{FormulaConstants.Zandfractie}) ^ 2)) - (0.1397 * ({FormulaConstants.Os} ^ -1)) - (57.2 * ({FormulaConstants.M50} ^ -1)) - (0.557 * Ln({FormulaConstants.Os})) - (0.02997 * {FormulaConstants.Bulkdichtheid} * (100-{FormulaConstants.Zandfractie}))) + 1";

            var formula = $"IF({FormulaConstants.Lutum}>=8,{formuleKlei},{formuleZand})";

            _surferApp.GridMath3(formula, gridMathInput.ToArray(), tmpFile);
            if (!File.Exists(tmpFile))
                throw new Exception("Cannot create temp N grid.");

            return tmpFile;
        }

        private string CalculateWaterretentieThetaPfTweePuntNul(string tmpBetaS, string tmpAlpha, string tmpN)
        {
            var gridMathInput = new List<IGridMathInput>
            {
                _surferApp.NewGridMathInput(tmpBetaS, "BetaS"),
                _surferApp.NewGridMathInput(tmpAlpha, "Alpha"),
                _surferApp.NewGridMathInput(tmpN, "N"),
            };

            var tmpFile = Path.Combine(Path.GetTempPath(), "ThetaPfTweePuntNul.grd");
            DeleteFile(tmpFile);

            const string formula = "0.01 + (BetaS - 0.01) / ((1 + (Alpha * 100) ^ N) ^ (1 - 1 / N))";

            _surferApp.GridMath3(formula, gridMathInput.ToArray(), tmpFile);
            if (!File.Exists(tmpFile))
                throw new Exception("Cannot create temp ThetaPfTweePuntNul grid.");

            return tmpFile;
        }
        private string CalculateWaterretentieThetaPfVierPuntTwee(string tmpBetaS, string tmpAlpha, string tmpN)
        {
            var gridMathInput = new List<IGridMathInput>
            {
                _surferApp.NewGridMathInput(tmpBetaS, "BetaS"),
                _surferApp.NewGridMathInput(tmpAlpha, "Alpha"),
                _surferApp.NewGridMathInput(tmpN, "N"),
            };

            var tmpFile = Path.Combine(Path.GetTempPath(), "ThetaPfVierPuntTwee.grd");
            DeleteFile(tmpFile);

            const string formula = "0.01 + (BetaS - 0.01) / ((1 + (Alpha * (10 ^ 4.2)) ^ N) ^ (1 - 1 / N))";

            _surferApp.GridMath3(formula, gridMathInput.ToArray(), tmpFile);
            if (!File.Exists(tmpFile))
                throw new Exception("Cannot create temp ThetaPfVierPuntTwee grid.");

            return tmpFile;
        }


        private void DeleteFile(string fileLocation)
        {
            if (File.Exists(fileLocation))
                File.Delete(fileLocation);
        }
    }
}
