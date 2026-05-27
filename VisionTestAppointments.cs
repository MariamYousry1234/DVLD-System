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
    public partial class VisionTestAppointments : Form
    {
        int _LDLAppID = -1;
        public VisionTestAppointments(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();

            _LDLAppID = LocalDrivingLicenseApplicationID;
        }

        private void VisionTestAppointments_Load(object sender, EventArgs e)
        {

        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            if(clsTestAppointment.IsHasActiveTestAppointment(_LDLAppID,clsTestAppointment.enTestType.VisionTest))
            {
                MessageBox.Show("Person already have an active appointment for this test,You can not add new appointment", "Not allowed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }




        }
    }
}
