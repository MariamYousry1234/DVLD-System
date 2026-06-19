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
    public partial class NewInternationalLicenseApplication : Form
    {
        int _LicenseID = -1;
        int _InternationalLiceneseID = -1;
        public NewInternationalLicenseApplication()
        {
            InitializeComponent();
        }

        private void NewInternationalLicenseApplication_Activated(object sender, EventArgs e)
        {
            ucDriverLicenseInfoWithFilter1.txtFocus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void LoadIntialInfo()
        {
            lblILAppID.Text = "???";
            lblILLicenseID.Text = "???";
            l.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblLocalLicenseID.Text = "???";
            lblIssueDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblExpirationDate.Text = DateTime.Now.AddYears(10).ToString("dd/MM/yyyy");
            lblFees.Text = clsApplicationTypes.Find((int)clsApplication.enApplicationType.NewInternational).Fees.ToString();
            lblCreatedBy.Text = clsGlobal.CurrentUser.Username;

        }
        private void NewInternationalLicenseApplication_Load(object sender, EventArgs e)
        {
            LoadIntialInfo();
        }

        private void ucDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            _LicenseID = obj;
      
            if (_LicenseID != -1)
            {
                 _InternationalLiceneseID =
              clsInternationalLicense.GetActiveInternationalLicenseID(ucDriverLicenseInfoWithFilter1.License.DriverID);

                if(_InternationalLiceneseID != -1)
                {
                    MessageBox.Show($"Person already have an active international license with ID= {_InternationalLiceneseID} ",
                        "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnIssue.Enabled = false;
                    llShowLicenseInfo.Enabled = true;
                    llShowLicenseHistory.Enabled = true;
                    return;
                }
                btnIssue.Enabled = true;
                lblLocalLicenseID.Text = obj.ToString();
                llShowLicenseHistory.Enabled = true;
                llShowLicenseInfo.Enabled = false;
            }
            else
            {
                btnIssue.Enabled = false;
                lblLocalLicenseID.Text = "???";
                llShowLicenseHistory.Enabled = false;
                llShowLicenseInfo.Enabled = false;
            }
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LicenseHistory frm = new LicenseHistory(ucDriverLicenseInfoWithFilter1.License.DriverInfo.PersonID);

            frm.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            InternationalDriverInfo frm = new InternationalDriverInfo(_InternationalLiceneseID);

            frm.ShowDialog();
        }

        int CreateNewApplication()
        {
            clsApplication NewApplication = new clsApplication();

            NewApplication.PersonID = ucDriverLicenseInfoWithFilter1.License.DriverInfo.PersonID;
            NewApplication.ApplicationDate = DateTime.Now;
            NewApplication.ApplicationTypeID = clsApplication.enApplicationType.NewInternational;
            NewApplication.ApplicationStaus = clsApplication.enApplicationStatus.Completed;
            NewApplication.LastStatusDate = DateTime.Now;
            NewApplication.ApplicationFees = clsApplicationTypes.Find(6).Fees;
            NewApplication.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (!NewApplication.SaveData()) return -1;

            return NewApplication.ApplicationID;
        }
        private void btnIssue_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to issue the license?","Confirm",MessageBoxButtons.YesNo,
                MessageBoxIcon.Information)==DialogResult.Yes)
            {
                int ApplciationID = CreateNewApplication();
                if (ApplciationID==-1)
                {
                    MessageBox.Show("Saved data falied", "Failed", MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
                    return;
                }


                int InternationalLicneseID = clsInternationalLicense.IssueInternationalLicense(ApplciationID,_LicenseID, clsGlobal.CurrentUser.UserID);
                if (InternationalLicneseID != -1)
                {
                    MessageBox.Show($"International License Issued Successfully with ID ={InternationalLicneseID}","License Issued",
                        MessageBoxButtons.OK,MessageBoxIcon.Information);
                    ucDriverLicenseInfoWithFilter1.FilterEnalbed = false;
                    btnIssue.Enabled = false;
                    lblILLicenseID.Text = InternationalLicneseID.ToString();
                    lblILAppID.Text = ApplciationID.ToString();
                    _InternationalLiceneseID = InternationalLicneseID;
                    llShowLicenseInfo.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Saved data falied", "Failed", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
          
        }
    }
}
