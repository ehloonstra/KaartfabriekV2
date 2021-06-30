using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace SurferTools
{
    /// <summary>
    /// Methods using System.Diagnostics.Process
    /// </summary>
    public class ProcessTools
    {
        const string OgrLocation = @"D:\dev\MapWindow\MapWinGIS\git\support\GDAL_SDK\v142\bin\x64\ogr2ogr"; // TODO: Make flexible
        const string GdalTranslateLocation = @"D:\dev\MapWindow\MapWinGIS\git\support\GDAL_SDK\v142\bin\x64\gdal_translate"; // TODO: Make flexible

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        /// <summary>
        /// Reproject a CSV file to a projected coordinate system
        /// </summary>
        /// <param name="fileName">The file to process</param>
        /// <param name="epsgCode">The EPSG-code to project to</param>
        /// <returns>The location of the new file</returns>
        /// <exception cref="NullReferenceException"></exception>
        public static string ConvertLatLongToProjected(string fileName, string epsgCode)
        {
            var arguments = $@"-f CSV -lco GEOMETRY=AS_XY -lco SEPARATOR=SEMICOLON -lco STRING_QUOTING=IF_NEEDED -s_srs ""EPSG:4326"" -t_srs ""{epsgCode}"" -oo X_POSSIBLE_NAMES=lon -oo Y_POSSIBLE_NAMES=lat";

            var newName = fileName.Replace(".csv", $"-{epsgCode}.csv");
            if (File.Exists(newName)) File.Delete(newName);

            if (!ProcessOgr2Ogr($"{arguments} \"{newName}\" \"{fileName}\""))
                throw new Exception("ProcessOgr2Ogr failed");

            return newName;
        }

        /// <summary>
        /// Get the AAN data from PDOK and save as shapefile
        /// </summary>
        /// <param name="limits">The boundingbox</param>
        /// <param name="workingFolder">The working folder</param>
        /// <returns>The location of the new shapefile</returns>
        public static string GetAanData(Limits limits, string workingFolder)
        {
            var arguments = $@"""WFS:https://geodata.nationaalgeoregister.nl/aan/wfs?service=WFS&request=GetFeature&version=2.0.0&typename=aan:aan&srsname=EPSG:28992&outputFormat=application/json&bbox={limits.Xmin},{limits.Ymin},{limits.Xmax},{limits.Ymax},EPSG:28992"" -f ""ESRI Shapefile"" -a_srs ""EPSG:28992""";

            var newName = Path.Combine(workingFolder, "Perceel-Aan.shp");
            if (File.Exists(newName))
            {
                File.Delete(newName);
                File.Delete(Path.ChangeExtension(newName, ".shx"));
                File.Delete(Path.ChangeExtension(newName, ".dbf"));
                File.Delete(Path.ChangeExtension(newName, ".prj"));
            }

            if (!ProcessOgr2Ogr($"\"{newName}\" {arguments}"))
                throw new Exception("ProcessOgr2Ogr failed");

            return newName;            
        }        
        
        /// <summary>
        /// Get the luchtfoto as JPEG from PDOK
        /// </summary>
        /// <param name="limits"></param>
        /// <param name="workingFolder"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetLuchtfotoImage(Limits limits, string workingFolder)
        {
            var newName = Path.Combine(workingFolder, "Luchtfoto.jpg");
            if (File.Exists(newName))
            {
                File.Delete(newName);
                File.Delete($"{newName}.aux.xml");
            }

            var jpgSettings = $"-of JPEG -outsize {limits.Xmax - limits.Xmin} {limits.Ymax - limits.Ymin}";
            var wmsSettings = $"\"WMS:https://service.pdok.nl/hwh/luchtfotorgb/wms/v1_0?SERVICE=WMS&VERSION=1.1.1&REQUEST=GetMap&LAYERS=Actueel_ortho25&SRS=EPSG:28992&BBOX={limits.Xmin},{limits.Ymin},{limits.Xmax},{limits.Ymax}&TRANSPARENT=FALSE\"";

            // -of JPEG -outsize 956 1440 "WMS:https://service.pdok.nl/hwh/luchtfotorgb/wms/v1_0?SERVICE=WMS&VERSION=1.1.1&REQUEST=GetMap&LAYERS=Actueel_ortho25&SRS=EPSG:28992&BBOX=162834,467386,163312,468106&TRANSPARENT=FALSE" luchtfoto2.jpg

            if (!ProcessGdalTranslate($"{jpgSettings} {wmsSettings} \"{newName}\""))
                throw new Exception("ProcessGdalTranslate failed");

            return newName;            
        }

        /// <summary>
        /// Open the url in the active browser
        /// </summary>
        /// <param name="url"></param>
        public static void OpenUrl(string url)
        {
            Process.Start(new ProcessStartInfo { FileName = url, UseShellExecute = true });
        }

        private static bool ProcessOgr2Ogr(string arguments)
        {
            return StartProcess(OgrLocation, arguments);
        }

        private static bool ProcessGdalTranslate(string arguments)
        {
            return StartProcess(GdalTranslateLocation, arguments);
        }

        private static bool StartProcess(string toolLocation, string arguments)
        {
            using var myProcess = new Process
            {
                StartInfo =
                {
                    CreateNoWindow = true, WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = toolLocation,
                    Arguments = arguments
                },
                EnableRaisingEvents = true
            };

            myProcess.Start();
            myProcess.WaitForExit(10_000);

            if (myProcess.ExitCode == 0) return true;

            // Something went wrong, show the window
            // TODO: Doesn't work:
            ShowWindow(myProcess.MainWindowHandle, 0);
            return false;
        }
    }
}
