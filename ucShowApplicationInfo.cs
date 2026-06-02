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
    public partial class ucShowApplicationInfo : UserControl
    {

        int _ApplicationID= -1;
        clsApplication ApplicationInfo;
        public ucShowApplicationInfo()
        {
            InitializeComponent();
        }

        public void LoadApplicationInfo(int ApplicationID)
        {
            _ApplicationID = ApplicationID;
            ApplicationInfo = clsApplication.Find(ApplicationID);

            if(ApplicationInfo==null)
            {
                MessageBox.Show("This Application Not Found !", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            lblApplicationID.Text = ApplicationInfo.ApplicationID.ToString();
            lblDate.Text = ApplicationInfo.ApplicationDate.ToString("dd/MM/yyyy");
            lblStatusDate.Text = ApplicationInfo.LastStatusDate.ToString("dd/MM/yyyy");
            lblCreatedBy.Text = ApplicationInfo.CreatedUserInfo.Username;

            lblStatus.Text = ApplicationInfo.StatusText;
            lblFees.Text = ApplicationInfo.ApplicationFees.ToString();

            lblType.Text = ApplicationInfo.ApplicationType.Title;
            lblApplicant.Text = ApplicationInfo.PersonInfo.GetFullName;

        }
        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void llViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PersonDetails frm = new PersonDetails(ApplicationInfo.PersonID);
            frm.ShowDialog();

            LoadApplicationInfo(_ApplicationID);
        }
    }
}
