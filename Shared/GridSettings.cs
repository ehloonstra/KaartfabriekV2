namespace Shared
{
    public class GridSettings
    {
        public string SearchMinData { get; set; } = "8";
        public string SearchMaxData { get; set; } = "64";
        public string SearchRadius { get; set; } = "30"; // in meters
        public string SearchNumSectors { get; set; } = "1";
        public string IdPower { get; set; } = "2";
        public string IdSmoothing { get; set; } = "20";
        public string GridSpacing { get; set; } = "3.5"; // in meters
        public string Limits { get; set; } = "20"; // in meters
    }
}