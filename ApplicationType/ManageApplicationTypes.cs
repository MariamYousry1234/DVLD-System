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
    public partial class ManageApplicationTypes : Form
    {
        public ManageApplicationTypes()
        {
            InitializeComponent();
        }

        DataTable _AllApplicationTypes;
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ManageApplicationTypes_Load(object sender, EventArgs e)
        {
            _AllApplicationTypes = clsApplicationTypes.GetApplicationTypesList();
            dgvData.DataSource = _AllApplicationTypes;
            lblRecords.Text = _AllApplicationTypes.Rows.Count.ToString();

            if(dgvData.Rows.Count>0)
            {
                dgvData.Columns["Title"].Width = 300;
            }
        }

        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateApplicationType frm =
                new UpdateApplicationType((int)dgvData.CurrentRow.Cells[0].Value);

            frm.ShowDialog();

            ManageApplicationTypes_Load(null,null);
        }
    }
}
