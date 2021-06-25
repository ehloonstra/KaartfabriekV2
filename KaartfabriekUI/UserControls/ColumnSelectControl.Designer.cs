
namespace KaartfabriekUI.UserControls
{
    partial class ColumnSelectControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label = new System.Windows.Forms.Label();
            this.cboColumn = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(3, -1);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(52, 15);
            this.label.TabIndex = 4;
            this.label.Text = "My label";
            // 
            // cboColumn
            // 
            this.cboColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboColumn.FormattingEnabled = true;
            this.cboColumn.Location = new System.Drawing.Point(3, 17);
            this.cboColumn.Name = "cboColumn";
            this.cboColumn.Size = new System.Drawing.Size(81, 23);
            this.cboColumn.TabIndex = 3;
            this.cboColumn.SelectedIndexChanged += new System.EventHandler(this.cboColumn_SelectedIndexChanged);
            // 
            // ColumnSelectControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label);
            this.Controls.Add(this.cboColumn);
            this.Name = "ColumnSelectControl";
            this.Size = new System.Drawing.Size(90, 44);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label;
        private System.Windows.Forms.ComboBox cboColumn;
    }
}
