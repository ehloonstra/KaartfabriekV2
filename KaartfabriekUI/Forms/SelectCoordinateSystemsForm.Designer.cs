
namespace KaartfabriekUI.Forms
{
    partial class SelectCoordinateSystemsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectCoordinateSystemsForm));
            this.TxtSearch = new System.Windows.Forms.TextBox();
            this.BtnSearchCoordinateSystem = new System.Windows.Forms.Button();
            this.GvCoordinateSystems = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.GvCoordinateSystems)).BeginInit();
            this.SuspendLayout();
            // 
            // TxtSearch
            // 
            this.TxtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtSearch.Location = new System.Drawing.Point(12, 12);
            this.TxtSearch.Name = "TxtSearch";
            this.TxtSearch.Size = new System.Drawing.Size(548, 23);
            this.TxtSearch.TabIndex = 0;
            // 
            // BtnSearchCoordinateSystem
            // 
            this.BtnSearchCoordinateSystem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSearchCoordinateSystem.Image = ((System.Drawing.Image)(resources.GetObject("BtnSearchCoordinateSystem.Image")));
            this.BtnSearchCoordinateSystem.Location = new System.Drawing.Point(566, 7);
            this.BtnSearchCoordinateSystem.Name = "BtnSearchCoordinateSystem";
            this.BtnSearchCoordinateSystem.Size = new System.Drawing.Size(30, 30);
            this.BtnSearchCoordinateSystem.TabIndex = 12;
            this.BtnSearchCoordinateSystem.UseVisualStyleBackColor = true;
            this.BtnSearchCoordinateSystem.Click += new System.EventHandler(this.BtnSearchCoordinateSystem_Click);
            // 
            // GvCoordinateSystems
            // 
            this.GvCoordinateSystems.AllowUserToAddRows = false;
            this.GvCoordinateSystems.AllowUserToDeleteRows = false;
            this.GvCoordinateSystems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GvCoordinateSystems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GvCoordinateSystems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GvCoordinateSystems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.GvCoordinateSystems.Location = new System.Drawing.Point(12, 41);
            this.GvCoordinateSystems.MultiSelect = false;
            this.GvCoordinateSystems.Name = "GvCoordinateSystems";
            this.GvCoordinateSystems.ReadOnly = true;
            this.GvCoordinateSystems.RowTemplate.Height = 25;
            this.GvCoordinateSystems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GvCoordinateSystems.Size = new System.Drawing.Size(584, 566);
            this.GvCoordinateSystems.TabIndex = 13;
            this.GvCoordinateSystems.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.GvCoordinateSystems_MouseDoubleClick);
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Column1.HeaderText = "EPSG Code";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 90;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.HeaderText = "Coordinatensysteem";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // SelectCoordinateSystemsForm
            // 
            this.AcceptButton = this.BtnSearchCoordinateSystem;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 619);
            this.Controls.Add(this.GvCoordinateSystems);
            this.Controls.Add(this.BtnSearchCoordinateSystem);
            this.Controls.Add(this.TxtSearch);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SelectCoordinateSystemsForm";
            this.ShowInTaskbar = false;
            this.Text = "Surfer coördinatensystemen";
            ((System.ComponentModel.ISupportInitialize)(this.GvCoordinateSystems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TxtSearch;
        private System.Windows.Forms.Button BtnSearchCoordinateSystem;
        private System.Windows.Forms.DataGridView GvCoordinateSystems;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    }
}