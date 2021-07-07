using System.Collections.Generic;
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
        private string _epsgCode;
        private string _gridNames;
        private string _fieldBorderBufferSize;
        private string _gwt;

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
            set { _sampleDataFileLocationProjected = value; NotifyPropertyChanged(); }
        }

        public string FieldDataFileLocation
        {
            get => _fieldDataFileLocation;
            set { _fieldDataFileLocation = value; NotifyPropertyChanged(); }
        }

        public string FieldDataFileLocationProjected
        {
            get => _fieldDataFileLocationProjected;
            set { _fieldDataFileLocationProjected = value; NotifyPropertyChanged(); }
        }

        public string FieldBorderLocation
        {
            get => _fieldBorderLocation;
            set { _fieldBorderLocation = value; NotifyPropertyChanged(); }
        }

        public string FieldBorderLocationBuffered
        {
            get => _fieldBorderLocationBuffered ?? "10";
            set { _fieldBorderLocationBuffered = value; NotifyPropertyChanged(); }
        }

        public string FieldBorderBufferSize
        {
            get => _fieldBorderBufferSize;
            set { _fieldBorderBufferSize = value; NotifyPropertyChanged(); }
        }

        public string EpsgCode
        {
            get => _epsgCode ?? "EPSG:28992";
            set { _epsgCode = value; NotifyPropertyChanged(); }
        }

        public string Gwt
        {
            get => _gwt;
            set { _gwt = value; NotifyPropertyChanged(); }
        }

        public string GridNames
        {
            get => _gridNames ?? "Alt;Cs137;K40;TC;Th232;U238;CaCO3;K-getal;Ligging; Lutum; M0; M50; Mg; Mn; Monsterpunten; OS; P-Al; pH; PW; Stikstof; Zandfractie; Bulkdichtheid; Slemp; Veldcapaciteit; Waterdoorlatendheid; Waterretentie";
            set { _gridNames = value; NotifyPropertyChanged(); }
        }

        // public ColumnIndexes ColumnIndexes { get; set; }
        public NuclideGridLocations NuclideGridLocations { get; set; } = new();
        public GridSettings GridSettings { get; set; } = new();
        public ParcelData ParcelData { get; set; } = new();
        public List<FormulaData> FormulaData { get; set; } = new();

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
            ProjectFileLocation = fileName;
            File.WriteAllText(ProjectFileLocation, "");
            return Save();
        }

        public bool Save()
        {
            if (!File.Exists(ProjectFileLocation)) return false;

            var jsonString = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true, Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping });
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
}

#pragma warning restore 1591
