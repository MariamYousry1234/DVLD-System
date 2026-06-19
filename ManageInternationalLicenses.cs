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
        }

        private void ManageInternationalLicenses_Load(object sender, EventArgs e)
        {
            _AllInternationalLicenses = clsInternationalLicense.GetAllInternationalLicenses();

            dgvData.DataSource = _AllInternationalLicenses;
            lblRecords.Text = dgvData.Rows.Count.ToString();

            if(dgvData.Rows.Count >0)
            {
                dgvData.Columns["Issue Date"].Width = 150;
                dgvData.Columns["Expiration Date"].Width = 150;
            }
            cbFilter.SelectedIndex = 0;
        }
    }
}
