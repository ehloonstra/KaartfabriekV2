
namespace KaartfabriekUI
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.BlankFileLocation = new KaartfabriekUI.UserControls.TextSelectControl();
            this.MonsterDataLocation = new KaartfabriekUI.UserControls.TextSelectControl();
            this.VeldDataLocation = new KaartfabriekUI.UserControls.TextSelectControl();
            this.WorkingFolder = new KaartfabriekUI.UserControls.TextSelectControl();
            this.btnReprojectMonsterdata = new System.Windows.Forms.Button();
            this.btnReprojectVelddata = new System.Windows.Forms.Button();
            this.BtnOpenProject = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.Voorbereiding = new System.Windows.Forms.GroupBox();
            this.GroupBoxVelddataKolommen = new System.Windows.Forms.GroupBox();
            this.CboTotalCount = new KaartfabriekUI.UserControls.ColumnSelectControl();
            this.CboCs137 = new KaartfabriekUI.UserControls.ColumnSelectControl();
            this.CboTh232 = new KaartfabriekUI.UserControls.ColumnSelectControl();
            this.CboU238 = new KaartfabriekUI.UserControls.ColumnSelectControl();
            this.CboK40 = new KaartfabriekUI.UserControls.ColumnSelectControl();
            this.CboAlt = new KaartfabriekUI.UserControls.ColumnSelectControl();
            this.CboYcoord = new KaartfabriekUI.UserControls.ColumnSelectControl();
            this.CboXcoord = new KaartfabriekUI.UserControls.ColumnSelectControl();
            this.BtnReadColumns = new System.Windows.Forms.Button();
            this.GroupBoxPerceelsgrens = new System.Windows.Forms.GroupBox();
            this.BtnLoadVelddataInSurfer = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.GroupBoxNuclideGrids = new System.Windows.Forms.GroupBox();
            this.BtnMaakNuclideGrids = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.LblVoortgang = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.Voorbereiding.SuspendLayout();
            this.GroupBoxVelddataKolommen.SuspendLayout();
            this.GroupBoxPerceelsgrens.SuspendLayout();
            this.GroupBoxNuclideGrids.SuspendLayout();
            this.SuspendLayout();
            // 
            // BlankFileLocation
            // 
            this.BlankFileLocation.FileFilter = "blank files|*.bln";
            this.BlankFileLocation.InitialDirectory = null;
            this.BlankFileLocation.Label = "Locatie van de blank file:";
            this.BlankFileLocation.Location = new System.Drawing.Point(6, 117);
            this.BlankFileLocation.Name = "BlankFileLocation";
            this.BlankFileLocation.SelectionType = KaartfabriekUI.UserControls.TextSelectControl.SelectType.File;
            this.BlankFileLocation.Size = new System.Drawing.Size(455, 44);
            this.BlankFileLocation.TabIndex = 2;
            this.BlankFileLocation.TextboxText = "";
            this.toolTip1.SetToolTip(this.BlankFileLocation, "Selecteer de locatie van de blank file");
            // 
            // MonsterDataLocation
            // 
            this.MonsterDataLocation.FileFilter = "csv files|*.csv";
            this.MonsterDataLocation.InitialDirectory = null;
            this.MonsterDataLocation.Label = "Locatie van de monsterdata:";
            this.MonsterDataLocation.Location = new System.Drawing.Point(6, 122);
            this.MonsterDataLocation.Name = "MonsterDataLocation";
            this.MonsterDataLocation.SelectionType = KaartfabriekUI.UserControls.TextSelectControl.SelectType.File;
            this.MonsterDataLocation.Size = new System.Drawing.Size(419, 44);
            this.MonsterDataLocation.TabIndex = 2;
            this.MonsterDataLocation.TextboxText = "";
            this.toolTip1.SetToolTip(this.MonsterDataLocation, "Selecteer de locatie van de monsterdata");
            // 
            // VeldDataLocation
            // 
            this.VeldDataLocation.FileFilter = "csv files|*.csv";
            this.VeldDataLocation.InitialDirectory = null;
            this.VeldDataLocation.Label = "Locatie van de velddata:";
            this.VeldDataLocation.Location = new System.Drawing.Point(6, 72);
            this.VeldDataLocation.Name = "VeldDataLocation";
            this.VeldDataLocation.SelectionType = KaartfabriekUI.UserControls.TextSelectControl.SelectType.File;
            this.VeldDataLocation.Size = new System.Drawing.Size(419, 44);
            this.VeldDataLocation.TabIndex = 1;
            this.VeldDataLocation.TextboxText = "";
            this.toolTip1.SetToolTip(this.VeldDataLocation, "Selecteer de locatie van de velddata");
            // 
            // WorkingFolder
            // 
            this.WorkingFolder.FileFilter = "csv files|*.csv";
            this.WorkingFolder.InitialDirectory = null;
            this.WorkingFolder.Label = "Selecteer de werkfolder:";
            this.WorkingFolder.Location = new System.Drawing.Point(6, 22);
            this.WorkingFolder.Name = "WorkingFolder";
            this.WorkingFolder.SelectionType = KaartfabriekUI.UserControls.TextSelectControl.SelectType.Folder;
            this.WorkingFolder.Size = new System.Drawing.Size(455, 44);
            this.WorkingFolder.TabIndex = 0;
            this.WorkingFolder.TextboxText = "";
            this.toolTip1.SetToolTip(this.WorkingFolder, "Selecteer de locatie van de werkfolder");
            // 
            // btnReprojectMonsterdata
            // 
            this.btnReprojectMonsterdata.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReprojectMonsterdata.AutoSize = true;
            this.btnReprojectMonsterdata.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnReprojectMonsterdata.Image = ((System.Drawing.Image)(resources.GetObject("btnReprojectMonsterdata.Image")));
            this.btnReprojectMonsterdata.Location = new System.Drawing.Point(431, 136);
            this.btnReprojectMonsterdata.Name = "btnReprojectMonsterdata";
            this.btnReprojectMonsterdata.Size = new System.Drawing.Size(30, 30);
            this.btnReprojectMonsterdata.TabIndex = 6;
            this.toolTip1.SetToolTip(this.btnReprojectMonsterdata, "Selecteer monsterdata en converteer naar RD");
            this.btnReprojectMonsterdata.UseVisualStyleBackColor = true;
            // 
            // btnReprojectVelddata
            // 
            this.btnReprojectVelddata.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReprojectVelddata.AutoSize = true;
            this.btnReprojectVelddata.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnReprojectVelddata.Image = ((System.Drawing.Image)(resources.GetObject("btnReprojectVelddata.Image")));
            this.btnReprojectVelddata.Location = new System.Drawing.Point(431, 86);
            this.btnReprojectVelddata.Name = "btnReprojectVelddata";
            this.btnReprojectVelddata.Size = new System.Drawing.Size(30, 30);
            this.btnReprojectVelddata.TabIndex = 7;
            this.toolTip1.SetToolTip(this.btnReprojectVelddata, "Selecteer velddata en converteer nar RD");
            this.btnReprojectVelddata.UseVisualStyleBackColor = true;
            // 
            // BtnOpenProject
            // 
            this.BtnOpenProject.Location = new System.Drawing.Point(12, 12);
            this.BtnOpenProject.Name = "BtnOpenProject";
            this.BtnOpenProject.Size = new System.Drawing.Size(107, 33);
            this.BtnOpenProject.TabIndex = 8;
            this.BtnOpenProject.Text = "Open project";
            this.toolTip1.SetToolTip(this.BtnOpenProject, "Open het project (*.json)");
            this.BtnOpenProject.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 51);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(490, 645);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.Voorbereiding);
            this.tabPage1.Controls.Add(this.GroupBoxVelddataKolommen);
            this.tabPage1.Controls.Add(this.GroupBoxPerceelsgrens);
            this.tabPage1.Controls.Add(this.GroupBoxNuclideGrids);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(482, 617);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Voorbereiding";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // Voorbereiding
            // 
            this.Voorbereiding.Controls.Add(this.btnReprojectVelddata);
            this.Voorbereiding.Controls.Add(this.btnReprojectMonsterdata);
            this.Voorbereiding.Controls.Add(this.MonsterDataLocation);
            this.Voorbereiding.Controls.Add(this.VeldDataLocation);
            this.Voorbereiding.Controls.Add(this.WorkingFolder);
            this.Voorbereiding.Location = new System.Drawing.Point(6, 6);
            this.Voorbereiding.Name = "Voorbereiding";
            this.Voorbereiding.Size = new System.Drawing.Size(467, 174);
            this.Voorbereiding.TabIndex = 8;
            this.Voorbereiding.TabStop = false;
            this.Voorbereiding.Text = "1. Voorbereiding";
            // 
            // GroupBoxVelddataKolommen
            // 
            this.GroupBoxVelddataKolommen.Controls.Add(this.CboTotalCount);
            this.GroupBoxVelddataKolommen.Controls.Add(this.CboCs137);
            this.GroupBoxVelddataKolommen.Controls.Add(this.CboTh232);
            this.GroupBoxVelddataKolommen.Controls.Add(this.CboU238);
            this.GroupBoxVelddataKolommen.Controls.Add(this.CboK40);
            this.GroupBoxVelddataKolommen.Controls.Add(this.CboAlt);
            this.GroupBoxVelddataKolommen.Controls.Add(this.CboYcoord);
            this.GroupBoxVelddataKolommen.Controls.Add(this.CboXcoord);
            this.GroupBoxVelddataKolommen.Controls.Add(this.BtnReadColumns);
            this.GroupBoxVelddataKolommen.Enabled = false;
            this.GroupBoxVelddataKolommen.Location = new System.Drawing.Point(5, 186);
            this.GroupBoxVelddataKolommen.Name = "GroupBoxVelddataKolommen";
            this.GroupBoxVelddataKolommen.Size = new System.Drawing.Size(468, 174);
            this.GroupBoxVelddataKolommen.TabIndex = 7;
            this.GroupBoxVelddataKolommen.TabStop = false;
            this.GroupBoxVelddataKolommen.Text = "2. Velddata kolommen";
            // 
            // CboTotalCount
            // 
            this.CboTotalCount.Data = null;
            this.CboTotalCount.Label = "Total count:";
            this.CboTotalCount.Location = new System.Drawing.Point(307, 61);
            this.CboTotalCount.Name = "CboTotalCount";
            this.CboTotalCount.PresetValue = "Countrate";
            this.CboTotalCount.SelectedText = "";
            this.CboTotalCount.Size = new System.Drawing.Size(90, 44);
            this.CboTotalCount.TabIndex = 8;
            // 
            // CboCs137
            // 
            this.CboCs137.Data = null;
            this.CboCs137.Label = "Cs-137:";
            this.CboCs137.Location = new System.Drawing.Point(307, 111);
            this.CboCs137.Name = "CboCs137";
            this.CboCs137.PresetValue = "137-Cs";
            this.CboCs137.SelectedText = "";
            this.CboCs137.Size = new System.Drawing.Size(90, 44);
            this.CboCs137.TabIndex = 7;
            // 
            // CboTh232
            // 
            this.CboTh232.Data = null;
            this.CboTh232.Label = "Th-232:";
            this.CboTh232.Location = new System.Drawing.Point(211, 111);
            this.CboTh232.Name = "CboTh232";
            this.CboTh232.PresetValue = "232-Th";
            this.CboTh232.SelectedText = "";
            this.CboTh232.Size = new System.Drawing.Size(90, 44);
            this.CboTh232.TabIndex = 6;
            // 
            // CboU238
            // 
            this.CboU238.Data = null;
            this.CboU238.Label = "U-238:";
            this.CboU238.Location = new System.Drawing.Point(115, 111);
            this.CboU238.Name = "CboU238";
            this.CboU238.PresetValue = "238-U";
            this.CboU238.SelectedText = "";
            this.CboU238.Size = new System.Drawing.Size(90, 44);
            this.CboU238.TabIndex = 5;
            // 
            // CboK40
            // 
            this.CboK40.Data = null;
            this.CboK40.Label = "K-40:";
            this.CboK40.Location = new System.Drawing.Point(19, 111);
            this.CboK40.Name = "CboK40";
            this.CboK40.PresetValue = "40-K";
            this.CboK40.SelectedText = "";
            this.CboK40.Size = new System.Drawing.Size(90, 44);
            this.CboK40.TabIndex = 4;
            // 
            // CboAlt
            // 
            this.CboAlt.Data = null;
            this.CboAlt.Label = "Altitude:";
            this.CboAlt.Location = new System.Drawing.Point(211, 61);
            this.CboAlt.Name = "CboAlt";
            this.CboAlt.PresetValue = "alt";
            this.CboAlt.SelectedText = "";
            this.CboAlt.Size = new System.Drawing.Size(90, 44);
            this.CboAlt.TabIndex = 3;
            // 
            // CboYcoord
            // 
            this.CboYcoord.Data = null;
            this.CboYcoord.Label = "Y-Coordinaat:";
            this.CboYcoord.Location = new System.Drawing.Point(115, 61);
            this.CboYcoord.Name = "CboYcoord";
            this.CboYcoord.PresetValue = "Y";
            this.CboYcoord.SelectedText = "";
            this.CboYcoord.Size = new System.Drawing.Size(90, 44);
            this.CboYcoord.TabIndex = 2;
            // 
            // CboXcoord
            // 
            this.CboXcoord.Data = null;
            this.CboXcoord.Label = "X-Coordinaat:";
            this.CboXcoord.Location = new System.Drawing.Point(19, 61);
            this.CboXcoord.Name = "CboXcoord";
            this.CboXcoord.PresetValue = "X";
            this.CboXcoord.SelectedText = "";
            this.CboXcoord.Size = new System.Drawing.Size(90, 44);
            this.CboXcoord.TabIndex = 1;
            // 
            // BtnReadColumns
            // 
            this.BtnReadColumns.Location = new System.Drawing.Point(19, 22);
            this.BtnReadColumns.Name = "BtnReadColumns";
            this.BtnReadColumns.Size = new System.Drawing.Size(378, 33);
            this.BtnReadColumns.TabIndex = 0;
            this.BtnReadColumns.Text = "Lees kolommen uit velddata";
            this.BtnReadColumns.UseVisualStyleBackColor = true;
            // 
            // GroupBoxPerceelsgrens
            // 
            this.GroupBoxPerceelsgrens.Controls.Add(this.BlankFileLocation);
            this.GroupBoxPerceelsgrens.Controls.Add(this.BtnLoadVelddataInSurfer);
            this.GroupBoxPerceelsgrens.Controls.Add(this.label1);
            this.GroupBoxPerceelsgrens.Enabled = false;
            this.GroupBoxPerceelsgrens.Location = new System.Drawing.Point(5, 366);
            this.GroupBoxPerceelsgrens.Name = "GroupBoxPerceelsgrens";
            this.GroupBoxPerceelsgrens.Size = new System.Drawing.Size(467, 170);
            this.GroupBoxPerceelsgrens.TabIndex = 6;
            this.GroupBoxPerceelsgrens.TabStop = false;
            this.GroupBoxPerceelsgrens.Text = "3. Perceelsgrens";
            // 
            // BtnLoadVelddataInSurfer
            // 
            this.BtnLoadVelddataInSurfer.Location = new System.Drawing.Point(6, 78);
            this.BtnLoadVelddataInSurfer.Name = "BtnLoadVelddataInSurfer";
            this.BtnLoadVelddataInSurfer.Size = new System.Drawing.Size(391, 33);
            this.BtnLoadVelddataInSurfer.TabIndex = 1;
            this.BtnLoadVelddataInSurfer.Text = "Open Surfer met de velddata om blank file te maken";
            this.BtnLoadVelddataInSurfer.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(364, 47);
            this.label1.TabIndex = 0;
            this.label1.Text = "Opent Surfer met de velddata en de monsterpunten.\r\nOok de luchtfoto en indien mog" +
    "elijk de AAN-data wordt ingeladen.\r\nVervolgens kan de blank-file handmatig gemaa" +
    "kt worden.";
            // 
            // GroupBoxNuclideGrids
            // 
            this.GroupBoxNuclideGrids.Controls.Add(this.BtnMaakNuclideGrids);
            this.GroupBoxNuclideGrids.Enabled = false;
            this.GroupBoxNuclideGrids.Location = new System.Drawing.Point(5, 542);
            this.GroupBoxNuclideGrids.Name = "GroupBoxNuclideGrids";
            this.GroupBoxNuclideGrids.Size = new System.Drawing.Size(468, 69);
            this.GroupBoxNuclideGrids.TabIndex = 5;
            this.GroupBoxNuclideGrids.TabStop = false;
            this.GroupBoxNuclideGrids.Text = "4. Maak nuclide grids";
            // 
            // BtnMaakNuclideGrids
            // 
            this.BtnMaakNuclideGrids.Location = new System.Drawing.Point(19, 22);
            this.BtnMaakNuclideGrids.Name = "BtnMaakNuclideGrids";
            this.BtnMaakNuclideGrids.Size = new System.Drawing.Size(378, 33);
            this.BtnMaakNuclideGrids.TabIndex = 0;
            this.BtnMaakNuclideGrids.Text = "Maak nuclide grids";
            this.BtnMaakNuclideGrids.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(482, 617);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Uitvoer";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // LblVoortgang
            // 
            this.LblVoortgang.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LblVoortgang.AutoEllipsis = true;
            this.LblVoortgang.BackColor = System.Drawing.Color.Transparent;
            this.LblVoortgang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblVoortgang.Image = ((System.Drawing.Image)(resources.GetObject("LblVoortgang.Image")));
            this.LblVoortgang.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.LblVoortgang.Location = new System.Drawing.Point(508, 75);
            this.LblVoortgang.Name = "LblVoortgang";
            this.LblVoortgang.Size = new System.Drawing.Size(349, 621);
            this.LblVoortgang.TabIndex = 7;
            this.LblVoortgang.Text = "Open eerst een bestaand of nieuw project";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(867, 703);
            this.Controls.Add(this.BtnOpenProject);
            this.Controls.Add(this.LblVoortgang);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Kaartfabriek v2";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.Voorbereiding.ResumeLayout(false);
            this.Voorbereiding.PerformLayout();
            this.GroupBoxVelddataKolommen.ResumeLayout(false);
            this.GroupBoxPerceelsgrens.ResumeLayout(false);
            this.GroupBoxPerceelsgrens.PerformLayout();
            this.GroupBoxNuclideGrids.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox Voorbereiding;
        private UserControls.TextSelectControl MonsterDataLocation;
        private UserControls.TextSelectControl VeldDataLocation;
        private UserControls.TextSelectControl WorkingFolder;
        private System.Windows.Forms.GroupBox GroupBoxVelddataKolommen;
        private UserControls.ColumnSelectControl CboTotalCount;
        private UserControls.ColumnSelectControl CboCs137;
        private UserControls.ColumnSelectControl CboTh232;
        private UserControls.ColumnSelectControl CboU238;
        private UserControls.ColumnSelectControl CboK40;
        private UserControls.ColumnSelectControl CboAlt;
        private UserControls.ColumnSelectControl CboYcoord;
        private UserControls.ColumnSelectControl CboXcoord;
        private System.Windows.Forms.Button BtnReadColumns;
        private System.Windows.Forms.GroupBox GroupBoxPerceelsgrens;
        private UserControls.TextSelectControl BlankFileLocation;
        private System.Windows.Forms.Button BtnLoadVelddataInSurfer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox GroupBoxNuclideGrids;
        private System.Windows.Forms.Button BtnMaakNuclideGrids;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label LblVoortgang;
        private System.Windows.Forms.Button btnReprojectVelddata;
        private System.Windows.Forms.Button btnReprojectMonsterdata;
        private System.Windows.Forms.Button BtnOpenProject;
    }
}

