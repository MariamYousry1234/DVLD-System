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
    public partial class ucScheduleTest : UserControl
    {
        enum enMode { AddNew=0,Update=1}
        enMode Mode = enMode.AddNew;


        clsLocalDrivingApplicationLicense LocalDrivingLicenseApplication;
        clsTestTypes.enTestType _TestType = clsTestTypes.enTestType.VisionTest;
        int _AppointmentID = -1;
        enum enStatusMode { FirstTime=0,RetakeTest=1}
        enStatusMode StatusMode=enStatusMode.FirstTime;

        clsTestAppointment _Appointment;
        clsApplication _RetakeTestApplication;


        public clsTestTypes.enTestType TestType
        {
            set
            {
                _TestType = value;

                switch (_TestType)
                {
                    case clsTestTypes.enTestType.VisionTest:
                        {
                            pbImage.Image = Properties.Resources.Vision_512;
                            gbScheduleTest.Text = "Vision Test";
                        }
                        break;


                    case clsTestTypes.enTestType.WrittenTest:
                        {
                            pbImage.Image = Properties.Resources.Written_Test_512;
                            gbScheduleTest.Text = "Written Test";
                        }
                        break;


                    case clsTestTypes.enTestType.PracticalTest:
                        {
                            pbImage.Image = Properties.Resources.wallpaper;
                            gbScheduleTest.Text = "Practical Test";
                        }
                        break;
                }
            }
            get
            {
                return _TestType;
            }
          }
        public ucScheduleTest()
        {
            InitializeComponent();
        }

        private void LoadIntialInfo()
        {
          
            lblDLAppID.Text = LocalDrivingLicenseApplication.LocalDrivingApplicationLicenseID.ToString();
            lblDrivingClass.Text = LocalDrivingLicenseApplication.LicenseClasssInfo.ClassName;
            lblName.Text = LocalDrivingLicenseApplication.PersonInfo.GetFullName;
            lblTrail.Text = LocalDrivingLicenseApplication.NumberOfTrailsForTest(_TestType).ToString();
            dtpDate.MinDate = DateTime.Now;
            lblFees.Text = clsTestTypes.Find(_TestType).Fees.ToString();


            if(StatusMode == enStatusMode.RetakeTest)
            {
                lblTitle.Text = "Retake Schedule Test";
                gbRetakeTest.Enabled = true;
                lblRAppFees.Text = clsApplicationTypes.Find((int)clsApplication.enApplicationType.RetakeTest).Fees.ToString();   
            }
            else
            {
                lblTitle.Text = "Schedule Test";
                gbRetakeTest.Enabled = false;
                lblRAppFees.Text = "0";
            }

            lblTotalFees.Text = (Convert.ToSingle(lblFees.Text) + Convert.ToSingle(lblRAppFees.Text)).ToString();
            lblRTestAppID.Text = "N/A";
        }

        private bool LoadInfoToUpdate()
        {
            _Appointment = clsTestAppointment.Find(_AppointmentID);

            if(_Appointment==null)
            {
                MessageBox.Show("Appointment Not Found !", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return false;
            }

            dtpDate.MinDate = (DateTime.Now < _Appointment.AppointmentDate)?
                DateTime.Now : _Appointment.AppointmentDate;

            dtpDate.Value = _Appointment.AppointmentDate;

            if (_Appointment.ReTakeTestApplicationID != -1)
            {
                lblRTestAppID.Text = _Appointment.ReTakeTestApplicationID.ToString();
            }

            return true;
        }

        bool HandleIsHaveActiveAppointment()
        {
            if (Mode == enMode.AddNew && LocalDrivingLicenseApplication.IsHasActiveTestAppointment(_TestType))
            {
                lblException.Visible = true;
                lblException.Text = "This Person already have Active appointment";
                btnSave.Enabled = false;
                dtpDate.Enabled = false;
                return true;
            }

            return false;
        }

        bool HandleIsAlreadyPassedThisTest()
        {
            if(Mode==enMode.AddNew && LocalDrivingLicenseApplication.IsPassedThisTest(_TestType))
            {
                lblException.Visible = true;
                lblException.Text = "This Person Already Passed This Test";
                btnSave.Enabled = false;
                dtpDate.Enabled = false;
                return true;
            }

            return false;
        }

        bool HandleIsPassedPreviousTest()
        {
            if(Mode==enMode.AddNew)
            {
                 switch (_TestType)
                {
                    case clsTestTypes.enTestType.VisionTest:
                        return true;


                    case clsTestTypes.enTestType.WrittenTest:
                        {
                            if (!LocalDrivingLicenseApplication.IsPassedThisTest(clsTestTypes.enTestType.VisionTest))
                            {
                                lblException.Visible = true;
                                lblException.Text = "This Person should pass vision test First";
                                btnSave.Enabled = false;
                                dtpDate.Enabled = false;
                                return false;
                            }
                            return true;
                        }



                    case clsTestTypes.enTestType.PracticalTest:
                        {
                            if (!LocalDrivingLicenseApplication.IsPassedThisTest(clsTestTypes.enTestType.WrittenTest))
                            {
                                lblException.Visible = true;
                                lblException.Text = "This Person should pass written test First";
                                btnSave.Enabled = false;
                                dtpDate.Enabled = false;
                                return false;
                            }
                            return true;
                        }
                  }
                }

            return true;
        }


        bool HandleIsAppointmentLocked()
        {
            if(Mode==enMode.Update && _Appointment.IsLocked)
            {
                lblException.Visible = true;
                lblException.Text = "Peson already sat for the test, appointment locked";
                btnSave.Enabled = false;
                dtpDate.Enabled = false;
                return true;
            }

            return false;
        }
       
        public void LoadInfo(int LocalDrivingLicenseApplicationID,int AppointmentID = -1)
        {
    
            Mode = AppointmentID == -1 ? enMode.AddNew : enMode.Update;

            _AppointmentID = AppointmentID;
            LocalDrivingLicenseApplication =  clsLocalDrivingApplicationLicense.FindByID(LocalDrivingLicenseApplicationID);

            
            if(LocalDrivingLicenseApplication==null)
            {
                MessageBox.Show("Local Driving Application Info Not Found !", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return;
            }


            StatusMode = 
                LocalDrivingLicenseApplication.DoesAttentTestBefore(_TestType) ? enStatusMode.RetakeTest :
                 enStatusMode.FirstTime;



            if (Mode == enMode.AddNew)
                _Appointment = new clsTestAppointment();

            LoadIntialInfo();

            if (Mode == enMode.Update)
            {
                if (!LoadInfoToUpdate()) return;

                if (HandleIsAppointmentLocked()) return;
            }

      
            if (HandleIsHaveActiveAppointment()) return;


            if (HandleIsAlreadyPassedThisTest()) return;


            if (!HandleIsPassedPreviousTest()) return;


        }

        bool HandleNewRetakeTestApplication()
        {
            _RetakeTestApplication = new clsApplication();

            _RetakeTestApplication.PersonID = LocalDrivingLicenseApplication.PersonID;
            _RetakeTestApplication.ApplicationDate = DateTime.Now;
            _RetakeTestApplication.ApplicationTypeID =clsApplication.enApplicationType.RetakeTest;
            _RetakeTestApplication.ApplicationStaus = clsApplication.enApplicationStatus.Completed;
            _RetakeTestApplication.LastStatusDate = DateTime.Now;
            _RetakeTestApplication.ApplicationFees = clsApplicationTypes.Find((int)clsApplication.enApplicationType.RetakeTest).Fees;
            _RetakeTestApplication.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            bool IsSaved = _RetakeTestApplication.SaveData();
            if (!IsSaved) return false;

            _Appointment.ReTakeTestApplicationID = _RetakeTestApplication.ApplicationID;

            return true;
        }

        bool HandleAppointmentDate()
         {
            if (dtpDate.Value < DateTime.Now)
            {
                MessageBox.Show("The appointment date must be in the future.",
                  "Invalid Date",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (!HandleAppointmentDate()) return;

            if(Mode==enMode.AddNew && StatusMode ==enStatusMode.RetakeTest)
            {
                if(!HandleNewRetakeTestApplication())
                {
                    MessageBox.Show("Faild to Create application", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            _Appointment.AppointmentDate = dtpDate.Value;
            _Appointment.PaidFees =Convert.ToSingle( lblFees.Text);
            _Appointment.IsLocked = false;
            _Appointment.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplication.LocalDrivingApplicationLicenseID;
            _Appointment.TestTypeID = _TestType;
            _Appointment.CreatedByUserID = clsGlobal.CurrentUser.UserID;
          

            if(_Appointment.Save())
            {
                if (Mode == enMode.AddNew && _Appointment.ReTakeTestApplicationID != -1)
                {
                    lblRTestAppID.Text = _Appointment.ReTakeTestApplicationID.ToString();
                }

                Mode = enMode.Update;
                MessageBox.Show("Data Saved Successfully", "Saved", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
    
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
        }
    }
}
