using System;
using System.IO;
using System.Text.Json;

#pragma warning disable 1591

namespace Shared
{
    /// <summary>
    /// Class to hold the project settings
    /// </summary>
    public class Project
    {
        public string WorkingFolder { get; set; }
        public string SampleDataFileLocation { get; set; }
        public string SampleDataFileLocationProjected { get; set; }
        public string FieldDataFileLocation { get; set; }
        public string FieldDataFileLocationProjected { get; set; }
        public string FieldBorderLocation { get; set; }
        public string FieldBorderLocationBuffered { get; set; }
        public ColumnIndexes ColumnIndexes { get; set; }
        public NuclideGridLocations NuclideGridLocations { get; set; }
        public GridSettings GridSettings { get; set; } = new();

        public bool Save(string fileName)
        {
            var jsonString = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(fileName, jsonString);

            return File.Exists(fileName);
        }

        public static Project Load(string fileName)
        {
            if (!File.Exists(fileName))
                throw new FileNotFoundException("Cannot find project json file", fileName);

            var jsonString = File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<Project>(jsonString);
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
        public string AltLocation { get; set; }
        public string TcLocation { get; set; }
        public string K40Location { get; set; }
        public string U238Location { get; set; }
        public string Th232Location { get; set; }
        public string Cs137Location { get; set; }
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
