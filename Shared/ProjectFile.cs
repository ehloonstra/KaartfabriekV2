using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;

#pragma warning disable 1591

namespace Shared
{
    /// <summary>
    /// Class to hold the project settings
    /// </summary>
    public class ProjectFile : INotifyPropertyChanged
    {
        public string ProjectFileLocation = string.Empty;
        private string _workingFolder = string.Empty;
        private string _sampleDataFileLocation = string.Empty;
        private string _sampleDataFileLocationProjected = string.Empty;
        private string _fieldDataFileLocation = string.Empty;
        private string _fieldDataFileLocationProjected = string.Empty;
        private string _fieldBorderLocation = string.Empty;
        private string _fieldBorderLocationBuffered = string.Empty;
        //private ColumnIndexes _columnIndexes = new();
        private NuclideGridLocations _nuclideGridLocations = new();
        private GridSettings _gridSettings = new();

        public string WorkingFolder
        {
            get => _workingFolder;
            set { _workingFolder = value; NotifyPropertyChanged(); }
        }

        public string SampleDataFileLocation
        {
            get => _sampleDataFileLocation;
            set { _sampleDataFileLocation = value; NotifyPropertyChanged(); }
        }

        public string SampleDataFileLocationProjected
        {
            get => _sampleDataFileLocationProjected;
            set { _sampleDataFileLocationProjected = value; NotifyPropertyChanged();}
        }

        public string FieldDataFileLocation
        {
            get => _fieldDataFileLocation;
            set { _fieldDataFileLocation = value; NotifyPropertyChanged();}
        }

        public string FieldDataFileLocationProjected
        {
            get => _fieldDataFileLocationProjected;
            set { _fieldDataFileLocationProjected = value; NotifyPropertyChanged();}
        }

        public string FieldBorderLocation
        {
            get => _fieldBorderLocation;
            set { _fieldBorderLocation = value; NotifyPropertyChanged();}
        }

        public string FieldBorderLocationBuffered
        {
            get => _fieldBorderLocationBuffered;
            set { _fieldBorderLocationBuffered = value; NotifyPropertyChanged();}
        }

        //public ColumnIndexes ColumnIndexes
        //{
        //    get => _columnIndexes;
        //    set { _columnIndexes = value; NotifyPropertyChanged();}
        //}

        public NuclideGridLocations NuclideGridLocations
        {
            get => _nuclideGridLocations;
            set { _nuclideGridLocations = value; NotifyPropertyChanged();}
        }

        public GridSettings GridSettings
        {
            get => _gridSettings;
            set { _gridSettings = value; NotifyPropertyChanged();}
        }

        public static ProjectFile Load(string fileName)
        {
            if (!File.Exists(fileName))
                throw new FileNotFoundException("Cannot find project json file", fileName);

            var jsonString = File.ReadAllText(fileName);
            var retValue = JsonSerializer.Deserialize<ProjectFile>(jsonString);
            if (retValue == null) throw new JsonException("Cannot load project file", fileName, null, null);

            retValue.ProjectFileLocation = fileName;
            return retValue;

        }

        public bool SaveAs(string fileName)
        {
            var jsonString = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(fileName, jsonString);

            ProjectFileLocation = fileName;
            return File.Exists(fileName);
        }

        public bool Save()
        {
            if (!File.Exists(ProjectFileLocation)) return false;

            var jsonString = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(ProjectFileLocation, jsonString);

            return File.Exists(ProjectFileLocation);
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
    }

    public class ColumnIndexes
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Alt { get; set; }
        public int Tc { get; set; }
        public int K40 { get; set; }
        public int U238 { get; set; }
        public int Th232 { get; set; }
        public int Cs137 { get; set; }
    }

    public class NuclideGridLocations
    {
        public string Alt { get; set; } = string.Empty;
        public string Tc { get; set; } = string.Empty;
        public string K40 { get; set; } = string.Empty;
        public string U238 { get; set; } = string.Empty;
        public string Th232 { get; set; } = string.Empty;
        public string Cs137 { get; set; } = string.Empty;
    }

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
