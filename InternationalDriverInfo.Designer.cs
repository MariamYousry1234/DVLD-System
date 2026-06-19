namespace DVLD_Project
{
    partial class InternationalDriverInfo
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.pbPeople = new System.Windows.Forms.PictureBox();
            this.ucShowInternationalLicenseInfo1 = new DVLD_Project.ucShowInternationalLicenseInfo();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbPeople)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Palatino Linotype", 24F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Red;
            this.lblTitle.Location = new System.Drawing.Point(279, 117);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(485, 43);
            this.lblTitle.TabIndex = 58;
            this.lblTitle.Text = "Driver International License Info";
            // 
            // pbPeople
            // 
            this.pbPeople.Image = global::DVLD_Project.Properties.Resources.International_321;
            this.pbPeople.Location = new System.Drawing.Point(443, 12);
            this.pbPeople.Name = "pbPeople";
            this.pbPeople.Size = new System.Drawing.Size(150, 68);
            this.pbPeople.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbPeople.TabIndex = 57;
            this.pbPeople.TabStop = false;
            // 
            // ucShowInternationalLicenseInfo1
            // 
            this.ucShowInternationalLicenseInfo1.Location = new System.Drawing.Point(12, 180);
            this.ucShowInternationalLicenseInfo1.Name = "ucShowInternationalLicenseInfo1";
            this.ucShowInternationalLicenseInfo1.Size = new System.Drawing.Size(1013, 386);
            this.ucShowInternationalLicenseInfo1.TabIndex = 59;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::DVLD_Project.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(858, 563);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(139, 40);
            this.btnClose.TabIndex = 60;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // InternationalDriverInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1021, 613);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.ucShowInternationalLicenseInfo1);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pbPeople);
            this.Name = "InternationalDriverInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "International Driver Info";
            this.Load += new System.EventHandler(this.InternationalDriverInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbPeople)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox pbPeople;
        private ucShowInternationalLicenseInfo ucShowInternationalLicenseInfo1;
        private System.Windows.Forms.Button btnClose;
    }
}