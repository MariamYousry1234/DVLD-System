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
    public partial class ucDriverLicenses : UserControl
    {
        int _DriverID = -1;
        clsDriver _Driver ;
        DataTable _AllLocalLicensesHistory=new DataTable();
        DataTable _AllInternationalLicenses=new DataTable();
        public ucDriverLicenses()
        {
            InitializeComponent();
        }

      
        public int DriverID
        {
            get
            {
                return _DriverID;
            }
        }


        void LoadLocalLicenses()
        {
            _AllLocalLicensesHistory = _Driver.GetLocalLicensesList();
            dgvLocal.DataSource = _AllLocalLicensesHistory;
            lblLocalRecords.Text = dgvLocal.Rows.Count.ToString();

            if (dgvLocal.Rows.Count > 0)
            {
                dgvLocal.Columns["Class Name"].Width = 200;
                dgvLocal.Columns["Issue Date"].Width = 150;
                dgvLocal.Columns["Expiration Date"].Width = 150;
            }
        }

        void LoadInternationalLicenses()
        {
            _AllInternationalLicenses = _Driver.GetInternationalLicense();
            dgvInternational.DataSource = _AllInternationalLicenses;
            lblInternationalRecords.Text = dgvInternational.Rows.Count.ToString();

            if (dgvLocal.Rows.Count > 0)
            {
                dgvInternational.Columns["Issue Date"].Width = 150;
                dgvInternational.Columns["Expiration Date"].Width = 150;
            }
        }

        public void LoadLicenses(int DriverID)
        {
            Clear();

            _Driver = clsDriver.Find(DriverID);

            if (_Driver == null)
            {
                MessageBox.Show($"There is no driver  with ID ={DriverID}", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                _DriverID = -1;
                return;
            }
            _DriverID = _Driver.DriverID;
            LoadLocalLicenses();
            LoadInternationalLicenses();

        }
        public void LoadLicensesByPersonID(int PersonID)
        {
            Clear();

            _Driver = clsDriver.FindByPersonID(PersonID);

            if (_Driver == null)
            {
                MessageBox.Show($"There is no driver linked with PersonID ={PersonID}", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                _DriverID = -1;

                return;
            }
            _DriverID = _Driver.DriverID;
            LoadLocalLicenses();
            LoadInternationalLicenses();
        }

        public void Clear()
        {
            _AllLocalLicensesHistory.Clear();
            _AllInternationalLicenses.Clear();
        }

        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LicenseInfo frm = new LicenseInfo((int)dgvLocal.CurrentRow.Cells[0].Value);

            frm.ShowDialog();
        }
    }
}
