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
    public partial class ScheduleTest : Form
    {

        enum enMode { AddNew=1,Update=2}
        enMode _Mode;

        int _LDLAppID = -1;
        clsLocalDrivingApplicationLicense LocalDrivingApplicationInfo;
        clsApplication RetakeTestApplication;
        clsTestAppointment TestAppointment;
        bool IsRetakeTest = false;
        clsTestTypes.enTestType _TestType = clsTestTypes.enTestType.None;
        int _AppointmentID=-1;
        int NumberOfTrails = 0;


        public ScheduleTest(int LDLAppID, clsTestTypes.enTestType TestType)
        {
            InitializeComponent();

            _LDLAppID = LDLAppID;
            _TestType = TestType;

            _Mode = enMode.AddNew;
        }

        public ScheduleTest(int AppointmentID,int LDLAppID,clsTestTypes.enTestType TestType)
        {
            InitializeComponent();

            _LDLAppID = LDLAppID;
            _TestType = TestType;
            _AppointmentID = AppointmentID;

            _Mode = enMode.Update;
        }

        private  void _LoadIntialData()
        {
            LocalDrivingApplicationInfo =
                clsLocalDrivingApplicationLicense.FindByID(_LDLAppID);

            if (LocalDrivingApplicationInfo == null)
            {
                MessageBox.Show("This Local Application Not Found !", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                this.Close();
                return;
            }


            TestAppointment = new clsTestAppointment();

            switch (_TestType)
            {
                case clsTestTypes.enTestType.VisionTest:
                    {
                        pbImage.Image = Properties.Resources.Vision_512;
                        groupBox1.Text = "Vision Test";
                    }
                    break;


                case clsTestTypes.enTestType.WrittenTest:
                    {
                        pbImage.Image = Properties.Resources.Written_Test_512;
                        groupBox1.Text = "Written Test";
                    }
                    break;


                case clsTestTypes.enTestType.PracticalTest:
                    {
                        pbImage.Image = Properties.Resources.wallpaper;
                        groupBox1.Text = "Practical Test";
                    }
                    break;

                default:
                    break;
            }

            lblLocked.Text = "";
            lblDLAppID.Text = "???";
            lblDrivingClass.Text = "???";
            lblName.Text = "???";
            lblTrail.Text = "???";
            dtpDate.MinDate = DateTime.Today;
            lblFees.Text = "???";
            gbRetakeTest.Enabled = false;
            NumberOfTrails = clsTestAppointment.NumberOfTrailsForTest(_LDLAppID, _TestType);

            if(NumberOfTrails > 0)
            {
                lblTitle.Text = "Schedual Retake Test";
                gbRetakeTest.Enabled = true;
            }
        }

        private void _CreateNewApplicationForRetakeTest()
        {
            RetakeTestApplication = new clsApplication();

            RetakeTestApplication.PersonID = LocalDrivingApplicationInfo.PersonID;
            RetakeTestApplication.ApplicationDate = DateTime.Now;
            RetakeTestApplication.ApplicationTypeID = clsApplication.enApplicationType.RetakeTest;
            RetakeTestApplication.ApplicationStaus = clsApplication.enApplicationStatus.New;
            RetakeTestApplication.LastStatusDate = DateTime.Now;
            RetakeTestApplication.ApplicationFees = clsApplicationTypes.Find("Retake Test").Fees;
            RetakeTestApplication.CreatedByUserID = clsGlobal.CurrentUser.UserID;
        }

   
        private void _SetAppointmentData()
        {
            float TestFees = clsTestTypes.Find(_TestType).Fees;

            lblDLAppID.Text = LocalDrivingApplicationInfo.LocalDrivingApplicationLicenseID.ToString();
            lblDrivingClass.Text = LocalDrivingApplicationInfo.LicenseClasssInfo.ClassName;
            lblName.Text = LocalDrivingApplicationInfo.PersonInfo.GetFullName;
            lblTrail.Text = NumberOfTrails.ToString();

            lblFees.Text = TestFees.ToString();

            if (NumberOfTrails > 0)
            {
                //make new application with retake test 
                IsRetakeTest = true;
                _CreateNewApplicationForRetakeTest();
        
                lblRAppFees.Text = RetakeTestApplication.ApplicationFees.ToString();
                lblTotalFees.Text = (TestFees + RetakeTestApplication.ApplicationFees).ToString();
            }
        }


        private void _FillData()
        {
            if(_Mode == enMode.AddNew)
            {
                TestAppointment.PaidFees = clsTestTypes.Find(_TestType).Fees;
                TestAppointment.IsLocked = false;
                TestAppointment.LocalDrivingLicenseApplicationID = _LDLAppID;
                TestAppointment.TestTypeID = (clsTestAppointment.enTestType)clsTestTypes.Find(_TestType).ID;
               

                if (IsRetakeTest)
                {
                    if (!RetakeTestApplication.SaveData())
                    {
                        MessageBox.Show("Data saved falied", "Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

                        return;
                    }
                    lblRTestAppID.Text = RetakeTestApplication.ApplicationID.ToString();
                    TestAppointment.ReTakeTestApplicationID = RetakeTestApplication.ApplicationID;
                }
            }

            TestAppointment.AppointmentDate = dtpDate.Value;
            TestAppointment.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (TestAppointment.Save())
            {
                MessageBox.Show("Data Saved Successfully", "Saved", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Data saved falied", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
            }
        }
      
        void _ShowDataToUpdate()
        {
            lblDLAppID.Text = TestAppointment.TestAppointmentID.ToString();
            lblDrivingClass.Text = LocalDrivingApplicationInfo.LicenseClasssInfo.ClassName;
            lblName.Text = LocalDrivingApplicationInfo.PersonInfo.GetFullName;
            lblTrail.Text = NumberOfTrails.ToString();
            dtpDate.Value = TestAppointment.AppointmentDate;
            lblFees.Text = TestAppointment.PaidFees.ToString();

            if(TestAppointment.ReTakeTestApplicationID != -1)
            {
                RetakeTestApplication=
                    clsApplication.Find(TestAppointment.ReTakeTestApplicationID);

                gbRetakeTest.Enabled = true;
                lblRTestAppID.Text = RetakeTestApplication.ApplicationID.ToString();
                lblRAppFees.Text = RetakeTestApplication.ApplicationFees.ToString();
                lblTotalFees.Text = (TestAppointment.PaidFees + RetakeTestApplication.ApplicationFees).ToString();
            }

            if (TestAppointment.IsLocked)
            {
                lblLocked.Text = "Peson already sat for the test, appointment locked";
                btnSave.Enabled = false;
                dtpDate.Enabled = false;
            }
        

        }
        void _ShowAppointmentDataToUpdate()
        {
            TestAppointment = clsTestAppointment.Find(_AppointmentID);

            if(TestAppointment==null)
            {
                
                MessageBox.Show("This Appointment Not Found !", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                this.Close();
                return;
            }

            _ShowDataToUpdate();
        }

        private void ScheduleTest_Load(object sender, EventArgs e)
        {
            _LoadIntialData();

            if (_Mode == enMode.AddNew)
                _SetAppointmentData();
            else
                _ShowAppointmentDataToUpdate();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _FillData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
