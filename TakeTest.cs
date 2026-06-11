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
        clsTestTypes.enTestType _TestType;

        clsTest Test;
        public TakeTest(int AppointmentID,clsTestTypes.enTestType TestType)
        {
            InitializeComponent();
            _AppointmentID = AppointmentID;

            _TestType = TestType;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TakeTest_Load(object sender, EventArgs e)
        {
            ucScheduledTest1.TestType = _TestType;
            ucScheduledTest1.LoadAppointmentData(_AppointmentID);
            
            if(ucScheduledTest1.AppointmentID == -1)
            {
                btnSave.Enabled = false;
             
                return;
            }

            
            if(ucScheduledTest1.TestID != -1)
            {
                lblValidationMessage.Visible = true;
                Test = clsTest.Find(ucScheduledTest1.TestID);

                if(Test.TestResult)
                {
                    rbPass.Checked = true;
                }
                else
                {
                    rbFail.Checked = true;
                }
                txtNotes.Text = Test.Notes;
                rbPass.Enabled = false;
                rbFail.Enabled = false;


                return;
            }


            Test = new clsTest();
        }

        private void btnSave_Click(object sender, EventArgs e)
        { 
           if( MessageBox.Show("Are you sure you want to save? After that you can not change the Pass/Fail results after you save","Confirm",
                MessageBoxButtons.YesNo,MessageBoxIcon.Warning) == DialogResult.Yes)
            {

                Test.TestAppointmentID = _AppointmentID;
                Test.TestResult = rbPass.Checked;
                Test.Notes = txtNotes.Text;
                Test.CreatedByUserID = clsGlobal.CurrentUser.UserID;

                if (Test.Save())
                {
                    MessageBox.Show("Data saves successfully", "Saved", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    btnSave.Enabled = false;
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
