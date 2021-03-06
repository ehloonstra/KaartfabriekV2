using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using Shared;

namespace SurferTools
{
    /// <summary>
    /// Methods using System.Diagnostics.Process
    /// </summary>
    public class ProcessTools
    {
        private string _gdalFolderLocation;

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private string GetGdalTranslateLocation()
        {
            if (!string.IsNullOrEmpty(_gdalFolderLocation))
                return Path.Combine(_gdalFolderLocation, "gdal_translate");
            // Read from application settings:
            var settings = ApplicationSettings.Load();
            if (!Directory.Exists(settings.GdalLocation))
                throw new DirectoryNotFoundException("Cannot find GDAL folder");
            _gdalFolderLocation = settings.GdalLocation;
            return Path.Combine(_gdalFolderLocation, "gdal_translate");
        }

        private string GetOgr2OgrLocation()
        {
            if (!string.IsNullOrEmpty(_gdalFolderLocation))
                return Path.Combine(_gdalFolderLocation, "ogr2ogr");
            // Read from application settings:
            var settings = ApplicationSettings.Load();
            if (!Directory.Exists(settings.GdalLocation))
                throw new DirectoryNotFoundException("Cannot find GDAL folder");
            _gdalFolderLocation = settings.GdalLocation;
            return Path.Combine(_gdalFolderLocation, "ogr2ogr");
        }

        /// <summary>
        /// Reproject a CSV file to a projected coordinate system
        /// </summary>
        /// <param name="fileName">The file to process</param>
        /// <param name="epsgCode">The EPSG-code to project to</param>
        /// <returns>The location of the new file</returns>
        /// <exception cref="NullReferenceException"></exception>
        public string ConvertLatLongToProjected(string fileName, string epsgCode)
        {
            var arguments = $@"-f CSV -lco GEOMETRY=AS_XY -lco SEPARATOR=SEMICOLON -lco STRING_QUOTING=IF_NEEDED -s_srs ""EPSG:4326"" -t_srs ""{epsgCode}"" -oo X_POSSIBLE_NAMES=lon -oo Y_POSSIBLE_NAMES=lat";

            var newName = fileName.Replace(".csv", $"-{epsgCode.Replace(":", "")}.csv");
            if (File.Exists(newName)) File.Delete(newName);

            // Check if file contains , as decimal point:
            var checkedFileName = ReplaceCommaInCsv(fileName);

            if (!ProcessOgr2Ogr($"{arguments} \"{newName}\" \"{checkedFileName}\""))
                throw new Exception("ProcessOgr2Ogr failed");

            // Delete temp file:
            if (File.Exists(checkedFileName)) File.Delete(checkedFileName);

            return newName;
        }

        private static string ReplaceCommaInCsv(string fileName)
        {
            var tempFileName = Path.GetTempFileName();
            tempFileName = Path.ChangeExtension(tempFileName, Path.GetExtension(fileName));
            var text = File.ReadAllText(fileName);
            text = text.Replace(",", ".");
            File.WriteAllText(tempFileName, text);

            if (!File.Exists(tempFileName))
                throw new Exception("Kan de csv niet fixen");

            return tempFileName;
        }

        /// <summary>
        /// Get the AAN data from PDOK and save as shapefile
        /// </summary>
        /// <param name="limits">The boundingbox</param>
        /// <param name="workingFolder">The working folder</param>
        /// <returns>The location of the new shapefile</returns>
        public string GetAanData(Limits limits, string workingFolder)
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
        public string GetLuchtfotoImage(Limits limits, string workingFolder)
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
            OpenFile(url);
        }

        /// <summary>
        /// Open the file with the registered application
        /// </summary>
        /// <param name="fileLocation"></param>
        public static void OpenFile(string fileLocation)
        {
            Process.Start(new ProcessStartInfo { FileName = fileLocation, UseShellExecute = true });
        }

        private bool ProcessOgr2Ogr(string arguments)
        {
            var ogrLocation = GetOgr2OgrLocation();
            return StartProcess(ogrLocation, arguments);
        }

        private bool ProcessGdalTranslate(string arguments)
        {
            var gdalTranslateLocation = GetGdalTranslateLocation();
            return StartProcess(gdalTranslateLocation, arguments);
        }

        private static bool StartProcess(string toolLocation, string arguments)
        {
            if (!File.Exists($"{toolLocation}.exe"))
                throw new FileNotFoundException($"Kan de GDAL tool '{toolLocation}' niet vinden.", toolLocation);

            using var myProcess = new Process
            {
                StartInfo =
                {
                    CreateNoWindow = true, WindowStyle = ProcessWindowStyle.Hidden,
                    UseShellExecute = false,
                    RedirectStandardError = true,
                    FileName = toolLocation,
                    Arguments = arguments
                },
                EnableRaisingEvents = true
            };

            myProcess.Start();

            // To avoid deadlocks, always read the output stream first and then wait.  
            var output = myProcess.StandardError.ReadToEnd();
            if (!string.IsNullOrEmpty(output) && output.Contains("Error", StringComparison.InvariantCultureIgnoreCase))
                throw new Exception($"Exception in processing GDAL tool ({Path.GetFileName(toolLocation)}). {output}");
            
            myProcess.WaitForExit(10_000);
            
            return myProcess.ExitCode == 0;
        }
    }
}
