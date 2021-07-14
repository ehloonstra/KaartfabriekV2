using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using KaartfabriekUI.Service;
using Shared;
using SurferTools;

namespace KaartfabriekUI.Forms
{
    /// <summary>
    /// The main form
    /// </summary>
    public partial class MainForm : Form
    {
        private ProjectFile _projectFile;
        private readonly ApplicationSettings _applicationSettings;

        /// <inheritdoc />
        public MainForm()
        {
            InitializeComponent();
            // Load ApplicationSettings"
            _applicationSettings = ApplicationSettings.Load();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Text = $@"{Application.ProductName} v{Application.ProductVersion}";

            // Disable other tabpages:
            TabPageTemplate.Enabled = false;
            TabPageUitvoer.Enabled = false;

            if (File.Exists(_applicationSettings.TemplateLocationAkkerbouw))
            {
                SurferTemplateLocation.TextboxText = _applicationSettings.TemplateLocationAkkerbouw;
            }

            var gdalFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GDAL");
            if (Directory.Exists(gdalFolder))
                _applicationSettings.GdalLocation = gdalFolder;

            GdalFolder.TextboxText = _applicationSettings.GdalLocation;
            LevelFilesFolder.TextboxText = _applicationSettings.LevelFilesFolder;
            if (!Directory.Exists(_applicationSettings.GdalLocation) ||
                !Directory.Exists(_applicationSettings.LevelFilesFolder))
            {
                // Activate settings tab:
                tabControl1.SelectedTab = tabControl1.TabPages[nameof(TabPageInstellingen)];
            }
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
                if (result != DialogResult.OK) return "Something went wrong";

                AddProgress(Path.GetFileName(ofd.FileName + " wordt geconverteerd."));
                var retVal = new ProcessTools().ConvertLatLongToProjected(ofd.FileName, _projectFile.EpsgCode);
                if (File.Exists(retVal))
                {
                    AddProgress("Het bestand is geconverteerd naar " +  _projectFile.EpsgCode);
                    return retVal;
                }

                throw new Exception("Er ging iets fout bij het converteren.");
            }
        }

