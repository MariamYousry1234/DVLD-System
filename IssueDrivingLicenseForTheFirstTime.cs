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
    
        clsLocalDrivingApplicationLicense LocalDrivingLicenseInfo;
       
        public IssueDrivingLicenseForTheFirstTime(int DrivingLicenseApplicationID)
        {
            InitializeComponent();

            _DLAppID = DrivingLicenseApplicationID;
        }

      
        private void IssueDrivingLicenseForTheFirstTime_Load(object sender, EventArgs e)
        {

            if (ucDrivingLicenseApplicationInfo1.LoadLocalApplicationInfo(_DLAppID))
            {
                LocalDrivingLicenseInfo = ucDrivingLicenseApplicationInfo1.LocalDrivingApplicationInfo;
            }
            else
                this.Close();

           txtNotes.Focus();


            if(!LocalDrivingLicenseInfo.IsPassedAllTests())
            {
                MessageBox.Show("Person should pass all tests first ", "Not Allowed", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                this.Close();
                return;
            }

            int LicenseID = LocalDrivingLicenseInfo.GetActiveLicenseClassForPerson();
            if (LicenseID  != -1)
            {
                MessageBox.Show($"Person already has license before with license ID= {LicenseID}", "Not Allowed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {

            int LicenseID = LocalDrivingLicenseInfo.IssueLicenseForFirstTime(txtNotes.Text, clsGlobal.CurrentUser.UserID);

          
            if(LicenseID != -1)
            {
                //change application status from new to completed
            
                MessageBox.Show($"License Issues Successfully with license ID = {LicenseID}", "Succeded", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("License Was Not Issued", "Failed", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
