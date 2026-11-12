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
    public partial class ManageDetainedLicenses : Form
    {
        public ManageDetainedLicenses()
        {
            InitializeComponent();
        }

        DataTable _AllDetainedLicenses;

        private void ManageDetainedLicenses_Load(object sender, EventArgs e)
        {
            _AllDetainedLicenses = clsDetainReleaseLicense.GetDetainedLicensesList();
            dgvData.DataSource = _AllDetainedLicenses;
            lblRecords.Text = _AllDetainedLicenses.Rows.Count.ToString();

            cbFilter.SelectedIndex = 0;

            if(dgvData.Rows.Count>0)
            {
                dgvData.Columns["DetainedDate"].Width = 150;
                dgvData.Columns["ReleaseDate"].Width = 150;
                dgvData.Columns["Full Name"].Width = 150;
                dgvData.Columns["ReleaseApplicationID"].Width = 115;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            DetainLicense frm = new DetainLicense();

            frm.ShowDialog();

            ManageDetainedLicenses_Load(null, null);
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            ReleaseDetainedLicense frm = new ReleaseDetainedLicense();

            frm.ShowDialog();

            ManageDetainedLicenses_Load(null, null);
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PersonDetails frm = new PersonDetails((string)dgvData.CurrentRow.Cells["NationalNo"].Value);

            frm.ShowDialog();

            ManageDetainedLicenses_Load(null, null);
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LicenseInfo frm = new LicenseInfo((int)dgvData.CurrentRow.Cells["LicenseID"].Value);

            frm.ShowDialog();

        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = clsPerson.Find((string)dgvData.CurrentRow.Cells["NationalNo"].Value).PersonID;
            LicenseHistory frm = new LicenseHistory(PersonID);

            frm.ShowDialog();

            ManageDetainedLicenses_Load(null, null);
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReleaseDetainedLicense frm = new ReleaseDetainedLicense((int)dgvData.CurrentRow.Cells["LicenseID"].Value);

            frm.ShowDialog();

            ManageDetainedLicenses_Load(null, null);
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            _AllDetainedLicenses.DefaultView.RowFilter = "";
            lblRecords.Text = _AllDetainedLicenses.Rows.Count.ToString();

            if (cbFilter.Text == "None")
            {
                txtFiltering.Visible = false;
                cbIsReleased.Visible = false;
                return;
            }

            if (cbFilter.Text == "IsReleased")
            {
                txtFiltering.Visible = false;
                cbIsReleased.Visible = true;
                cbIsReleased.SelectedIndex = 0;

                return;
            }

            cbIsReleased.Visible = false;
            txtFiltering.Visible = true;
            txtFiltering.Clear();
            txtFiltering.Focus();
        }

        private void txtFiltering_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFiltering.Text) || cbFilter.Text == "None")
            {
                _AllDetainedLicenses.DefaultView.RowFilter = "";
            }
            else if (cbFilter.SelectedItem.ToString() == "ReleaseApplicationID" ||
                cbFilter.SelectedItem.ToString() == "DetainedID")
            {
                _AllDetainedLicenses.DefaultView.RowFilter = $"{cbFilter.Text} ={txtFiltering.Text.Trim()}";
            }
            else
                _AllDetainedLicenses.DefaultView.RowFilter = $"[{cbFilter.Text}] like '{txtFiltering.Text.Trim()}%'";

            lblRecords.Text = dgvData.Rows.Count.ToString();
        }

        private void cbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbIsReleased.SelectedItem.ToString())
            {
                case "Yes":
                    _AllDetainedLicenses.DefaultView.RowFilter = "IsReleased = 1";
                    break;

                case "No":
                    _AllDetainedLicenses.DefaultView.RowFilter = "IsReleased = 0";
                    break;

                default:
                    _AllDetainedLicenses.DefaultView.RowFilter = "";
                    break;
            }

            lblRecords.Text = dgvData.Rows.Count.ToString();
        }

        private void txtFiltering_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.SelectedItem.ToString() == "ReleaseApplicationID" ||
          cbFilter.SelectedItem.ToString() == "DetainedID")
            {
                e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

         releaseDetainedLicenseToolStripMenuItem.Enabled = !(bool)dgvData.CurrentRow.Cells["IsReleased"].Value;
                  
        }
    }
}
