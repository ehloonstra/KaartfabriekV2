#pragma warning disable 1591

namespace SurferTools
{
    public static class SurferConstants
    {
        //public const string Epsg28992 = "Amersfoort / RD New";

        public static string GetProjectionName(string epsgCode)
        {
            if (epsgCode.ToLower().Trim().Equals("EPSG:28992"))
                return "Amersfoort / RD New";

            return "Unknown projection";
        }
    }
}

#pragma warning restore 1591