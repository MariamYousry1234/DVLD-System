using Busniess_Logic;
using DVLD_Project.GlobalClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_Project
{
    public partial class AddEditPersonInfo : Form
    {

        public delegate void SendPersonID(int PersonID);

        public SendPersonID SendBackPersonID;

        private int _PersonID = -1;

        enum enMode { AddNew, Update }

        private enMode _Mode;
        private clsPerson _Person;

        public AddEditPersonInfo()
        {
            InitializeComponent();

            _Mode = enMode.AddNew;
        }
        public AddEditPersonInfo(int PersonID)
        {
            InitializeComponent();

            _PersonID = PersonID;
            _Mode = enMode.Update;

        }


        void FillCountries()
        {
            DataTable Countries = clsCountry.GetAllCountries();

            foreach (DataRow Row in Countries.Rows)
            {
                cbCountry.Items.Add(Row["CountryName"]);
            }

            cbCountry.SelectedItem = "Egypt";
        }

        void SetDefualtData()
        {
            dtpDateOfBirth.MaxDate = DateTime.Today.AddYears(-18);
            dtpDateOfBirth.MinDate = DateTime.Today.AddYears(-100);
            FillCountries();
            rbMale.Checked = true;
            pbImage.Image = Properties.Resources.Male_512;
            llRemove.Visible = false;
            _Person = new clsPerson();
        }


        void LoadDataToUpdate()
        {
            lblTitle.Text = "Update Person";
            _Person = clsPerson.Find(_PersonID);

            if (_Person == null)
            {
                MessageBox.Show("This person not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            lblPersonID.Text = _Person.PersonID.ToString();
            txtFirstName.Text = _Person.FirstName;
            txtSecondName.Text = _Person.SecondName;
            txtThirdName.Text = _Person.ThirdName;
            txtLastName.Text = _Person.LastName;
            txtNationalNo.Text = _Person.NationalNo;
            dtpDateOfBirth.Value = _Person.DateOfBirth;
            rbMale.Checked = (_Person.Gender == 1);
            rbFemale.Checked = (_Person.Gender == 2);
            txtPhone.Text = _Person.Phone;
            txtEmail.Text = _Person.Email;
            txtAddress.Text = _Person.Address;
            cbCountry.Text = _Person.PersonCountryInfo.CountryName;

            if (!string.IsNullOrEmpty(_Person.ImagePath))
            {
                pbImage.ImageLocation = _Person.ImagePath;
                llRemove.Visible = true;
                return;
            }

            pbImage.Image = rbMale.Checked ? Properties.Resources.Male_512 :
                 Properties.Resources.Female_512;

        }

        bool SaveImage()
        {
            if ((_Person.ImagePath != pbImage.ImageLocation) &&
                !(string.IsNullOrEmpty(_Person.ImagePath) &&
                string.IsNullOrEmpty(pbImage.ImageLocation)))
            {

                if (!string.IsNullOrEmpty(_Person.ImagePath))
                {
                    try
                    {
                        File.Delete(_Person.ImagePath);
                    }
                    catch (IOException e)
                    {

                    }
                }

                if (pbImage.ImageLocation != null)
                {
                    string OriginPath = pbImage.ImageLocation;

                    if (clsUtil.AddNewImageToFolder(ref OriginPath))
                    {
                        pbImage.ImageLocation = OriginPath;
                        return true;
                    }
                    else
                        return false;
                }
            }
            return true;

        }

        bool SaveData()
        {
            if (!SaveImage()) return false;

            if (pbImage.ImageLocation != null)
                _Person.ImagePath = pbImage.ImageLocation;
            else
                _Person.ImagePath = "";

            _Person.FirstName = txtFirstName.Text.Trim();
            _Person.SecondName = txtSecondName.Text.Trim();
            _Person.ThirdName = txtThirdName.Text.Trim();
            _Person.LastName = txtLastName.Text.Trim();
            _Person.NationalNo = txtNationalNo.Text.Trim();
            _Person.DateOfBirth = dtpDateOfBirth.Value;
            _Person.Gender = (short)(rbMale.Checked ? 1 : 2);
            _Person.Phone = txtPhone.Text;
            _Person.Email = string.IsNullOrEmpty(txtEmail.Text) ? "" : txtEmail.Text.Trim();
            _Person.Address = txtAddress.Text;
            _Person.NationalityID = clsCountry.Find(cbCountry.Text).CountryID;

            return _Person.Save();
        }



        void ShowDataSavedOrFailed(bool IsSaved)
        {
            //Show saved or failed
            if (IsSaved)
                MessageBox.Show("Data Saved Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            else
                MessageBox.Show("The save operation failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        void Save()
        {
            bool IsSaved = false;

            if ((IsSaved = SaveData()))
            {
                if (_Mode == enMode.AddNew)
                {
                    lblPersonID.Text = _Person.PersonID.ToString();

                    _Mode = enMode.Update;
                    lblTitle.Text = "Update Person";
                }

                SendBackPersonID?.Invoke(_Person.PersonID);
            }

            ShowDataSavedOrFailed(IsSaved);
        }




        private void AddEditPersonInfo_Load(object sender, EventArgs e)
        {
            SetDefualtData();

            if (_Mode == enMode.Update)
                LoadDataToUpdate();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Something went wrong!\nPlease enter all required data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Save();
        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.webp";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pbImage.ImageLocation = openFileDialog1.FileName;
                llRemove.Visible = true;
            }
        }

        private void llRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbImage.Image?.Dispose();
            pbImage.Image = null;
            pbImage.ImageLocation = null;
            pbImage.Image = rbMale.Checked ? Properties.Resources.Male_512 : Properties.Resources.Female_512;
            llRemove.Visible = false;
        }

        private void rb_CheckedChanged(object sender, EventArgs e)
        {
            if (pbImage.ImageLocation == null)
                pbImage.Image = rbFemale.Checked ? Properties.Resources.Female_512 : Properties.Resources.Male_512;
        }

        private void txt_Validating(object sender, CancelEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (string.IsNullOrEmpty(txt.Text))
            {
                errorProvider1.SetError(txt, "This Field is required");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txt, "");
            }
        }

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNationalNo.Text))
            {
                errorProvider1.SetError(txtNationalNo, "This Field is required");
                e.Cancel = true;
            }
            else if (clsPerson.IsNationalNoExists(txtNationalNo.Text)
                && _Person.NationalNo != txtNationalNo.Text)
            {
                errorProvider1.SetError(txtNationalNo, "National Number is used for another person");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtNationalNo, "");
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {

            if (!string.IsNullOrEmpty(txtEmail.Text) && !txtEmail.Text.Contains("@gmail.com"))
            {
                errorProvider1.SetError(txtEmail, "Invalid Email Address Format!");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtEmail, "");
            }
        }
    }
}
