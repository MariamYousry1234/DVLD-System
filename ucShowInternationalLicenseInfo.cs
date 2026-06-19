using Busniess_Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class ucShowInternationalLicenseInfo : UserControl
    {
        int _InternationalLicenseID = -1;
        clsInternationalLicense _InternationalLicenseInfo;
        public ucShowInternationalLicenseInfo()
        {
            InitializeComponent();
        }

        public int InternationalLicenseID
        {
            get {
                return _InternationalLicenseID;
                 }
        }
        public  void LoadInternationalLicenseInfo(int IntLicenseID)
        {
           _InternationalLicenseID = IntLicenseID;
            _InternationalLicenseInfo =  clsInternationalLicense.Find(IntLicenseID);

            if(_InternationalLicenseInfo == null)
            {
                _InternationalLicenseID = -1;
                MessageBox.Show("Could not found International License ID", "Not Found", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            lblName.Text = _InternationalLicenseInfo.DriverInfo.FullName;
            lblIntLicenseID.Text = _InternationalLicenseInfo.InternationalLicenseID.ToString();
            lblLicenseID.Text = _InternationalLicenseInfo.LocalLicenseID.ToString();
            lblNationalNo.Text = _InternationalLicenseInfo.DriverInfo.PersonInfo.NationalNo;
            lblGender.Text = _InternationalLicenseInfo.DriverInfo.PersonInfo.Gender == 1 ? "Male" : "Female";
            lblIssueDate.Text = _InternationalLicenseInfo.IssueDate.ToString("dd/MM/yyyy");
            lblApplicationID.Text = _InternationalLicenseInfo.ApplicationID.ToString();
            lblIsActive.Text = _InternationalLicenseInfo.IsActive ? "Yes" : "No";
            lblDateOfBirth.Text = _InternationalLicenseInfo.DriverInfo.PersonInfo.DateOfBirth.ToString("dd/MM/yyyy");
            lblDriverID.Text = _InternationalLicenseInfo.DriverID.ToString();
            lblExpirationDate.Text = _InternationalLicenseInfo.ExpiryDate.ToString("dd/MM/yyyy");

            if(!string.IsNullOrEmpty(_InternationalLicenseInfo.DriverInfo.PersonInfo.ImagePath))
            {
                if (!File.Exists(_InternationalLicenseInfo.DriverInfo.PersonInfo.ImagePath))
                {
                    MessageBox.Show("Could not found image", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                }
                else
                    pbImage.ImageLocation = _InternationalLicenseInfo.DriverInfo.PersonInfo.ImagePath;
            }
            else
            {
                pbImage.Image = _InternationalLicenseInfo.DriverInfo.PersonInfo.Gender == 1 ?
                    Properties.Resources.Male_512 : Properties.Resources.Female_512;
            }
        }
    }
}
