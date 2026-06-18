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
    public partial class ucDriverLicenseInfoWithFilter : UserControl
    {
      
        public ucDriverLicenseInfoWithFilter()
        {
            InitializeComponent();
        }

        public event Action <int> OnDriverSelected;

        public int DriverID
        {
            get
            {
                return ucShowDriverLicenseInfo1.DriverID;
            }
        }

        public int LicenseID
        {
            get
            {
                return ucShowDriverLicenseInfo1.LicenseID;
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
                btnSearching.PerformClick();

            e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));
        }

        private void btnSearching_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtSearch.Text))
            {
                MessageBox.Show("Enter DriverID", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

          
            int LicenseID = 
                clsLicense.GetActiveLicenseClassForDriver(Convert.ToInt32(txtSearch.Text.Trim()), 3);

            ucShowDriverLicenseInfo1.LoadDriverLicenseInfo(LicenseID);

            if (OnDriverSelected != null)
                OnDriverSelected(ucShowDriverLicenseInfo1.DriverID);
        }

        public void txtFocus()
        {
            txtSearch.Focus();
        }
     
    }
}
