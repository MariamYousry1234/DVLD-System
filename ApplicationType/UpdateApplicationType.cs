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
    public partial class UpdateApplicationType : Form
    {
        int _ApplicationTypeID = -1;
        clsApplicationTypes _ApplicationType;
        public UpdateApplicationType(int ApplicationTypeID)
        {
            InitializeComponent();

            _ApplicationTypeID = ApplicationTypeID;
        }


        void _LoadData()
        {
            _ApplicationType = clsApplicationTypes.Find(_ApplicationTypeID);

            if(_ApplicationType ==null)
            {
                this.Close();
                MessageBox.Show("This Type Not Found !", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            lblID.Text = _ApplicationType.ApplicationTypeID.ToString();
            txtTitle.Text = _ApplicationType.Title;
            txtFees.Text = _ApplicationType.Fees.ToString();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UpdateApplicationType_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTitle, "Title can not be blank");
            }
            else
                errorProvider1.SetError(txtTitle, null);
        }

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Fees can not be blank");
            }
            else if(!float.TryParse(txtFees.Text,out float result))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Please enter a valid fees amount");
            }
            else
                errorProvider1.SetError(txtFees, null);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Enter a valid data", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            //Update Data
            _ApplicationType.Title = txtTitle.Text;
            _ApplicationType.Fees =float.Parse(txtFees.Text);

            if(_ApplicationType.Save())
            {
                MessageBox.Show("Data Saved Successfully", "Saved", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Saved failed", "Error", MessageBoxButtons.OK,
                  MessageBoxIcon.Error);
            }

        }
    }
}
