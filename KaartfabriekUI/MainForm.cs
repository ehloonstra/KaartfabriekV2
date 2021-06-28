using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using KaartfabriekUI.Service;
using Shared;
using SurferTools;

namespace KaartfabriekUI
{
    /// <summary>
    /// The main form
    /// </summary>
    public partial class MainForm : Form
    {

        private ProjectFile _projectFile;

        /// <inheritdoc />
        public MainForm()
        {
            InitializeComponent();
        }

        private void BtnReprojectVelddataClick(object sender, EventArgs e)
        {
            var result = SelectFileToProject(@"Selecteer Velddata in LL");
            if (!File.Exists(result)) return;

            VeldDataLocation.TextboxText = result;
            _projectFile.FieldDataFileLocationProjected = result;
        }

        private void BtnReprojectMonsterdataClick(object sender, EventArgs e)
        {
            var result = SelectFileToProject(@"Selecteer Monsterdata in LL");
            if (File.Exists(result))
            {
                MonsterDataLocation.TextboxText = result;
            }
        }

        private string SelectFileToProject(string title)
        {
            using (var ofd = new OpenFileDialog
            {
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = "csv",
                Filter = @"csv files|*.csv|All files (*.*)|*.*",
                Multiselect = false,
                SupportMultiDottedExtensions = true,
                Title = title
            })
            {
                var workingFolder = WorkingFolder.TextboxText;
                if (!string.IsNullOrWhiteSpace(workingFolder) && Directory.Exists(workingFolder))
                {
                    ofd.InitialDirectory = workingFolder;
                }

                var result = ofd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    return ProcessTools.ConvertLatLongToProjected(ofd.FileName, _projectFile.EpsgCode);
                }
            }

            return "Something went wrong";
        }