        private void BtnLoadVelddataInSurfer_Click(object sender, EventArgs e)
        {
            try
            {
                var service = new KaartfabriekService(_projectFile, AddProgress);
                var result = service.OpenDataForBlanking(WorkingFolder.TextboxText, VeldDataLocation.TextboxText,
                    MonsterDataLocation.TextboxText,
                    CboXcoord.SelectedIndex, CboYcoord.SelectedIndex, CboK40.SelectedIndex);

                // Enable when success:
                BlankFileLocation.Enabled = result;
            }
            catch (Exception exception)
            {
                AddProgress("Er ging wat fout met OpenDataForBlanking. Error: " + exception.Message);
                // swallow: throw;
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
            GroupBoxProjectInstellingen.Enabled = false;
            if (CboXcoord.SelectedIndex > -1 && CboYcoord.SelectedIndex > -1 && CboAlt.SelectedIndex > -1 &&
                CboK40.SelectedIndex > -1 && CboCs137.SelectedIndex > -1 && CboTh232.SelectedIndex > -1 &&
                CboU238.SelectedIndex > -1 && CboTotalCount.SelectedIndex > -1)
            {
                // Enable next group box:
                GroupBoxProjectInstellingen.Enabled = true;
                if (!string.IsNullOrEmpty(TxtEpsgCode.Text))
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
            // Remove possible double quotes:
            header = header.Replace("\"", "");
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
            // Save project file first to save potential grid settings changes:
            _projectFile.Save();

            var service = new KaartfabriekService(_projectFile, AddProgress);
            AddProgress("De nuclide grids worden gemaakt.");
            var bufferDistance = 10d;
            if (double.TryParse(TxtBuffer.Text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture,
                out var result))
            {
                bufferDistance = result;
            }
            service.CreateNuclideGrids(WorkingFolder.TextboxText, VeldDataLocation.TextboxText, BlankFileLocation.TextboxText, bufferDistance,
                CboXcoord.SelectedIndex, CboYcoord.SelectedIndex, CboAlt.SelectedIndex,
                CboK40.SelectedIndex, CboU238.SelectedIndex, CboTh232.SelectedIndex,
                CboCs137.SelectedIndex, CboTotalCount.SelectedIndex);
        }

        private void BtnOpenProjectFileClick(object sender, EventArgs e)
        {
            // Select or create json file:
            using var ofd = new OpenFileDialog
            {
                CheckFileExists = true,
                CheckPathExists = true,
                FileName = "project.json",
                DefaultExt = "json",
                Filter = @"project v2 (.json)|*.json|All files (*.*)|*.*",
                Multiselect = false,
                SupportMultiDottedExtensions = true,
                Title = @"Selecteer een project v2 bestand"
            };

            var result = ofd.ShowDialog();
            if (result != DialogResult.OK) return;

            if (!File.Exists(ofd.FileName)) return;

            try
            {
                _projectFile = ProjectFile.Load(ofd.FileName);

            }
            catch (Exception exception)
            {
                AddProgress("Kon het projectbestand niet openen. Error: " + exception.Message);
                return;
            }

            // Enable next groupbox:
            GroupBoxVoorbereiding.Enabled = true;

            AddProgress("Projectbestand is geopend.", true);
            ProjectFile2Gui();
        }

        private void ResetGui()
        {
            // Reset:
            VeldDataLocation.TextboxText = string.Empty;
            MonsterDataLocation.TextboxText = string.Empty;
           BlankFileLocation.TextboxText = string.Empty;
            
            GroupBoxVelddataKolommen.Enabled = false;
            GroupBoxProjectInstellingen.Enabled = false;
            GroupBoxNuclideGrids.Enabled = false;
        }

        private void ProjectFile2Gui()
        {
            // Enable tabpages:
            // TODO: Check inputs first:
            TabPageTemplate.Enabled = true;
            TabPageUitvoer.Enabled = true;

            ResetGui();

            WorkingFolder.TextboxText = Directory.Exists(_projectFile.WorkingFolder) ? _projectFile.WorkingFolder : "";
            if (File.Exists(_projectFile.FieldDataFileLocationProjected))
            {
                VeldDataLocation.TextboxText = _projectFile.FieldDataFileLocationProjected;
            }
            else
            {
                if (File.Exists(_projectFile.FieldDataFileLocation))
                {
                    var fileName = new ProcessTools().ConvertLatLongToProjected(_projectFile.FieldDataFileLocation, _projectFile.EpsgCode);
                    if (File.Exists(fileName))
                    {
                        AddProgress("Velddata is geconverteerd naar RD");
                        _projectFile.FieldDataFileLocationProjected = fileName;
                        VeldDataLocation.TextboxText = _projectFile.FieldDataFileLocationProjected;
                    }
                }
                else
                {
                    VeldDataLocation.TextboxText = string.Empty;
                    GroupBoxVelddataKolommen.Enabled = false;
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
                    var fileName = new ProcessTools().ConvertLatLongToProjected(_projectFile.SampleDataFileLocation, _projectFile.EpsgCode);
                    if (File.Exists(fileName))
                    {
                        AddProgress("Monsterdata is geconverteerd naar RD");
                        _projectFile.SampleDataFileLocationProjected = fileName;
                        MonsterDataLocation.TextboxText = _projectFile.SampleDataFileLocationProjected;
                    }
                }
                else
                {
                    MonsterDataLocation.TextboxText = string.Empty;
                    GroupBoxVelddataKolommen.Enabled = false;
                }
            }

            BlankFileLocation.TextboxText = File.Exists(_projectFile.FieldBorderLocation) ? _projectFile.FieldBorderLocation : string.Empty;

            TxtBuffer.Text = _projectFile.FieldBorderBufferSize ?? "10";

            // GWT
            CboGrondwatertrap.Data = "I; II; III+; V+; III-; V-; IV; VI; VII";
            CboGrondwatertrap.PresetValue = !string.IsNullOrEmpty(_projectFile.Gwt) ? _projectFile.Gwt : null;

            // EPSG:
            if (!string.IsNullOrEmpty(_projectFile.EpsgCode))
                TxtEpsgCode.Text = _projectFile.EpsgCode;

            TxtTemplateProjectNr.Text = _projectFile.ProjectNr;

            var pData = _projectFile.ParcelData;
            if (pData is not null)
            {
                TxtTemplateNaam.Text = pData.Customer;
                TxtTemplatePerceel.Text = pData.Name;
                TxtTemplateOmvang.Text = pData.Size;
            }

            FillGridViewFormulas();

            FillGridSettings();
        }

        private void FillGridSettings()
        {
            if (_projectFile.GridSettings is null)
            {
                _projectFile.GridSettings = new GridSettings();
                _projectFile.Save();
            }

            var gridSettings = _projectFile.GridSettings;
            TxtIdPower.Text = gridSettings.IdPower;
            TxtIdSmoothing.Text = gridSettings.IdSmoothing;
            TxtSearchNumSectors.Text = gridSettings.SearchNumSectors;
            TxtLimits.Text = gridSettings.Limits;
            TxtMaxData.Text = gridSettings.SearchMaxData;
            TxtMinData.Text = gridSettings.SearchMinData;
            TxtSearchRadius.Text = gridSettings.SearchRadius;
            TxtGridSpacing.Text = gridSettings.GridSpacing;
        }

        private void FillGridViewFormulas()
        {
            GridViewFormulas.Rows.Clear();

            // Load formulas from project file:
            List<FormulaData> formulas;
            if (_projectFile.FormulaData is not null && _projectFile.FormulaData.Any())
            {
                formulas = _projectFile.FormulaData;
            }
            else
            {
                formulas = FormulaData.GetDefaultFormulas();
                // Save to project file:
                _projectFile.FormulaData = formulas;
                _projectFile.Save();
            }

            // chbox, output, Formula, gridA, gridB GridC, gridD, minimum, maximum, level

            foreach (var formula in formulas)
            {
                GridViewFormulas.Rows.Add(formula.ToParams(false));
            }
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
            // TODO: Get default formulas from application settings
            FillGridViewFormulas();

            _projectFile.SaveAs(sfd.FileName);

            // Enable next groupbox:
            GroupBoxVoorbereiding.Enabled = true;

            AddProgress("Een nieuw projectbestand is aangemaakt.", true);
        }

        private void AddProgress(string text)
        {
            AddProgress(text, false);
        }
        private void AddProgress(string text, bool clear)
        {
            if (clear)
                LblVoortgang.Text = string.Empty;

            LblVoortgang.Text += $@"{text}{Environment.NewLine}";
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
            if (GroupBoxPerceelsgrens.Enabled)
                GroupBoxNuclideGrids.Enabled = true;
        }

        private void ComboboxSelectedIndexChanged(object sender, EventArgs e)
        {
            CheckColumns();
        }

        private void GridViewFormulas_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0) return;

            // Clicked on the header
            if (e.ColumnIndex != 0) return;
            // Clicked on the checkbox header, toggle all checkboxes:
            ToggleCheckboxes();
        }

