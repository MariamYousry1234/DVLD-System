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
    public partial class ucDrivingLicenseApplicationInfo : UserControl
    {
       
        clsLocalDrivingApplicationLicense _LocalDrivingApplicationInfo;
        public ucDrivingLicenseApplicationInfo()
        {
            InitializeComponent();

        }

        public bool LoadLocalApplicationInfo(int LDAppID)
        {

            _LocalDrivingApplicationInfo = clsLocalDrivingApplicationLicense.FindByID(LDAppID);

            if (_LocalDrivingApplicationInfo == null)
            {
                MessageBox.Show("Local Application Not Found !", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }


            _ShowLocalApplicationInfo();
            return true;
        }

        public bool LoadLocalAppInfoByApplicationID(int ApplicationID)
        {
            _LocalDrivingApplicationInfo = clsLocalDrivingApplicationLicense.FindByApplicationID(ApplicationID);

            if (_LocalDrivingApplicationInfo == null)
            {
                MessageBox.Show("Local Application Not Found !", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }


            _ShowLocalApplicationInfo();
            return true;
        }

        private void _ShowLocalApplicationInfo()
        {

            llShowLicenceInfo.Enabled =_LocalDrivingApplicationInfo.IsHaveActiveLicenseClass();
            lblDLAppID.Text = _LocalDrivingApplicationInfo.LocalDrivingApplicationLicenseID.ToString();
            lblLIcenseClass.Text = _LocalDrivingApplicationInfo.LicenseClasssInfo.ClassName;
            lblPassedTests.Text = _LocalDrivingApplicationInfo.GetNumberOfPassedTests().ToString();

            ucShowApplicationInfo1.LoadApplicationInfo(_LocalDrivingApplicationInfo.ApplicationID);
        }

        private void llShowLicenceInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LicenseInfo frm = new LicenseInfo(_LocalDrivingApplicationInfo.ApplicationID);
            frm.ShowDialog();
        }
    }  
}