        private void BtnLoadVelddataInSurfer_Click(object sender, EventArgs e)
        {
            try
            {
                var service = new KaartfabriekService(_projectFile);
                var result = service.OpenDataForBlanking(WorkingFolder.TextboxText, VeldDataLocation.TextboxText,
                    MonsterDataLocation.TextboxText,
                    CboXcoord.SelectedIndex + 1, CboYcoord.SelectedIndex + 1, CboK40.SelectedIndex + 1);

                // Enable when success:
                BlankFileLocation.Enabled = result;

                // Save column indexes to project file:
                //_projectFile.ColumnIndexes.X = CboXcoord.SelectedIndex + 1;
                //_projectFile.ColumnIndexes.Y = CboYcoord.SelectedIndex + 1;
                //_projectFile.ColumnIndexes.Alt = CboAlt.SelectedIndex + 1;
                //_projectFile.ColumnIndexes.Cs137 = CboCs137.SelectedIndex + 1;
                //_projectFile.ColumnIndexes.K40 = CboK40.SelectedIndex + 1;
                //_projectFile.ColumnIndexes.Th232 = CboTh232.SelectedIndex + 1;
                //_projectFile.ColumnIndexes.Tc = CboTotalCount.SelectedIndex + 1;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }

        }

        private void BtnReadColumns_Click(object sender, EventArgs e)
        {
            PushHeaderVelddataToComboboxes();

            AddProgress("Kolommen zijn uitgelezen uit de velddata.");
            CheckColumns();
        }


        private void CheckColumns()
        {
            if (CboXcoord.SelectedIndex > -1 && CboYcoord.SelectedIndex > -1 && CboAlt.SelectedIndex > -1 &&
                CboK40.SelectedIndex > -1 && CboCs137.SelectedIndex > -1 && CboTh232.SelectedIndex > -1 &&
                CboU238.SelectedIndex > -1 && CboTotalCount.SelectedIndex > -1)
            {
                // Enable next group box:
                GroupBoxPerceelsgrens.Enabled = true;
            }
        }

        private void PushHeaderVelddataToComboboxes()
        {
            if (string.IsNullOrEmpty(VeldDataLocation.TextboxText))
            {
                MessageBox.Show(@"Selecteer eerst de velddata");
                return;
            }

            if (!File.Exists(VeldDataLocation.TextboxText))
            {
                MessageBox.Show(@"Kan de velddata niet vinden");
                return;
            }

            var header = File.ReadLines(VeldDataLocation.TextboxText).First();
            if (string.IsNullOrWhiteSpace(header))
            {
                MessageBox.Show(@"De velddata heeft geen inhoud");
                return;
            }

            // Send data to the comboboxes:
            CboXcoord.Data = header;
            CboYcoord.Data = header;
            CboAlt.Data = header;
            CboK40.Data = header;
            CboU238.Data = header;
            CboTh232.Data = header;
            CboCs137.Data = header;
            CboTotalCount.Data = header;
        }

        private void BtnMaakNuclideGrids_Click(object sender, EventArgs e)
        {
            var service = new KaartfabriekService(_projectFile);
            service.CreateNuclideGrids(WorkingFolder.TextboxText, VeldDataLocation.TextboxText, BlankFileLocation.TextboxText,
                CboXcoord.SelectedIndex + 1, CboYcoord.SelectedIndex + 1, CboAlt.SelectedIndex + 1,
                CboK40.SelectedIndex + 1, CboU238.SelectedIndex + 1, CboTh232.SelectedIndex + 1,
                CboCs137.SelectedIndex + 1, CboTotalCount.SelectedIndex + 1);
        }

        private void BtnOpenProjectFileClick(object sender, EventArgs e)
        {
            // Select or create json file:
            using var ofd = new OpenFileDialog
            {
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = "json",
                Filter = @"project v2 (.json)|*.json|All files (*.*)|*.*",
                Multiselect = false,
                SupportMultiDottedExtensions = true,
                Title = @"Selecteer een project v2 bestand"
            };

            var result = ofd.ShowDialog();
            if (result != DialogResult.OK) return;

            if (!File.Exists(ofd.FileName)) return;

            _projectFile = ProjectFile.Load(ofd.FileName);

            // Enable next groupbox:
            GroupBoxVoorbereiding.Enabled = true;

            AddProgress("Projectbestand is geopend.", true);
            ProjectFile2Gui();
        }

        private void ProjectFile2Gui()
        {
            if (Directory.Exists(_projectFile.WorkingFolder))
            {
                WorkingFolder.TextboxText = _projectFile.WorkingFolder;
                //WorkingFolder_TextboxUpdated(this, null);
            }
            if (File.Exists(_projectFile.FieldDataFileLocationProjected))
            {
                VeldDataLocation.TextboxText = _projectFile.FieldDataFileLocationProjected;
            }
            else
            {
                if (File.Exists(_projectFile.FieldDataFileLocation))
                {
                    // TODO: Use EPSG-code from GUI:
                    var fileName = ProcessTools.ConvertLatLongToProjected(_projectFile.FieldDataFileLocation, _projectFile.EpsgCode);
                    if (File.Exists(fileName))
                    {
                        AddProgress("Velddata is geconverteerd naar RD");
                        _projectFile.FieldDataFileLocationProjected = fileName;
                        VeldDataLocation.TextboxText = _projectFile.FieldDataFileLocationProjected;
                    }
                }
            }

            if (File.Exists(_projectFile.SampleDataFileLocationProjected))
            {
                MonsterDataLocation.TextboxText = _projectFile.SampleDataFileLocationProjected;
            }
            else
            {
                if (File.Exists(_projectFile.SampleDataFileLocation))
                {
                    // TODO: Use EPSG-code from GUI:
                    var fileName = ProcessTools.ConvertLatLongToProjected(_projectFile.SampleDataFileLocation, _projectFile.EpsgCode);
                    if (File.Exists(fileName))
                    {
                        AddProgress("Monsterdata is geconverteerd naar RD");
                        _projectFile.SampleDataFileLocationProjected = fileName;
                        MonsterDataLocation.TextboxText = _projectFile.SampleDataFileLocationProjected;
                    }
                }
            }

            if (File.Exists(_projectFile.FieldBorderLocation))
                BlankFileLocation.TextboxText = _projectFile.FieldBorderLocation;
        }

        private void BtnNewProjectFile_Click(object sender, EventArgs e)
        {
            using var sfd = new SaveFileDialog
            {
                AddExtension = true,
                DefaultExt = "json",
                Filter = @"project v2 (.json)|*.json|All files (*.*)|*.*",
                CheckPathExists = true,
                CheckFileExists = false,
                FileName = "project.json",
                SupportMultiDottedExtensions = true,
                Title = @"Maak een project v2 bestand"
            };

            var result = sfd.ShowDialog();
            if (result != DialogResult.OK) return;

            _projectFile = new ProjectFile();
            _projectFile.SaveAs(sfd.FileName);

            // Enable next groupbox:
            GroupBoxVoorbereiding.Enabled = true;

            AddProgress("Een nieuw projectbestand is aangemaakt.", true);
        }

        private void AddProgress(string text, bool clear = false)
        {
            if (clear) LblVoortgang.Text = string.Empty;

            LblVoortgang.Text += text + Environment.NewLine;
        }

        private void WorkingFolder_TextboxUpdated(object sender, EventArgs e)
        {
            if (!Directory.Exists(WorkingFolder.TextboxText)) return;

            // Set update working folder in other select controls:
            VeldDataLocation.InitialDirectory = WorkingFolder.TextboxText;
            MonsterDataLocation.InitialDirectory = WorkingFolder.TextboxText;
            BlankFileLocation.InitialDirectory = WorkingFolder.TextboxText;

            // Save to project file:
            _projectFile.WorkingFolder = WorkingFolder.TextboxText;
        }

        private void VeldDataLocation_TextboxUpdated(object sender, EventArgs e)
        {
            if (!File.Exists(VeldDataLocation.TextboxText)) return;

            // Save to project file:
            _projectFile.FieldDataFileLocationProjected = VeldDataLocation.TextboxText;

            AddProgress("Locatie velddata-bestand is bekend. De kolommen worden ingelezen.");
            // Enable next groupbox
            GroupBoxVelddataKolommen.Enabled = true;
            // Load columns:
            PushHeaderVelddataToComboboxes();
        }

        private void MonsterDataLocation_TextboxUpdated(object sender, EventArgs e)
        {
            if (!File.Exists(MonsterDataLocation.TextboxText)) return;

            // Save to project file:
            _projectFile.SampleDataFileLocationProjected = MonsterDataLocation.TextboxText;
        }

        private void BlankFileLocation_TextboxUpdated(object sender, EventArgs e)
        {
            if (!File.Exists(BlankFileLocation.TextboxText)) return;

            // Save to project file:
            _projectFile.FieldBorderLocation = BlankFileLocation.TextboxText;

            AddProgress("De locatie van de perceelsgrens is bekend. De nuclide grids kunnen worden gemaakt.");
            // Enable next groupbox:
            GroupBoxNuclideGrids.Enabled = true;
        }

        private void ComboboxSelectedIndexChanged(object sender, EventArgs e)
        {
            CheckColumns();
        }
    }
}
