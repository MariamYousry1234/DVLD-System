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
    public partial class ManageDrivers : Form
    {
        public ManageDrivers()
        {
            InitializeComponent();
        }

        DataTable _AllDrivers ;
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ManageDrivers_Load(object sender, EventArgs e)
        {
            _AllDrivers = clsDriver.GetDriversList();
            dgvData.DataSource = _AllDrivers;
            lblRecords.Text = dgvData.Rows.Count.ToString();

            if (dgvData.Rows.Count > 0)
            {
                dgvData.Columns["Date"].Width = 150;
                dgvData.Columns["Full Name"].Width = 200;
            }

            cbFilter.SelectedIndex = 0;
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if( txtFiltering.Visible = (cbFilter.Text != "None"))
            {
                txtFiltering.Focus();
                txtFiltering.Clear();  
            }

        }

        private void txtFiltering_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.Text == "DriverID" || cbFilter.Text == "PersonID")
                e.Handled = (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar));
        }

        private void txtFiltering_TextChanged(object sender, EventArgs e)
        {
            if (cbFilter.Text == "None")
                _AllDrivers.DefaultView.RowFilter = "";

            if(cbFilter.Text=="NationalNo" || cbFilter.Text=="Full Name")
                _AllDrivers.DefaultView.RowFilter = $"{cbFilter.Text}like '{txtFiltering.Text}%'";
            else
                _AllDrivers.DefaultView.RowFilter = $"{cbFilter.Text}={txtFiltering.Text}";

            lblRecords.Text = dgvData.Rows.Count.ToString();

        }

    }
}
