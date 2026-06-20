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
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_Project
{
    public partial class NewInternationalLicenseApplication : Form
    {

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
            lblApplicationDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
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



            if (obj == -1)
            {
                btnIssue.Enabled = false;
                llShowLicenseHistory.Enabled = false;
                llShowLicenseInfo.Enabled = false;
                lblLocalLicenseID.Text = "???";
                return;
            }


            llShowLicenseHistory.Enabled = true;
            lblLocalLicenseID.Text = obj.ToString();

            if (ucDriverLicenseInfoWithFilter1.License.LicenseClassID != 3)
            {
                MessageBox.Show("Selected License should be class 3 ,select another one", "Not allowed", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                btnIssue.Enabled = false;

                return;
            }

            _InternationalLiceneseID =
           clsInternationalLicense.GetActiveInternationalLicenseID(ucDriverLicenseInfoWithFilter1.License.DriverID);

            if (_InternationalLiceneseID != -1)
            {
                MessageBox.Show($"Person already have an active international license with ID= {_InternationalLiceneseID} ",
                    "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssue.Enabled = false;
                llShowLicenseInfo.Enabled = true;
                return;
            }

            btnIssue.Enabled = true;



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


        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to issue the license?", "Confirm", MessageBoxButtons.YesNo,
                MessageBoxIcon.Information) == DialogResult.Yes)
            {

                clsInternationalLicense NewInternationalLicense = new clsInternationalLicense();

                NewInternationalLicense.IssueDate = DateTime.Now;
                NewInternationalLicense.ExpiryDate = DateTime.Now.AddYears(10);
                NewInternationalLicense.IsActive = true;
                NewInternationalLicense.DriverID = ucDriverLicenseInfoWithFilter1.License.DriverID;
                NewInternationalLicense.LocalLicenseID = ucDriverLicenseInfoWithFilter1.License.LicenseID;
                NewInternationalLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;


                NewInternationalLicense.PersonID = ucDriverLicenseInfoWithFilter1.License.DriverInfo.PersonID;
                NewInternationalLicense.ApplicationDate = DateTime.Now;
                NewInternationalLicense.ApplicationTypeID = clsApplication.enApplicationType.NewInternational;
                NewInternationalLicense.ApplicationStaus = clsApplication.enApplicationStatus.Completed;
                NewInternationalLicense.LastStatusDate = DateTime.Now;
                NewInternationalLicense.ApplicationFees = clsApplicationTypes.Find((int)clsApplication.enApplicationType.NewInternational).Fees;
                NewInternationalLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;



                if (!NewInternationalLicense.Save())
                {

                    MessageBox.Show("Saved data falied", "Failed", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show($"International License Issued Successfully with ID ={NewInternationalLicense.InternationalLicenseID}", "License Issued",
                   MessageBoxButtons.OK, MessageBoxIcon.Information);
                ucDriverLicenseInfoWithFilter1.FilterEnalbed = false;
                btnIssue.Enabled = false;
                lblILLicenseID.Text = NewInternationalLicense.InternationalLicenseID.ToString();
                lblILAppID.Text = NewInternationalLicense.ApplicationID.ToString();
                _InternationalLiceneseID = NewInternationalLicense.InternationalLicenseID;
                llShowLicenseInfo.Enabled = true;
            }
        }
    }
}