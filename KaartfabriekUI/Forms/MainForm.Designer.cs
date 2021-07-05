
namespace KaartfabriekUI.Forms
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
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
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
            this.TxtBuffer = new System.Windows.Forms.TextBox();
            this.TxtGridSpacing = new System.Windows.Forms.TextBox();
            this.TxtMinData = new System.Windows.Forms.TextBox();
            this.TxtMaxData = new System.Windows.Forms.TextBox();
            this.TxtLimits = new System.Windows.Forms.TextBox();
            this.TxtIdSmoothing = new System.Windows.Forms.TextBox();
            this.TxtIdPower = new System.Windows.Forms.TextBox();
            this.TxtSearchRadius = new System.Windows.Forms.TextBox();
            this.TxtSearchNumSectors = new System.Windows.Forms.TextBox();
            this.BtnMaakNuclideGrids = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.BtnTemplateCreate = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.TxtTemplateNummer = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.TxtTemplateOmvang = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TxtTemplatePerceel = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtTemplateNaam = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SurferTemplateLocation = new KaartfabriekUI.UserControls.TextSelectControl();
            this.TabPageUitvoer = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.GridViewFormulas = new System.Windows.Forms.DataGridView();
            this.GridColEnable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.GridColOutput = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GridColFormule = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GridColLevelFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BtnCreateSoilMaps = new System.Windows.Forms.Button();
            this.BtnAddFormula = new System.Windows.Forms.Button();
            this.TabPageInstellingen = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.LevelFilesFolder = new KaartfabriekUI.UserControls.TextSelectControl();
            this.GdalFolder = new KaartfabriekUI.UserControls.TextSelectControl();
            this.panelVoortgang = new System.Windows.Forms.Panel();
            this.LblVoortgang = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.TabPageVoorbereiding.SuspendLayout();
            this.GroupBoxVoorbereiding.SuspendLayout();
            this.GroupBoxVelddataKolommen.SuspendLayout();
            this.GroupBoxPerceelsgrens.SuspendLayout();
            this.GroupBoxNuclideGrids.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.TabPageUitvoer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewFormulas)).BeginInit();
            this.TabPageInstellingen.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panelVoortgang.SuspendLayout();
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
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(211, 51);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(111, 15);
            this.label11.TabIndex = 3;
            this.label11.Text = "Search radius (m):";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.label11, "This provides the search ellipse radius");
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(6, 22);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(155, 15);
            this.label12.TabIndex = 5;
            this.label12.Text = "Inverse distance power:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.label12, "This specifies the inverse distance to a power, power number. \r\nPowers should usu" +
        "ally fall between 1 and 3.");
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(6, 51);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(155, 15);
            this.label13.TabIndex = 7;
            this.label13.Text = "Inverse distance smoothing:";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.label13, resources.GetString("label13.ToolTip"));
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(30, 80);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(131, 15);
            this.label14.TabIndex = 9;
            this.label14.Text = "Limits (m):";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.label14, "Hoeveel groter moet het grid worden.");
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(211, 109);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(111, 15);
            this.label15.TabIndex = 11;
            this.label15.Text = "Max data to use:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.label15, "The Max data to use from ALL sectors value limits the total number of points used" +
        " when interpolating a grid node.");
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(211, 80);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(111, 15);
            this.label16.TabIndex = 13;
            this.label16.Text = "Min data to use:";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.label16, resources.GetString("label16.ToolTip"));
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(38, 109);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(123, 15);
            this.label17.TabIndex = 15;
            this.label17.Text = "Grid spacing (m):";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.label17, "This specifies the spacing of the output grid.");
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(211, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(111, 15);
            this.label6.TabIndex = 19;
            this.label6.Text = "Number of Sectors:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.label6, "The Number of sectors to search option divides the search area into smaller secti" +
        "ons to which you can apply the following three search rules. \r\nYou can specify u" +
        "p to 32 search sectors.");
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(382, 22);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(129, 15);
            this.label18.TabIndex = 20;
            this.label18.Text = "Buffer om perceel (m):";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.label18, "Hoe groot moet de buffer zijn");
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.TabPageVoorbereiding);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.TabPageUitvoer);
            this.tabControl1.Controls.Add(this.TabPageInstellingen);
            this.tabControl1.Location = new System.Drawing.Point(12, 51);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(812, 756);
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
            this.TabPageVoorbereiding.Size = new System.Drawing.Size(804, 728);
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
            this.label1.Margin = new System.Windows.Forms.Padding(3);
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
            this.GroupBoxNuclideGrids.Controls.Add(this.label18);
            this.GroupBoxNuclideGrids.Controls.Add(this.label6);
            this.GroupBoxNuclideGrids.Controls.Add(this.TxtBuffer);
            this.GroupBoxNuclideGrids.Controls.Add(this.TxtGridSpacing);
            this.GroupBoxNuclideGrids.Controls.Add(this.label17);
            this.GroupBoxNuclideGrids.Controls.Add(this.TxtMinData);
            this.GroupBoxNuclideGrids.Controls.Add(this.label16);
            this.GroupBoxNuclideGrids.Controls.Add(this.TxtMaxData);
            this.GroupBoxNuclideGrids.Controls.Add(this.label15);
            this.GroupBoxNuclideGrids.Controls.Add(this.TxtLimits);
            this.GroupBoxNuclideGrids.Controls.Add(this.label14);
            this.GroupBoxNuclideGrids.Controls.Add(this.TxtIdSmoothing);
            this.GroupBoxNuclideGrids.Controls.Add(this.label13);
            this.GroupBoxNuclideGrids.Controls.Add(this.TxtIdPower);
            this.GroupBoxNuclideGrids.Controls.Add(this.label12);
            this.GroupBoxNuclideGrids.Controls.Add(this.TxtSearchRadius);
            this.GroupBoxNuclideGrids.Controls.Add(this.label11);
            this.GroupBoxNuclideGrids.Controls.Add(this.TxtSearchNumSectors);
            this.GroupBoxNuclideGrids.Controls.Add(this.BtnMaakNuclideGrids);
            this.GroupBoxNuclideGrids.Enabled = false;
            this.GroupBoxNuclideGrids.Location = new System.Drawing.Point(5, 542);
            this.GroupBoxNuclideGrids.Name = "GroupBoxNuclideGrids";
            this.GroupBoxNuclideGrids.Size = new System.Drawing.Size(793, 180);
            this.GroupBoxNuclideGrids.TabIndex = 5;
            this.GroupBoxNuclideGrids.TabStop = false;
            this.GroupBoxNuclideGrids.Text = "4. Maak nuclide grids";
            // 
            // TxtBuffer
            // 
            this.TxtBuffer.Location = new System.Drawing.Point(517, 19);
            this.TxtBuffer.Name = "TxtBuffer";
            this.TxtBuffer.Size = new System.Drawing.Size(37, 23);
            this.TxtBuffer.TabIndex = 18;
            this.TxtBuffer.Text = "10";
            this.TxtBuffer.TextChanged += new System.EventHandler(this.TxtBuffer_TextChanged);
            // 
            // TxtGridSpacing
            // 
            this.TxtGridSpacing.Location = new System.Drawing.Point(168, 106);
            this.TxtGridSpacing.Name = "TxtGridSpacing";
            this.TxtGridSpacing.Size = new System.Drawing.Size(37, 23);
            this.TxtGridSpacing.TabIndex = 16;
            this.TxtGridSpacing.Text = "8";
            this.TxtGridSpacing.TextChanged += new System.EventHandler(this.TxtGridSpacing_TextChanged);
            // 
            // TxtMinData
            // 
            this.TxtMinData.Location = new System.Drawing.Point(328, 77);
            this.TxtMinData.Name = "TxtMinData";
            this.TxtMinData.Size = new System.Drawing.Size(37, 23);
            this.TxtMinData.TabIndex = 14;
            this.TxtMinData.Text = "8";
            this.TxtMinData.TextChanged += new System.EventHandler(this.TxtMinData_TextChanged);
            // 
            // TxtMaxData
            // 
            this.TxtMaxData.Location = new System.Drawing.Point(328, 106);
            this.TxtMaxData.Name = "TxtMaxData";
            this.TxtMaxData.Size = new System.Drawing.Size(37, 23);
            this.TxtMaxData.TabIndex = 12;
            this.TxtMaxData.Text = "64";
            this.TxtMaxData.TextChanged += new System.EventHandler(this.TxtMaxData_TextChanged);
            // 
            // TxtLimits
            // 
            this.TxtLimits.Location = new System.Drawing.Point(168, 77);
            this.TxtLimits.Name = "TxtLimits";
            this.TxtLimits.Size = new System.Drawing.Size(37, 23);
            this.TxtLimits.TabIndex = 10;
            this.TxtLimits.Text = "20";
            this.TxtLimits.TextChanged += new System.EventHandler(this.TxtLimits_TextChanged);
            // 
            // TxtIdSmoothing
            // 
            this.TxtIdSmoothing.Location = new System.Drawing.Point(168, 48);
            this.TxtIdSmoothing.Name = "TxtIdSmoothing";
            this.TxtIdSmoothing.Size = new System.Drawing.Size(37, 23);
            this.TxtIdSmoothing.TabIndex = 8;
            this.TxtIdSmoothing.Text = "20";
            this.TxtIdSmoothing.TextChanged += new System.EventHandler(this.TxtIdSmoothing_TextChanged);
            // 
            // TxtIdPower
            // 
            this.TxtIdPower.Location = new System.Drawing.Point(168, 19);
            this.TxtIdPower.Name = "TxtIdPower";
            this.TxtIdPower.Size = new System.Drawing.Size(37, 23);
            this.TxtIdPower.TabIndex = 6;
            this.TxtIdPower.Text = "2";
            this.TxtIdPower.TextChanged += new System.EventHandler(this.TxtIdPower_TextChanged);
            // 
            // TxtSearchRadius
            // 
            this.TxtSearchRadius.Location = new System.Drawing.Point(328, 48);
            this.TxtSearchRadius.Name = "TxtSearchRadius";
            this.TxtSearchRadius.Size = new System.Drawing.Size(37, 23);
            this.TxtSearchRadius.TabIndex = 4;
            this.TxtSearchRadius.Text = "30";
            this.TxtSearchRadius.TextChanged += new System.EventHandler(this.TxtSearchRadius_TextChanged);
            // 
            // TxtSearchNumSectors
            // 
            this.TxtSearchNumSectors.Location = new System.Drawing.Point(328, 19);
            this.TxtSearchNumSectors.Name = "TxtSearchNumSectors";
            this.TxtSearchNumSectors.Size = new System.Drawing.Size(37, 23);
            this.TxtSearchNumSectors.TabIndex = 2;
            this.TxtSearchNumSectors.Text = "1";
            this.TxtSearchNumSectors.TextChanged += new System.EventHandler(this.TxtSearchNumSectors_TextChanged);
            // 
            // BtnMaakNuclideGrids
            // 
            this.BtnMaakNuclideGrids.Location = new System.Drawing.Point(7, 141);
            this.BtnMaakNuclideGrids.Name = "BtnMaakNuclideGrids";
            this.BtnMaakNuclideGrids.Size = new System.Drawing.Size(547, 33);
            this.BtnMaakNuclideGrids.TabIndex = 0;
            this.BtnMaakNuclideGrids.Text = "Maak nuclide grids";
            this.BtnMaakNuclideGrids.UseVisualStyleBackColor = true;
            this.BtnMaakNuclideGrids.Click += new System.EventHandler(this.BtnMaakNuclideGrids_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.BtnTemplateCreate);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(804, 728);
            this.tabPage1.TabIndex = 3;
            this.tabPage1.Text = "Template";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // BtnTemplateCreate
            // 
            this.BtnTemplateCreate.Location = new System.Drawing.Point(12, 227);
            this.BtnTemplateCreate.Name = "BtnTemplateCreate";
            this.BtnTemplateCreate.Size = new System.Drawing.Size(739, 33);
            this.BtnTemplateCreate.TabIndex = 3;
            this.BtnTemplateCreate.Text = "Maak template";
            this.BtnTemplateCreate.UseVisualStyleBackColor = true;
            this.BtnTemplateCreate.Click += new System.EventHandler(this.BtnTemplateCreate_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.TxtTemplateNummer);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.TxtTemplateOmvang);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.TxtTemplatePerceel);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.TxtTemplateNaam);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.SurferTemplateLocation);
            this.groupBox2.Location = new System.Drawing.Point(6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(782, 204);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Template gegevens";
            // 
            // TxtTemplateNummer
            // 
            this.TxtTemplateNummer.Location = new System.Drawing.Point(74, 166);
            this.TxtTemplateNummer.Name = "TxtTemplateNummer";
            this.TxtTemplateNummer.Size = new System.Drawing.Size(78, 23);
            this.TxtTemplateNummer.TabIndex = 13;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(6, 169);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 15);
            this.label10.TabIndex = 12;
            this.label10.Text = "Perceelnr.:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(132, 139);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(20, 15);
            this.label5.TabIndex = 11;
            this.label5.Text = "ha";
            // 
            // TxtTemplateOmvang
            // 
            this.TxtTemplateOmvang.Location = new System.Drawing.Point(74, 136);
            this.TxtTemplateOmvang.Name = "TxtTemplateOmvang";
            this.TxtTemplateOmvang.Size = new System.Drawing.Size(52, 23);
            this.TxtTemplateOmvang.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(6, 139);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "Omvang:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TxtTemplatePerceel
            // 
            this.TxtTemplatePerceel.Location = new System.Drawing.Point(74, 107);
            this.TxtTemplatePerceel.Name = "TxtTemplatePerceel";
            this.TxtTemplatePerceel.Size = new System.Drawing.Size(671, 23);
            this.TxtTemplatePerceel.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "Perceel:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TxtTemplateNaam
            // 
            this.TxtTemplateNaam.Location = new System.Drawing.Point(74, 78);
            this.TxtTemplateNaam.Name = "TxtTemplateNaam";
            this.TxtTemplateNaam.Size = new System.Drawing.Size(665, 23);
            this.TxtTemplateNaam.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 23);
            this.label2.TabIndex = 5;
            this.label2.Text = "Naam:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // SurferTemplateLocation
            // 
            this.SurferTemplateLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SurferTemplateLocation.FileFilter = "Surfer files|*.srf";
            this.SurferTemplateLocation.InitialDirectory = null;
            this.SurferTemplateLocation.Label = "Locatie van de basis Surfer template:";
            this.SurferTemplateLocation.Location = new System.Drawing.Point(6, 22);
            this.SurferTemplateLocation.Name = "SurferTemplateLocation";
            this.SurferTemplateLocation.SelectionType = KaartfabriekUI.UserControls.TextSelectControl.SelectType.File;
            this.SurferTemplateLocation.Size = new System.Drawing.Size(770, 44);
            this.SurferTemplateLocation.TabIndex = 4;
            this.SurferTemplateLocation.TextboxText = "";
            this.SurferTemplateLocation.TextboxUpdated += new System.EventHandler(this.SurferTemplateLocation_TextboxUpdated);
            // 
            // TabPageUitvoer
            // 
            this.TabPageUitvoer.Controls.Add(this.splitContainer1);
            this.TabPageUitvoer.Location = new System.Drawing.Point(4, 24);
            this.TabPageUitvoer.Name = "TabPageUitvoer";
            this.TabPageUitvoer.Padding = new System.Windows.Forms.Padding(3);
            this.TabPageUitvoer.Size = new System.Drawing.Size(804, 728);
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
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.BtnCreateSoilMaps);
            this.splitContainer1.Panel2.Controls.Add(this.BtnAddFormula);
            this.splitContainer1.Panel2.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainer1.Size = new System.Drawing.Size(798, 722);
            this.splitContainer1.SplitterDistance = 666;
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
            this.GridColOutput,
            this.GridColFormule,
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
            this.GridViewFormulas.Size = new System.Drawing.Size(798, 666);
            this.GridViewFormulas.TabIndex = 1;
            this.GridViewFormulas.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.GridViewFormulas_CellMouseClick);
            this.GridViewFormulas.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.GridViewFormulas_CellMouseDoubleClick);
            // 
            // GridColEnable
            // 
            this.GridColEnable.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.GridColEnable.FalseValue = "false";
            this.GridColEnable.FillWeight = 10F;
            this.GridColEnable.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.GridColEnable.Frozen = true;
            this.GridColEnable.HeaderText = "√";
            this.GridColEnable.Name = "GridColEnable";
            this.GridColEnable.ToolTipText = "Klik om de checkboxen te wisselen";
            this.GridColEnable.TrueValue = "true";
            this.GridColEnable.Width = 21;
            // 
            // GridColOutput
            // 
            this.GridColOutput.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.GridColOutput.Frozen = true;
            this.GridColOutput.HeaderText = "Output";
            this.GridColOutput.Name = "GridColOutput";
            this.GridColOutput.ReadOnly = true;
            this.GridColOutput.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.GridColOutput.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.GridColOutput.Width = 51;
            // 
            // GridColFormule
            // 
            this.GridColFormule.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.GridColFormule.HeaderText = "Formule";
            this.GridColFormule.Name = "GridColFormule";
            this.GridColFormule.ReadOnly = true;
            this.GridColFormule.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.GridColFormule.Width = 57;
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
            this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Maximum";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // GridColLevelFile
            // 
            this.GridColLevelFile.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.GridColLevelFile.HeaderText = "Level file";
            this.GridColLevelFile.Name = "GridColLevelFile";
            this.GridColLevelFile.ReadOnly = true;
            this.GridColLevelFile.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.GridColLevelFile.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.GridColLevelFile.Width = 59;
            // 
            // BtnCreateSoilMaps
            // 
            this.BtnCreateSoilMaps.Location = new System.Drawing.Point(142, 6);
            this.BtnCreateSoilMaps.Name = "BtnCreateSoilMaps";
            this.BtnCreateSoilMaps.Size = new System.Drawing.Size(224, 38);
            this.BtnCreateSoilMaps.TabIndex = 16;
            this.BtnCreateSoilMaps.Text = "Maak de bodemkaarten";
            this.BtnCreateSoilMaps.UseVisualStyleBackColor = true;
            this.BtnCreateSoilMaps.Click += new System.EventHandler(this.BtnCreateSoilMaps_Click);
            // 
            // BtnAddFormula
            // 
            this.BtnAddFormula.AutoSize = true;
            this.BtnAddFormula.Image = ((System.Drawing.Image)(resources.GetObject("BtnAddFormula.Image")));
            this.BtnAddFormula.Location = new System.Drawing.Point(10, 6);
            this.BtnAddFormula.Name = "BtnAddFormula";
            this.BtnAddFormula.Size = new System.Drawing.Size(126, 38);
            this.BtnAddFormula.TabIndex = 15;
            this.BtnAddFormula.Text = "Nieuwe formule";
            this.BtnAddFormula.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnAddFormula.UseVisualStyleBackColor = true;
            this.BtnAddFormula.Click += new System.EventHandler(this.BtnAddFormula_Click);
            // 
            // TabPageInstellingen
            // 
            this.TabPageInstellingen.Controls.Add(this.groupBox3);
            this.TabPageInstellingen.Controls.Add(this.groupBox1);
            this.TabPageInstellingen.Location = new System.Drawing.Point(4, 24);
            this.TabPageInstellingen.Name = "TabPageInstellingen";
            this.TabPageInstellingen.Size = new System.Drawing.Size(804, 728);
            this.TabPageInstellingen.TabIndex = 2;
            this.TabPageInstellingen.Text = "Instellingen";
            this.TabPageInstellingen.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Location = new System.Drawing.Point(13, 148);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(782, 268);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Project instellingen";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.Location = new System.Drawing.Point(153, 227);
            this.label7.Name = "label7";
            this.label7.Padding = new System.Windows.Forms.Padding(5);
            this.label7.Size = new System.Drawing.Size(139, 27);
            this.label7.TabIndex = 2;
            this.label7.Text = "TODO: Grondwatertrap";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Location = new System.Drawing.Point(6, 227);
            this.label8.Name = "label8";
            this.label8.Padding = new System.Windows.Forms.Padding(5);
            this.label8.Size = new System.Drawing.Size(114, 27);
            this.label8.TabIndex = 1;
            this.label8.Text = "TODO: EPGS Code";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.Location = new System.Drawing.Point(6, 164);
            this.label9.Name = "label9";
            this.label9.Padding = new System.Windows.Forms.Padding(5);
            this.label9.Size = new System.Drawing.Size(122, 27);
            this.label9.TabIndex = 0;
            this.label9.Text = "TODO: Grid settings";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.LevelFilesFolder);
            this.groupBox1.Controls.Add(this.GdalFolder);
            this.groupBox1.Location = new System.Drawing.Point(13, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(782, 128);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Algemene instellingen";
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
            this.LevelFilesFolder.TextboxUpdated += new System.EventHandler(this.LevelFilesFolder_TextboxUpdated);
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
            this.GdalFolder.TextboxUpdated += new System.EventHandler(this.GdalFolder_TextboxUpdated);
            // 
            // panelVoortgang
            // 
            this.panelVoortgang.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelVoortgang.AutoScroll = true;
            this.panelVoortgang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelVoortgang.Controls.Add(this.LblVoortgang);
            this.panelVoortgang.Location = new System.Drawing.Point(830, 75);
            this.panelVoortgang.Name = "panelVoortgang";
            this.panelVoortgang.Size = new System.Drawing.Size(374, 732);
            this.panelVoortgang.TabIndex = 10;
            // 
            // LblVoortgang
            // 
            this.LblVoortgang.AutoEllipsis = true;
            this.LblVoortgang.AutoSize = true;
            this.LblVoortgang.BackColor = System.Drawing.Color.Transparent;
            this.LblVoortgang.Image = global::KaartfabriekUI.Properties.Resources.logolevdw200_50_;
            this.LblVoortgang.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.LblVoortgang.Location = new System.Drawing.Point(0, 0);
            this.LblVoortgang.Margin = new System.Windows.Forms.Padding(3);
            this.LblVoortgang.MaximumSize = new System.Drawing.Size(350, 0);
            this.LblVoortgang.MinimumSize = new System.Drawing.Size(360, 725);
            this.LblVoortgang.Name = "LblVoortgang";
            this.LblVoortgang.Size = new System.Drawing.Size(360, 725);
            this.LblVoortgang.TabIndex = 8;
            this.LblVoortgang.Text = "Open eerst een bestaand of nieuw project";
            this.LblVoortgang.DoubleClick += new System.EventHandler(this.LblVoortgang_DoubleClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1214, 814);
            this.Controls.Add(this.panelVoortgang);
            this.Controls.Add(this.BtnNewProjectFile);
            this.Controls.Add(this.BtnOpenProjectFile);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(844, 742);
            this.Name = "MainForm";
            this.Text = "Kaartfabriek v2";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.TabPageVoorbereiding.ResumeLayout(false);
            this.GroupBoxVoorbereiding.ResumeLayout(false);
            this.GroupBoxVoorbereiding.PerformLayout();
            this.GroupBoxVelddataKolommen.ResumeLayout(false);
            this.GroupBoxPerceelsgrens.ResumeLayout(false);
            this.GroupBoxPerceelsgrens.PerformLayout();
            this.GroupBoxNuclideGrids.ResumeLayout(false);
            this.GroupBoxNuclideGrids.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.TabPageUitvoer.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridViewFormulas)).EndInit();
            this.TabPageInstellingen.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.panelVoortgang.ResumeLayout(false);
            this.panelVoortgang.PerformLayout();
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
        private System.Windows.Forms.Button BtnReprojectVelddata;
        private System.Windows.Forms.Button btnReprojectMonsterdata;
        private System.Windows.Forms.Button BtnOpenProjectFile;
        private System.Windows.Forms.Button BtnNewProjectFile;
        private System.Windows.Forms.TabPage TabPageInstellingen;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private UserControls.TextSelectControl LevelFilesFolder;
        private UserControls.TextSelectControl GdalFolder;
        private System.Windows.Forms.Button BtnAddFormula;
        private System.Windows.Forms.Button BtnCreateSoilMaps;
        private System.Windows.Forms.DataGridViewCheckBoxColumn GridColEnable;
        private System.Windows.Forms.DataGridViewTextBoxColumn GridColOutput;
        private System.Windows.Forms.DataGridViewTextBoxColumn GridColFormule;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn GridColLevelFile;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button BtnTemplateCreate;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox TxtTemplateNummer;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TxtTemplateOmvang;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TxtTemplatePerceel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TxtTemplateNaam;
        private System.Windows.Forms.Label label2;
        private UserControls.TextSelectControl SurferTemplateLocation;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridView GridViewFormulas;
        private System.Windows.Forms.Panel panelVoortgang;
        private System.Windows.Forms.Label LblVoortgang;
        private System.Windows.Forms.TextBox TxtSearchNumSectors;
        private System.Windows.Forms.TextBox TxtSearchRadius;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox TxtIdPower;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox TxtIdSmoothing;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox TxtLimits;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox TxtMaxData;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox TxtMinData;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox TxtGridSpacing;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox TxtBuffer;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label6;
    }
}

