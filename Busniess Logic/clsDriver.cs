using Data_Access;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Busniess_Logic
{
    public class clsDriver
    {
        private enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;

        public int DriverID {set;get;}

        public int PersonID { set; get; }

        public clsPerson PersonInfo;

        public string FullName
        {
            get
            {
                return PersonInfo.GetFullName;
            }
        }
        public int CreatedbyUserID { set; get; }

        public clsUser UserInfo;

        public DateTime CreatedDate { set; get; }

        public clsDriver()
        {
            DriverID = -1;
            PersonID = -1;
            CreatedbyUserID = -1;
            CreatedDate = DateTime.Now;

            _Mode = enMode.AddNew;
        }

        private clsDriver(int DriverID,
            int PersonID,
            int CreatedbyUserID,
            DateTime CreatedDate)
        {
            this.DriverID = DriverID;
            this.PersonID = PersonID;
            this.CreatedbyUserID = CreatedbyUserID;
            this.CreatedDate = CreatedDate;

            PersonInfo = clsPerson.Find(PersonID);
            UserInfo = clsUser.Find(CreatedbyUserID);

            _Mode = enMode.Update;
        }

        private bool _AddNewDriver()
        {
            this.DriverID =
                clsDriverDataAccess.AddNewDriver(
                    this.PersonID,
                    this.CreatedbyUserID);

            return (DriverID != -1);
        }

        private bool _UpdateDriver()
        {
            return clsDriverDataAccess.UpdateDriver(
                this.DriverID,
                this.PersonID,
                this.CreatedbyUserID,
                this.CreatedDate);
        }

        public static clsDriver Find(int DriverID)
        {
            int PersonID = -1;
            int CreatedbyUserID = -1;
            DateTime CreatedDate = DateTime.Now;

            if (clsDriverDataAccess.FindDriver(
                DriverID,
                ref PersonID,
                ref CreatedbyUserID,
                ref CreatedDate))
            {
                return new clsDriver(
                    DriverID,
                    PersonID,
                    CreatedbyUserID,
                    CreatedDate);
            }

            return null;
        }

        public static clsDriver FindByPersonID(int PersonID)
        {
            int DriverID = -1;
            int CreatedbyUserID = -1;
            DateTime CreatedDate = DateTime.Now;

            if (clsDriverDataAccess.FindDriverByPersonID(
                ref DriverID,
                PersonID,
                ref CreatedbyUserID,
                ref CreatedDate))
            {
                return new clsDriver(
                    DriverID,
                    PersonID,
                    CreatedbyUserID,
                    CreatedDate);
            }

            return null;
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:

                    if (_AddNewDriver())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }

                    return false;

                case enMode.Update:

                    return _UpdateDriver();
            }

            return false;
        }

        public static bool IsDriverExists(int DriverID)
        {
            return clsDriverDataAccess.IsDriverExists(DriverID);
        }

        public static bool IsDriverExistsByPersonID(int PersonID)
        {
            return clsDriverDataAccess.IsDriverExistsByPersonID(PersonID);
        }

        
        public static DataTable GetDriversList()
        {
            return clsDriverDataAccess.GetDriversList();
        }

        public  DataTable GetLocalLicensesList()
        {
            return clsLicense.GetLocalLicensesList(this.DriverID);
        }

        public DataTable GetInternationalLicense()
        {
            return clsInternationalLicense.GetInternationaLicenses(this.DriverID);
        }
     }
}
