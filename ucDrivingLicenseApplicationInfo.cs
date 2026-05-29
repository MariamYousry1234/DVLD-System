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
        int _LDAppID=-1;
        clsLocalDrivingApplicationLicense _LocalDrivingApplicationInfo;
        public ucDrivingLicenseApplicationInfo()
        {
            InitializeComponent();


        }

        public bool LoadLocalApplicationInfo(int LDAppID)
        {
            _LDAppID = LDAppID;
            _LocalDrivingApplicationInfo = clsLocalDrivingApplicationLicense.FindByID(LDAppID);

            if(_LocalDrivingApplicationInfo == null)
            {
                MessageBox.Show("This Application Not Found !", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }


            _ShowLocalApplicationInfo();
            return true;
        }

        private void _ShowLocalApplicationInfo()
        {
            lblDLAppID.Text = _LocalDrivingApplicationInfo.LocalDrivingApplicationLicenseID.ToString();
            lblLIcenseClass.Text = _LocalDrivingApplicationInfo.LicenseClasssInfo.ClassName;
            lblPassedTests.Text = clsTest.GetNumberOfPassedTests(_LDAppID).ToString();
            lblApplicationID.Text = _LocalDrivingApplicationInfo.ApplicationID.ToString();
            lblDate.Text = _LocalDrivingApplicationInfo.ApplicationDate.ToString("dd/MM/yyyy");
            lblStatusDate.Text=_LocalDrivingApplicationInfo.LastStatusDate.ToString("dd/MM/yyyy");
            lblCreatedBy.Text = _LocalDrivingApplicationInfo.CreatedUserInfo.Username;

            switch(_LocalDrivingApplicationInfo.ApplicationStaus)
            {
                case clsLocalDrivingApplicationLicense.enApplicationStatus.New:
                    lblStatus.Text = "New";
                    break;

                case clsLocalDrivingApplicationLicense.enApplicationStatus.Cancelled:
                    lblStatus.Text = "Cancelled";
                    break;
            
                case clsLocalDrivingApplicationLicense.enApplicationStatus.Completed:
                    lblStatus.Text = "Completed";
                    break;
            }

            lblFees.Text = _LocalDrivingApplicationInfo.ApplicationFees.ToString();

            lblType.Text = _LocalDrivingApplicationInfo.ApplicationType.Title;
            lblApplicant.Text = _LocalDrivingApplicationInfo.PersonInfo.GetFullName;
        }


        private void llViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PersonDetails frm = new PersonDetails(_LocalDrivingApplicationInfo.PersonID);
            frm.ShowDialog();

            LoadLocalApplicationInfo(_LDAppID);
        }
    }
}
