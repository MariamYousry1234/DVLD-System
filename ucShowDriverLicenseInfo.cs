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
using System.IO;

namespace DVLD_Project
{
    public partial class ucShowDriverLicenseInfo : UserControl
    {
        int _ApplicationID = -1;
        clsLicense License;
        clsDriver Driver;

        public ucShowDriverLicenseInfo()
        {
            InitializeComponent();
        }


        public void LoadDriverLicenseInfo(int ApplicationID)
        {
           
            License = clsLicense.FindByApplicationID(ApplicationID);

            if(License==null)
            {
                MessageBox.Show("This license Not found !", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
       
                return;
            }

            Driver = clsDriver.Find(License.DriverID);

            lblClass.Text = License.LicenseClassInfo.ClassName;
            lblName.Text = Driver.PersonInfo.GetFullName;
            lblLicenseID.Text = License.LicenseID.ToString();
            lblNationalNo.Text = Driver.PersonInfo.NationalNo;
            lblGender.Text = Driver.PersonInfo.Gender == 1 ? "Male" : "Female";
            lblIssueDate.Text = License.IssueDate.ToString("dd/MM/yyyy");

            switch (License.IssueReason)
            {
                case clsLicense.enIssueReason.FirstTime:
                    lblIssueReason.Text = "First Time";
                    break;

                case clsLicense.enIssueReason.Renew:
                    lblIssueReason.Text = "Renew";
                    break;

                case clsLicense.enIssueReason.ReplacementForDamaged:
                    lblIssueReason.Text = "Replacement For Damaged";
                    break;

                case clsLicense.enIssueReason.ReplacementForLost:
                    lblIssueReason.Text = "Replacement For Lost";
                    break;

            }

            lblNotes.Text = License.Notes == "" ? "No Notes" : License.Notes;
            lblIsActive.Text = License.IsActive ? "Yes" : "No";
            lblDateOfBirth.Text = Driver.PersonInfo.DateOfBirth.ToString("dd/MM/yyyy");
            lblDriverID.Text = Driver.DriverID.ToString();
            lblExpirationDate.Text = License.ExpiryDate.ToString("dd/MM/yyyy");

            lblIsDetained.Text = "No";


            if(!string.IsNullOrEmpty(Driver.PersonInfo.ImagePath))
                pbImage.ImageLocation=Driver.PersonInfo.ImagePath;
            else
                pbImage.Image =Driver.PersonInfo.Gender == 1 ?Properties.Resources.Male_512
                    : Properties.Resources.Female_512;

        }
        private void ucShowDriverLicenseInfo_Load(object sender, EventArgs e)
        {

        }
    }
}
