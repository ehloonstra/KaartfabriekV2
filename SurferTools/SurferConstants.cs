using System.IO;

#pragma warning disable 1591

namespace SurferTools
{
    public static class SurferConstants
    {
        public const string NuclideGridsFolder = "Nuclide grids";
        public const string BodemkaartenFolder = "Bodemkaarten";
        public static string BodemkaartenGridsFolder = Path.Combine(BodemkaartenFolder, "Grids");
        public static string BodemkaartenResultaatFolder = Path.Combine(BodemkaartenFolder, "Resultaat");
        public static string BodemkaartenResultaatSurferFolder = Path.Combine(BodemkaartenResultaatFolder, "Surfer");
        public static string BodemkaartenResultaatShapefileFolder = Path.Combine(BodemkaartenResultaatFolder, "Shapefile");
        public static string BodemkaartenResultaatPowerPointFolder = Path.Combine(BodemkaartenResultaatFolder, "PowerPoint");

        public const string OutGridOptions = "UseDefaults=1, ForgetOptions=1, SaveRefInfoAsInternal=1, SaveRefInfoAsGSIREF2=1";

        public static string GetProjectionName(string epsgCode)
        {
            if (epsgCode.ToLower().Trim().Equals("EPSG:28992"))
                return "Amersfoort / RD New";
#if DEBUG
            return "Amersfoort / RD New";
#else
            throw new Exception("Unknown projection: " + epsgCode);
#endif
        }
    }
}

#pragma warning restore 1591