using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace KaartfabriekUI.UserControls
{
    /// <summary>
    /// User control to select a file of folder
    /// </summary>
    public partial class TextSelectControl : UserControl
    {
        private SelectType _selectType;

        /// <inheritdoc />
        public TextSelectControl()
        {
            InitializeComponent();
            SelectionType = SelectType.Folder; // Default value
        }


        /// <summary>
        /// Text displayed in the textbox
        /// </summary>
        [Description("Text displayed in the textbox"), Category("Data")]
        public string TextboxText
        {
            get => textbox.Text;
            set
            {
                textbox.Text = value;
                textbox.Select(textbox.Text.Length, 0); // Show end of text
            }
        }

        /// <summary>
        /// Label displayed above the textbox
        /// </summary>
        [Description("Label displayed above the textbox"), Category("Data")]
        public string Label
        {
            get => label.Text;
            set => label.Text = value;
        }

        /// <summary>
        /// Initial directory for the file select dialogs
        /// </summary>
        [Description("Initial directory for the file select dialogs"), Category("Data")]
        public string InitialDirectory { get; set; }

        /// <summary>
        /// Type of selection, file or folder
        /// </summary>
        [Description("Type of selection"), Category("Data")]
        public SelectType SelectionType
        {
            get => _selectType;
            set
            {
                _selectType = value;
                btnSelectFile.Visible = _selectType == SelectType.File;
                btnSelectFolder.Visible = _selectType == SelectType.Folder;
            }
        }

        /// <summary>
        /// File filter
        /// </summary>
        [Description("File Filter"), Category("Custom")]
        public string FileFilter { get; set; } = "csv files|*.csv";

        /// <summary>
        /// Event raised when the textbox was changed
        /// </summary>
        [Description("Event raised when the textbox was changed")]
        public event EventHandler TextboxUpdated;

        private void BtnSelectFolderClick(object sender, EventArgs e)
        {
            using var fbd = new FolderBrowserDialog
            {
                Description = @"Selecteer de werkfolder voor dit project",
                ShowNewFolderButton = false
            };
            // Show the FolderBrowserDialog.
            var result = fbd.ShowDialog();
            if (result != DialogResult.OK) return;

            if (!Directory.Exists(fbd.SelectedPath)) return;

            TextboxText = fbd.SelectedPath;
            TextboxUpdated?.Invoke(this, new EventArgs());
        }

        private void BtnSelectFileClick(object sender, System.EventArgs e)
        {
            using var ofd = new OpenFileDialog
            {
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = "csv",
                Filter = @"All files (*.*)|*.*",
                Multiselect = false,
                SupportMultiDottedExtensions = true,
                Title = @"Selecteer CSV-bestand"
            };

            if (!string.IsNullOrWhiteSpace(FileFilter))
            {
                ofd.Filter = $@"{FileFilter}|{ofd.Filter}";
            }

            if (!string.IsNullOrWhiteSpace(InitialDirectory) && Directory.Exists(InitialDirectory))
            {
                ofd.InitialDirectory = InitialDirectory;
            }

            var result = ofd.ShowDialog();
            if (result != DialogResult.OK) return;

            if (!File.Exists(ofd.FileName)) return;
            TextboxText = ofd.FileName;
            TextboxUpdated?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Enumaration of the possible types, file or folder
        /// </summary>
        public enum SelectType
        {
            /// <summary>
            /// Folder
            /// </summary>
            Folder,

            /// <summary>
            /// File
            /// </summary>
            File
        }

        private void textbox_TextChanged(object sender, EventArgs e)
        {
            TextboxUpdated?.Invoke(sender, e);
        }
    }
}
