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
    public partial class DetainLicense : Form
    {
        int _LicenseID = -1;
        public DetainLicense()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtFineFees_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtFineFees.Text))
            {
                errorProvider1.SetError(txtFineFees, "Fine Fees is required!");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtFineFees, null);
                e.Cancel = false;
            }


        }

        private void txtFineFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));
        }

        private void ucDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            if(obj==-1)
            {
                btnDetain.Enabled = false;
                llShowLicenseHistory.Enabled = false;
                llShowNewLicenseInfo.Enabled = false;
                return;
            }

            llShowLicenseHistory.Enabled = true;
            lblLicenseID.Text = obj.ToString();
            _LicenseID = obj;
            llShowNewLicenseInfo.Enabled = true;

            if (ucDriverLicenseInfoWithFilter1.License.IsDetained)
            {
                MessageBox.Show("Selected License already detained,choose another one", "Detained",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnDetain.Enabled = false;

                return;
            }

            btnDetain.Enabled = true;
        }

        private void DetainLicense_Load(object sender, EventArgs e)
        {
            lblDetainDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            lblCreatedBy.Text = clsGlobal.CurrentUser.Username;

            txtFineFees.Text = "10";
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to detain this license?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;


            int DetainedID =
                ucDriverLicenseInfoWithFilter1.License.Detain(Convert.ToSingle(txtFineFees.Text), clsGlobal.CurrentUser.UserID);

            if(DetainedID ==-1)
            {
                MessageBox.Show("Detained Data Falied to save", "Falied", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            MessageBox.Show($"License Detained Successfully with id ={DetainedID}", "", MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            ucDriverLicenseInfoWithFilter1.FilterEnalbed = false;
            btnDetain.Enabled = false;
       
            lblDetainID.Text = DetainedID.ToString();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LicenseHistory frm = new LicenseHistory(ucDriverLicenseInfoWithFilter1.License.DriverInfo.PersonID);

            frm.ShowDialog();
        }

        private void llShowNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LicenseInfo frm = new LicenseInfo(_LicenseID);

            frm.ShowDialog();
        }

        private void DetainLicense_Activated(object sender, EventArgs e)
        {
            ucDriverLicenseInfoWithFilter1.txtFocus();
        }
    }
}
