using System.IO;

namespace KaartfabriekUI.Service
{
    /// <summary>
    /// Static class with helper methods
    /// </summary>
    public static class Helpers
    {
        /// <summary>
        /// Check if the folder exists, create it if not
        /// </summary>
        /// <param name="folder"></param>
        /// <returns>The folder</returns>
        public static string CheckFolder(string folder)
        {
            if (!Directory.Exists(folder)) 
                Directory.CreateDirectory(folder);

            return folder;
        }

    }
}
