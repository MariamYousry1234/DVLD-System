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

  

        int _LocalDrivingLicenseApplicationID = -1;
        clsTestTypes.enTestType _TestType = clsTestTypes.enTestType.VisionTest;
        int _AppointmentID = -1;
     

        public ScheduleTest(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestType TestType, int AppointmentID = -1)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestType = TestType;
            _AppointmentID = AppointmentID;
        }


        private void ScheduleTest_Load(object sender, EventArgs e)
        {
            ucScheduleTest1.TestType = _TestType;
            ucScheduleTest1.LoadInfo(_LocalDrivingLicenseApplicationID, _AppointmentID);
        }

   

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }   
    }
}
