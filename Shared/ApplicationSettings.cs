using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;

#pragma warning disable 1591
namespace Shared
{
    public class ApplicationSettings : INotifyPropertyChanged
    {
        private static readonly string FileName = Path.Combine(Environment.GetFolderPath(
            Environment.SpecialFolder.ApplicationData), "Kaartfabriek", "Settings.json");

        private string _gdalLocation;
        private string _levelFilesFolder;
        private string _templateLocationAkkerbouw;

        public string GdalLocation
        {
            get => _gdalLocation;
            set { _gdalLocation = value; NotifyPropertyChanged(); }
        }

        public string LevelFilesFolder
        {
            get => _levelFilesFolder;
            set { _levelFilesFolder = value; NotifyPropertyChanged(); }
        }

        public string TemplateLocationAkkerbouw
        {
            get => _templateLocationAkkerbouw;
            set { _templateLocationAkkerbouw = value; NotifyPropertyChanged(); }
        }

        public static ApplicationSettings Load()
        {
            if (!File.Exists(FileName))
            {
                // Create default settings
                return CreateDefaultSettings();
            }

            var jsonString = File.ReadAllText(FileName);
            var retValue = JsonSerializer.Deserialize<ApplicationSettings>(jsonString);
            if (retValue == null) throw new JsonException("Cannot load application settings", FileName, null, null);

            return retValue;
        }

        public bool Save()
        {
            var jsonString = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true, Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping });
            File.WriteAllText(FileName, jsonString);

            return File.Exists(FileName);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            Save();
        }

        private static ApplicationSettings CreateDefaultSettings()
        {
            // Check folder:
            var folder = Path.GetDirectoryName(FileName);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder ??
                                          throw new DirectoryNotFoundException("Cannot find appdata folder with the settings."));
            }

            var settings = new ApplicationSettings
            {
                LevelFilesFolder = @"D:\dev\TopX\Loonstra\TSC Tools\Kaartfabriek\Steven"
            };

            // During development
            var gdalLocation = @"D:\dev\MapWindow\MapWinGIS\git\support\GDAL_SDK\v142\bin\x64";
            if (Directory.Exists(gdalLocation)) settings.GdalLocation = gdalLocation;

            // During production GDAL will be in sub folder:
            gdalLocation = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "GDAL");
            Debug.WriteLine(gdalLocation);
            if (Directory.Exists(gdalLocation)) settings.GdalLocation = gdalLocation;

            // Save default formulas:


            return settings;
        }
    }
}

#pragma warning restore 1591
