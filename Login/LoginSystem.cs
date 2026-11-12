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
using System.IO;

namespace DVLD_Project
{
    public partial class LoginSystem : Form
    {
        private const string FileName = 
            "C:\\Users\\Admin\\Desktop\\LoginInfo.txt";

        public LoginSystem()
        {
            InitializeComponent();
        }


        void _SaveUsernameandPasswordToFile(string Username,string Password)
        {
            if (chkRememberMe.Checked)
                File.WriteAllText(FileName, $"{Username}\n{Password}");
            else
                {
                if(File.Exists(FileName))
                    File.Delete(FileName);
               }       
        }


        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(!clsUser.IsUsernameAndPasswordValid(txtUsername.Text,
                txtPassword.Text))
            {
                MessageBox.Show("Invalid Username/Password", "Wrong Credinatials", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }


            clsUser User = clsUser.Find(txtUsername.Text);
            if(!User.IsActive)
            {
                MessageBox.Show("Your account is deactivated please contact your Admin",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            clsGlobal.CurrentUser = User;

            //save user to file
            _SaveUsernameandPasswordToFile(txtUsername.Text, txtPassword.Text);

            this.Hide();
            MainMenu frm = new MainMenu(this);
            frm.Show();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoginSystem_Load(object sender, EventArgs e)
        {
            if (chkRememberMe.Checked = File.Exists(FileName))
            {
                string [] Text = File.ReadAllLines(FileName);

                if (Text.Length >= 2)
                {
                    txtUsername.Text = Text[0];
                    txtPassword.Text = Text[1];
                }

                txtUsername.SelectionStart = txtUsername.TextLength;
               
            }
        }

        private void LoginSystem_Activated(object sender, EventArgs e)
        {
            if (chkRememberMe.Checked)
                btnLogin.Focus();
            else
            {
                txtUsername.Clear();
                txtPassword.Clear();
                txtUsername.Focus();
            }
                  
        }
    }
}
