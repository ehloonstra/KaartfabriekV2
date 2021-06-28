using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Shared;

namespace KaartfabriekUI
{
    /// <inheritdoc />
    public partial class AddEditFormula : Form
    {
        /// <summary>
        /// The formula data as a JSON string
        /// </summary>
        public FormulaData FormulaData { get; set; }

        /// <summary>
        /// The possible names of the grids
        /// </summary>
        public string GridNames { get; set; }

        /// <summary>
        /// Is it a new formula?
        /// </summary>
        public bool IsNewFormula { get; set; }

        /// <inheritdoc />
        public AddEditFormula()
        {
            InitializeComponent();
        }

        private void BtnSaveClose_Click(object sender, System.EventArgs e)
        {
            // Save form data to object:
            FormulaData.Formula = TxtBoxFormule.Text;
            FormulaData.Output = CboOutput.SelectedText;
            FormulaData.GridA = CboGridA.SelectedText;
            FormulaData.GridB = CboGridB.SelectedText;
            FormulaData.GridC = CboGridC.SelectedText;
            FormulaData.GridD = CboGridD.SelectedText;
            FormulaData.Minimum = TxtBoxMinimum.Text;
            FormulaData.Maximum = TxtBoxMaximum.Text;
            FormulaData.LevelFile = CboLevels.SelectedText;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void AddEditFormula_Load(object sender, EventArgs e)
        {
            // Load data for the comboboxes:
            SetLevelFiles();
            SetGridComboxes();

            // Pre-select the form items:
            // Formula, output, gridA, gridB GridC, gridD, minimum, maximum, level
            TxtBoxFormule.Text = FormulaData.Formula;
            CboOutput.PresetValue = FormulaData.Output;
            CboGridA.PresetValue = FormulaData.GridA;
            CboGridB.PresetValue = FormulaData.GridB;
            CboGridC.PresetValue = FormulaData.GridC;
            CboGridD.PresetValue = FormulaData.GridD;
            TxtBoxMinimum.Text = FormulaData.Minimum;
            TxtBoxMaximum.Text = FormulaData.Maximum;
            CboLevels.PresetValue = FormulaData.LevelFile;
        }

        private void SetGridComboxes()
        {
            CboOutput.Data = GridNames;
            CboGridA.Data = GridNames;
            CboGridB.Data = GridNames;
            CboGridC.Data = GridNames;
            CboGridD.Data = GridNames;
        }

        private void SetLevelFiles()
        {
            // TODO: Make path flexible:
            var fileEntries = Directory.GetFiles(@"D:\dev\TopX\Loonstra\TSC Tools\Kaartfabriek\Steven", "*.lvl", SearchOption.TopDirectoryOnly);
            var data = fileEntries.Select(Path.GetFileName).ToList();
            CboLevels.Data = string.Join(';', data);
        }

        private void BtnAddFormula_Click(object sender, EventArgs e)
        {
            var value = string.Empty;
            if (GuiHelpers.InputBox("Geef de naam van het nieuwe output bestand op.", "De naam van het nieuwe output bestand",
                ref value) != DialogResult.OK) return;

            IsNewFormula = true;

            CboOutput.PresetValue = value;

            // Check if not already exist:
            var names = GridNames.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .ToList();
            if (!names.Contains(value))
            {
                // Add to combobox list:
                GridNames = $"{GridNames};{value}";
                // Reset combo box:
                CboOutput.Data = GridNames;
            }

            // Maak overige forms leeg:
            CboGridA.Reset();

        }

        private void CboOutput_ComboboxSelectedIndexChanged(object sender, EventArgs e)
        {
            // Enable next combobox: 
            CboGridA.PresetValue = FormulaData.GridA;
            CboGridA.Enabled = !string.IsNullOrWhiteSpace(CboOutput.SelectedText);

        }

        private void CboGridA_ComboboxSelectedIndexChanged(object sender, EventArgs e)
        {
            // Enable next combobox: 
            CboGridB.PresetValue = FormulaData.GridB;
            CboGridB.Enabled = !string.IsNullOrWhiteSpace(CboGridA.SelectedText);
            TxtBoxFormule.Enabled = !string.IsNullOrWhiteSpace(CboGridA.SelectedText);
            CboLevels.Enabled = true;
        }

        private void CboGridC_ComboboxSelectedIndexChanged(object sender, EventArgs e)
        {
            // Enable next combobox: 
            CboGridC.PresetValue = FormulaData.GridC;
            CboGridC.Enabled = !string.IsNullOrWhiteSpace(CboGridB.SelectedText);
        }

        private void CboGridD_ComboboxSelectedIndexChanged(object sender, EventArgs e)
        {
            // Enable next combobox: 
            CboGridD.PresetValue = FormulaData.GridD;
            CboGridD.Enabled = !string.IsNullOrWhiteSpace(CboGridC.SelectedText);
        }

        private void TxtBoxFormule_TextChanged(object sender, EventArgs e)
        {
            var result = TxtBoxFormule.Text.Trim() != string.Empty;
            TxtBoxMinimum.Enabled = result;
            TxtBoxMaximum.Enabled = result;
            BtnSaveClose.Enabled = result;
        }
    }
}
