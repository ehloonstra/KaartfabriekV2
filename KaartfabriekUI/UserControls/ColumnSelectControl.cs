using System;
using System.Collections.Generic;
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
        private string _presetValue;
        private List<ComboboxItem> _cboItems;

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
        public string PresetValue
        {
            get => _presetValue;
            set
            {
                _presetValue = value;
                PreSelect();
            }
        }

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

        /// <summary>
        /// Event raised when the textbox was changed
        /// </summary>
        [Description("Event raised when the combobox was changed")]
        public event EventHandler ComboboxSelectedIndexChanged;


        /// <summary>
        /// Reset the selection:
        /// </summary>
        public void Reset()
        {
            cboColumn.SelectedIndex = -1;
        }


        private void cboColumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboboxSelectedIndexChanged?.Invoke(this, e);
        }

        private void SetDataSource()
        {
            if (string.IsNullOrWhiteSpace(Data)) return;
            var columns = Data.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .ToList();
            _cboItems = columns.Select((column, index) => new ComboboxItem
            {
                Id = index + 1,
                Value = column
            }).ToList();

            _cboItems.Insert(0, new ComboboxItem { Id = -1, Value = "" });

            cboColumn.DataSource = _cboItems;
            cboColumn.DisplayMember = nameof(ComboboxItem.Value);
            cboColumn.ValueMember = nameof(ComboboxItem.Id);

            if (string.IsNullOrEmpty(PresetValue)) return;

            // Preselect:
            PreSelect();
        }

        private void PreSelect()
        {
            if (string.IsNullOrWhiteSpace(PresetValue)) return;

            var item = _cboItems?.FirstOrDefault(x => x.Value.Trim().ToLower().Equals(PresetValue.Trim().ToLower()));
            if (item != null)
            {
                cboColumn.SelectedIndex = item.Id;
            }
            else
            {
                cboColumn.SelectedIndex = -1;
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
