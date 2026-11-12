using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access;

namespace Busniess_Logic
{
    public class clsPerson
    {

        private enum enMode { AddNew = 0, Update = 1 };
        enMode _Mode;
        public int PersonID { set; get; }
        public string NationalNo { set; get; }
        public string FirstName { set; get; }

        public string SecondName { set; get; }

        public string ThirdName { set; get; }

        public string LastName { set; get; }

        public string GetFullName
        {
            get { return FirstName + " " + SecondName + " " + ThirdName + " " + LastName; }
        }
        public short Gender { set; get; }

        public DateTime DateOfBirth { set; get; }

        public string Phone { set; get; }

        public string Email { set; get; }

        public string Address { set; get; }

        public int NationalityID { set; get; }

        public clsCountry PersonCountryInfo { set; get; }

        public string ImagePath { set; get; }


        private bool _UpdatePerson()
        {
            return clsPersonDataAccess.UpdatePerson(this.PersonID, this.NationalNo, this.FirstName, this.SecondName,
             this.ThirdName, this.LastName, this.Gender, this.DateOfBirth,
             this.Phone, this.Email, this.Address, this.NationalityID, this.ImagePath);
        }

        private bool _AddNewPerson()
        {
            this.PersonID = (clsPersonDataAccess.AddNewPerson(this.NationalNo, this.FirstName, this.SecondName,
             this.ThirdName, this.LastName, this.Gender, this.DateOfBirth,
             this.Phone, this.Email, this.Address, this.NationalityID, this.ImagePath));

            return PersonID != -1;
        }
        public clsPerson()
        {
            this.PersonID = -1;
            this.NationalNo = "";
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.Gender = -1;
            this.DateOfBirth = DateTime.Now;
            this.Phone = "";
            this.Email = "";
            this.Address = "";
            this.NationalityID = -1;
            this.ImagePath = "";

            _Mode = enMode.AddNew;
        }
        private clsPerson(int PersonID, string NationalNo, string FirstName, string SecondName,
            string ThirdName, string LastName, short Gender, DateTime DateOfBirth,
            string Phone, string Email, string Address, int NationalityID, string ImagePath)
        {
            this.PersonID = PersonID;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.Gender = Gender;
            this.DateOfBirth = DateOfBirth;
            this.Phone = Phone;
            this.Email = Email;
            this.Address = Address;
            this.NationalityID = NationalityID;
            this.PersonCountryInfo = clsCountry.Find(NationalityID);
            this.ImagePath = ImagePath;

            _Mode = enMode.Update;
        }


        public static clsPerson Find(int PersonID)
        {
            string NationalNo = "", FirstName = "", SecondName = "",
             ThirdName = "", LastName = "", Phone = "", Email = "", Address = "", ImagePath = "";
            short Gender = -1;
            DateTime DateOfBirth = DateTime.Now;
            int NationalityID = -1;

            if (clsPersonDataAccess.FindPerson(PersonID, ref NationalNo, ref FirstName, ref SecondName,
             ref ThirdName, ref LastName, ref Gender, ref DateOfBirth,
            ref Phone, ref Email, ref Address, ref NationalityID, ref ImagePath))
            {
                return new clsPerson(PersonID, NationalNo, FirstName, SecondName,
             ThirdName, LastName, Gender, DateOfBirth,
             Phone, Email, Address, NationalityID, ImagePath);
            }

            return null;
        }

        public static clsPerson Find(string NationalNo)
        {
            int PersonID = -1;
            string FirstName = "", SecondName = "",
             ThirdName = "", LastName = "", Phone = "", Email = "", Address = "", ImagePath = "";
            short Gender = -1;
            DateTime DateOfBirth = DateTime.Now;
            int NationalityID = -1;

            if (clsPersonDataAccess.FindPersonByNationalNo(ref PersonID, NationalNo, ref FirstName, ref SecondName,
             ref ThirdName, ref LastName, ref Gender, ref DateOfBirth,
            ref Phone, ref Email, ref Address, ref NationalityID, ref ImagePath))
            {
                return new clsPerson(PersonID, NationalNo, FirstName, SecondName,
             ThirdName, LastName, Gender, DateOfBirth,
             Phone, Email, Address, NationalityID, ImagePath);
            }

            return null;
        }
        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.Update:
                    {
                        return _UpdatePerson();


                    }

                case enMode.AddNew:
                    {
                        if (_AddNewPerson())
                        {

                            _Mode = enMode.Update;
                            return true;
                        }
                        return false;
                    }


            }
            return false;
        }


        public static bool IsPersonExists(int PersonID)
        {
            return clsPersonDataAccess.IsPersonExists(PersonID);
        }

        public static bool IsNationalNoExists(string NationalNo)
        {
            return clsPersonDataAccess.IsNationalNoExists(NationalNo);
        }
        public static bool DeletePerson(int PersonID)
        {
            return clsPersonDataAccess.DeletePerson(PersonID);

        }

        public static DataTable GetPeopleList()
        {
            return clsPersonDataAccess.GetPeopleList();

        }


    }
}
