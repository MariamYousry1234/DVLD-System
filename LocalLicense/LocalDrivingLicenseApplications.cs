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
    public partial class LocalDrivingLicenseApplications : Form
    {
        public LocalDrivingLicenseApplications()
        {
            InitializeComponent();
        }

          
        DataTable _AllLocalDrivingLincenseApplications;
      

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LocalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            _AllLocalDrivingLincenseApplications = clsLocalDrivingApplicationLicense.GetLocalApplicationsList();
            dgvData.DataSource = _AllLocalDrivingLincenseApplications;
            lblRecords.Text = dgvData.Rows.Count.ToString();


            if(dgvData.Rows.Count >0)
            {
                dgvData.Columns["Driving Class"].Width = 190;
                dgvData.Columns["FullName"].Width = 180;
                dgvData.Columns["ApplicationDate"].Width = 150;
            }

            cbFilter.SelectedIndex = 0;
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            NewUpdateLocalDrivingLicenseApplication frm = new NewUpdateLocalDrivingLicenseApplication();

            frm.ShowDialog();

            LocalDrivingLicenseApplications_Load(null, null);
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtFiltering.Visible = (cbFilter.Text != "None"))
            {
                txtFiltering.Clear();
                txtFiltering.Focus();
            }     
        }

        private void txtFiltering_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFiltering.Text) || cbFilter.Text == "None")
                _AllLocalDrivingLincenseApplications.DefaultView.RowFilter = "";

            else if (cbFilter.Text != "L.D.L.AppID")
                _AllLocalDrivingLincenseApplications.DefaultView.RowFilter =
                    $"{cbFilter.Text} like '{txtFiltering.Text}%'";

            else
                _AllLocalDrivingLincenseApplications.DefaultView.RowFilter =
                   $"{cbFilter.Text} = {txtFiltering.Text}";
            lblRecords.Text = dgvData.Rows.Count.ToString();
        }

        private void txtFiltering_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.Text == "L.D.L.AppID")
                e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

            clsLocalDrivingApplicationLicense _LocalDrivingLicenseApplicaton =
                clsLocalDrivingApplicationLicense.FindByID((int)dgvData.CurrentRow.Cells[0].Value);


            editApplicationToolStripMenuItem.Enabled =
               (_LocalDrivingLicenseApplicaton.ApplicationStaus == clsLocalDrivingApplicationLicense.enApplicationStatus.New);


            deleteApplicationToolStripMenuItem.Enabled=
                  (_LocalDrivingLicenseApplicaton.ApplicationStaus == clsLocalDrivingApplicationLicense.enApplicationStatus.New);


            cancelApplicationToolStripMenuItem.Enabled=
                  (_LocalDrivingLicenseApplicaton.ApplicationStaus == clsLocalDrivingApplicationLicense.enApplicationStatus.New);

            schduleTestsToolStripMenuItem.Enabled = (!_LocalDrivingLicenseApplicaton.IsPassedAllTests() &&
                    _LocalDrivingLicenseApplicaton.ApplicationStaus == clsLocalDrivingApplicationLicense.enApplicationStatus.New);

            if (schduleTestsToolStripMenuItem.Enabled)
            {
             
                bool IsPassVisionTest= _LocalDrivingLicenseApplicaton.IsPassedThisTest(clsTestTypes.enTestType.VisionTest);
                bool IsPassWrittenTest = _LocalDrivingLicenseApplicaton.IsPassedThisTest(clsTestTypes.enTestType.WrittenTest);
                bool IsPassStreetTest = _LocalDrivingLicenseApplicaton.IsPassedThisTest(clsTestTypes.enTestType.PracticalTest);

                scheduleVisionTestToolStripMenuItem.Enabled = !IsPassVisionTest;
                scheduleWrittenTestToolStripMenuItem.Enabled = IsPassVisionTest && !IsPassWrittenTest;
                scheduleStreetToolStripMenuItem.Enabled = IsPassVisionTest && IsPassWrittenTest && !IsPassStreetTest;
            }

            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled =
                (_LocalDrivingLicenseApplicaton.IsPassedAllTests() &&
                !_LocalDrivingLicenseApplicaton.IsHaveActiveLicenseClass() &&
                _LocalDrivingLicenseApplicaton.ApplicationStaus==clsLocalDrivingApplicationLicense.enApplicationStatus.New);


            showLicenseToolStripMenuItem.Enabled = _LocalDrivingLicenseApplicaton.IsHaveActiveLicenseClass();

        }

        private void scheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddEditTestAppointments frmTest =
                new AddEditTestAppointments((int)dgvData.CurrentRow.Cells[0].Value,clsTestTypes.enTestType.VisionTest);

            frmTest.ShowDialog();

            LocalDrivingLicenseApplications_Load(null, null);
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddEditTestAppointments frmTest =
            new AddEditTestAppointments((int)dgvData.CurrentRow.Cells[0].Value, clsTestTypes.enTestType.WrittenTest);

            frmTest.ShowDialog();

            LocalDrivingLicenseApplications_Load(null, null);
        }

        private void scheduleStreetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddEditTestAppointments frmTest =
            new AddEditTestAppointments((int)dgvData.CurrentRow.Cells[0].Value, clsTestTypes.enTestType.PracticalTest);

            frmTest.ShowDialog();

            LocalDrivingLicenseApplications_Load(null, null);
        }

        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IssueDrivingLicenseForTheFirstTime frmIssueLicense =
                new IssueDrivingLicenseForTheFirstTime((int)dgvData.CurrentRow.Cells[0].Value);

            frmIssueLicense.ShowDialog();

            LocalDrivingLicenseApplications_Load(null, null);
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsLocalDrivingApplicationLicense LocalDrivingApplicationInfo =
                clsLocalDrivingApplicationLicense.FindByID((int)dgvData.CurrentRow.Cells[0].Value);

            int LicenseID = LocalDrivingApplicationInfo.GetActiveLicenseClassForPerson();

            if (LicenseID == -1)
            {
                MessageBox.Show("No License Found!", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            LicenseInfo frmLicenseInfo = new LicenseInfo(LicenseID);

            frmLicenseInfo.ShowDialog();
        }

        private void editApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewUpdateLocalDrivingLicenseApplication frm = 
                new NewUpdateLocalDrivingLicenseApplication((int)dgvData.CurrentRow.Cells[0].Value);

            frm.ShowDialog();

            LocalDrivingLicenseApplications_Load(null, null);
        }

        private void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsLocalDrivingApplicationLicense _LocalDrivingLicenseApplicaton =
              clsLocalDrivingApplicationLicense.FindByID((int)dgvData.CurrentRow.Cells[0].Value);


            if (_LocalDrivingLicenseApplicaton == null)
            {
                MessageBox.Show("This Local Application Not found !", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this application?","Confirm",MessageBoxButtons.YesNo,
                MessageBoxIcon.Information)==DialogResult.Yes)
            {
                if(_LocalDrivingLicenseApplicaton.DeletLocalDrivingLicenseApplication())
                {
                    MessageBox.Show("Local Application Deleted Successuflly", "Deleted", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    LocalDrivingLicenseApplications_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Delete Application Failed", "Failed", MessageBoxButtons.OK,
                      MessageBoxIcon.Error);
                }
            }

        }

        private void cancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
         
            clsLocalDrivingApplicationLicense _LocalDrivingLicenseApplicaton =
              clsLocalDrivingApplicationLicense.FindByID((int)dgvData.CurrentRow.Cells[0].Value);


            if (_LocalDrivingLicenseApplicaton == null)
            {
                MessageBox.Show("This Local Application Not found !", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Are you sure you want to Cancel this application?", "Confirm", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information) == DialogResult.Yes)
            {
                if (_LocalDrivingLicenseApplicaton.SetCancelled())
                {
                    MessageBox.Show("Application Cancelled Successuflly", "Deleted", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    LocalDrivingLicenseApplications_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Cancel Application Failed", "Faile", MessageBoxButtons.OK,
                      MessageBoxIcon.Error);
                }
            }

          
        }

        private void showPersonLicnenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = clsPerson.Find((string)dgvData.CurrentRow.Cells[2].Value).PersonID;

            LicenseHistory frmHistory = new LicenseHistory(PersonID);

            frmHistory.ShowDialog();

            LocalDrivingLicenseApplications_Load(null, null);
        }

        private void showApplicationDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LocalDrivingLicenseApplicationInfo frmInfo =
              new LocalDrivingLicenseApplicationInfo((int)dgvData.CurrentRow.Cells[0].Value);

            frmInfo.ShowDialog();

            LocalDrivingLicenseApplications_Load(null, null);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {

        }
    }
}
