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
    public partial class TakeTest : Form
    {
        int _AppointmentID = -1;
        clsTestAppointment TestAppointment;
   
        clsTest TestResult;
        public TakeTest(int AppointmentID)
        {
            InitializeComponent();
            _AppointmentID = AppointmentID;
   
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void _LoadIntialData()
        {
            lblDLAppID.Text = TestAppointment.LocalDrivingLicenseApplicationID.ToString();

            clsLocalDrivingApplicationLicense LocalDrivingAppInfo = clsLocalDrivingApplicationLicense.FindByID(TestAppointment.LocalDrivingLicenseApplicationID);

            lblDrivingClass.Text = LocalDrivingAppInfo.LicenseClasssInfo.ClassName;
            lblName.Text = LocalDrivingAppInfo.PersonInfo.GetFullName;

            int NumberOfTrails = clsTestAppointment.NumberOfTrailsForTest(TestAppointment.LocalDrivingLicenseApplicationID,
             (clsTestTypes.enTestType)TestAppointment.TestTypeID);

            lblTrail.Text = NumberOfTrails.ToString();
            lblDate.Text = TestAppointment.AppointmentDate.ToString("dd/MM/yyy");
            lblFees.Text = TestAppointment.PaidFees.ToString();
            lblTesID.Text = "Not Token Yet";



            switch (TestAppointment.TestTypeID)
            {
                case clsTestAppointment.enTestType.VisionTest:
                    {
                        pbImage.Image = Properties.Resources.Vision_512;
                        groupBox1.Text = "Vision Test";
                    }
                    break;


                case clsTestAppointment.enTestType.WrittenTest:
                    {
                        pbImage.Image = Properties.Resources.Written_Test_512;
                        groupBox1.Text = "Written Test";
                    }
                    break;


                case clsTestAppointment.enTestType.PracticalTest:
                    {
                        pbImage.Image = Properties.Resources.wallpaper;
                        groupBox1.Text = "Street Test";
                    }
                    break;
            }

            rbPass.Checked = true;

            TestResult = new clsTest();
        }

        private void TakeTest_Load(object sender, EventArgs e)
        {
            TestAppointment = clsTestAppointment.Find(_AppointmentID);

            if(TestAppointment ==null)
            {
                MessageBox.Show("This Appointmnet Not Found !", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                this.Close();
                return;
            }

            _LoadIntialData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        { 
           if( MessageBox.Show("Are you sure you want to save? After that you can not change the Pass/Fail results after you save","Confirm",
                MessageBoxButtons.OKCancel,MessageBoxIcon.Warning) ==DialogResult.OK)
            {

                TestResult.TestAppointmentID = TestAppointment.TestAppointmentID;
                TestResult.TestResult = rbPass.Checked;
                TestResult.Notes = txtNotes.Text;
                TestResult.CreatedByUserID = clsGlobal.CurrentUser.UserID;

                if (TestResult.Save())
                {
                    MessageBox.Show("Data saves successfully", "Saved", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    lblTesID.Text = TestResult.TestID.ToString();
                    TestAppointment.IsLocked = true;
                    TestAppointment.Save();
                }
                else
                {
                    MessageBox.Show("Data saved failed", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }
    }
}
