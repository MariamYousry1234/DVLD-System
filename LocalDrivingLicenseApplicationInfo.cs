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
    public partial class LocalDrivingLicenseApplicationInfo : Form
    {
        int _LDLAppID = -1;
        public LocalDrivingLicenseApplicationInfo(int LDLAppID)
        {
            InitializeComponent();

            _LDLAppID = LDLAppID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LocalDrivingLicenseApplicationInfo_Load(object sender, EventArgs e)
        {
            ucDrivingLicenseApplicationInfo1.LoadLocalApplicationInfo(_LDLAppID);
        }
    }
}
