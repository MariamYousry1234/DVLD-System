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
    public partial class ucPersonInformationWithFilteration : UserControl
    {
     
        public ucPersonInformationWithFilteration()
        {
            InitializeComponent();
        }


        public event Action<int> OnPersonSelected;

        public int PersonID
        {
            get { return ucPersonInformation1.PersonID; }
        }
        public bool EnableFilter
         {
            set {
                gbFilter.Enabled = value;
            }
            get
            {
               return  gbFilter.Enabled;
            }
         }

        public void LoadData(int PersonID)
        {
            cbFilter.SelectedIndex = 0;
            txtFilteration.Text = PersonID.ToString();
            ucPersonInformation1.LoadData(PersonID);
        }


        public void FilterFocus()
        {
            txtFilteration.Focus();
        }
        private void ucPersonInformationWithFilteration_Load(object sender, EventArgs e)
        {
            cbFilter.SelectedIndex = 0;
            txtFilteration.Focus();
        }

        private void txtFilteration_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void txtFilteration_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar ==(char)13)
            {
                btnSearching.PerformClick();
            }

            if (cbFilter.SelectedItem.ToString() == "PersonID")
            {
                e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));
            }
        }

        private void btnSearching_Click(object sender, EventArgs e)
        {
           
            switch (cbFilter.SelectedItem.ToString())
            {
                case "PersonID":
                    {
                        if (string.IsNullOrEmpty(txtFilteration.Text.Trim()))
                        {
                            MessageBox.Show("Please enter avalid ID");
                            return;
                        }
                      ucPersonInformation1.LoadData(Convert.ToInt32(txtFilteration.Text.Trim()));      
                    }
                    
                    break;

                case "NationalNo":
                   ucPersonInformation1.LoadData(txtFilteration.Text.Trim());
                    break;
            }


            //Return PersonID
            if(OnPersonSelected != null)
               OnPersonSelected(ucPersonInformation1.PersonID);
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            AddEditPersonInfo frm = new AddEditPersonInfo();

            frm.SendBackPersonID += LoadData;
            frm.ShowDialog();

        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilteration.Clear();
            txtFilteration.Focus();
        }
    }
}
