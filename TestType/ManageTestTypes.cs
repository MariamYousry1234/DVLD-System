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
    public partial class ManageTestTypes : Form
    {
        public ManageTestTypes()
        {
            InitializeComponent();
        }

        DataTable _AllTestTypes;

        private void ManageTestTypes_Load(object sender, EventArgs e)
        {
            _AllTestTypes = clsTestTypes.GetTestTypesList();
            dgvData.DataSource = _AllTestTypes;
            lblRecords.Text = _AllTestTypes.Rows.Count.ToString();

            if (dgvData.Rows.Count > 0)
            {
                dgvData.Columns["Title"].Width = 150;
                dgvData.Columns["Description"].Width = 450;
                dgvData.Columns["Fees"].Width =90;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editTestTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateTestType frm =
                new UpdateTestType((clsTestTypes.enTestType)dgvData.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
         
            ManageTestTypes_Load(null, null);
        }
    }
}
