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
using System.IO;
using System.Drawing.Text;

namespace DVLD_Project
{
    public partial class ucPersonInformation : UserControl
    {
        int _PersonID = -1;
  
        clsPerson _Person;
        public ucPersonInformation()
        {
            InitializeComponent();
  
        }

        public int PersonID
        {
            get { return _PersonID; }   
        }

        void _SetDefaultData()
        {
            llEditPersonInfo.Enabled = false;
            lblPersonID.Text = "???";
            lblName.Text = "???";
            lblNationalNo.Text = "???";
            lblGender.Text = "???";
            lblEmail.Text = "???";
            lblAddress.Text = "???";
            lblDateOfBirth.Text = "???";
            lblPhone.Text = "???";
            lblCountry.Text = "???";

            pbImage.Image = Properties.Resources.Male_512;
        }
        public void LoadData(string NationalNo)
        {
            _Person = clsPerson.Find(NationalNo);

            if (_Person == null)
            {
                _SetDefaultData();
                _PersonID = -1;
                MessageBox.Show($"No Person with National No. = {NationalNo}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _PersonID = _Person.PersonID;
            ShowPersonDetails();
        }

        public void LoadData(int PersonID)
        {
            _Person = clsPerson.Find(PersonID);

            if (_Person == null)
            {
                _SetDefaultData();
                _PersonID = -1;
                MessageBox.Show($"No Person with PersonID = {PersonID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ;
            }
            _PersonID = _Person.PersonID;
            ShowPersonDetails();
        }
        void ShowPersonDetails()
        {
            llEditPersonInfo.Enabled = true;
            lblPersonID.Text = _Person.PersonID.ToString();
            lblName.Text = _Person.GetFullName;
            lblNationalNo.Text = _Person.NationalNo;
            lblGender.Text = _Person.Gender == 1 ? "Male" : "Female";
            lblEmail.Text = _Person.Email != "" ? _Person.Email : "Not Found";
            lblAddress.Text = _Person.Address;
            lblDateOfBirth.Text = _Person.DateOfBirth.ToShortDateString();
            lblPhone.Text = _Person.Phone;
            lblCountry.Text = _Person.PersonCountryInfo.CountryName;

            if (_Person.ImagePath != "" && File.Exists(_Person.ImagePath))
            {
                pbImage.ImageLocation = _Person.ImagePath;
            }
            else
            {
                pbImage.Image = _Person.Gender == 1 ? Properties.Resources.Male_512 :
                    Properties.Resources.Female_512;
            }
        }

        private void ucPersonInformation_Load(object sender, EventArgs e)
        {

        }

        private void llEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AddEditPersonInfo frm = new AddEditPersonInfo(_Person.PersonID);
            frm.ShowDialog();


            LoadData(_Person.PersonID);
        }
    }
}
