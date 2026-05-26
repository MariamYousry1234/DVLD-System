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
    public partial class AddEditUserInfo : Form
    {
        private enum enMode { AddNew=0,Update=1 }
        enMode _Mode;

        private int _UserID = -1;
        private clsUser _User;

        public AddEditUserInfo()
        {
            InitializeComponent();

            _Mode = enMode.AddNew;
        }

        public AddEditUserInfo(int UserID)
        {
            InitializeComponent();

            _UserID = UserID;
            _Mode = enMode.Update;
        }


        void _SetIntialData()
        {

            if (_Mode == enMode.AddNew)
            {

                this.Text = "Add New User";
                lblTitle.Text = "Add New User";

                ucPersonInformationWithFilteration1.FilterFocus();
                btnSave.Enabled = false;
                gbLogin.Enabled = false;
                _User = new clsUser();
            }
            else
            {
                this.Text = "Update User";
                lblTitle.Text = "Update User";

                btnSave.Enabled = true;
            }
        }

        void _LoadDataToUpdate()
        {
            _User = clsUser.Find(_UserID);

            if(_User == null)
            {
                this.Close();
                MessageBox.Show("User Not Found !", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            ucPersonInformationWithFilteration1.LoadData(_User.PersonID);
            ucPersonInformationWithFilteration1.EnableFilter = false;

            lblUserID.Text = _User.UserID.ToString(); 
            txtUsername.Text = _User.Username;
            txtPassword.Text = _User.Password;
            txtConfrimPassword.Text = _User.Password;
            chkIsActive.Checked = _User.IsActive;
        }


        void _SaveUserData()
        {
            _User.Username = txtUsername.Text.Trim();
            _User.Password = txtPassword.Text.Trim();
            _User.IsActive = chkIsActive.Checked;

            if(_User.Save())
            {
                if(_Mode == enMode.AddNew)
                {
                    lblUserID.Text = _User.UserID.ToString();
                    this.Text = "Update User";
                    lblTitle.Text = "Update User";
                    _Mode = enMode.Update;
                }

                if(_User.UserID == clsGlobal.CurrentUser.UserID)
                {
                    clsGlobal.CurrentUser = _User;
                }

                MessageBox.Show("Data saved succesfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Data Not saved ", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        private void AddEditUserInfo_Load(object sender, EventArgs e)
        {
            _SetIntialData();

            if (_Mode == enMode.Update)
                _LoadDataToUpdate();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

      
      
        private void txtUsername_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUsername, "Username cannot be blank");
             
            }
            if (_Mode == enMode.AddNew && clsUser.IsPersonIDExists(_User.PersonID))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUsername, "username is used by another user");
            }
            if (_Mode == enMode.Update && txtUsername.Text != _User.Username 
                && clsUser.IsPersonIDExists(_User.PersonID) )
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUsername, "username is used by another user");
            }
            else
                errorProvider1.SetError(txtUsername, "");
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPassword, "Password cannot be blank");
   
            }
            else
                errorProvider1.SetError(txtPassword, "");
        }

        private void txtConfrimPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtConfrimPassword.Text != txtPassword.Text)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfrimPassword, "Password Confirmation does not match password");
            }

            else
                errorProvider1.SetError(txtConfrimPassword, "");

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_User.PersonID == -1)
            {
                btnSave.Enabled = false;
                MessageBox.Show("Please select a person !", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (_Mode == enMode.AddNew && clsUser.IsPersonIDExists(_User.PersonID))
            {
                btnSave.Enabled = false;
                MessageBox.Show("Selected Person already has a user,choose another one",
                    "Select another person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

           
            tabControl1.SelectedIndex = 1;
            btnSave.Enabled = true;
            gbLogin.Enabled = true;
        } 

        private void txtConfrimPassword_TextChanged(object sender, EventArgs e)
        {
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucPersonInformationWithFilteration1_OnPersonSelected(int obj)
        {
            _User.PersonID = obj;
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(txtPassword.Text);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid ,put the mouse over the red icon(s) to see the error");
                return;
            }

            _SaveUserData();
        }
    }
}
