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
    public partial class ChangePassword : Form
    {
        int _UserID = -1;
        clsUser _User;
        public ChangePassword(int UserID)
        {
            InitializeComponent();

            _UserID = UserID;
          
        }

        void _Reset()
        {
            txtCurrentPassword.Clear();
            txtNewPassword.Clear();
            txtConfrimPassword.Clear();
            txtCurrentPassword.Focus();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ChangePassword_Load(object sender, EventArgs e)
        {
            _Reset();
            _User = clsUser.Find(_UserID);
            if (_User == null)
            {
                this.Close();
                MessageBox.Show("This user not found !", "", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            ucShowUserInfo1.LoadUserData(_UserID);
        }

        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtCurrentPassword.Text))
            {
                errorProvider1.SetError(txtCurrentPassword, "Current Password can not be blank!");
                e.Cancel = true;
            }
            else if (txtCurrentPassword.Text != _User.Password)
            {
                errorProvider1.SetError(txtCurrentPassword, "Current Password is wrong!");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtCurrentPassword, null);
            }
        }

        private void txtNewPassword_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrEmpty(txtNewPassword.Text))
            {
                errorProvider1.SetError(txtNewPassword, "New Password can not be blank!");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtNewPassword, null);
            }
        }

        private void txtConfrimPassword_Validating(object sender, CancelEventArgs e)
        {
            if(txtConfrimPassword.Text != txtNewPassword.Text)
            {
                errorProvider1.SetError(txtConfrimPassword, "Password Confirmation does not match new password !");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtConfrimPassword, null);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid !,put the mouse over the red icon (s) to see the error",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_User.ChangePassword(txtNewPassword.Text))
            {
                MessageBox.Show("Password Updated Successfully", "Updated", MessageBoxButtons.OK,
                   MessageBoxIcon.Information);
                _Reset();
            }
               
            else
                MessageBox.Show("Updated Failed", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
