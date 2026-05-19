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

namespace DVLD_Project
{
    public partial class ManagePeople : Form
    {
        public ManagePeople()
        {
            InitializeComponent();
        }

        static DataTable _AllPeople = clsPerson.GetPeopleList();

        DataTable _AllPeopleView = _AllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo",
               "FirstName", "SecondName", "ThirdName", "LastName", "Gender", "DateOfBirth", "Phone",
               "Email", "Nationality");

        void RefreshData()
        {
            _AllPeople = clsPerson.GetPeopleList();

            _AllPeopleView = _AllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo",
                "FirstName", "SecondName", "ThirdName", "LastName", "Gender", "DateOfBirth", "Phone",
                "Email", "Nationality");

            dgvData.DataSource = _AllPeopleView;
            lblRecords.Text = _AllPeopleView.Rows.Count.ToString();
        }
        void DeletePerson(int PersonID)
        {
            if (MessageBox.Show($"Are you sure you want to delete person [{PersonID}]?", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                == DialogResult.OK)
            {
                string ImagePath = clsPerson.Find(PersonID).ImagePath;
                if (clsPerson.DeletePerson(PersonID))
                {
                    if (!string.IsNullOrEmpty(ImagePath))
                        File.Delete(ImagePath);
                    MessageBox.Show("Person Deleted Successfully", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //resefresh
                    RefreshData();
                }
                else
                    MessageBox.Show("Person wasn't deleted because it has data linked to it");
            }
        }

        private void ManagePeople_Load(object sender, EventArgs e)
        {
            dgvData.DataSource = _AllPeopleView;
            lblRecords.Text = _AllPeopleView.Rows.Count.ToString();
            cbFilter.SelectedIndex = 0;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((txtFiltering.Visible = (cbFilter.SelectedIndex != 0)))
            {
                txtFiltering.Clear();
                txtFiltering.Focus();
            }
        }

        private void txtFiltering_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.SelectedItem.ToString() == "PersonID")
            {
                e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));
            }
        }

        private void txtFiltering_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFiltering.Text))
            {
                _AllPeopleView.DefaultView.RowFilter = "";
                lblRecords.Text = dgvData.Rows.Count.ToString();
                return;
            }

            if (cbFilter.SelectedItem.ToString() == "PersonID")
                _AllPeopleView.DefaultView.RowFilter = $"PersonID={txtFiltering.Text}";
            else
                _AllPeopleView.DefaultView.RowFilter = $"{cbFilter.Text} like '{txtFiltering.Text}%'";

            lblRecords.Text = dgvData.Rows.Count.ToString();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            Form frmAddEdit = new AddEditPersonInfo();
            frmAddEdit.ShowDialog();

            RefreshData();
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frmAddEdit = new AddEditPersonInfo();
            frmAddEdit.ShowDialog();

            RefreshData();

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frmAddEdit =
                new AddEditPersonInfo((int)dgvData.CurrentRow.Cells[0].Value);
            frmAddEdit.ShowDialog();

            RefreshData();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeletePerson((int)dgvData.CurrentRow.Cells[0].Value);
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frmPersonDetails = new PersonDetails((int)dgvData.CurrentRow.Cells[0].Value);
            frmPersonDetails.ShowDialog();

            RefreshData();
        }

        private void dgvData_DoubleClick(object sender, EventArgs e)
        {
            Form frmPersonDetails = new PersonDetails((int)dgvData.CurrentRow.Cells[0].Value);
            frmPersonDetails.ShowDialog();
        }

    }
}
