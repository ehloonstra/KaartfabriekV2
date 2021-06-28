
namespace KaartfabriekUI
{
    partial class AddEditFormula
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddEditFormula));
            this.CboOutput = new KaartfabriekUI.UserControls.ColumnSelectControl();
            this.CboGridA = new KaartfabriekUI.UserControls.ColumnSelectControl();
            this.CboGridB = new KaartfabriekUI.UserControls.ColumnSelectControl();
            this.CboGridC = new KaartfabriekUI.UserControls.ColumnSelectControl();
            this.CboGridD = new KaartfabriekUI.UserControls.ColumnSelectControl();
            this.TxtBoxFormule = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtBoxMinimum = new System.Windows.Forms.TextBox();
            this.TxtBoxMaximum = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.CboLevels = new KaartfabriekUI.UserControls.ColumnSelectControl();
            this.BtnSaveClose = new System.Windows.Forms.Button();
            this.BtnAddFormula = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CboOutput
            // 
            this.CboOutput.Data = null;
            this.CboOutput.Label = "Output:";
            this.CboOutput.Location = new System.Drawing.Point(12, 12);
            this.CboOutput.Name = "CboOutput";
            this.CboOutput.PresetValue = null;
            this.CboOutput.SelectedText = "";
            this.CboOutput.Size = new System.Drawing.Size(200, 44);
            this.CboOutput.TabIndex = 0;
            this.CboOutput.ComboboxSelectedIndexChanged += new System.EventHandler(this.CboOutput_ComboboxSelectedIndexChanged);
            // 
            // CboGridA
            // 
            this.CboGridA.Data = null;
            this.CboGridA.Enabled = false;
            this.CboGridA.Label = "Grid A:";
            this.CboGridA.Location = new System.Drawing.Point(12, 62);
            this.CboGridA.Name = "CboGridA";
            this.CboGridA.PresetValue = null;
            this.CboGridA.SelectedText = "";
            this.CboGridA.Size = new System.Drawing.Size(200, 44);
            this.CboGridA.TabIndex = 1;
            this.CboGridA.ComboboxSelectedIndexChanged += new System.EventHandler(this.CboGridA_ComboboxSelectedIndexChanged);
            // 
            // CboGridB
            // 
            this.CboGridB.Data = null;
            this.CboGridB.Enabled = false;
            this.CboGridB.Label = "Grid B:";
            this.CboGridB.Location = new System.Drawing.Point(218, 62);
            this.CboGridB.Name = "CboGridB";
            this.CboGridB.PresetValue = null;
            this.CboGridB.SelectedText = "";
            this.CboGridB.Size = new System.Drawing.Size(200, 44);
            this.CboGridB.TabIndex = 2;
            // 
            // CboGridC
            // 
            this.CboGridC.Data = null;
            this.CboGridC.Enabled = false;
            this.CboGridC.Label = "Grid C:";
            this.CboGridC.Location = new System.Drawing.Point(12, 112);
            this.CboGridC.Name = "CboGridC";
            this.CboGridC.PresetValue = null;
            this.CboGridC.SelectedText = "";
            this.CboGridC.Size = new System.Drawing.Size(200, 44);
            this.CboGridC.TabIndex = 3;
            this.CboGridC.ComboboxSelectedIndexChanged += new System.EventHandler(this.CboGridC_ComboboxSelectedIndexChanged);
            // 
            // CboGridD
            // 
            this.CboGridD.Data = null;
            this.CboGridD.Enabled = false;
            this.CboGridD.Label = "Grid D:";
            this.CboGridD.Location = new System.Drawing.Point(218, 112);
            this.CboGridD.Name = "CboGridD";
            this.CboGridD.PresetValue = null;
            this.CboGridD.SelectedText = "";
            this.CboGridD.Size = new System.Drawing.Size(200, 44);
            this.CboGridD.TabIndex = 4;
            this.CboGridD.ComboboxSelectedIndexChanged += new System.EventHandler(this.CboGridD_ComboboxSelectedIndexChanged);
            // 
            // TxtBoxFormule
            // 
            this.TxtBoxFormule.Enabled = false;
            this.TxtBoxFormule.Location = new System.Drawing.Point(12, 177);
            this.TxtBoxFormule.Name = "TxtBoxFormule";
            this.TxtBoxFormule.Size = new System.Drawing.Size(400, 23);
            this.TxtBoxFormule.TabIndex = 5;
            this.TxtBoxFormule.TextChanged += new System.EventHandler(this.TxtBoxFormule_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 159);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "Formule:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 212);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 15);
            this.label2.TabIndex = 8;
            this.label2.Text = "Minimum:";
            // 
            // TxtBoxMinimum
            // 
            this.TxtBoxMinimum.Enabled = false;
            this.TxtBoxMinimum.Location = new System.Drawing.Point(12, 230);
            this.TxtBoxMinimum.Name = "TxtBoxMinimum";
            this.TxtBoxMinimum.Size = new System.Drawing.Size(189, 23);
            this.TxtBoxMinimum.TabIndex = 9;
            // 
            // TxtBoxMaximum
            // 
            this.TxtBoxMaximum.Enabled = false;
            this.TxtBoxMaximum.Location = new System.Drawing.Point(218, 230);
            this.TxtBoxMaximum.Name = "TxtBoxMaximum";
            this.TxtBoxMaximum.Size = new System.Drawing.Size(194, 23);
            this.TxtBoxMaximum.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(218, 212);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 15);
            this.label3.TabIndex = 10;
            this.label3.Text = "Maximum:";
            // 
            // CboLevels
            // 
            this.CboLevels.Data = null;
            this.CboLevels.Enabled = false;
            this.CboLevels.Label = "Level file:";
            this.CboLevels.Location = new System.Drawing.Point(12, 259);
            this.CboLevels.Name = "CboLevels";
            this.CboLevels.PresetValue = null;
            this.CboLevels.SelectedText = "";
            this.CboLevels.Size = new System.Drawing.Size(189, 44);
            this.CboLevels.TabIndex = 12;
            // 
            // BtnSaveClose
            // 
            this.BtnSaveClose.Enabled = false;
            this.BtnSaveClose.Location = new System.Drawing.Point(12, 318);
            this.BtnSaveClose.Name = "BtnSaveClose";
            this.BtnSaveClose.Size = new System.Drawing.Size(399, 33);
            this.BtnSaveClose.TabIndex = 13;
            this.BtnSaveClose.TabStop = false;
            this.BtnSaveClose.Text = "Opslaan && sluiten";
            this.BtnSaveClose.UseVisualStyleBackColor = true;
            this.BtnSaveClose.Click += new System.EventHandler(this.BtnSaveClose_Click);
            // 
            // BtnAddFormula
            // 
            this.BtnAddFormula.AutoSize = true;
            this.BtnAddFormula.Image = ((System.Drawing.Image)(resources.GetObject("BtnAddFormula.Image")));
            this.BtnAddFormula.Location = new System.Drawing.Point(218, 26);
            this.BtnAddFormula.Name = "BtnAddFormula";
            this.BtnAddFormula.Size = new System.Drawing.Size(37, 30);
            this.BtnAddFormula.TabIndex = 14;
            this.BtnAddFormula.UseVisualStyleBackColor = true;
            this.BtnAddFormula.Click += new System.EventHandler(this.BtnAddFormula_Click);
            // 
            // AddEditFormula
            // 
            this.AcceptButton = this.BtnSaveClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(426, 363);
            this.Controls.Add(this.BtnAddFormula);
            this.Controls.Add(this.BtnSaveClose);
            this.Controls.Add(this.CboLevels);
            this.Controls.Add(this.TxtBoxMaximum);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TxtBoxMinimum);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TxtBoxFormule);
            this.Controls.Add(this.CboGridD);
            this.Controls.Add(this.CboGridC);
            this.Controls.Add(this.CboGridB);
            this.Controls.Add(this.CboGridA);
            this.Controls.Add(this.CboOutput);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "AddEditFormula";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Edit Formula";
            this.Load += new System.EventHandler(this.AddEditFormula_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UserControls.ColumnSelectControl CboOutput;
        private UserControls.ColumnSelectControl CboGridA;
        private UserControls.ColumnSelectControl CboGridB;
        private UserControls.ColumnSelectControl CboGridC;
        private UserControls.ColumnSelectControl CboGridD;
        private System.Windows.Forms.TextBox TxtBoxFormule;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtBoxMinimum;
        private System.Windows.Forms.TextBox TxtBoxMaximum;
        private System.Windows.Forms.Label label3;
        private UserControls.ColumnSelectControl CboLevels;
        private System.Windows.Forms.Button BtnSaveClose;
        private System.Windows.Forms.Button BtnAddFormula;
    }
}