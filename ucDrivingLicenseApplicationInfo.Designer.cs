namespace DVLD_Project
{
    partial class ucDrivingLicenseApplicationInfo
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblPassedTests = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblLIcenseClass = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblDLAppID = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ucShowApplicationInfo1 = new DVLD_Project.ucShowApplicationInfo();
            this.llShowLicenceInfo = new System.Windows.Forms.LinkLabel();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.llShowLicenceInfo);
            this.groupBox1.Controls.Add(this.lblPassedTests);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblLIcenseClass);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblDLAppID);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(8, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(728, 124);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Driving License Application Info";
            // 
            // lblPassedTests
            // 
            this.lblPassedTests.AutoSize = true;
            this.lblPassedTests.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassedTests.Location = new System.Drawing.Point(196, 88);
            this.lblPassedTests.Name = "lblPassedTests";
            this.lblPassedTests.Size = new System.Drawing.Size(35, 18);
            this.lblPassedTests.TabIndex = 5;
            this.lblPassedTests.Text = "???";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(31, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 18);
            this.label3.TabIndex = 4;
            this.label3.Text = "Passed Tests :";
            // 
            // lblLIcenseClass
            // 
            this.lblLIcenseClass.AutoSize = true;
            this.lblLIcenseClass.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLIcenseClass.Location = new System.Drawing.Point(444, 46);
            this.lblLIcenseClass.Name = "lblLIcenseClass";
            this.lblLIcenseClass.Size = new System.Drawing.Size(35, 18);
            this.lblLIcenseClass.TabIndex = 3;
            this.lblLIcenseClass.Text = "???";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(256, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(166, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "Applied For License :";
            // 
            // lblDLAppID
            // 
            this.lblDLAppID.AutoSize = true;
            this.lblDLAppID.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDLAppID.Location = new System.Drawing.Point(158, 46);
            this.lblDLAppID.Name = "lblDLAppID";
            this.lblDLAppID.Size = new System.Drawing.Size(35, 18);
            this.lblDLAppID.TabIndex = 1;
            this.lblDLAppID.Text = "???";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(31, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "D.L.App ID :";
            // 
            // ucShowApplicationInfo1
            // 
            this.ucShowApplicationInfo1.Location = new System.Drawing.Point(3, 143);
            this.ucShowApplicationInfo1.Name = "ucShowApplicationInfo1";
            this.ucShowApplicationInfo1.Size = new System.Drawing.Size(728, 242);
            this.ucShowApplicationInfo1.TabIndex = 1;
            // 
            // llShowLicenceInfo
            // 
            this.llShowLicenceInfo.AutoSize = true;
            this.llShowLicenceInfo.Location = new System.Drawing.Point(339, 88);
            this.llShowLicenceInfo.Name = "llShowLicenceInfo";
            this.llShowLicenceInfo.Size = new System.Drawing.Size(140, 20);
            this.llShowLicenceInfo.TabIndex = 193;
            this.llShowLicenceInfo.TabStop = true;
            this.llShowLicenceInfo.Text = "Show License Info";
            this.llShowLicenceInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llShowLicenceInfo_LinkClicked);
            // 
            // ucDrivingLicenseApplicationInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ucShowApplicationInfo1);
            this.Controls.Add(this.groupBox1);
            this.Name = "ucDrivingLicenseApplicationInfo";
            this.Size = new System.Drawing.Size(739, 393);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblLIcenseClass;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblDLAppID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblPassedTests;
        private System.Windows.Forms.Label label3;
        private ucShowApplicationInfo ucShowApplicationInfo1;
        private System.Windows.Forms.LinkLabel llShowLicenceInfo;
    }
}
