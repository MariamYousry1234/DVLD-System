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
    public partial class LicenseHistory : Form
    {
        int _PersonID = -1;

        public LicenseHistory()
        {
            InitializeComponent();          
        }

        public LicenseHistory(int PersonID)
        {
            InitializeComponent();
  
            _PersonID = PersonID;
        }

        private void LicenseHistory_Load(object sender, EventArgs e)
        {
            if (_PersonID != -1)
            {
                ucPersonInformationWithFilteration1.LoadData(_PersonID);
                ucPersonInformationWithFilteration1.EnableFilter = false;
                ucDriverLicenses1.LoadLicensesByPersonID(_PersonID);

                if (ucDriverLicenses1.DriverID == -1) this.Close();
                return;
            }

            ucPersonInformationWithFilteration1.EnableFilter = true;
            ucPersonInformationWithFilteration1.FilterFocus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucPersonInformationWithFilteration1_OnPersonSelected(int obj)
        {
            ucDriverLicenses1.LoadLicensesByPersonID(obj);
        }


    }
}
