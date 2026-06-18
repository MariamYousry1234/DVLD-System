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
    public partial class NewInternationalLicenseApplication : Form
    {
        public NewInternationalLicenseApplication()
        {
            InitializeComponent();
        }

        private void NewInternationalLicenseApplication_Activated(object sender, EventArgs e)
        {
            ucDriverLicenseInfoWithFilter1.txtFocus();
        }
    }
}
