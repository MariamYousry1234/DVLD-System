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
            int NumberOfTests = (int)dgvData.CurrentRow.Cells["PassedTests"].Value;
            string Status = (string)dgvData.CurrentRow.Cells["Status"].Value;

            scheduleVisionTestToolStripMenuItem.Enabled = (NumberOfTests == 0);
            scheduleWrittenTestToolStripMenuItem.Enabled = (NumberOfTests == 1);
            scheduleStreetToolStripMenuItem.Enabled = (NumberOfTests == 2);

            schduleTestsToolStripMenuItem.Enabled =
                (NumberOfTests != 3) && (Status != "Cancelled");


            int ApplicationID = 
                clsLocalDrivingApplicationLicense.FindByID((int)dgvData.CurrentRow.Cells[0].Value).ApplicationID;

            bool IsHasLicenseForThisClass =
                         clsLicense.IsPersonHasLicenseForThisClass(ApplicationID);

            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled =
                ((NumberOfTests == 3) && !IsHasLicenseForThisClass);

            showLicenseToolStripMenuItem.Enabled = IsHasLicenseForThisClass;

            editApplicationToolStripMenuItem.Enabled = (Status == "New");
            deleteApplicationToolStripMenuItem.Enabled = (Status != "Completed");
            cancelApplicationToolStripMenuItem.Enabled= (Status == "New");

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

            int ApplicatonID =
                clsLocalDrivingApplicationLicense.FindByID((int)dgvData.CurrentRow.Cells[0].Value).ApplicationID;
            LicenseInfo frmLicenseInfo = new LicenseInfo(ApplicatonID);

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
            int LDLAppID = (int)dgvData.CurrentRow.Cells[0].Value;

            clsLocalDrivingApplicationLicense LDLAppInfo =
                clsLocalDrivingApplicationLicense.FindByID(LDLAppID);

            if(LDLAppInfo==null)
            {
                MessageBox.Show("This Local Driving license Application Not found !", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;

            }

            if(MessageBox.Show("Are you sure you want to delete this application?","Confirm",MessageBoxButtons.OKCancel,
                MessageBoxIcon.Information)==DialogResult.OK)
            {
                if(clsLocalDrivingApplicationLicense.DeletLocalDrivingLicenseApplication(LDLAppID))
                {
                    MessageBox.Show("Application Deleted Successuflly","Deleted",MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Delete Application Failed", "Faile", MessageBoxButtons.OK,
                      MessageBoxIcon.Error);
                }
            }

            LocalDrivingLicenseApplications_Load(null, null);
        }

        private void cancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LDLAppID = (int)dgvData.CurrentRow.Cells[0].Value;

            if (MessageBox.Show("Are you sure you want to Cancel this application?", "Confirm", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Information) == DialogResult.OK)
            {
                if (clsLocalDrivingApplicationLicense.CancelLocalDrivingLicenseApplication(LDLAppID))
                {
                    MessageBox.Show("Application Cancelled Successuflly", "Deleted", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Cancel Application Failed", "Faile", MessageBoxButtons.OK,
                      MessageBoxIcon.Error);
                }
            }

            LocalDrivingLicenseApplications_Load(null, null);
        }

        private void showPersonLicnenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {

            LicenseHistory frmHistory = 
                new LicenseHistory((string)dgvData.CurrentRow.Cells[2].Value);

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
    }
}
