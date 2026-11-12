using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class UserInfo : Form
    {
        int _UserID = -1;
        public UserInfo(int UserID)
        {
            InitializeComponent();

            _UserID = UserID;
        }

        private void ucShowUserInfo1_Load(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UserInfo_Load(object sender, EventArgs e)
        {
            ucShowUserInfo1.LoadUserData(_UserID);
        }
    }
}
