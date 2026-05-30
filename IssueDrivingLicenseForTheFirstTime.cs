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
    public partial class IssueDrivingLicenseForTheFirstTime : Form
    {
        int _DLAppID = -1;
        clsDriver Driver;
        clsLicense License;
        clsLocalDrivingApplicationLicense LocalDrivingLicenseInfo;
        clsApplication Application;

        public IssueDrivingLicenseForTheFirstTime(int DrivingLicenseApplicationID)
        {
            InitializeComponent();

            _DLAppID = DrivingLicenseApplicationID;
        }

        int CreateNewDriver()
        {
            Driver = new clsDriver();
            Driver.PersonID = LocalDrivingLicenseInfo.PersonID;
            Driver.CreatedbyUserID = clsGlobal.CurrentUser.UserID;
            Driver.CreatedDate = DateTime.Now;

            if(Driver.Save())
            {
                return Driver.DriverID;
            }


            return -1;
        }
        private void IssueDrivingLicenseForTheFirstTime_Load(object sender, EventArgs e)
        {
  
            if(ucDrivingLicenseApplicationInfo1.LoadLocalApplicationInfo(_DLAppID))
            {
                LocalDrivingLicenseInfo = clsLocalDrivingApplicationLicense.FindByID(_DLAppID);
            }
            txtNotes.Focus();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            int DriverID = CreateNewDriver();

            if(DriverID ==-1)
            {
                MessageBox.Show("Data Saved failed", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            License = new clsLicense();

            License.DriverID = DriverID;
            License.ApplicationID = LocalDrivingLicenseInfo.ApplicationID;
            License.LicenseClassID = LocalDrivingLicenseInfo.LicenseClassID;
            License.IssueDate = DateTime.Now;
            short HowManyYearsLicenseValid = clsLicenseClass.Find(LocalDrivingLicenseInfo.LicenseClassID).DeafualtValidityLength;
            License.ExpiryDate = DateTime.Now.AddYears(HowManyYearsLicenseValid);
            License.IssueReason = clsLicense.enIssueReason.FirstTime;
            License.IsActive = true;
            License.PaidFees = clsLicenseClass.Find(LocalDrivingLicenseInfo.LicenseClassID).ClassFees;
            License.Notes = txtNotes.Text;
            License.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if(License.Save())
            {
                //change application status from new to completed
                Application = clsApplication.Find(LocalDrivingLicenseInfo.ApplicationID);
                Application.ApplicationStaus = clsApplication.enApplicationStatus.Completed;
                Application.LastStatusDate = DateTime.Now;
                Application.SaveData();

                MessageBox.Show($"License Issues Successfully with license ID = {License.LicenseID}", "Succeded", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Data Saved failed", "Error", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
            }


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
