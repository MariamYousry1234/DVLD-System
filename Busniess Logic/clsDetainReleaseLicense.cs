using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access;

namespace Busniess_Logic
{
    public class clsDetainReleaseLicense
    {

        private enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;

        public int DetainedID { get; set; }
        public float FineFees { set; get; }
        public DateTime DetainedDate { set; get; }

        public int LicenseID { set; get; }

    
        public int CreatedByUserID { set; get; }

        public clsUser CreatedUserInfo;

        public bool IsReleased { set; get; }

        public int ReleaseApplicationID { set; get; }

        public clsApplication ReleaseApplicationInfo;

        public DateTime ReleaseDate { set; get; }

        public int ReleasedByUserID { set; get; }


        public clsUser ReleasedUserInfo;

        private clsDetainReleaseLicense(
        int DetainedID,
        float FineFees,
        DateTime DetainedDate,
        int LicenseID,
        int CreatedByUserID,
        bool IsReleased,
        int ReleaseApplicationID,
        DateTime ReleaseDate,
        int ReleasedByUserID)
        {
            this.DetainedID = DetainedID;
            this.FineFees = FineFees;
            this.DetainedDate = DetainedDate;

            this.LicenseID = LicenseID;
      
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedUserInfo = clsUser.Find(CreatedByUserID);

            this.IsReleased = IsReleased;

            this.ReleaseApplicationID = ReleaseApplicationID;

            this.ReleaseApplicationInfo = clsApplication.Find(ReleaseApplicationID);

            this.ReleaseDate = ReleaseDate;

            this.ReleasedByUserID = ReleasedByUserID;

            this.ReleasedUserInfo = clsUser.Find(ReleasedByUserID);

            _Mode = enMode.Update;
        }

        public clsDetainReleaseLicense()
        {
            DetainedID = -1;
            FineFees = 0;
            DetainedDate = DateTime.Now;

            LicenseID = -1;
            CreatedByUserID = -1;

            IsReleased = false;

            ReleaseApplicationID = -1;
            ReleaseDate = DateTime.MinValue;
            ReleasedByUserID = -1;

            _Mode = enMode.AddNew;
        }


        private bool _AddNewDetainedLicense()
        {
            this.DetainedID =
                clsDetainReleaseLicenseDataAccess.AddNewDetainedLicense(
                    this.FineFees,
                    this.DetainedDate,
                    this.LicenseID,
                    this.CreatedByUserID,
                    this.IsReleased,
                    this.ReleaseApplicationID,
                    this.ReleaseDate,
                    this.ReleasedByUserID);

            return (this.DetainedID != -1);
        }


        private bool _UpdateDetainedLicense()
        {
            return clsDetainReleaseLicenseDataAccess.UpdateDetainedLicense(
                this.DetainedID,
                this.FineFees,
                this.DetainedDate,
                this.LicenseID,
                this.CreatedByUserID,
                this.IsReleased,
                this.ReleaseApplicationID,
                this.ReleaseDate,
                this.ReleasedByUserID);
        }


        public static clsDetainReleaseLicense Find(int DetainedID)
        {
            float FineFees = 0;
            DateTime DetainedDate = DateTime.Now;
            int LicenseID = -1;
            int CreatedByUserID = -1;
            bool IsReleased = false;
            int ReleaseApplicationID = -1;
            DateTime ReleaseDate = DateTime.MinValue;
            int ReleasedByUserID = -1;

            if (clsDetainReleaseLicenseDataAccess.FindDetainedLicense(
                DetainedID,
                ref FineFees,
                ref DetainedDate,
                ref LicenseID,
                ref CreatedByUserID,
                ref IsReleased,
                ref ReleaseApplicationID,
                ref ReleaseDate,
                ref ReleasedByUserID))
            {
                return new clsDetainReleaseLicense(
                DetainedID,
                FineFees,
                DetainedDate,
                LicenseID,
                CreatedByUserID,
                IsReleased,
                ReleaseApplicationID,
                ReleaseDate,
                ReleasedByUserID);
            }

            return null;
        }

        public static clsDetainReleaseLicense GetDetainedInfobyLicenseID(int LicenseID)
        {
            int DetainedID = -1;
            float FineFees = 0;
            DateTime DetainedDate = DateTime.Now;
           
            int CreatedByUserID = -1;
            bool IsReleased = false;
            int ReleaseApplicationID = -1;
            DateTime ReleaseDate = DateTime.MinValue;
            int ReleasedByUserID = -1;

            if (clsDetainReleaseLicenseDataAccess.GetDetainedInfoByLicenseID(
                ref DetainedID,
                ref FineFees,
                ref DetainedDate,
                 LicenseID,
                ref CreatedByUserID,
                ref IsReleased,
                ref ReleaseApplicationID,
                ref ReleaseDate,
                ref ReleasedByUserID))
            {
                return new clsDetainReleaseLicense(
                DetainedID,
                FineFees,
                DetainedDate,
                LicenseID,
                CreatedByUserID,
                IsReleased,
                ReleaseApplicationID,
                ReleaseDate,
                ReleasedByUserID);
            }

            return null;
        }
        public static int GetDetainedIDForLicenseID(int LicenseID)
        {
            return clsDetainReleaseLicenseDataAccess.GetDetainedIDForLicenseID(LicenseID);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:

                    if (_AddNewDetainedLicense())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }

                    return false;

                case enMode.Update:
                    return _UpdateDetainedLicense();
            }

            return false;
        }

        public static bool IsDetainedLicenseExists(int DetainedID)
        {
            return clsDetainReleaseLicenseDataAccess.IsDetainedLicenseExists(DetainedID);
        }

        public static bool DeleteDetainedLicense(int DetainedID)
        {
            return clsDetainReleaseLicenseDataAccess.DeleteDetainedLicense(DetainedID);
        }

        public static DataTable GetDetainedLicensesList()
        {
            return clsDetainReleaseLicenseDataAccess.GetDetainedLicensesList();
        }


        public static bool IsLicenseDetained(int LicenseID)
        {
            return clsDetainReleaseLicenseDataAccess.IsLicenseDetained(LicenseID);
        }

        public bool Release(int ReleaseedapplicationID,int ReleasedUserID)
        {
            return clsDetainReleaseLicenseDataAccess.ReleaseDetainedLicense(this.DetainedID, ReleasedUserID, ReleaseedapplicationID);
        }

    }
}
