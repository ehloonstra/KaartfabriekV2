namespace Shared
{
    public class GridSettings
    {
        public int SearchMinData { get; set; } = 8;
        public int SearchMaxData { get; set; } = 64;
        public int SearchRadius { get; set; } = 30; // in meters
        public int SearchNumSectors { get; set; } = 1;
        public int IdPower { get; set; } = 2;
        public int IdSmoothing { get; set; } = 20;
        public float GridSpacing { get; set; } = 3.5f; // in meters
        public int Limits { get; set; } = 20; // in meters
    }
}