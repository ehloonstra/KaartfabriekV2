using Shared;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace KaartfabriekUI.Forms
{
    /// <inheritdoc />
    public partial class SelectCoordinateSystemsForm : Form
    {
        /// <summary>
        /// The list of coordinate systems supported by Surfer
        /// </summary>
        public IEnumerable<SurferEpsgData> CoordSystems;

        /// <summary>
        /// The selected EPSG code by the user
        /// </summary>
        public string SelectedEpsgCode;

        /// <inheritdoc />
        public SelectCoordinateSystemsForm()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Fill the grid with the coordinate systems
        /// </summary>
        public void FillGridView()
        {
            // Show all systems:
            FilterGridView(CoordSystems);
        }

        private void BtnSearchCoordinateSystem_Click(object sender, System.EventArgs e)
        {
            if (TxtSearch.Text?.Length > 2)
            {
                FilterGridView(CoordSystems.Where(x =>
                    x.Posc.ToUpper().Contains(TxtSearch.Text.ToUpper()) ||
                    x.Name.ToUpper().Contains(TxtSearch.Text.ToUpper())));
            }
        }

        private void FilterGridView(IEnumerable<SurferEpsgData> filteredData)
        {
            GvCoordinateSystems.Rows.Clear();
            foreach (var coordSystem in filteredData)
            {
                GvCoordinateSystems.Rows.Add(coordSystem.Posc, coordSystem.SplitName);
            }
        }

        private void GvCoordinateSystems_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Save return value:
            SelectedEpsgCode = GvCoordinateSystems.SelectedRows[0].Cells[0].Value.ToString();
            DialogResult = DialogResult.OK;
            // Close this form:
            Close();
        }
    }
}
