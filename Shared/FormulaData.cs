using System.Collections.Generic;

namespace Shared
{
    public class FormulaData
    {
        public string Output { get; set; }
        public string Formula { get; set; }
        public string GridA { get; set; }
        public string GridB { get; set; }
        public string GridC { get; set; }
        public string GridD { get; set; }
        public string Minimum { get; set; }
        public string Maximum { get; set; }
        public string LevelFile { get; set; }

        public int RowIndex;

        public FormulaData() { }

        public FormulaData(string output, string formula, string gridA, string gridB, string gridC, string gridD,
            string minimum, string maximum, string levelFile, int rowIndex = -1)
        {
            Output = output;
            Formula = formula;
            GridA = gridA;
            GridB = gridB;
            GridC = gridC;
            GridD = gridD;
            Minimum = minimum;
            Maximum = maximum;
            LevelFile = levelFile;
            RowIndex = rowIndex;
        }

        public object[] ToParams(bool isCheckboxChecked)
        {
            return new object[]
            {
                isCheckboxChecked, Output, Formula, GridA, GridB, GridC, GridD, Minimum, Maximum, LevelFile
            };
        }


        /// <summary>
        /// Get the default formulas from the application settings
        /// </summary>
        /// <returns></returns>
        public static List<FormulaData> GetDefaultFormulas()
        {
            // TODO: Get from application settings:

            // If application settings has no formulas, use these hard-coded ones:
            var retVal = new List<FormulaData>
            {
                new(FormulaConstants.Lutum, "0.7*Th232-4", "Th232", "", "", "", "", "", "Lutum 0-10.lvl"),
                new(FormulaConstants.Zandfractie, "-0.8*(Th232 + U238) + 103", "Th232", "U238", "", "", "", "",
                    "Zandfractie 75-100.lvl"),
                new("Leem", "100-Zandfractie", "Zandfractie", "", "", "", "", "", "Leem 0 30 Oud.lvl"),
                new("M0", "-0.5*K40+250", "K40", "", "", "", "", "", "M0 0-130.lvl"),
                new(FormulaConstants.M50, "-0.36709706918763*K40+235", "K40", "", "", "", "", "", "M50 150 250.lvl"),
                new(FormulaConstants.Os, "5.23297521874561 -0.0235 * Cs137 -0.012 * K40", "Cs137", "K40", "", "", "", ""
                    , "OS 0-5.lvl"),
                new("pH", "1.21876485586487 + 0.0299 * Th232 + 0.4333 * U238", "Th232", "U238", "", "", "5.5", "6.9",
                    "Ph 4-7 0.25.lvl"),
                new("K-getal", "191.122454275834 -0.7771 * TC", "TC", "", "", "", "15", "29", "K-getal.lvl"),
                new("PW", "52.3556817192375 -7.7197 * Th232 + 7.3653 * U238", "Th232", "U238", "", "", "40", "75",
                    "Pw.lvl"),
                new("Mg", "77.4222684918912 + 1.8301 * Th232 -2.8296 * U238", "Th232", "U238", "", "", "57", "71",
                    "Mg 0-125 25.lvl"),
                new("Stikstof", "4067.52188017669 -13.2469 * TC+60", "TC", "", "", "", "1040", "1290",
                    "Bgr 0-2000 200.lvl"),

                new(FormulaConstants.Bulkdichtheid, FormulaConstants.Bulkdichtheid, "", "", "", "", "", "",
                    "Bulkdichtheid.lvl"),
                new(FormulaConstants.Waterretentie, FormulaConstants.Waterretentie, "", "", "", "", "", "",
                    "Waterretentie 20-30.lvl"),
                new(FormulaConstants.Veldcapaciteit, FormulaConstants.Veldcapaciteit, "", "", "", "", "", "",
                    "Veldcapaciteit 0.3-0.38 0.1.lvl"),
                new(FormulaConstants.Waterdoorlatendheid, FormulaConstants.Waterdoorlatendheid, "", "", "", "", "", "",
                    "Waterdoorlatendheid 0-50 5.lvl"),
                new(FormulaConstants.BodemclassificatieNietEolisch, FormulaConstants.BodemclassificatieNietEolisch, "",
                    "", "", "", "", "", "Bodemclassificatie Niet-Eolisch.lvl"),
                new(FormulaConstants.BodemclassificatieEolisch, FormulaConstants.BodemclassificatieEolisch, "", "", "",
                    "", "", "", "Bodemclassificatie Eolisch.lvl"),
                new(FormulaConstants.Slemp, FormulaConstants.Slemp, "", "", "", "", "", "", "Slemp.lvl"),

                new(FormulaConstants.Monsterpunten, FormulaConstants.Monsterpunten, "", "", "", "", "", "",
                    "Tc 200 250.lvl"),
                new("Ligging", "Alt", "Alt", "", "", "", "", "", "Ligging 2-7.lvl")
            };

            return retVal;
        }
    }
}
