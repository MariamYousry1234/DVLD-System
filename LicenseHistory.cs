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
    public partial class LicenseHistory : Form
    {
        int _PersonID = -1;

        DataTable _AllLocalLicensesHistory;

        public LicenseHistory(string NationalNo)
        {
            InitializeComponent();
  
            _PersonID = clsPerson.Find(NationalNo).PersonID;
        }

        private void LicenseHistory_Load(object sender, EventArgs e)
        {
            ucPersonInformationWithFilteration1.LoadData(_PersonID);
            ucPersonInformationWithFilteration1.EnableFilter = false;

            _AllLocalLicensesHistory = clsLicense.GetLocalLicensesList(_PersonID);
            dgvLocal.DataSource = _AllLocalLicensesHistory;
            lblLocalRecords.Text = dgvLocal.Rows.Count.ToString();

            if(dgvLocal.Rows.Count>0)
            {
                dgvLocal.Columns["Class Name"].Width = 200;
                dgvLocal.Columns["Issue Date"].Width = 150;
                dgvLocal.Columns["Expiration Date"].Width = 150;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
