using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace KaartfabriekUI.UserControls
{
    /// <summary>
    /// User control to select the column
    /// </summary>
    public partial class ColumnSelectControl : UserControl
    {
        private string _data;

        /// <inheritdoc />
        public ColumnSelectControl()
        {
            InitializeComponent();

            // Could be already set on design time:
            SetDataSource();
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
        /// Text selected in the combobox
        /// </summary>
        [Description("Text selected in the combobox"), Category("Data")]
        public string SelectedText
        {
            get => cboColumn.Text;
            set => cboColumn.Text = value;
        }

        /// <summary>
        /// Selected index of the combobox (zero-based)
        /// </summary>
        [Description("Selected index of the combobox (zero-based)"), Category("Data")]
        public int SelectedIndex => cboColumn.SelectedIndex;

        /// <summary>
        /// Preselect this value in the combobox
        /// </summary>
        [Description("Preselect this value in the combobox"), Category("Data")]
        public string PresetValue { get; set; }

        /// <summary>
        /// String with the possible columns, seperated by semicolon
        /// </summary>
        [Description("String with the possible columns, seperated by semicolon"), Category("Data")]
        public string Data
        {
            get => _data;
            set
            {
                _data = value;
                // When set at runtime
                SetDataSource();
            }
        }

        private void SetDataSource()
        {
            if (string.IsNullOrWhiteSpace(Data)) return;
            var columns = Data.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .ToList();
            var cboItems = columns.Select((column, index) => new ComboboxItem
            {
                Id = index,
                Value = column
            }).ToList();

            cboColumn.DataSource = cboItems;
            cboColumn.DisplayMember = nameof(ComboboxItem.Value);
            cboColumn.ValueMember = nameof(ComboboxItem.Id);

            // Preselect:
            var item = cboItems.FirstOrDefault(x => x.Value.Trim().Equals(PresetValue.Trim()));
            if (item is not null)
            {
                cboColumn.SelectedIndex = item.Id;
            }
        }

        /// <summary>
        /// Helper class to hold the combobox item
        /// </summary>
        public class ComboboxItem
        {
            /// <summary>
            /// The id of the item
            /// </summary>
            public int Id { get; set; }
            
            /// <summary>
            /// The display value of the item
            /// </summary>
            public string Value { get; set; }   
        }
    }
}
