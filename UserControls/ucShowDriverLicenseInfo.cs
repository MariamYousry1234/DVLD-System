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

        int _LicenseID = -1;
        int _DriverID = -1;
        clsLicense _License;
    
        public ucShowDriverLicenseInfo()
        {
            InitializeComponent();
        }

        public int LicenseID
        {
            get
            {
                return _LicenseID;
            }
        }

        public int DriverID
        {
            get
            {
                return _DriverID;
            }
        }

        public clsLicense License
        {
            get
            {
                return _License;
            }
        }
        public void LoadDriverLicenseInfo(int LicenseID)
        {

            _License = clsLicense.Find(LicenseID);
            
            if(_License == null)
            {
                SetIntialInfo();
                _LicenseID = -1;
                _DriverID = -1;
                MessageBox.Show("Couldn't find license ID!", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            _LicenseID = LicenseID;
            _DriverID = _License.DriverID;

            lblClass.Text = _License.LicenseClassInfo.ClassName;
            lblName.Text = _License.DriverInfo.PersonInfo.GetFullName;
            lblLicenseID.Text = _License.LicenseID.ToString();
            lblNationalNo.Text = _License.DriverInfo.PersonInfo.NationalNo;
            lblGender.Text = _License.DriverInfo.PersonInfo.Gender == 1 ? "Male" : "Female";
            lblIssueDate.Text = _License.IssueDate.ToString("dd/MM/yyyy");
            lblIssueReason.Text = _License.txtIssueReason;
            lblNotes.Text = _License.Notes == "" ? "No Notes" : _License.Notes;
            lblIsActive.Text = _License.IsActive ? "Yes" : "No";
            lblDateOfBirth.Text = _License.DriverInfo.PersonInfo.DateOfBirth.ToString("dd/MM/yyyy");
            lblDriverID.Text = _License.DriverInfo.DriverID.ToString();
            lblExpirationDate.Text = _License.ExpiryDate.ToString("dd/MM/yyyy");

            
            lblIsDetained.Text = _License.IsDetained ? "Yes":"No" ;


            if(!string.IsNullOrEmpty(_License.DriverInfo.PersonInfo.ImagePath))
            {
                if (File.Exists(_License.DriverInfo.PersonInfo.ImagePath))
                    pbImage.ImageLocation = _License.DriverInfo.PersonInfo.ImagePath;
                else
                    MessageBox.Show("Could not find this image!", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
            } 
            else
                pbImage.Image = _License.DriverInfo.PersonInfo.Gender == 1 ?Properties.Resources.Male_512
                    : Properties.Resources.Female_512;

        }

        void SetIntialInfo()
        {
            lblClass.Text = "???";
            lblName.Text = "???";
            lblLicenseID.Text = "???";
            lblNationalNo.Text = "???";
            lblGender.Text = "???";
            lblIssueDate.Text = "???";
            lblIssueReason.Text = "???";
            lblNotes.Text = "???";
            lblIsActive.Text = "???";
            lblDateOfBirth.Text = "???";
            lblDriverID.Text = "???";
            lblExpirationDate.Text = "???";

            //
            lblIsDetained.Text = "???";

            pbImage.Image = Properties.Resources.Male_512;
                
        }
        private void ucShowDriverLicenseInfo_Load(object sender, EventArgs e)
        {

        }
    }
}
