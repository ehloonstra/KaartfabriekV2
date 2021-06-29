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

        public FormulaData() { }

        public FormulaData(string output, string formula, string gridA, string gridB, string gridC, string gridD, string minimum, string maximum, string levelFile)
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
        }

        public object[] ToParams(bool IsCheckboxChecked)
        {
            return new object[]
            {
                IsCheckboxChecked, Output, Formula, GridA, GridB, GridC, GridD, Minimum, Maximum, LevelFile
            };
        }
    }
}
