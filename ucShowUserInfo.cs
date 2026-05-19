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
    public partial class ucShowUserInfo : UserControl
    {
        clsUser User;
        public ucShowUserInfo()
        {
            InitializeComponent();
        }

        public void LoadUserData(int UserID)
        {
            User = clsUser.Find(UserID);

            if(User==null)
            {
                MessageBox.Show("This User Not Found !");
                return;
            }
            ucPersonInformation1.LoadData(User.PersonID);

            lblUserID.Text = User.UserID.ToString();
            lblUsername.Text = User.Username;
            lblIsActive.Text = User.IsActive ? "Yes" : "No";
        }
        private void ucShowUserInfo_Load(object sender, EventArgs e)
        {

        }
    }
}