        private void ToggleCheckboxes()
        {
            // Get value of first row:
            if (GridViewFormulas.Rows[0].Cells[0] is not DataGridViewCheckBoxCell cell) return;

            GridViewFormulas.ClearSelection();

            var currentValue = Convert.ToBoolean(cell.Value);

            // Loop:
            foreach (DataGridViewRow row in GridViewFormulas.Rows)
            {
                row.Cells[0].Value = !currentValue;
            }

            GridViewFormulas.RefreshEdit();
        }

        private void GridViewFormulas_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return; // Clicked on the header

            var row = GridViewFormulas.Rows[e.RowIndex];

            // Open modal form:
            var addEditFormulaForm = new AddEditFormula
            {
                // Send values to form:
                GridNames = _projectFile.GridNames,
                FormulaData = Row2Formula(row),
                LevelsFolder = _applicationSettings.LevelFilesFolder
            };

            var dialogResult = addEditFormulaForm.ShowDialog(this);

            // ReSharper disable once ConvertIfStatementToSwitchStatement
            if (dialogResult == DialogResult.OK)
            {
                // Get updated values from form:
                var formulaData = addEditFormulaForm.FormulaData;
                // Update grid view:
                // chbox, output, Formula, gridA, gridB GridC, gridD, minimum, maximum, level
                row.SetValues(formulaData.ToParams(true));

                // Save to project:
                _projectFile.GridNames = addEditFormulaForm.GridNames;

                // Save formulas to project file:
                SaveFormulasToProjectFile();
            }
            else if (dialogResult == DialogResult.Yes)
            {
                // Delete formula:
                GridViewFormulas.Rows.Remove(row);
                // Save formulas to project file:
                SaveFormulasToProjectFile();
            }

