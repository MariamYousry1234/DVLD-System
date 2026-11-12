using Data_Access;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Busniess_Logic
{
    public class clsInternationalLicense :clsApplication
    {
        private enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;

        public int InternationalLicenseID { set; get; }
        public DateTime IssueDate { set; get; }
        public DateTime ExpiryDate { set; get; }
        public bool IsActive { set; get; }
      
        public int DriverID { set; get; }
        public clsDriver DriverInfo;

        public int LocalLicenseID { set; get; }
        public clsLicense LicenseInfo;

        public int CreatedByUserID { set; get; }
 

        public clsInternationalLicense()
        {
            this.ApplicationTypeID = clsApplication.enApplicationType.NewInternational;
            this.InternationalLicenseID = -1;
            this.IssueDate = DateTime.Now;
            this.ExpiryDate = DateTime.Now;
            this.IsActive = true;
            this.DriverID = -1;
            this.LocalLicenseID = -1;
            this.CreatedByUserID = -1;

            _Mode = enMode.AddNew;
        }

        private clsInternationalLicense(
            int internationalLicenseID,
            DateTime issueDate,
            DateTime expiryDate,
            bool isActive,
            int applicationID,
            int driverID,
            int localLicenseID,
            int createdByUserID, int PersonID, DateTime ApplicationDate, enApplicationType ApplicationTypeID,
           enApplicationStatus ApplicationStaus, DateTime LastStatusDate, float ApplicationFees
         )
        {
            base.ApplicationID = applicationID;
            base.PersonID = PersonID;
            base.ApplicationDate = ApplicationDate;
            base.ApplicationTypeID = ApplicationTypeID;
            base.ApplicationStaus = ApplicationStaus;
            base.LastStatusDate = LastStatusDate;
            base.ApplicationFees = ApplicationFees;
            base.CreatedByUserID = createdByUserID;


            this.InternationalLicenseID = internationalLicenseID;
            this.IssueDate = issueDate;
            this.ExpiryDate = expiryDate;
            this.IsActive = isActive;
            this.DriverID = driverID;
            this.LocalLicenseID = localLicenseID;
            this.CreatedByUserID = createdByUserID;         
            this.DriverInfo = clsDriver.Find(driverID);
            this.LicenseInfo = clsLicense.Find(localLicenseID);
           

            
            _Mode = enMode.Update;
        }

        private bool _AddNew()
        {
            this.InternationalLicenseID =
                clsInternationalLicenseDataAccess.AddNewInternationalLicense(
                this.IssueDate,
               this.ExpiryDate,
                        this.IsActive,
                this.ApplicationID,
                  this.DriverID,
                 this.LocalLicenseID,
                    this.CreatedByUserID
                    );

            return this.InternationalLicenseID != -1;
        }

        private bool _Update()
        {
            return clsInternationalLicenseDataAccess.UpdateInternationalLicense(this.InternationalLicenseID,
                this.IssueDate,
               this.ExpiryDate,
                        this.IsActive,
                this.ApplicationID,
                  this.DriverID,
                 this.LocalLicenseID,
                    this.CreatedByUserID);
        }

        public bool Save()
        {

            base.Mode=(clsApplication.enMode)_Mode;
            if (!base.SaveData()) return false;

            switch (_Mode)
            {
                case enMode.AddNew:

                    if (_AddNew())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }

                    return false;

                case enMode.Update:

                    return _Update();
            }

            return false;
        }

        public static clsInternationalLicense Find(int InternationalLicenseID)
        {
          
            DateTime IssueDate = DateTime.Now;
            DateTime ExpiryDate = DateTime.Now;
            bool IsActive = false;
            int ApplicationID = -1;
            int DriverID = -1;
            int LocalLicenseID = -1;
            int CreatedByUserID = -1;

            if (clsInternationalLicenseDataAccess.FindInternationalLicense(
                InternationalLicenseID,
                
               ref IssueDate,
               ref ExpiryDate,
                 ref  IsActive,
                ref ApplicationID,
                 ref  DriverID,
                ref  LocalLicenseID,
                  ref   CreatedByUserID))
            {

                clsApplication Application = clsApplication.Find(ApplicationID);

                return new clsInternationalLicense(
                    InternationalLicenseID,
                 
                    IssueDate,
                    ExpiryDate,
                    IsActive,
                    ApplicationID,
                    DriverID,
                    LocalLicenseID,
                    CreatedByUserID, Application.PersonID, Application.ApplicationDate, Application.ApplicationTypeID,
               Application.ApplicationStaus, Application.LastStatusDate, Application.ApplicationFees);
            }

            return null;
        }

        public static bool Delete(int InternationalLicenseID)
        {
            return clsInternationalLicenseDataAccess.DeleteInternationalLicense(InternationalLicenseID);
        }

        public static bool IsExists(int InternationalLicenseID)
        {
            return clsInternationalLicenseDataAccess.IsInternationalLicenseExists(InternationalLicenseID);
        }

        public static DataTable GetAllInternationalLicenses()
        {
            return clsInternationalLicenseDataAccess.GetAllInternationalLicenses();
        }
     

        public static bool IsDriverHasActiveInternationalLicense(int DriverID)
        {
            return GetActiveInternationalLicenseID(DriverID) != -1;
        }
        public static int GetActiveInternationalLicenseID(int DriverID)
        {
            return clsInternationalLicenseDataAccess.GetActiveInternationalLicenseID(DriverID);
        }

        public static DataTable GetInternationaLicenses(int DriverID)
        {
            return clsInternationalLicenseDataAccess.GetInternationaLicenses(DriverID);
        }

   
      }
}
