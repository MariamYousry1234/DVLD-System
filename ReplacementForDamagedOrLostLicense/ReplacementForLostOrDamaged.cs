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
    public partial class ReplacementForLostOrDamaged : Form
    {


        int _NewLicenseID = -1;
        public ReplacementForLostOrDamaged()
        {
            InitializeComponent();
        }


        void LoadIntialInfo()
        {
        

            rbDamagedLicense.Checked = true;
            lblTitle.Text = "Replacement For Damaged License";
            this.Text = lblTitle.Text;
            lblRLAppID.Text = "???";
            lblRenewedLicenseID.Text = "???";
            lblApplicationDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblOldLicenseID.Text = "???";
            lblIssueDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblExpirationDate.Text = "???";
            lblApplicationFees.Text = clsApplicationTypes.Find((int)clsApplication.enApplicationType.ReplacementForDamaged).Fees.ToString();
            lblCreatedBy.Text = clsGlobal.CurrentUser.Username;
        

        }

        private void ReplacementForLostOrDamaged_Load(object sender, EventArgs e)
        {
            LoadIntialInfo();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            if( rbDamagedLicense.Checked)
            {
                lblTitle.Text = "Replacement For Damaged License";
                lblApplicationFees.Text = clsApplicationTypes.Find((int)clsApplication.enApplicationType.ReplacementForDamaged).Fees.ToString();
            }
            else
            {    

                lblTitle.Text = "Replacement For Lost License";
                lblApplicationFees.Text = clsApplicationTypes.Find((int)clsApplication.enApplicationType.ReplacementForLost).Fees.ToString();
            }

            this.Text = lblTitle.Text;

        }

        private void ucDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            if(obj==-1)
            {
                btnIssueReplacement.Enabled = false;
                llShowLicenseHistory.Enabled = false;
                llShowNewLicenseInfo.Enabled = false;

                return;
            }

            llShowLicenseHistory.Enabled = true;
            lblOldLicenseID.Text = obj.ToString();

            if (ucDriverLicenseInfoWithFilter1.License.IsExpired() &&
                ucDriverLicenseInfoWithFilter1.License.IsActive)
            {
                MessageBox.Show("This License is Expired,Go to Renew License Section", "Expired", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                btnIssueReplacement.Enabled = false;
                return;
            }

            if(!ucDriverLicenseInfoWithFilter1.License.IsActive)
            {
                MessageBox.Show("Selected License is Not Active,Choose an active license", "Not Active", MessageBoxButtons.OK,
                  MessageBoxIcon.Error);

                btnIssueReplacement.Enabled = false;
                return;
            }

            btnIssueReplacement.Enabled = true;
            lblExpirationDate.Text = DateTime.Now.AddYears(ucDriverLicenseInfoWithFilter1.License.LicenseClassInfo.DeafualtValidityLength).ToString("dd/MM/yyyy"); ;
           
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LicenseHistory frm = new LicenseHistory(ucDriverLicenseInfoWithFilter1.License.DriverInfo.PersonID);

            frm.ShowDialog();
        }

        public clsLicense.enIssueReason GetIssueReason()
        {

            return rbDamagedLicense.Checked ? clsLicense.enIssueReason.ReplacementForDamaged :
                clsLicense.enIssueReason.ReplacementForLost;
        }
        private void btnIssueReplacement_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Issue a Repalcement the license?", "Confirm",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;

            clsLicense NewLicense=
            ucDriverLicenseInfoWithFilter1.License.ReplaceFroLostOrDamaged(clsGlobal.CurrentUser.UserID, GetIssueReason());

            if (NewLicense != null)
            {
                _NewLicenseID = NewLicense.LicenseID;
                MessageBox.Show($"License Replaced Successfully with id = {NewLicense.LicenseID}", "Renewed",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                ucDriverLicenseInfoWithFilter1.FilterEnalbed = false;
                btnIssueReplacement.Enabled = false;
                llShowNewLicenseInfo.Enabled = true;
                lblRLAppID.Text = NewLicense.ApplicationID.ToString();
                lblRenewedLicenseID.Text = _NewLicenseID.ToString();
            }
            else
            {
                MessageBox.Show("Data Saved Failed", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void llShowNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LicenseInfo frm = new LicenseInfo(_NewLicenseID);

            frm.ShowDialog();
        }

        private void ReplacementForLostOrDamaged_Activated(object sender, EventArgs e)
        {
            ucDriverLicenseInfoWithFilter1.txtFocus();
        }
    }
}
