using CsvHelper.Configuration.Attributes;

namespace KaartfabriekUI.Service
{
    /// <summary>
    /// Class for the Surfer Dat-file structure
    /// </summary>
    public class DatStructure
    {
#pragma warning disable 1591

        [Index(0)]
        public double X { get; set; }


        [Index(1)]
        public double Y { get; set; }

        
        [Index(2)]
        public double Z { get; set; }

#pragma warning restore 1591
    }
}
