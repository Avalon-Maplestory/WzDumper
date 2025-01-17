﻿namespace HaCreator.CustomControls
{
    partial class MapBrowser
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
            this.mapNamesBox = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.mapNotExist = new System.Windows.Forms.Label();
            this.linkLabel = new System.Windows.Forms.Label();
            this.minimapBox = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.minimapBox)).BeginInit();
            this.SuspendLayout();
            // 
            // mapNamesBox
            // 
            this.mapNamesBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapNamesBox.FormattingEnabled = true;
            this.mapNamesBox.ItemHeight = 23;
            this.mapNamesBox.Location = new System.Drawing.Point(0, 0);
            this.mapNamesBox.Name = "mapNamesBox";
            this.mapNamesBox.Size = new System.Drawing.Size(616, 501);
            this.mapNamesBox.TabIndex = 19;
            this.mapNamesBox.SelectedIndexChanged += new System.EventHandler(this.mapNamesBox_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.mapNotExist);
            this.panel1.Controls.Add(this.linkLabel);
            this.panel1.Controls.Add(this.minimapBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(616, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(349, 501);
            this.panel1.TabIndex = 18;
            // 
            // mapNotExist
            // 
            this.mapNotExist.AutoSize = true;
            this.mapNotExist.ForeColor = System.Drawing.Color.Red;
            this.mapNotExist.Location = new System.Drawing.Point(65, 96);
            this.mapNotExist.Name = "mapNotExist";
            this.mapNotExist.Size = new System.Drawing.Size(218, 23);
            this.mapNotExist.TabIndex = 19;
            this.mapNotExist.Text = "Map does not actually exist";
            this.mapNotExist.Visible = false;
            // 
            // linkLabel
            // 
            this.linkLabel.AutoSize = true;
            this.linkLabel.ForeColor = System.Drawing.Color.Red;
            this.linkLabel.Location = new System.Drawing.Point(96, 96);
            this.linkLabel.Name = "linkLabel";
            this.linkLabel.Size = new System.Drawing.Size(110, 23);
            this.linkLabel.TabIndex = 18;
            this.linkLabel.Text = "Map is linked";
            this.linkLabel.Visible = false;
            // 
            // minimapBox
            // 
            this.minimapBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.minimapBox.Location = new System.Drawing.Point(0, 0);
            this.minimapBox.Name = "minimapBox";
            this.minimapBox.Size = new System.Drawing.Size(349, 501);
            this.minimapBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.minimapBox.TabIndex = 6;
            this.minimapBox.TabStop = false;
            // 
            // MapBrowser
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.mapNamesBox);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.Name = "MapBrowser";
            this.Size = new System.Drawing.Size(965, 501);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.minimapBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox mapNamesBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label mapNotExist;
        private System.Windows.Forms.Label linkLabel;
        private System.Windows.Forms.PictureBox minimapBox;

    }
}
