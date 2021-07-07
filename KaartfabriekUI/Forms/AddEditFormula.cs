using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Shared;
using SurferTools;

namespace KaartfabriekUI.Forms
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

        /// <summary>
        /// The location of the levels folder
        /// </summary>
        public string LevelsFolder { get; set; }

        /// <inheritdoc />
        public AddEditFormula()
        {
            InitializeComponent();
        }

        private void BtnSaveClose_Click(object sender, EventArgs e)
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
            if (IsNewFormula)
            {
                
            }
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
            var fileEntries = Directory.GetFiles(LevelsFolder, "*.lvl", SearchOption.TopDirectoryOnly);
            var data = fileEntries.Select(Path.GetFileName).ToList();
            CboLevels.Data = string.Join(';', data);
        }

        private void CboOutput_ComboboxSelectedIndexChanged(object sender, EventArgs e)
        {
            // Enable next combobox: 
            CboGridA.Enabled = !string.IsNullOrWhiteSpace(CboOutput.SelectedText);
        }

        private void CboGridA_ComboboxSelectedIndexChanged(object sender, EventArgs e)
        {
            // Enable next combobox: 
            var enabled = !string.IsNullOrWhiteSpace(CboGridA.SelectedText);
            CboGridB.Enabled = enabled;
            TxtBoxFormule.Enabled = enabled;
        }
        private void TxtBoxFormule_TextChanged(object sender, EventArgs e)
        {
            var enabled = TxtBoxFormule.Text.Trim() != string.Empty;
            TxtBoxMinimum.Enabled = enabled;
            TxtBoxMaximum.Enabled = enabled;
            CboLevels.Enabled = enabled;
            BtnSaveClose.Enabled = enabled;
        }
        private void CboGridB_ComboboxSelectedIndexChanged(object sender, EventArgs e)
        {
            // Enable next combobox: 
            CboGridC.Enabled = !string.IsNullOrWhiteSpace(CboGridB.SelectedText);
        }

        private void CboGridC_ComboboxSelectedIndexChanged(object sender, EventArgs e)
        {
            // Enable next combobox: 
            CboGridD.Enabled = !string.IsNullOrWhiteSpace(CboGridC.SelectedText);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Navigate to the URL.
            ProcessTools.OpenUrl("http://surferhelp.goldensoftware.com/wtopics/mathematical_functions.htm");
        }

        private void BtnRemoveFormula_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                @$"Weet je zeker dat je deze formule wilt verwijderen?{Environment.NewLine}Dit kan niet ongedaan worden gemaakt.",
                @"Verwijder formule", MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button1) != DialogResult.Yes) return;

            DialogResult = DialogResult.Yes;
            Close();
        }
    }
}
