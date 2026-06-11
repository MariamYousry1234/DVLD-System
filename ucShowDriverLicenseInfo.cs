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


        clsLicense License;
    
        public ucShowDriverLicenseInfo()
        {
            InitializeComponent();
        }


        public void LoadDriverLicenseInfo(int LicenseID)
        {
           
            License = clsLicense.Find(LicenseID);

            if(License==null)
            {
                MessageBox.Show("This license Not found !", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
       
                return;
            }


            lblClass.Text = License.LicenseClassInfo.ClassName;
            lblName.Text = License.DriverInfo.PersonInfo.GetFullName;
            lblLicenseID.Text = License.LicenseID.ToString();
            lblNationalNo.Text = License.DriverInfo.PersonInfo.NationalNo;
            lblGender.Text = License.DriverInfo.PersonInfo.Gender == 1 ? "Male" : "Female";
            lblIssueDate.Text = License.IssueDate.ToString("dd/MM/yyyy");
            lblIssueReason.Text = License.txtIssueReason;
            lblNotes.Text = License.Notes == "" ? "No Notes" : License.Notes;
            lblIsActive.Text = License.IsActive ? "Yes" : "No";
            lblDateOfBirth.Text = License.DriverInfo.PersonInfo.DateOfBirth.ToString("dd/MM/yyyy");
            lblDriverID.Text = License.DriverInfo.DriverID.ToString();
            lblExpirationDate.Text = License.ExpiryDate.ToString("dd/MM/yyyy");

            //
            lblIsDetained.Text = "No";


            if(!string.IsNullOrEmpty(License.DriverInfo.PersonInfo.ImagePath))
            {
                if (File.Exists(License.DriverInfo.PersonInfo.ImagePath))
                    pbImage.ImageLocation = License.DriverInfo.PersonInfo.ImagePath;
                else
                    MessageBox.Show("Could not find this image!", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
            } 
            else
                pbImage.Image = License.DriverInfo.PersonInfo.Gender == 1 ?Properties.Resources.Male_512
                    : Properties.Resources.Female_512;

        }
        private void ucShowDriverLicenseInfo_Load(object sender, EventArgs e)
        {

        }
    }
}
