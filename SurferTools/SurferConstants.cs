using System;
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
        public static string BodemkaartenResultaatEmfFolder = Path.Combine(BodemkaartenResultaatFolder, "Emf");
        public static string BodemkaartenResultaatCsvFolder = Path.Combine(BodemkaartenResultaatFolder, "Csv");

        public const string OutGridOptions = "UseDefaults=1, ForgetOptions=1, SaveRefInfoAsInternal=1, SaveRefInfoAsGSIREF2=1";
        public const string TemplateName = "Template.srf";
        public const string TemplateMapName = "Template";

        //public static string GetCoordinateSystemName(string epsgCode)
        //{
        //    if (epsgCode.ToUpper().Trim().Equals("EPSG:28992"))
        //        return "Amersfoort / RD New";

        //    if (epsgCode.ToUpper().Trim().Equals("EPSG:2180"))
        //        return "ETRS89 / Poland CS92";

        //    if (epsgCode.ToUpper().Trim().Equals("EPSG:2150"))
        //        return "Canada NAD83(CSRS98) / UTM zone 17N";

        //    // TODO: Read from file:

        //    throw new Exception("Unknown projection: " + epsgCode);

        //}
    }
}

#pragma warning restore 1591