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

        int _LicenseID = -1;
        public ucDriverLicenseInfoWithFilter()
        {
            InitializeComponent();
        }

        public event Action <int> OnLicenseSelected;

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

        public clsLicense License
        {
            get
            {
                return ucShowDriverLicenseInfo1.License;
            }
        }

        public bool FilterEnalbed
        {
            set
            {
                gbFilter.Enabled = value;
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
            if (this.ValidateChildren())
            {
                _LicenseID =
                    clsInternationalLicense.IsLocalLicenseAllowedApplyToInternational(Convert.ToInt32(txtSearch.Text.Trim()))?
                    Convert.ToInt32(txtSearch.Text.Trim()) : -1;
            }
     
            ucShowDriverLicenseInfo1.LoadDriverLicenseInfo(_LicenseID);

            if (OnLicenseSelected != null)
                OnLicenseSelected(ucShowDriverLicenseInfo1.LicenseID);
        }

        public void txtFocus()
        {
            txtSearch.Focus();
        }

        private void txtSearch_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                errorProvider1.SetError(txtSearch, "This field is required !");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtSearch, null);
                e.Cancel = false;

            }
        }
    }
}
