
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
            this.BtnReprojectVelddata = new System.Windows.Forms.Button();
            this.BtnOpenProjectFile = new System.Windows.Forms.Button();
            this.BtnNewProjectFile = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.TabPageVoorbereiding = new System.Windows.Forms.TabPage();
            this.GroupBoxVoorbereiding = new System.Windows.Forms.GroupBox();
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
            this.TabPageUitvoer = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.GridViewFormulas = new System.Windows.Forms.DataGridView();
            this.GridColEnable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.GridColFormule = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GridColOutput = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GridColLevelFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TabPageInstellingen = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SurferTemplateLocation = new KaartfabriekUI.UserControls.TextSelectControl();
            this.LevelFilesFolder = new KaartfabriekUI.UserControls.TextSelectControl();
            this.GdalFolder = new KaartfabriekUI.UserControls.TextSelectControl();
            this.LblVoortgang = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.TabPageVoorbereiding.SuspendLayout();
            this.GroupBoxVoorbereiding.SuspendLayout();
            this.GroupBoxVelddataKolommen.SuspendLayout();
            this.GroupBoxPerceelsgrens.SuspendLayout();
            this.GroupBoxNuclideGrids.SuspendLayout();
            this.TabPageUitvoer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewFormulas)).BeginInit();
            this.TabPageInstellingen.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BlankFileLocation
            // 
            this.BlankFileLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BlankFileLocation.FileFilter = "blank files|*.bln";
            this.BlankFileLocation.InitialDirectory = null;
            this.BlankFileLocation.Label = "Locatie van de blank file:";
            this.BlankFileLocation.Location = new System.Drawing.Point(6, 117);
            this.BlankFileLocation.Name = "BlankFileLocation";
            this.BlankFileLocation.SelectionType = KaartfabriekUI.UserControls.TextSelectControl.SelectType.File;
            this.BlankFileLocation.Size = new System.Drawing.Size(781, 44);
            this.BlankFileLocation.TabIndex = 2;
            this.BlankFileLocation.TextboxText = "";
            this.toolTip1.SetToolTip(this.BlankFileLocation, "Selecteer de locatie van de blank file");
            this.BlankFileLocation.TextboxUpdated += new System.EventHandler(this.BlankFileLocation_TextboxUpdated);
            // 
            // MonsterDataLocation
            // 
            this.MonsterDataLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MonsterDataLocation.FileFilter = "csv files|*.csv";
            this.MonsterDataLocation.InitialDirectory = null;
            this.MonsterDataLocation.Label = "Locatie van de monsterdata (monsterdata-RD.csv):";
            this.MonsterDataLocation.Location = new System.Drawing.Point(6, 122);
            this.MonsterDataLocation.Name = "MonsterDataLocation";
            this.MonsterDataLocation.SelectionType = KaartfabriekUI.UserControls.TextSelectControl.SelectType.File;
            this.MonsterDataLocation.Size = new System.Drawing.Size(744, 44);
            this.MonsterDataLocation.TabIndex = 2;
            this.MonsterDataLocation.TextboxText = "";
            this.toolTip1.SetToolTip(this.MonsterDataLocation, "Selecteer de locatie van de monsterdata");
            this.MonsterDataLocation.TextboxUpdated += new System.EventHandler(this.MonsterDataLocation_TextboxUpdated);
            // 
            // VeldDataLocation
            // 
            this.VeldDataLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.VeldDataLocation.FileFilter = "csv files|*.csv";
            this.VeldDataLocation.InitialDirectory = null;
            this.VeldDataLocation.Label = "Locatie van de velddata (velddata-RD.csv):";
            this.VeldDataLocation.Location = new System.Drawing.Point(6, 72);
            this.VeldDataLocation.Name = "VeldDataLocation";
            this.VeldDataLocation.SelectionType = KaartfabriekUI.UserControls.TextSelectControl.SelectType.File;
            this.VeldDataLocation.Size = new System.Drawing.Size(744, 44);
            this.VeldDataLocation.TabIndex = 1;
            this.VeldDataLocation.TextboxText = "";
            this.toolTip1.SetToolTip(this.VeldDataLocation, "Selecteer de locatie van de velddata");
            this.VeldDataLocation.TextboxUpdated += new System.EventHandler(this.VeldDataLocation_TextboxUpdated);
            // 
            // WorkingFolder
            // 
            this.WorkingFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.WorkingFolder.FileFilter = "csv files|*.csv";
            this.WorkingFolder.InitialDirectory = null;
            this.WorkingFolder.Label = "Selecteer de werkfolder (klantnaam/datum meting/perceelnaam):";
            this.WorkingFolder.Location = new System.Drawing.Point(6, 22);
            this.WorkingFolder.Name = "WorkingFolder";
            this.WorkingFolder.SelectionType = KaartfabriekUI.UserControls.TextSelectControl.SelectType.Folder;
            this.WorkingFolder.Size = new System.Drawing.Size(780, 44);
            this.WorkingFolder.TabIndex = 0;
            this.WorkingFolder.TextboxText = "";
            this.toolTip1.SetToolTip(this.WorkingFolder, "Selecteer de locatie van de werkfolder");
            this.WorkingFolder.TextboxUpdated += new System.EventHandler(this.WorkingFolder_TextboxUpdated);
            // 
            // btnReprojectMonsterdata
            // 
            this.btnReprojectMonsterdata.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReprojectMonsterdata.AutoSize = true;
            this.btnReprojectMonsterdata.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnReprojectMonsterdata.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnReprojectMonsterdata.FlatAppearance.BorderSize = 2;
            this.btnReprojectMonsterdata.Image = ((System.Drawing.Image)(resources.GetObject("btnReprojectMonsterdata.Image")));
            this.btnReprojectMonsterdata.Location = new System.Drawing.Point(756, 136);
            this.btnReprojectMonsterdata.Name = "btnReprojectMonsterdata";
            this.btnReprojectMonsterdata.Size = new System.Drawing.Size(30, 30);
            this.btnReprojectMonsterdata.TabIndex = 6;
            this.toolTip1.SetToolTip(this.btnReprojectMonsterdata, "Selecteer monsterdata en converteer naar RD");
            this.btnReprojectMonsterdata.UseVisualStyleBackColor = true;
            this.btnReprojectMonsterdata.Click += new System.EventHandler(this.BtnReprojectMonsterdataClick);
            // 
            // BtnReprojectVelddata
            // 
            this.BtnReprojectVelddata.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnReprojectVelddata.AutoSize = true;
            this.BtnReprojectVelddata.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BtnReprojectVelddata.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BtnReprojectVelddata.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.BtnReprojectVelddata.FlatAppearance.BorderSize = 2;
            this.BtnReprojectVelddata.Image = ((System.Drawing.Image)(resources.GetObject("BtnReprojectVelddata.Image")));
            this.BtnReprojectVelddata.Location = new System.Drawing.Point(756, 86);
            this.BtnReprojectVelddata.Name = "BtnReprojectVelddata";
            this.BtnReprojectVelddata.Size = new System.Drawing.Size(30, 30);
            this.BtnReprojectVelddata.TabIndex = 7;
            this.toolTip1.SetToolTip(this.BtnReprojectVelddata, "Selecteer velddata en converteer nar RD");
            this.BtnReprojectVelddata.UseVisualStyleBackColor = false;
            this.BtnReprojectVelddata.Click += new System.EventHandler(this.BtnReprojectVelddataClick);
            // 
            // BtnOpenProjectFile
            // 
            this.BtnOpenProjectFile.Location = new System.Drawing.Point(125, 12);
            this.BtnOpenProjectFile.Name = "BtnOpenProjectFile";
            this.BtnOpenProjectFile.Size = new System.Drawing.Size(107, 33);
            this.BtnOpenProjectFile.TabIndex = 8;
            this.BtnOpenProjectFile.Text = "Open project";
            this.toolTip1.SetToolTip(this.BtnOpenProjectFile, "Open het project (*.json)");
            this.BtnOpenProjectFile.UseVisualStyleBackColor = true;
            this.BtnOpenProjectFile.Click += new System.EventHandler(this.BtnOpenProjectFileClick);
            // 
            // BtnNewProjectFile
            // 
            this.BtnNewProjectFile.Location = new System.Drawing.Point(12, 12);
            this.BtnNewProjectFile.Name = "BtnNewProjectFile";
            this.BtnNewProjectFile.Size = new System.Drawing.Size(107, 33);
            this.BtnNewProjectFile.TabIndex = 9;
            this.BtnNewProjectFile.Text = "Nieuw project";
            this.toolTip1.SetToolTip(this.BtnNewProjectFile, "Open het project (*.json)");
            this.BtnNewProjectFile.UseVisualStyleBackColor = true;
            this.BtnNewProjectFile.Click += new System.EventHandler(this.BtnNewProjectFile_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.TabPageVoorbereiding);
            this.tabControl1.Controls.Add(this.TabPageUitvoer);
            this.tabControl1.Controls.Add(this.TabPageInstellingen);
            this.tabControl1.Location = new System.Drawing.Point(12, 51);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(812, 645);
            this.tabControl1.TabIndex = 6;
            // 
            // TabPageVoorbereiding
            // 
            this.TabPageVoorbereiding.Controls.Add(this.GroupBoxVoorbereiding);
            this.TabPageVoorbereiding.Controls.Add(this.GroupBoxVelddataKolommen);
            this.TabPageVoorbereiding.Controls.Add(this.GroupBoxPerceelsgrens);
            this.TabPageVoorbereiding.Controls.Add(this.GroupBoxNuclideGrids);
            this.TabPageVoorbereiding.Location = new System.Drawing.Point(4, 24);
            this.TabPageVoorbereiding.Name = "TabPageVoorbereiding";
            this.TabPageVoorbereiding.Padding = new System.Windows.Forms.Padding(3);
            this.TabPageVoorbereiding.Size = new System.Drawing.Size(804, 617);
            this.TabPageVoorbereiding.TabIndex = 0;
            this.TabPageVoorbereiding.Text = "Voorbereiding";
            this.TabPageVoorbereiding.UseVisualStyleBackColor = true;
            // 
            // GroupBoxVoorbereiding
            // 
            this.GroupBoxVoorbereiding.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBoxVoorbereiding.Controls.Add(this.BtnReprojectVelddata);
            this.GroupBoxVoorbereiding.Controls.Add(this.btnReprojectMonsterdata);
            this.GroupBoxVoorbereiding.Controls.Add(this.MonsterDataLocation);
            this.GroupBoxVoorbereiding.Controls.Add(this.VeldDataLocation);
            this.GroupBoxVoorbereiding.Controls.Add(this.WorkingFolder);
            this.GroupBoxVoorbereiding.Enabled = false;
            this.GroupBoxVoorbereiding.Location = new System.Drawing.Point(6, 6);
            this.GroupBoxVoorbereiding.Name = "GroupBoxVoorbereiding";
            this.GroupBoxVoorbereiding.Size = new System.Drawing.Size(792, 174);
            this.GroupBoxVoorbereiding.TabIndex = 8;
            this.GroupBoxVoorbereiding.TabStop = false;
            this.GroupBoxVoorbereiding.Text = "1. Voorbereiding";
            // 
            // GroupBoxVelddataKolommen
            // 
            this.GroupBoxVelddataKolommen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.GroupBoxVelddataKolommen.Size = new System.Drawing.Size(793, 174);
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
            this.CboTotalCount.ComboboxSelectedIndexChanged += new System.EventHandler(this.ComboboxSelectedIndexChanged);
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
            this.CboCs137.ComboboxSelectedIndexChanged += new System.EventHandler(this.ComboboxSelectedIndexChanged);
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
            this.CboTh232.ComboboxSelectedIndexChanged += new System.EventHandler(this.ComboboxSelectedIndexChanged);
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
            this.CboU238.ComboboxSelectedIndexChanged += new System.EventHandler(this.ComboboxSelectedIndexChanged);
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
            this.CboK40.ComboboxSelectedIndexChanged += new System.EventHandler(this.ComboboxSelectedIndexChanged);
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
            this.CboAlt.ComboboxSelectedIndexChanged += new System.EventHandler(this.ComboboxSelectedIndexChanged);
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
            this.CboYcoord.ComboboxSelectedIndexChanged += new System.EventHandler(this.ComboboxSelectedIndexChanged);
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
            this.CboXcoord.ComboboxSelectedIndexChanged += new System.EventHandler(this.ComboboxSelectedIndexChanged);
            // 
            // BtnReadColumns
            // 
            this.BtnReadColumns.Location = new System.Drawing.Point(19, 22);
            this.BtnReadColumns.Name = "BtnReadColumns";
            this.BtnReadColumns.Size = new System.Drawing.Size(378, 33);
            this.BtnReadColumns.TabIndex = 0;
            this.BtnReadColumns.Text = "Lees kolommen uit velddata";
            this.BtnReadColumns.UseVisualStyleBackColor = true;
            this.BtnReadColumns.Click += new System.EventHandler(this.BtnReadColumns_Click);
            // 
            // GroupBoxPerceelsgrens
            // 
            this.GroupBoxPerceelsgrens.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBoxPerceelsgrens.Controls.Add(this.BlankFileLocation);
            this.GroupBoxPerceelsgrens.Controls.Add(this.BtnLoadVelddataInSurfer);
            this.GroupBoxPerceelsgrens.Controls.Add(this.label1);
            this.GroupBoxPerceelsgrens.Enabled = false;
            this.GroupBoxPerceelsgrens.Location = new System.Drawing.Point(5, 366);
            this.GroupBoxPerceelsgrens.Name = "GroupBoxPerceelsgrens";
            this.GroupBoxPerceelsgrens.Size = new System.Drawing.Size(793, 170);
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
            this.BtnLoadVelddataInSurfer.Click += new System.EventHandler(this.BtnLoadVelddataInSurfer_Click);
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
            this.GroupBoxNuclideGrids.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBoxNuclideGrids.Controls.Add(this.BtnMaakNuclideGrids);
            this.GroupBoxNuclideGrids.Enabled = false;
            this.GroupBoxNuclideGrids.Location = new System.Drawing.Point(5, 542);
            this.GroupBoxNuclideGrids.Name = "GroupBoxNuclideGrids";
            this.GroupBoxNuclideGrids.Size = new System.Drawing.Size(793, 69);
            this.GroupBoxNuclideGrids.TabIndex = 5;
            this.GroupBoxNuclideGrids.TabStop = false;
            this.GroupBoxNuclideGrids.Text = "4. Maak nuclide grids";
            // 
            // BtnMaakNuclideGrids
            // 
            this.BtnMaakNuclideGrids.Location = new System.Drawing.Point(7, 22);
            this.BtnMaakNuclideGrids.Name = "BtnMaakNuclideGrids";
            this.BtnMaakNuclideGrids.Size = new System.Drawing.Size(390, 33);
            this.BtnMaakNuclideGrids.TabIndex = 0;
            this.BtnMaakNuclideGrids.Text = "Maak nuclide grids";
            this.BtnMaakNuclideGrids.UseVisualStyleBackColor = true;
            this.BtnMaakNuclideGrids.Click += new System.EventHandler(this.BtnMaakNuclideGrids_Click);
            // 
            // TabPageUitvoer
            // 
            this.TabPageUitvoer.Controls.Add(this.splitContainer1);
            this.TabPageUitvoer.Location = new System.Drawing.Point(4, 24);
            this.TabPageUitvoer.Name = "TabPageUitvoer";
            this.TabPageUitvoer.Padding = new System.Windows.Forms.Padding(3);
            this.TabPageUitvoer.Size = new System.Drawing.Size(804, 617);
            this.TabPageUitvoer.TabIndex = 1;
            this.TabPageUitvoer.Text = "Uitvoer";
            this.TabPageUitvoer.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.GridViewFormulas);
            this.splitContainer1.Size = new System.Drawing.Size(798, 611);
            this.splitContainer1.SplitterDistance = 561;
            this.splitContainer1.TabIndex = 1;
            // 
            // GridViewFormulas
            // 
            this.GridViewFormulas.AllowUserToAddRows = false;
            this.GridViewFormulas.AllowUserToDeleteRows = false;
            this.GridViewFormulas.AllowUserToResizeColumns = false;
            this.GridViewFormulas.AllowUserToResizeRows = false;
            this.GridViewFormulas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridViewFormulas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GridColEnable,
            this.GridColFormule,
            this.GridColOutput,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.GridColLevelFile});
            this.GridViewFormulas.Cursor = System.Windows.Forms.Cursors.Default;
            this.GridViewFormulas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridViewFormulas.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.GridViewFormulas.Location = new System.Drawing.Point(0, 0);
            this.GridViewFormulas.MultiSelect = false;
            this.GridViewFormulas.Name = "GridViewFormulas";
            this.GridViewFormulas.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.GridViewFormulas.RowHeadersVisible = false;
            this.GridViewFormulas.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.GridViewFormulas.RowTemplate.Height = 25;
            this.GridViewFormulas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridViewFormulas.Size = new System.Drawing.Size(798, 561);
            this.GridViewFormulas.TabIndex = 1;
            this.GridViewFormulas.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.GridViewFormulas_CellMouseDoubleClick);
            // 
            // GridColEnable
            // 
            this.GridColEnable.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.GridColEnable.FillWeight = 10F;
            this.GridColEnable.Frozen = true;
            this.GridColEnable.HeaderText = "";
            this.GridColEnable.Name = "GridColEnable";
            this.GridColEnable.Width = 5;
            // 
            // GridColFormule
            // 
            this.GridColFormule.Frozen = true;
            this.GridColFormule.HeaderText = "Formule";
            this.GridColFormule.Name = "GridColFormule";
            this.GridColFormule.ReadOnly = true;
            // 
            // GridColOutput
            // 
            this.GridColOutput.Frozen = true;
            this.GridColOutput.HeaderText = "Output";
            this.GridColOutput.Name = "GridColOutput";
            this.GridColOutput.ReadOnly = true;
            this.GridColOutput.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "GridA";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "GridB";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "GridC";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "GridD";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Minimum";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Maximum";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // GridColLevelFile
            // 
            this.GridColLevelFile.HeaderText = "Level file";
            this.GridColLevelFile.Name = "GridColLevelFile";
            this.GridColLevelFile.ReadOnly = true;
            this.GridColLevelFile.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.GridColLevelFile.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // TabPageInstellingen
            // 
            this.TabPageInstellingen.Controls.Add(this.groupBox2);
            this.TabPageInstellingen.Controls.Add(this.groupBox1);
            this.TabPageInstellingen.Location = new System.Drawing.Point(4, 24);
            this.TabPageInstellingen.Name = "TabPageInstellingen";
            this.TabPageInstellingen.Size = new System.Drawing.Size(804, 617);
            this.TabPageInstellingen.TabIndex = 2;
            this.TabPageInstellingen.Text = "Instellingen";
            this.TabPageInstellingen.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(13, 346);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(782, 268);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Project instellingen";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Location = new System.Drawing.Point(16, 151);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(5);
            this.label5.Size = new System.Drawing.Size(153, 27);
            this.label5.TabIndex = 3;
            this.label5.Text = "NAW + Perceelsgegevens";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Location = new System.Drawing.Point(16, 110);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(5);
            this.label4.Size = new System.Drawing.Size(139, 27);
            this.label4.TabIndex = 2;
            this.label4.Text = "TODO: Grondwatertrap";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Location = new System.Drawing.Point(16, 70);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(5);
            this.label3.Size = new System.Drawing.Size(114, 27);
            this.label3.TabIndex = 1;
            this.label3.Text = "TODO: EPGS Code";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Location = new System.Drawing.Point(16, 34);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(5);
            this.label2.Size = new System.Drawing.Size(122, 27);
            this.label2.TabIndex = 0;
            this.label2.Text = "TODO: Grid settings";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.SurferTemplateLocation);
            this.groupBox1.Controls.Add(this.LevelFilesFolder);
            this.groupBox1.Controls.Add(this.GdalFolder);
            this.groupBox1.Location = new System.Drawing.Point(13, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(782, 313);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Algemene instellingen";
            // 
            // SurferTemplateLocation
            // 
            this.SurferTemplateLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SurferTemplateLocation.FileFilter = "Surfer files|*.srf";
            this.SurferTemplateLocation.InitialDirectory = null;
            this.SurferTemplateLocation.Label = "Locatie van de Surfer template:";
            this.SurferTemplateLocation.Location = new System.Drawing.Point(6, 122);
            this.SurferTemplateLocation.Name = "SurferTemplateLocation";
            this.SurferTemplateLocation.SelectionType = KaartfabriekUI.UserControls.TextSelectControl.SelectType.File;
            this.SurferTemplateLocation.Size = new System.Drawing.Size(770, 44);
            this.SurferTemplateLocation.TabIndex = 2;
            this.SurferTemplateLocation.TextboxText = "";
            // 
            // LevelFilesFolder
            // 
            this.LevelFilesFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LevelFilesFolder.FileFilter = "csv files|*.csv";
            this.LevelFilesFolder.InitialDirectory = null;
            this.LevelFilesFolder.Label = "Locatie van de level files";
            this.LevelFilesFolder.Location = new System.Drawing.Point(6, 72);
            this.LevelFilesFolder.Name = "LevelFilesFolder";
            this.LevelFilesFolder.SelectionType = KaartfabriekUI.UserControls.TextSelectControl.SelectType.Folder;
            this.LevelFilesFolder.Size = new System.Drawing.Size(770, 44);
            this.LevelFilesFolder.TabIndex = 1;
            this.LevelFilesFolder.TextboxText = "";
            // 
            // GdalFolder
            // 
            this.GdalFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GdalFolder.FileFilter = "csv files|*.csv";
            this.GdalFolder.InitialDirectory = null;
            this.GdalFolder.Label = "Locatie van de GDAL binaries:";
            this.GdalFolder.Location = new System.Drawing.Point(6, 22);
            this.GdalFolder.Name = "GdalFolder";
            this.GdalFolder.SelectionType = KaartfabriekUI.UserControls.TextSelectControl.SelectType.Folder;
            this.GdalFolder.Size = new System.Drawing.Size(770, 44);
            this.GdalFolder.TabIndex = 0;
            this.GdalFolder.TextboxText = "";
            // 
            // LblVoortgang
            // 
            this.LblVoortgang.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LblVoortgang.AutoEllipsis = true;
            this.LblVoortgang.BackColor = System.Drawing.Color.Transparent;
            this.LblVoortgang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblVoortgang.Image = ((System.Drawing.Image)(resources.GetObject("LblVoortgang.Image")));
            this.LblVoortgang.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.LblVoortgang.Location = new System.Drawing.Point(830, 73);
            this.LblVoortgang.Name = "LblVoortgang";
            this.LblVoortgang.Size = new System.Drawing.Size(374, 621);
            this.LblVoortgang.TabIndex = 7;
            this.LblVoortgang.Text = "Open eerst een bestaand of nieuw project";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1214, 703);
            this.Controls.Add(this.BtnNewProjectFile);
            this.Controls.Add(this.BtnOpenProjectFile);
            this.Controls.Add(this.LblVoortgang);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(844, 742);
            this.Name = "MainForm";
            this.Text = "Kaartfabriek v2";
            this.tabControl1.ResumeLayout(false);
            this.TabPageVoorbereiding.ResumeLayout(false);
            this.GroupBoxVoorbereiding.ResumeLayout(false);
            this.GroupBoxVoorbereiding.PerformLayout();
            this.GroupBoxVelddataKolommen.ResumeLayout(false);
            this.GroupBoxPerceelsgrens.ResumeLayout(false);
            this.GroupBoxPerceelsgrens.PerformLayout();
            this.GroupBoxNuclideGrids.ResumeLayout(false);
            this.TabPageUitvoer.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridViewFormulas)).EndInit();
            this.TabPageInstellingen.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage TabPageVoorbereiding;
        private System.Windows.Forms.GroupBox GroupBoxVoorbereiding;
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
        private System.Windows.Forms.TabPage TabPageUitvoer;
        private System.Windows.Forms.Label LblVoortgang;
        private System.Windows.Forms.Button BtnReprojectVelddata;
        private System.Windows.Forms.Button btnReprojectMonsterdata;
        private System.Windows.Forms.Button BtnOpenProjectFile;
        private System.Windows.Forms.Button BtnNewProjectFile;
        private System.Windows.Forms.TabPage TabPageInstellingen;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView GridViewFormulas;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private UserControls.TextSelectControl SurferTemplateLocation;
        private UserControls.TextSelectControl LevelFilesFolder;
        private UserControls.TextSelectControl GdalFolder;
        private System.Windows.Forms.DataGridViewCheckBoxColumn GridColEnable;
        private System.Windows.Forms.DataGridViewTextBoxColumn GridColFormule;
        private System.Windows.Forms.DataGridViewTextBoxColumn GridColOutput;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn GridColLevelFile;
    }
}

