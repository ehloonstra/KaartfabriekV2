using System;
using System.IO;
using System.Windows.Forms;
using KaartfabriekUI.Service;
using Surfer;
using SurferTools;

namespace KaartfabriekUI
{
    public partial class MainForm : Form
    {
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
                    MonsterDataLocation.TextboxText);

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
    }
}
