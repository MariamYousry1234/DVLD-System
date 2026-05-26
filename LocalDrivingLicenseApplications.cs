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
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            NewUpdateLocalDrivingLicenseApplication frm = new NewUpdateLocalDrivingLicenseApplication();

            frm.ShowDialog();

            LocalDrivingLicenseApplications_Load(null, null);
        }
    }
}
