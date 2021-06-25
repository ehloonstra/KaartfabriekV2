
namespace KaartfabriekUI.UserControls
{
    partial class TextSelectControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextSelectControl));
            this.label = new System.Windows.Forms.Label();
            this.textbox = new System.Windows.Forms.TextBox();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(0, 0);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(107, 15);
            this.label.TabIndex = 0;
            this.label.Text = "Hier komt de label:";
            // 
            // textbox
            // 
            this.textbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textbox.Location = new System.Drawing.Point(3, 18);
            this.textbox.Name = "textbox";
            this.textbox.Size = new System.Drawing.Size(277, 23);
            this.textbox.TabIndex = 1;
            this.textbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectFolder.AutoSize = true;
            this.btnSelectFolder.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSelectFolder.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectFolder.Image")));
            this.btnSelectFolder.Location = new System.Drawing.Point(286, 13);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(30, 30);
            this.btnSelectFolder.TabIndex = 2;
            this.btnSelectFolder.UseVisualStyleBackColor = true;
            this.btnSelectFolder.Visible = false;
            this.btnSelectFolder.Click += new System.EventHandler(this.BtnSelectFolderClick);
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectFile.AutoSize = true;
            this.btnSelectFile.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSelectFile.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectFile.Image")));
            this.btnSelectFile.Location = new System.Drawing.Point(286, 14);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(30, 30);
            this.btnSelectFile.TabIndex = 3;
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Visible = false;
            this.btnSelectFile.Click += new System.EventHandler(this.BtnSelectFileClick);
            // 
            // TextSelectControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSelectFile);
            this.Controls.Add(this.btnSelectFolder);
            this.Controls.Add(this.textbox);
            this.Controls.Add(this.label);
            this.Name = "TextSelectControl";
            this.Size = new System.Drawing.Size(316, 44);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label;
        private System.Windows.Forms.TextBox textbox;
        private System.Windows.Forms.Button btnSelectFolder;
        private System.Windows.Forms.Button btnSelectFile;
    }
}
