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
            string minimum, string maximum, string levelFile, int rowIndex = -1 )
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
    }
}
