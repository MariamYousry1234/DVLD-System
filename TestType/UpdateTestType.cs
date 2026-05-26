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
    public partial class UpdateTestType : Form
    {
        clsTestTypes.enTestType _TestTypeID = clsTestTypes.enTestType.None;
        clsTestTypes _TestType;
        public UpdateTestType(clsTestTypes.enTestType TestTypeID)
        {
            InitializeComponent();

            _TestTypeID = TestTypeID;
        }
        void _LoadData()
        {
            _TestType = clsTestTypes.Find(_TestTypeID);

            if (_TestType == null)
            {
                this.Close();
                MessageBox.Show("This Type Not Found !", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            lblID.Text = ((int)_TestType.ID).ToString();
            txtTitle.Text = _TestType.Title;
            txtDescription.Text = _TestType.Description;
            txtFees.Text = _TestType.Fees.ToString();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UpdateTestType_Load(object sender, EventArgs e)
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

        private void txtDescription_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtDescription.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtDescription, "Description can not be blank");
            }
            else
                errorProvider1.SetError(txtDescription, null);
        }

        private void UpdateTestType_Validating(object sender, CancelEventArgs e)
        {

        }

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Fees can not be blank");
            }
            else if (!float.TryParse(txtFees.Text.Trim(), out float result))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Not valid fees amount");
            }
            else
                errorProvider1.SetError(txtFees, null);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Enter a valid data", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            //Update Data
            _TestType.Title = txtTitle.Text;
            _TestType.Description = txtDescription.Text;
            _TestType.Fees = float.Parse(txtFees.Text);

            if (_TestType.Save())
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
