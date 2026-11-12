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
    public partial class ManageUsers : Form
    {
        public ManageUsers()
        {
            InitializeComponent();
        }


        static DataTable _AllUsers ;

     
        void _DeleteUser(int UserID)
        {
            if (MessageBox.Show($"Are you sure you want to delete user [{UserID}]?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
                == DialogResult.OK)
            {
                if (clsUser.DeleteUser(UserID))
                    MessageBox.Show("User has been deleted successfully", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("User deletion failed", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }


        private void ManageUsers_Load(object sender, EventArgs e)
        {
            _AllUsers = clsUser.GetUsersList();

            dgvData.DataSource = _AllUsers;

            lblRecords.Text = _AllUsers.Rows.Count.ToString();

            if (dgvData.Rows.Count > 0)
            {
                dgvData.Columns["FullName"].Width = 250;
                dgvData.Columns["Username"].Width = 150;
            }

            cbFilter.SelectedIndex = 0;
            cbIsActive.SelectedIndex = 0;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            //_AllUsersView.DefaultView.RowFilter = "";
            //lblRecords.Text = _AllUsersView.Rows.Count.ToString();

            if (cbFilter.SelectedItem.ToString() == "IsActive")
            {
                txtFiltering.Visible = false;
                cbIsActive.Visible = true;
                cbIsActive.SelectedIndex = 0;
                return;
            }

            cbIsActive.Visible = false;
            txtFiltering.Visible = (cbFilter.Text != "None");
            txtFiltering.Clear();
            txtFiltering.Focus();
        }

        private void txtFiltering_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.SelectedItem.ToString() == "PersonID" ||
              cbFilter.SelectedItem.ToString() == "UserID")
            {
                e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));
            }
        }

        private void txtFiltering_TextChanged(object sender, EventArgs e)
        { 
            if (string.IsNullOrEmpty(txtFiltering.Text) || cbFilter.Text == "None")
            {
                _AllUsers.DefaultView.RowFilter = "";
            }
            else if (cbFilter.SelectedItem.ToString() == "PersonID" ||
                cbFilter.SelectedItem.ToString() == "UserID")
            {
                _AllUsers.DefaultView.RowFilter = $"{cbFilter.Text} ={txtFiltering.Text.Trim()}";
            }
            else
                _AllUsers.DefaultView.RowFilter = $"{cbFilter.Text} like '{txtFiltering.Text.Trim()}%'";

            lblRecords.Text = dgvData.Rows.Count.ToString();
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbIsActive.SelectedItem.ToString())
            {
                case "Yes":
                    _AllUsers.DefaultView.RowFilter = "IsActive = 1";
                    break;

                case "No":
                    _AllUsers.DefaultView.RowFilter = "IsActive = 0";
                    break;

                default:
                    _AllUsers.DefaultView.RowFilter = "";
                    break;
            }

            lblRecords.Text = dgvData.Rows.Count.ToString();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _DeleteUser((int)dgvData.CurrentRow.Cells[0].Value);
            ManageUsers_Load(null, null);
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new UserInfo((int)dgvData.CurrentRow.Cells[0].Value);

            frm.ShowDialog();

            ManageUsers_Load(null, null);
        }

        private void dgvData_DoubleClick(object sender, EventArgs e)
        {
            Form frm = new UserInfo((int)dgvData.CurrentRow.Cells[0].Value);

            frm.ShowDialog();

            ManageUsers_Load(null, null);
        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new AddEditUserInfo();

            frm.ShowDialog();
            ManageUsers_Load(null, null);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new AddEditUserInfo((int)dgvData.CurrentRow.Cells[0].Value);

            frm.ShowDialog();
            ManageUsers_Load(null, null);
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            Form frm = new AddEditUserInfo();

            frm.ShowDialog();
            ManageUsers_Load(null, null);
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new ChangePassword((int)dgvData.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }
    }
}
