
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
            this.Voorbereiding = new System.Windows.Forms.GroupBox();
            this.btnReprojectMonsterdata = new System.Windows.Forms.Button();
            this.btnReprojectVelddata = new System.Windows.Forms.Button();
            this.MonsterDataLocation = new KaartfabriekUI.UserControls.TextSelectControl();
            this.VeldDataLocation = new KaartfabriekUI.UserControls.TextSelectControl();
            this.WorkingFolder = new KaartfabriekUI.UserControls.TextSelectControl();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BlankFileLocation = new KaartfabriekUI.UserControls.TextSelectControl();
            this.BtnLoadVelddataInSurfer = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Voorbereiding.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Voorbereiding
            // 
            this.Voorbereiding.Controls.Add(this.btnReprojectMonsterdata);
            this.Voorbereiding.Controls.Add(this.btnReprojectVelddata);
            this.Voorbereiding.Controls.Add(this.MonsterDataLocation);
            this.Voorbereiding.Controls.Add(this.VeldDataLocation);
            this.Voorbereiding.Controls.Add(this.WorkingFolder);
            this.Voorbereiding.Location = new System.Drawing.Point(12, 12);
            this.Voorbereiding.Name = "Voorbereiding";
            this.Voorbereiding.Size = new System.Drawing.Size(467, 174);
            this.Voorbereiding.TabIndex = 0;
            this.Voorbereiding.TabStop = false;
            this.Voorbereiding.Text = "Voorbereiding";
            // 
            // btnReprojectMonsterdata
            // 
            this.btnReprojectMonsterdata.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReprojectMonsterdata.AutoSize = true;
            this.btnReprojectMonsterdata.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnReprojectMonsterdata.Image = ((System.Drawing.Image)(resources.GetObject("btnReprojectMonsterdata.Image")));
            this.btnReprojectMonsterdata.Location = new System.Drawing.Point(424, 136);
            this.btnReprojectMonsterdata.Name = "btnReprojectMonsterdata";
            this.btnReprojectMonsterdata.Size = new System.Drawing.Size(30, 30);
            this.btnReprojectMonsterdata.TabIndex = 5;
            this.toolTip1.SetToolTip(this.btnReprojectMonsterdata, "Selecteer monsterdata en converteer naar RD");
            this.btnReprojectMonsterdata.UseVisualStyleBackColor = true;
            this.btnReprojectMonsterdata.Click += new System.EventHandler(this.BtnReprojectMonsterdataClick);
            // 
            // btnReprojectVelddata
            // 
            this.btnReprojectVelddata.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReprojectVelddata.AutoSize = true;
            this.btnReprojectVelddata.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnReprojectVelddata.Image = ((System.Drawing.Image)(resources.GetObject("btnReprojectVelddata.Image")));
            this.btnReprojectVelddata.Location = new System.Drawing.Point(424, 86);
            this.btnReprojectVelddata.Name = "btnReprojectVelddata";
            this.btnReprojectVelddata.Size = new System.Drawing.Size(30, 30);
            this.btnReprojectVelddata.TabIndex = 4;
            this.toolTip1.SetToolTip(this.btnReprojectVelddata, "Selecteer velddata en converteer nar RD");
            this.btnReprojectVelddata.UseVisualStyleBackColor = true;
            this.btnReprojectVelddata.Click += new System.EventHandler(this.BtnReprojectVelddataClick);
            // 
            // MonsterDataLocation
            // 
            this.MonsterDataLocation.InitialDirectory = null;
            this.MonsterDataLocation.Label = "Locatie van de monsterdata:";
            this.MonsterDataLocation.Location = new System.Drawing.Point(6, 122);
            this.MonsterDataLocation.Name = "MonsterDataLocation";
            this.MonsterDataLocation.SelectionType = KaartfabriekUI.UserControls.TextSelectControl.SelectType.File;
            this.MonsterDataLocation.Size = new System.Drawing.Size(412, 44);
            this.MonsterDataLocation.TabIndex = 2;
            this.MonsterDataLocation.TextboxText = "";
            // 
            // VeldDataLocation
            // 
            this.VeldDataLocation.InitialDirectory = null;
            this.VeldDataLocation.Label = "Locatie van de velddata:";
            this.VeldDataLocation.Location = new System.Drawing.Point(6, 72);
            this.VeldDataLocation.Name = "VeldDataLocation";
            this.VeldDataLocation.SelectionType = KaartfabriekUI.UserControls.TextSelectControl.SelectType.File;
            this.VeldDataLocation.Size = new System.Drawing.Size(412, 44);
            this.VeldDataLocation.TabIndex = 1;
            this.VeldDataLocation.TextboxText = "";
            // 
            // WorkingFolder
            // 
            this.WorkingFolder.InitialDirectory = null;
            this.WorkingFolder.Label = "Selecteer de werkfolder:";
            this.WorkingFolder.Location = new System.Drawing.Point(6, 22);
            this.WorkingFolder.Name = "WorkingFolder";
            this.WorkingFolder.SelectionType = KaartfabriekUI.UserControls.TextSelectControl.SelectType.Folder;
            this.WorkingFolder.Size = new System.Drawing.Size(455, 44);
            this.WorkingFolder.TabIndex = 0;
            this.WorkingFolder.TextboxText = "";
            this.WorkingFolder.TextboxUpdated += new System.EventHandler(this.WorkingFolder_TextboxUpdated);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BlankFileLocation);
            this.groupBox1.Controls.Add(this.BtnLoadVelddataInSurfer);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(11, 192);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(468, 161);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Perceelsgrens";
            // 
            // BlankFileLocation
            // 
            this.BlankFileLocation.Enabled = false;
            this.BlankFileLocation.InitialDirectory = null;
            this.BlankFileLocation.Label = "Locatie van de blank file:";
            this.BlankFileLocation.Location = new System.Drawing.Point(18, 106);
            this.BlankFileLocation.Name = "BlankFileLocation";
            this.BlankFileLocation.SelectionType = KaartfabriekUI.UserControls.TextSelectControl.SelectType.File;
            this.BlankFileLocation.Size = new System.Drawing.Size(437, 44);
            this.BlankFileLocation.TabIndex = 2;
            this.BlankFileLocation.TextboxText = "";
            // 
            // BtnLoadVelddataInSurfer
            // 
            this.BtnLoadVelddataInSurfer.Location = new System.Drawing.Point(18, 67);
            this.BtnLoadVelddataInSurfer.Name = "BtnLoadVelddataInSurfer";
            this.BtnLoadVelddataInSurfer.Size = new System.Drawing.Size(299, 33);
            this.BtnLoadVelddataInSurfer.TabIndex = 1;
            this.BtnLoadVelddataInSurfer.Text = "Open Surfer met de velddata";
            this.BtnLoadVelddataInSurfer.UseVisualStyleBackColor = true;
            this.BtnLoadVelddataInSurfer.Click += new System.EventHandler(this.BtnLoadVelddataInSurfer_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(299, 45);
            this.label1.TabIndex = 0;
            this.label1.Text = "Open Surfer met de velddata en de monsterpunten.\r\nLaad ook de luchtfoto en indien" +
    " mogelijk de AAN-data.\r\nVervolgens kan de blank-file gemaakt worden.";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 581);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Voorbereiding);
            this.Name = "MainForm";
            this.Text = "Kaartfabriek v2";
            this.Voorbereiding.ResumeLayout(false);
            this.Voorbereiding.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox Voorbereiding;
        private UserControls.TextSelectControl WorkingFolder;
        private UserControls.TextSelectControl VeldDataLocation;
        private UserControls.TextSelectControl MonsterDataLocation;
        private System.Windows.Forms.Button btnReprojectVelddata;
        private System.Windows.Forms.Button btnReprojectMonsterdata;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox1;
        private UserControls.TextSelectControl BlankFileLocation;
        private System.Windows.Forms.Button BtnLoadVelddataInSurfer;
        private System.Windows.Forms.Label label1;
    }
}

