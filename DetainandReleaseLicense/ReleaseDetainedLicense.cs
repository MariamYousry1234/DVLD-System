using Busniess_Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class ReleaseDetainedLicense : Form
    {
        int _LicenseID = -1;

     
        public ReleaseDetainedLicense()
        {
            InitializeComponent();
        }

        public ReleaseDetainedLicense(int LicenseID)
        {
            InitializeComponent();

            _LicenseID = LicenseID;

            ucDriverLicenseInfoWithFilter1.LoadInfo(_LicenseID);
        }

      
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            if (obj == -1)
            {
                btnRelease.Enabled = false;
                llShowLicenseHistory.Enabled = false;
                llShowNewLicenseInfo.Enabled = false;
                return;
            }

            llShowLicenseHistory.Enabled = true;
            lblLicenseID.Text = obj.ToString();
            _LicenseID = obj;
            llShowNewLicenseInfo.Enabled = true;

            if (!ucDriverLicenseInfoWithFilter1.License.IsDetained)
            {
                MessageBox.Show("Selected License is Not detained,choose another one", "Not Detained",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRelease.Enabled = false;             
            }
            else
            {

                lblDetainID.Text = ucDriverLicenseInfoWithFilter1.License.DetainedInfo.DetainedID.ToString();
                lblDetainDate.Text = ucDriverLicenseInfoWithFilter1.License.DetainedInfo.DetainedDate.ToString("dd/MM/yyyy");
                lblCreatedBy.Text = ucDriverLicenseInfoWithFilter1.License.DetainedInfo.CreatedUserInfo.Username;
                lblFineFees.Text = ucDriverLicenseInfoWithFilter1.License.DetainedInfo.FineFees.ToString();

                float ApplicationFees = clsApplication.Find((int)clsApplication.enApplicationType.ReleaseDetained).ApplicationFees;
                lblApplicationFees.Text = ApplicationFees.ToString();

                lblTotalFees.Text = (ucDriverLicenseInfoWithFilter1.License.DetainedInfo.FineFees + ApplicationFees).ToString();

                btnRelease.Enabled = true;
            }
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LicenseHistory frm = new LicenseHistory(ucDriverLicenseInfoWithFilter1.License.DriverInfo.PersonID);

            frm.ShowDialog();
        }

        private void llShowNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LicenseInfo frm = new LicenseInfo(_LicenseID);

            frm.ShowDialog();
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Release this license?", "Confirm",
              MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;

            int ApplicationID = -1;
            bool IsReleased = ucDriverLicenseInfoWithFilter1.License.Release(clsGlobal.CurrentUser.UserID,ref  ApplicationID);


            if (!IsReleased)
            {
                MessageBox.Show("Failed To save Released Data", "Falied", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            MessageBox.Show($"License Released Successfully ", "", MessageBoxButtons.OK,
               MessageBoxIcon.Information);

            ucDriverLicenseInfoWithFilter1.FilterEnalbed = false;
            btnRelease.Enabled = false;

            lblReleasedApplicationID.Text = ApplicationID.ToString();
        }

        private void ReleaseDetainedLicense_Activated(object sender, EventArgs e)
        {
            ucDriverLicenseInfoWithFilter1.txtFocus();
        }
    }
}
