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
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        /// <summary>
        /// Reproject a CSV file to a projected coordinate system
        /// </summary>
        /// <param name="fileName">The file to process</param>
        /// <param name="epsgCode">The EPSG-code to project to</param>
        /// <param name="label">The label which will be added to the new file name</param>
        /// <returns>The location of the new file</returns>
        /// <exception cref="NullReferenceException"></exception>
        public static string ConvertLatLongToProjected(string fileName, string epsgCode, string label)
        {
            const string ogrLocation = @"D:\dev\MapWindow\MapWinGIS\git\support\GDAL_SDK\v142\bin\x64\ogr2ogr"; // TODO: Make flexible

            var arguments = $@"-f CSV -lco GEOMETRY=AS_XY -lco SEPARATOR=SEMICOLON -lco STRING_QUOTING=IF_NEEDED -s_srs ""EPSG:4326"" -t_srs ""{epsgCode}"" -oo X_POSSIBLE_NAMES=lon -oo Y_POSSIBLE_NAMES=lat";

            var newName = fileName.Replace(".csv", $"-{label}.csv");
            if (File.Exists(newName)) File.Delete(newName);

            using var myProcess = new Process
            {
                StartInfo = { CreateNoWindow = true, WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = ogrLocation,
                    Arguments = $"{arguments} \"{newName}\" \"{fileName}\""
                },
                EnableRaisingEvents = true
            };
            
            myProcess.Start();
            myProcess.WaitForExit(10_000);

            if (myProcess.ExitCode != 0)
            {
                // Something went wrong, show the window
                // TODO: Doesn't work:
                ShowWindow(myProcess.MainWindowHandle, 0);
            }

            return newName;
        }
    }
}
