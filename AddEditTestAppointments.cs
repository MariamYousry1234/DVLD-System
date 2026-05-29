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
    public partial class AddEditTestAppointments : Form
    {
        int _LDLAppID = -1;

        DataTable _AllApointments;
        clsTestTypes.enTestType _TestType = clsTestTypes.enTestType.None;

        public AddEditTestAppointments(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestType TestType)
        {
            InitializeComponent();

            _LDLAppID = LocalDrivingLicenseApplicationID;
            _TestType = TestType;
        }

        private void VisionTestAppointments_Load(object sender, EventArgs e)
        {
            switch (_TestType)
            {
                case clsTestTypes.enTestType.VisionTest:
                    {
                        pbImage.Image = Properties.Resources.Vision_512;
                        this.Text = "Vision Test Appointments";
                        lblTitle.Text = "Vision Test Appointments";
                    }
                    break;


                case clsTestTypes.enTestType.WrittenTest:
                    {
                        pbImage.Image = Properties.Resources.Written_Test_512;
                        this.Text = "Written Test Appointments";
                        lblTitle.Text = "Written Test Appointments";
                    }
                    break;


                case clsTestTypes.enTestType.PracticalTest:
                    {
                        pbImage.Image = Properties.Resources.wallpaper;
                        this.Text = "Street Test Appointments";
                        lblTitle.Text = "Street Test Appointments";
                    }
                    break;

                default:
                    break;
            }

            if (ucDrivingLicenseApplicationInfo1.LoadLocalApplicationInfo(_LDLAppID))
            {
                _AllApointments =
                 clsTestAppointment.GetAllAppointmentsForApplication(_LDLAppID,_TestType);

                dgvData.DataSource = _AllApointments;
                lblRecords.Text = dgvData.Rows.Count.ToString();


                if(dgvData.Rows.Count >0)
                {
                    dgvData.Columns["TestAppointmentID"].Width = 200;
                    dgvData.Columns["AppointmentDate"].Width = 200;
                }
            }
        }


        private void btnAddNew_Click(object sender, EventArgs e)
        {
            if(clsTestAppointment.IsHasActiveTestAppointment(_LDLAppID,_TestType))
            {
                MessageBox.Show("This person already have an active appointment for this test,You can not add new appointment", "Not allowed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (clsTest.IsPassedThisTest(_LDLAppID, _TestType))
            {
                MessageBox.Show("This person already passed this test befor,you can only retake failed test", "Not allowed", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            ScheduleTest frmAppointment = new ScheduleTest(_LDLAppID,_TestType);

            frmAppointment.ShowDialog();

            VisionTestAppointments_Load(null, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if((bool)dgvData.CurrentRow.Cells[3].Value)
            {
                MessageBox.Show("The test has already been completed and the result has been recorded.", "Finshed",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            TakeTest frmTakeTest = new TakeTest((int)dgvData.CurrentRow.Cells[0].Value);

            frmTakeTest.ShowDialog();

            VisionTestAppointments_Load(null, null);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int AppointmentID =(int)dgvData.CurrentRow.Cells[0].Value;

            ScheduleTest frmAppointment = new ScheduleTest(AppointmentID,_LDLAppID, _TestType);

            frmAppointment.ShowDialog();
           
            VisionTestAppointments_Load(null, null);
        }
    }
}
