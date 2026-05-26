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
    public partial class NewUpdateLocalDrivingLicenseApplication : Form
    {

        float LocalApplicationFees = clsApplicationTypes.Find("New Local Driving License Service").Fees;
        enum enMode { AddNew=0,Update=1}
        enMode _Mode;
        clsLocalDrivingApplicationLicense LocalDrivingLicenseApplication;
        int _LocalDrivingApplicationID = -1;
        public NewUpdateLocalDrivingLicenseApplication()
        {
            InitializeComponent();

            _Mode = enMode.AddNew;
        }


        public NewUpdateLocalDrivingLicenseApplication(int LocalDrivingApplicationID)
        {
            InitializeComponent();

            _LocalDrivingApplicationID = LocalDrivingApplicationID;
            _Mode = enMode.Update;
        }


        private void _FillLicenseClasses()
        {
            DataTable AllClasses = clsLicenseClass.GetLicenseClassesList();

            foreach (DataRow Row in AllClasses.Rows)
            {
                cbApplicationClass.Items.Add(Row["ClassName"]);
            }

            cbApplicationClass.SelectedIndex = 0;
        }


        private void _LoadIntialInfo()
        {
            if(_Mode==enMode.AddNew)
            {
                LocalDrivingLicenseApplication = new clsLocalDrivingApplicationLicense();

                this.Text = "New Local Driving License Application";
                lblTitle.Text = "New Local Driving License Application";

                lblApplicationID.Text = "[???]";
                lblApplicationDate.Text = DateTime.Now.ToShortDateString();
                lblApplicationFees.Text =
                          (LocalApplicationFees).ToString();

                lblCreatedBy.Text = clsGlobal.CurrentUser.Username;

            }
            else
            {
                this.Text = "Update Local Driving License Application";
                lblTitle.Text = "Update Local Driving License Application";
                btnSave.Enabled = true;
                gbApplicationInfo.Enabled = true;
            }
                
            _FillLicenseClasses();  
        }

        private void _LoadData()
        {
            LocalDrivingLicenseApplication =
                clsLocalDrivingApplicationLicense.FindByID(_LocalDrivingApplicationID);

            if(LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("Not Found !", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            ucPersonInformationWithFilteration1.LoadData(LocalDrivingLicenseApplication.PersonID);

            ucPersonInformationWithFilteration1.EnableFilter = false;

            lblApplicationID.Text = LocalDrivingLicenseApplication.ApplicationID.ToString();

            lblApplicationDate.Text = LocalDrivingLicenseApplication.ApplicationDate.ToString();

            cbApplicationClass.SelectedIndex = (LocalDrivingLicenseApplication.LicenseClassID - 1);

            lblApplicationFees.Text = LocalDrivingLicenseApplication.ApplicationFees.ToString();

            lblCreatedBy.Text =clsUser.Find( LocalDrivingLicenseApplication.CreatedByUserID).Username;


        }

        private void SaveData()
        {
            LocalDrivingLicenseApplication.ApplicationDate =
                Convert.ToDateTime(lblApplicationDate.Text);

            LocalDrivingLicenseApplication.PersonID = ucPersonInformationWithFilteration1.PersonID;

            LocalDrivingLicenseApplication.ApplicationTypeID = clsApplication.enApplicationType.NewLocal;

            LocalDrivingLicenseApplication.ApplicationStaus = clsApplication.enApplicationStatus.New;

            LocalDrivingLicenseApplication.ApplicationFees = LocalApplicationFees;

            LocalDrivingLicenseApplication.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            LocalDrivingLicenseApplication.LicenseClassID = (cbApplicationClass.SelectedIndex + 1);

            if (LocalDrivingLicenseApplication.Save())
            {
                lblApplicationID.Text = LocalDrivingLicenseApplication.LocalDrivingApplicationLicenseID.ToString();
                _Mode = enMode.Update;
                this.Text = "Update Local Driving License Application";
                lblTitle.Text = "Update Local Driving License Application";

                MessageBox.Show("Data Saved Successfully","Saved",MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Data Saved Failed", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
        }


        private void btnNext_Click(object sender, EventArgs e)
        {
            if(_Mode == enMode.AddNew &&
                ucPersonInformationWithFilteration1.PersonID == -1)
            {
                MessageBox.Show("Please select a person !", "Error", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                 return;
            }

            ucPersonInformationWithFilteration1.EnableFilter = false;
            tabControl1.SelectedIndex = 1;
            gbApplicationInfo.Enabled = true;
            btnSave.Enabled = true;
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NewUpdateLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            _LoadIntialInfo();

            if (_Mode == enMode.Update)
                _LoadData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int ClassID = clsLicenseClass.Find(cbApplicationClass.Text).LicenseClassID;

            int Id = 
                clsLocalDrivingApplicationLicense.CheckApplicationActivation(
                    ucPersonInformationWithFilteration1.PersonID,
                    clsLocalDrivingApplicationLicense.enApplicationStatus.New,
               ClassID);

            if (Id != -1)
            {
                MessageBox.Show($"Choose another License Class,the selected person already have an active application for the selected class with id= {Id}",
                    "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }


            SaveData();

        }
    }
}
