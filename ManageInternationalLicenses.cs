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
    public partial class ManageInternationalLicenses : Form
    {
        public ManageInternationalLicenses()
        {
            InitializeComponent();
        }

        DataTable _AllInternationalLicenses;

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            NewInternationalLicenseApplication frm = new NewInternationalLicenseApplication();

            frm.ShowDialog();

            ManageInternationalLicenses_Load(null, null);
        }

        private void ManageInternationalLicenses_Load(object sender, EventArgs e)
        {
            _AllInternationalLicenses = clsInternationalLicense.GetAllInternationalLicenses();

            dgvData.DataSource = _AllInternationalLicenses;
            lblRecords.Text = dgvData.Rows.Count.ToString();

            if(dgvData.Rows.Count >0)
            {
                dgvData.Columns["InternationalLicenseID"].Width = 150;
                dgvData.Columns["IssueDate"].Width = 150;
                dgvData.Columns["ExpiryDate"].Width = 150;
            }
            cbFilter.SelectedIndex = 0;
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if( cbFilter.Text=="IsActive")
            {
                cbIsActive.Visible = true;
                txtFiltering.Visible = false;
                cbIsActive.SelectedIndex = 0;
                cbIsActive.Focus();
            }
            else
            {
                txtFiltering.Visible = (cbFilter.Text != "None");
                cbIsActive.Visible = false;
                txtFiltering.Clear();
                txtFiltering.Focus();
            }

            _AllInternationalLicenses.DefaultView.RowFilter = "";
            lblRecords.Text = dgvData.Rows.Count.ToString();
        }

        private void txtFiltering_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(txtFiltering.Visible)
            {
                e.Handled =(!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));
            }
        }

        private void txtFiltering_TextChanged(object sender, EventArgs e)
        {

            if(string.IsNullOrEmpty(txtFiltering.Text))
                _AllInternationalLicenses.DefaultView.RowFilter ="";
            else
                _AllInternationalLicenses.DefaultView.RowFilter = $"{cbFilter.Text} ={txtFiltering.Text}";
            lblRecords.Text = dgvData.Rows.Count.ToString();
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbIsActive.Text)
            {
                case "Yes":
                    _AllInternationalLicenses.DefaultView.RowFilter = $"{cbFilter.Text} =1";
                    break;

                case "No":
                    _AllInternationalLicenses.DefaultView.RowFilter = $"{cbFilter.Text} =0";
                    break;

                default:
                    _AllInternationalLicenses.DefaultView.RowFilter = "";
                    break;
            }

           
            lblRecords.Text = dgvData.Rows.Count.ToString();
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = clsDriver.Find((int)dgvData.CurrentRow.Cells[2].Value).PersonID;

            PersonDetails frm = new PersonDetails(PersonID);

            frm.ShowDialog();

          
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InternationalDriverInfo frm = new InternationalDriverInfo((int)dgvData.CurrentRow.Cells[0].Value);

            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = clsDriver.Find((int)dgvData.CurrentRow.Cells[2].Value).PersonID;

            LicenseHistory frm = new LicenseHistory(PersonID);

            frm.ShowDialog();
        }
    }
}
