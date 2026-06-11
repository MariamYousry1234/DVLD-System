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
    public partial class ucScheduledTest : UserControl
    {


        clsTestTypes.enTestType _TestType = clsTestTypes.enTestType.VisionTest;

        clsTestAppointment _TestAppointment;
        clsLocalDrivingApplicationLicense LocalDrivingAppInfo;
        int  _AppointmentID = -1;
        public ucScheduledTest()
        {
            InitializeComponent();
        }

        public clsTestTypes.enTestType TestType
        {
            get
            {
                return _TestType;
            }
            set
            {
                _TestType = value;
                switch (_TestType)
                {
                    case clsTestTypes.enTestType.VisionTest:
                        {
                            pbImage.Image = Properties.Resources.Vision_512;
                            gbScheduledTest.Text = "Vision Test";
                        }


                        break;


                    case clsTestTypes.enTestType.WrittenTest:
                        {
                            pbImage.Image = Properties.Resources.Written_Test_512;
                            gbScheduledTest.Text = "Written Test";
                        }

                        break;


                    case clsTestTypes.enTestType.PracticalTest:
                        {
                            pbImage.Image = Properties.Resources.wallpaper;
                            gbScheduledTest.Text = "Street Test";
                        }
                        break;
                }

            }
        }

        public int AppointmentID
        {
            get
            {
                return _AppointmentID;
            }
        }

        public int LocalDrivingApplicationID
        {
            get
            {
                return _TestAppointment.LocalDrivingLicenseApplicationID;
            }
        }

        public int TestID
        {
            get
            {
                return _TestAppointment.TestID;
            }
        }
        void LoadData()
        {
            lblDLAppID.Text = _TestAppointment.LocalDrivingLicenseApplicationID.ToString();

            lblDrivingClass.Text = LocalDrivingAppInfo.LicenseClasssInfo.ClassName;
            lblName.Text = LocalDrivingAppInfo.PersonInfo.GetFullName;

            lblTrail.Text =LocalDrivingAppInfo.NumberOfTrailsForTest(_TestType).ToString();
            lblDate.Text = _TestAppointment.AppointmentDate.ToString("dd/MM/yyy");
            lblFees.Text = _TestAppointment.PaidFees.ToString();
           
            
            lblTesID.Text =_TestAppointment.TestID==-1 ? "Not Token Yet": _TestAppointment.TestID.ToString();
            lblTitle.Text = _TestAppointment.ReTakeTestApplicationID == -1 ?
                "Scheduled Test" : "Retake Scheduled Test";
        }

        public void LoadAppointmentData(int AppointmentID)
        {
            _AppointmentID = AppointmentID;
            _TestAppointment = clsTestAppointment.Find(AppointmentID);

            if (_TestAppointment == null)
            {
                MessageBox.Show("This Appointmnet Not Found !", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                _AppointmentID = -1;
                return;
            }

            LocalDrivingAppInfo = clsLocalDrivingApplicationLicense.FindByID(_TestAppointment.LocalDrivingLicenseApplicationID);

            if (LocalDrivingAppInfo == null)
            {
                MessageBox.Show("Local Application Not Found !", "Error", MessageBoxButtons.OK,
                          MessageBoxIcon.Error);
                _TestAppointment.LocalDrivingLicenseApplicationID = -1;
                return;
            }


            LoadData();
        }
    }

}
