using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using KaartfabriekUI.Service;
using Surfer;
using SurferTools;

namespace KaartfabriekUI
{
    /// <summary>
    /// The main form
    /// </summary>
    public partial class MainForm : Form
    {
        /// <inheritdoc />
        public MainForm()
        {
            InitializeComponent();
        }

        private void BtnReprojectVelddataClick(object sender, EventArgs e)
        {
            var result = SelectFileToProject(@"Selecteer Velddata in LL");
            if (File.Exists(result))
            {
                VeldDataLocation.TextboxText = result;
            }
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
                    return ProcessTools.ConvertLatLongToProjected(ofd.FileName, "EPSG:28992", "RD");
                }
            }

            return "Something went wrong";
        }

        private void BtnLoadVelddataInSurfer_Click(object sender, EventArgs e)
        {
            // TODO: Check input, enable this button when input files exist.

            try
            {
                var service = new KaartfabriekService();
                var result = service.OpenDataForBlanking(WorkingFolder.TextboxText, VeldDataLocation.TextboxText,
                    MonsterDataLocation.TextboxText, 
                    CboXcoord.SelectedIndex + 1, CboYcoord.SelectedIndex + 1,  CboK40.SelectedIndex + 1);

                // Enable when success:
                BlankFileLocation.Enabled = result;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }

        }

        private void WorkingFolder_TextboxUpdated(object sender, EventArgs e)
        {
            // Set update working folder in other select controls:
            VeldDataLocation.InitialDirectory = WorkingFolder.TextboxText;
            MonsterDataLocation.InitialDirectory = WorkingFolder.TextboxText;
            BlankFileLocation.InitialDirectory = WorkingFolder.TextboxText;
        }

        private void BtnReadColumns_Click(object sender, EventArgs e)
        {
            PushHeaderVelddataToComboboxes();
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
            var service = new KaartfabriekService();
            service.CreateNuclideGrids(WorkingFolder.TextboxText, VeldDataLocation.TextboxText, BlankFileLocation.TextboxText,
                CboXcoord.SelectedIndex + 1, CboYcoord.SelectedIndex + 1, CboAlt.SelectedIndex + 1,
                CboK40.SelectedIndex + 1, CboU238.SelectedIndex + 1, CboTh232.SelectedIndex + 1,
                CboCs137.SelectedIndex + 1, CboTotalCount.SelectedIndex + 1);
        }
    }
}