            // Clean up:
            addEditFormulaForm.Dispose();
        }

        private void SaveFormulasToProjectFile()
        {
            if (_projectFile.FormulaData is null)
            {
                _projectFile.FormulaData = new List<FormulaData>(GridViewFormulas.Rows.Count);
            }
            else
            {
                _projectFile.FormulaData.Clear();
            }

            var formulas = (from DataGridViewRow row in GridViewFormulas.Rows select Row2Formula(row))
                .ToList();
            _projectFile.FormulaData = formulas;
            _projectFile.Save();
        }

        private static FormulaData Row2Formula(DataGridViewRow row)
        {
            return new(
                row.Cells[1].Value.ToString(),
                row.Cells[2].Value.ToString(),
                row.Cells[3].Value.ToString(),
                row.Cells[4].Value.ToString(),
                row.Cells[5].Value.ToString(),
                row.Cells[6].Value.ToString(),
                row.Cells[7].Value.ToString(),
                row.Cells[8].Value.ToString(),
                row.Cells[9].Value.ToString(),
                row.Index
            );
        }

        private void BtnAddFormula_Click(object sender, EventArgs e)
        {
            var value = string.Empty;
            if (GuiHelpers.InputBox("Geef de naam van het nieuwe output bestand op.", "De naam van het nieuwe output bestand",
                ref value) != DialogResult.OK) return;

            // Check if not already exist:
            var names = _projectFile.GridNames.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .ToList();
            if (!names.Contains(value))
            {
                // Add to combobox list:
                _projectFile.GridNames = $"{_projectFile.GridNames};{value}";
            }

            // Open modal form:
            var addEditFormulaForm = new AddEditFormula
            {
                // Send values to form:
                GridNames = _projectFile.GridNames,
                FormulaData = new FormulaData { Output = value }
            };

            if (addEditFormulaForm.ShowDialog(this) == DialogResult.OK)
            {
                // Get updated values from form:
                var formulaData = addEditFormulaForm.FormulaData;
                // Update grid view:
                // chbox, output, Formula, gridA, gridB GridC, gridD, minimum, maximum, level
                GridViewFormulas.Rows.Add(formulaData.ToParams(true));

                // Save to project:
                _projectFile.GridNames = addEditFormulaForm.GridNames;

                // Save formulas to project:
                SaveFormulasToProjectFile();
            }

            // Clean up:
            addEditFormulaForm.Dispose();
        }

        private void BtnCreateSoilMaps_Click(object sender, EventArgs e)
        {
            // Get selected formulas:
            var selectedFormulas = (from DataGridViewRow row in GridViewFormulas.Rows
                                    let cell = row.Cells[0] as DataGridViewCheckBoxCell
                                    where cell?.Value != null
                                    where Convert.ToBoolean(cell.Value)
                                    select Row2Formula(row)).ToList();

            if (!selectedFormulas.Any())
            {
                const string msg = "Vink eerst 1 of meerdere formules aan";
                AddProgress(msg);
                MessageBox.Show(msg);
                return;
            }

            var service = new KaartfabriekService(_projectFile, AddProgress);
            GridViewFormulas.ClearSelection();
            service.CreateSoilMaps(selectedFormulas, _applicationSettings.LevelFilesFolder, ColorRow);
            AddProgress("De geselecteerde grids zijn berekend.");
        }

        private void ColorRow(int rowIndex, Color color)
        {
            GridViewFormulas.Rows[rowIndex].DefaultCellStyle.BackColor = color;

            if (color != Color.DarkGreen) return;

            if (GridViewFormulas.Rows[rowIndex].Cells[0] is not DataGridViewCheckBoxCell cell) return;
            cell.Value = false;
        }

        private void BtnTemplateCreate_Click(object sender, EventArgs e)
        {
            _projectFile.ParcelData.Customer = TxtTemplateNaam.Text;
            _projectFile.ParcelData.Name = TxtTemplatePerceel.Text;
            _projectFile.ParcelData.Size = TxtTemplateOmvang.Text;
            _projectFile.ProjectNr = TxtTemplateProjectNr.Text;
            _projectFile.Save();

            var service = new KaartfabriekService(_projectFile, AddProgress);
            service.CreateTemplate(SurferTemplateLocation.TextboxText);
            AddProgress("De template is aangemaakt.");
        }

        private void LblVoortgang_DoubleClick(object sender, EventArgs e)
        {
            // Create temp file and open it in Notepad:
            var fileLocation = Path.Combine(Path.GetTempPath(), Path.ChangeExtension(Path.GetTempFileName(), ".txt"));
            var header = $"{Text}{Environment.NewLine}Datum: {DateTime.UtcNow:f}{Environment.NewLine}Werkfolder: {_projectFile.WorkingFolder}{Environment.NewLine}";
            File.WriteAllText(fileLocation, $"{header}{Environment.NewLine}{LblVoortgang.Text}");
            ProcessTools.OpenFile(fileLocation);
        }

        private void GdalFolder_TextboxUpdated(object sender, EventArgs e)
        {
            if (!File.Exists(Path.Combine(GdalFolder.TextboxText, "gdal_translate.exe")))
            {
                MessageBox.Show(@"Kan gdal_translate niet vinden!");
                return;
            }
            if (!File.Exists(Path.Combine(GdalFolder.TextboxText, "ogr2ogr.exe")))
            {
                MessageBox.Show(@"Kan ogr2ogr niet vinden!");
                return;
            }

            _applicationSettings.GdalLocation = GdalFolder.TextboxText;
        }

        private void LevelFilesFolder_TextboxUpdated(object sender, EventArgs e)
        {
            if (!Directory.Exists(LevelFilesFolder.TextboxText))
            {
                MessageBox.Show(@"Kan de levels folder niet vinden");
                return;
            }

            _applicationSettings.LevelFilesFolder = LevelFilesFolder.TextboxText;
        }

        private void SurferTemplateLocation_TextboxUpdated(object sender, EventArgs e)
        {
            if (File.Exists(SurferTemplateLocation.TextboxText))
            {
                _applicationSettings.TemplateLocationAkkerbouw = SurferTemplateLocation.TextboxText;
            }
        }

        private void TxtSearchNumSectors_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(TxtSearchNumSectors.Text, out _))
            {
                _projectFile.GridSettings.SearchNumSectors = TxtSearchNumSectors.Text;
            }
            else
            {
                MessageBox.Show(@"Number of sectors to search is niet correct! Het moet een heel getal zijn tussen 1 en 32.");
            }
        }

        private void TxtSearchRadius_TextChanged(object sender, EventArgs e)
        {
            if (float.TryParse(TxtSearchRadius.Text, out _))
            {
                _projectFile.GridSettings.SearchRadius = TxtSearchRadius.Text;
            }
            else
            {
                MessageBox.Show(@"Search radius is niet correct! Het moet een getal zijn.");
            }
        }

        private void TxtIdPower_TextChanged(object sender, EventArgs e)
        {
            if (float.TryParse(TxtIdPower.Text, out _))
            {
                _projectFile.GridSettings.IdPower = TxtIdPower.Text;
            }
            else
            {
                MessageBox.Show(@"Inverse Distance Power is niet correct! Het moet een getal zijn.");
            }
        }

        private void TxtIdSmoothing_TextChanged(object sender, EventArgs e)
        {
            if (float.TryParse(TxtIdSmoothing.Text, out _))
            {
                _projectFile.GridSettings.IdSmoothing = TxtIdSmoothing.Text;
            }
            else
            {
                MessageBox.Show(@"Inverse Distance Smoothing is niet correct! Het moet een getal zijn.");
            }
        }

        private void TxtLimits_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(TxtLimits.Text, out _))
            {
                _projectFile.GridSettings.Limits = TxtLimits.Text;
            }
            else
            {
                MessageBox.Show(@"De Limits zijn niet correct! Het moet een heel getal zijn.");
            }
        }

        private void TxtMaxData_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(TxtMaxData.Text, out _))
            {
                _projectFile.GridSettings.SearchMaxData = TxtMaxData.Text;
            }
            else
            {
                MessageBox.Show(@"Max data to use is niet correct! Het moet een heel getal zijn tussen 1 en 128.");
            }
        }

        private void TxtMinData_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(TxtMinData.Text, out _))
            {
                _projectFile.GridSettings.SearchMinData = TxtMinData.Text;
            }
            else
            {
                MessageBox.Show(@"Min data to use is niet correct! Het moet een heel getal zijn tussen 1 en 128.");
            }
        }

        private void TxtGridSpacing_TextChanged(object sender, EventArgs e)
        {
            if (float.TryParse(TxtGridSpacing.Text, out _))
            {
                _projectFile.GridSettings.GridSpacing = TxtGridSpacing.Text;
            }
            else
            {
                MessageBox.Show(@"De Grid Spacing is niet correct! Het moet een getal zijn.");
            }
        }

        private void TxtBuffer_TextChanged(object sender, EventArgs e)
        {
            if (float.TryParse(TxtBuffer.Text, out _))
            {
                _projectFile.FieldBorderBufferSize = TxtBuffer.Text;
            }
            else
            {
                MessageBox.Show(@"De Buffer is niet correct! Het moet een getal zijn.");
            }
        }

        private void TxtEpsgCode_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TxtEpsgCode.Text)) return;

            _projectFile.EpsgCode = TxtEpsgCode.Text;
            EnableGroupBoxPerceelsgrens();
        }

        private void CboGrondwatertrap_ComboboxSelectedIndexChanged(object sender, EventArgs e)
        {
            if (CboGrondwatertrap.SelectedIndex <= 0) return;

            _projectFile.Gwt = CboGrondwatertrap.SelectedText;
            EnableGroupBoxPerceelsgrens();
        }

        private void EnableGroupBoxPerceelsgrens()
        {
            // Enable next group:
            GroupBoxPerceelsgrens.Enabled = GroupBoxVelddataKolommen.Enabled &&
                                            !string.IsNullOrEmpty(TxtEpsgCode.Text) &&
                                            !string.IsNullOrEmpty(CboGrondwatertrap.SelectedText);
        }

        private void GroupBoxPerceelsgrens_EnabledChanged(object sender, EventArgs e)
        {
            // Enable next groupbox:
            GroupBoxNuclideGrids.Enabled = File.Exists(BlankFileLocation.TextboxText);
        }

        private void BtnProjectImport_Click(object sender, EventArgs e)
        {
            AddProgress("Het projectbestand wordt geimporteerd.", true);
            using var ofd = new OpenFileDialog
            {
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = "xml",
                Filter = @"xml files|*.xml|All files (*.*)|*.*",
                Multiselect = false,
                SupportMultiDottedExtensions = true,
                Title = @"Selecteer project.xml",
                FileName = "project.xml",
                ClientGuid = Guid.Parse("{BA4B80C8-7022-4E34-BFAA-FF9C00F7F4E3}")
            };
            var result = ofd.ShowDialog();
            if (result != DialogResult.OK) return;

            try
            {
                _projectFile = new ProjectFile();
                var service = new KaartfabriekService(_projectFile, AddProgress);
                if (!service.ConvertOldProjectFile(ofd.FileName)) return;
            }
            catch (Exception exception)
            {
                AddProgress(exception.Message);
                // swallow throw;
                return;
            }

            // Select new location
            using var sfd = new SaveFileDialog
            {
                AddExtension = true,
                DefaultExt = "json",
                Filter = @"project v2 (.json)|*.json|All files (*.*)|*.*",
                CheckPathExists = true,
                CheckFileExists = false,
                FileName = "project.json",
                SupportMultiDottedExtensions = true,
                Title = @"Sla het geconverteerde bestand als een project v2 bestand op"
            };

            var resultSave = sfd.ShowDialog();
            if (resultSave != DialogResult.OK) return;

            _projectFile.WorkingFolder = Path.GetDirectoryName(sfd.FileName);
            _projectFile.SaveAs(sfd.FileName);

            ProjectFile2Gui();

            // Enable next groupbox:
            GroupBoxVoorbereiding.Enabled = true;
            // Select first tab:
            tabControl1.SelectedTab = tabControl1.TabPages[nameof(TabPageVoorbereiding)];

            AddProgress("Het projectbestand is geimporteerd en geopend.");
        }

        private void GroupBoxVelddataKolommen_EnabledChanged(object sender, EventArgs e)
        {
            CheckColumns();
            if (!GroupBoxVelddataKolommen.Enabled)
                GroupBoxProjectInstellingen.Enabled = false;
        }

        private void GroupBoxProjectInstellingen_EnabledChanged(object sender, EventArgs e)
        {
            if (!GroupBoxProjectInstellingen.Enabled)
                GroupBoxPerceelsgrens.Enabled = false;

        }

        private void BtnExportEmf_Click(object sender, EventArgs e)
        {
            try
            {
                var service = new KaartfabriekService(_projectFile, AddProgress);
                service.ExportEmf();
                AddProgress("Het exporteren naar EMF is voltooid.");
            }
            catch (Exception exception)
            {
                AddProgress("Er ging wat fout bij het exporteren naar EMF. Error: " + exception.Message);
                // swallow: throw;
            }
        }

        private void BtnExportCsv_Click(object sender, EventArgs e)
        {
            try
            {
                var service = new KaartfabriekService(_projectFile, AddProgress);
                service.ExportCsv();
                AddProgress("Het exporteren naar Csv is voltooid.");
            }
            catch (Exception exception)
            {
                AddProgress("Er ging wat fout bij het exporteren naar Csv. Error: " + exception.Message);
                // swallow: throw;
            }
        }

        private void BtnMonsterpuntenExport_Click(object sender, EventArgs e)
        {
            try
            {
                var service = new KaartfabriekService(_projectFile, AddProgress);
                service.ExportSamplePointsData();
                AddProgress("Het exporteren van de monsterpunt-data is voltooid.");
            }
            catch (Exception exception)
            {
                AddProgress("Er ging wat fout bij het exporteren van de monsterpunten. Error: " + exception.Message);
                // swallow: throw;
            }
        }
    }
}
