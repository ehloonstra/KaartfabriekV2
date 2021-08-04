using System.Collections.Generic;
using System.Linq;

namespace Shared
{
    public class SurferEpsgFile
    {
        public string WorkingFolder { get; set; }
        public List<SurferEpsgData> Data { get; set; } = new();
    }

    public class SurferEpsgData
    {
        public string Name { get; set; }
        public string SplitName => Name.Split('|').Last();
        public string Token { get; set; }
        public string Legacyname { get; set; }
        public string Datum { get; set; }
        public string Proj { get; set; }
        public string Ellipsoid { get; set; }
        public string Lon0 { get; set; }
        public string Lat0 { get; set; }
        public string Kscale { get; set; }
        public string Xorg { get; set; }
        public string Yorg { get; set; }
        public string Scale { get; set; }
        public string Usgs { get; set; }
        public string Projtype { get; set; }
        public string K0 { get; set; }
        public string Posc { get; set; }
        public string Comment { get; set; }
    }
}
