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
    public partial class RenewLocalDrivingLicense : Form
    {
        int _RenewLicenseID = -1;
        public RenewLocalDrivingLicense()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            llShowNewLicenseInfo.Enabled = false;
            if (obj==-1)
            {
                llShowLicenseHistory.Enabled = false;
                btnRenew.Enabled = false;
                return;
            }

            llShowLicenseHistory.Enabled = true;
            if (!ucDriverLicenseInfoWithFilter1.License.IsExpired())
            {
                MessageBox.Show($"Selected License is not yet expired,it will expire on {ucDriverLicenseInfoWithFilter1.License.ExpiryDate.ToString("dd/MM/yyyy")}"
                    , "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);     
                btnRenew.Enabled = false;
                return;
            }

            if (!ucDriverLicenseInfoWithFilter1.License.IsActive)
            {
                MessageBox.Show("Selected License is Not Active,choose another one"
                    , "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRenew.Enabled = false;
                return;
            }

            lblOldLicenseID.Text = obj.ToString();
            btnRenew.Enabled = true;

        
            lblExpirationDate.Text = DateTime.Now.AddYears(ucDriverLicenseInfoWithFilter1.License.LicenseClassInfo.DeafualtValidityLength).ToString("dd/MM/yyyy");
            lblLicenseFees.Text = ucDriverLicenseInfoWithFilter1.License.LicenseClassInfo.ClassFees.ToString();

            lblTotalFees.Text = (Convert.ToSingle(lblApplicationFees.Text) + Convert.ToSingle(lblLicenseFees.Text)).ToString();
        }


        void LoadIntialInfo()
        {
            lblRLAppID.Text = "???";
            lblRenewedLicenseID.Text = "???";
            lblApplicationDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblOldLicenseID.Text = "???";
            lblIssueDate.Text= DateTime.Now.ToString("dd/MM/yyyy");
            lblExpirationDate.Text = "???";
            lblApplicationFees.Text = clsApplicationTypes.Find((int)clsApplication.enApplicationType.Renew).Fees.ToString();
            lblCreatedBy.Text = clsGlobal.CurrentUser.Username;
            lblLicenseFees.Text = "$$$";
            lblTotalFees.Text = "$$$";
        }
        private void RenewLocalDrivingLicense_Load(object sender, EventArgs e)
        {
            LoadIntialInfo();
        }


        private void btnRenew_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to renew the license?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;

             clsLicense NewLicense = ucDriverLicenseInfoWithFilter1.License.Renew(txtNotes.Text,clsGlobal.CurrentUser.UserID);

            if(NewLicense != null)
            {
                _RenewLicenseID = NewLicense.LicenseID;
                MessageBox.Show($"License Renewed Successfully with id = {NewLicense.LicenseID}", "Renewed",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                ucDriverLicenseInfoWithFilter1.FilterEnalbed = false;
                btnRenew.Enabled = false;
                llShowNewLicenseInfo.Enabled = true;
                lblRLAppID.Text = NewLicense.ApplicationID.ToString();
                lblRenewedLicenseID.Text = _RenewLicenseID.ToString();
            }
            else
            {
                MessageBox.Show("Data Saved Failed", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void llShowNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LicenseInfo frm = new LicenseInfo(_RenewLicenseID);

            frm.ShowDialog();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LicenseHistory frm = new LicenseHistory(ucDriverLicenseInfoWithFilter1.License.DriverInfo.PersonID);

            frm.ShowDialog();
        }
    }
}
