using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private readonly IApplication5 _surferApp;
        private readonly Action<string> _addProgress;

        public SpecialCalculations(string workingFolder, IApplication5 surferApp, Action<string> addProgress)
        {
            _workingFolder = workingFolder;
            _surferApp = surferApp;
            _addProgress = addProgress;
        }

        public bool CalculateBodemclassificatie(string outGrid, bool eolisch)
        {
            // Moerig, OS, Lutum, Zandfactie, CaCO3
            return false;
        }

        /// <summary>
        /// Calculate Bulkdichtheid
        /// </summary>
        /// <param name="outGrid"></param>
        /// <returns></returns>
        public bool CalculateBulkdichtheid(string outGrid)
        {
            // Bij OS > 15% aftoppen, omdat bij die waarden
            // de berekening niet werkt. OS > 15% = Veen:
            var gridMathInput = new List<IGridMathInput>
            {
                _surferApp.NewGridMathInput(SurferService.GetFullPath(_workingFolder, FormulaConstants.Os), FormulaConstants.Os)
            };
            var tmpOs = Path.Combine(Path.GetTempPath(), "OS15.grd");
            DeleteFile(tmpOs);
            _surferApp.GridMath3("IF(OS>=15,14.99,OS)", gridMathInput.ToArray(), tmpOs);
            if (!File.Exists(tmpOs))
                throw new Exception("Cannot create temp OS-15 grid.");

            // TODO: In VB6-code wordt ook Lutum soms afgetopt

            // OS, Lutum, Zandfactie, M50
            gridMathInput = new List<IGridMathInput>
            {
                _surferApp.NewGridMathInput(tmpOs, FormulaConstants.Os),
                _surferApp.NewGridMathInput(SurferService.GetFullPath(_workingFolder, FormulaConstants.Lutum), FormulaConstants.Lutum),
                _surferApp.NewGridMathInput(SurferService.GetFullPath(_workingFolder, FormulaConstants.Zandfractie), FormulaConstants.Zandfractie),
                _surferApp.NewGridMathInput(SurferService.GetFullPath(_workingFolder, FormulaConstants.M50), FormulaConstants.M50)
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
            var tcGridLocation = SurferService.GetFullPath(_workingFolder, inputGrid);
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

        public bool CalculateVeldcapaciteit(string outGrid)
        {
            // Zie Waterretentie
            return false;
        }

        public bool CalculateWaterdoorlatendheid(string outGrid)
        {
            // OS, Lutum, Zandfractie, M50, Bulkdichtheid
            return false;
        }

        public bool CalculateWaterretentie(string outGrid)
        {
            // OS, Lutum, Zandfactie, M50, Bulkdichtheid
            return false;
        }
        
        private void DeleteFile(string fileLocation)
        {
            if (File.Exists(fileLocation))
                File.Delete(fileLocation);
        }
    }
}
