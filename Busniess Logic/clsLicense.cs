using Data_Access;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Busniess_Logic
{
    public class clsLicense
    {
        private enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;

        public enum enIssueReason {FirstTime=1, Renew=2,
            ReplacementForDamaged=3, ReplacementForLost=4 }
        public int LicenseID { set; get; }

        public DateTime IssueDate { set; get; }

        public DateTime ExpiryDate { set; get; }

        public enIssueReason IssueReason { set; get; }

        public string Notes { set; get; }

        public float PaidFees { set; get; }

        public bool IsActive { set; get; }

        public int LicenseClassID { set; get; }

        public clsLicenseClass LicenseClassInfo;

        public int ApplicationID { set; get; }

        public clsApplication ApplicationInfo;

        public int DriverID { set; get; }

        public clsDriver DriverInfo;

        public int CreatedByUserID { set; get; }

        public clsUser UserInfo;

        public string txtIssueReason
        {
            get
            {
                switch (IssueReason)
                {
                    case clsLicense.enIssueReason.FirstTime:
                        return "First Time";
                       

                    case clsLicense.enIssueReason.Renew:
                        return  "Renew";
                     

                    case clsLicense.enIssueReason.ReplacementForDamaged:
                        return "Replacement For Damaged";
                    

                    case clsLicense.enIssueReason.ReplacementForLost:
                        return "Replacement For Lost";

                    default:
                        return "UnKown";

                }
            }
        }


        public bool IsDetained
        {
            get
            {
                return IsLicenseDetained();
            }
        }

        public clsDetainReleaseLicense DetainedInfo;
        private bool _AddNewLicense()
        {
            this.LicenseID = clsLicenseDataAccess.AddNewLicense(
                this.IssueDate,
                this.ExpiryDate,
                (byte) this.IssueReason,
                this.Notes,
                this.PaidFees,
                this.IsActive,
                this.LicenseClassID,
                this.ApplicationID,
                this.DriverID,
                this.CreatedByUserID
            );

            return (this.LicenseID != -1);
        }



        private bool _UpdateLicense()
        {
            return clsLicenseDataAccess.UpdateLicense(
                this.LicenseID,
                this.IssueDate,
                this.ExpiryDate,
                (byte)this.IssueReason,
                this.Notes,
                this.PaidFees,
                this.IsActive,
                this.LicenseClassID,
                this.ApplicationID,
                this.DriverID,
                this.CreatedByUserID
            );
        }



        public clsLicense()
        {
            this.LicenseID = -1;
            this.IssueDate = DateTime.Now;
            this.ExpiryDate = DateTime.Now;
            this.IssueReason = enIssueReason.FirstTime;
            this.Notes = "";
            this.PaidFees = 0;
            this.IsActive = true;

            this.LicenseClassID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.CreatedByUserID = -1;

            _Mode = enMode.AddNew;
        }

        private clsLicense(int LicenseID,
            DateTime IssueDate,
            DateTime ExpiryDate,
            enIssueReason IssueReason,
            string Notes,
            float PaidFees,
            bool IsActive,
            int LicenseClassID,
            int ApplicationID,
            int DriverID,
            int CreatedByUserID)
        {
            this.LicenseID = LicenseID;
            this.IssueDate = IssueDate;
            this.ExpiryDate = ExpiryDate;
            this.IssueReason = IssueReason;
            this.Notes = Notes;
            this.PaidFees = PaidFees;
            this.IsActive = IsActive;

            this.LicenseClassID = LicenseClassID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.CreatedByUserID = CreatedByUserID;

            this.LicenseClassInfo = clsLicenseClass.Find(LicenseClassID);
            this.ApplicationInfo = clsApplication.Find(ApplicationID);
            this.DriverInfo = clsDriver.Find(DriverID);
            this.UserInfo = clsUser.Find(CreatedByUserID);
            this.DetainedInfo = clsDetainReleaseLicense.GetDetainedInfobyLicenseID(LicenseID);

            _Mode = enMode.Update;
        }



        public static clsLicense Find(int LicenseID)
        {
            DateTime IssueDate = DateTime.Now;
            DateTime ExpiryDate = DateTime.Now;
            byte IssueReason = 0;
            string Notes = "";
            float PaidFees = 0;
            bool IsActive = false;

            int LicenseClassID = -1;
            int ApplicationID = -1;
            int DriverID = -1;
            int CreatedByUserID = -1;

            if (clsLicenseDataAccess.FindLicense(
                LicenseID,
                ref IssueDate,
                ref ExpiryDate,
                ref IssueReason,
                ref Notes,
                ref PaidFees,
                ref IsActive,
                ref LicenseClassID,
                ref ApplicationID,
                ref DriverID,
                ref CreatedByUserID))
            {
                return new clsLicense(
                    LicenseID,
                    IssueDate,
                    ExpiryDate,
                    (enIssueReason)IssueReason,
                    Notes,
                    PaidFees,
                    IsActive,
                    LicenseClassID,
                    ApplicationID,
                    DriverID,
                    CreatedByUserID
                );
            }

            return null;
        }

        public static clsLicense FindByApplicationID(int ApplicationID)
        {
            DateTime IssueDate = DateTime.Now;
            DateTime ExpiryDate = DateTime.Now;
            byte IssueReason = 0;
            string Notes = "";
            float PaidFees = 0;
            bool IsActive = false;

            int LicenseClassID = -1;
            int LicenseID = -1;
            int DriverID = -1;
            int CreatedByUserID = -1;

            if (clsLicenseDataAccess.FindLicense(
                ref LicenseID,
                ref IssueDate,
                ref ExpiryDate,
                ref IssueReason,
                ref Notes,
                ref PaidFees,
                ref IsActive,
                ref LicenseClassID,
                ApplicationID,
                ref DriverID,
                ref CreatedByUserID))
            {
                return new clsLicense(
                    LicenseID,
                    IssueDate,
                    ExpiryDate,
                    (enIssueReason)IssueReason,
                    Notes,
                    PaidFees,
                    IsActive,
                    LicenseClassID,
                    ApplicationID,
                    DriverID,
                    CreatedByUserID
                );
            }

            return null;
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:

                    if (_AddNewLicense())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:

                    return _UpdateLicense();
            }

            return false;
        }

   

        public static bool IsLicenseExists(int LicenseID)
        {
            return clsLicenseDataAccess.IsLicenseExists(LicenseID);
        }

   
        public static bool DeleteLicense(int LicenseID)
        {
            return clsLicenseDataAccess.DeleteLicense(LicenseID);
        }

        public static bool IsPersonHasActiveLicenseClass(int ApplicationID)
        {
            return clsLicenseDataAccess.IsPersonHasActiveLicenseClass(ApplicationID);
        }

        public static bool IsPersonHasActiveLicenseClass(int PersonID, int LicenseClassID)
        {
            return (GetActiveLicenseClassForPerson(PersonID,LicenseClassID) != -1);
        }

        public static int GetActiveLicenseClassForPerson(int PersonID,int LicenseClassID)
        {
            return clsLicenseDataAccess.GetActiveLicenseClassForPerson(PersonID, LicenseClassID);
        }

        public static DataTable GetLocalLicensesList(int DriverID)
        {
            return clsLicenseDataAccess.GetLocalLicensesList(DriverID);
        }

        public static int GetActiveLicenseClassForDriver(int DriverID, int LicenseClassID)
        {
            return clsLicenseDataAccess.GetActiveLicenseClassForDriver(DriverID, LicenseClassID);
        }

        public bool IsExpired()
        {
            return DateTime.Now > this.ExpiryDate;
        }

        public clsLicense Renew(string Notes,int CreatedByUserID)
        {
            clsApplication NewApplication = new clsApplication();
            NewApplication.PersonID = this.DriverInfo.PersonID;
            NewApplication.ApplicationDate = DateTime.Now;
            NewApplication.ApplicationTypeID = clsApplication.enApplicationType.Renew;
            NewApplication.ApplicationStaus = clsApplication.enApplicationStatus.Completed;
            NewApplication.LastStatusDate = DateTime.Now;
            NewApplication.ApplicationFees = clsApplicationTypes.Find((int)clsApplication.enApplicationType.Renew).Fees;
            NewApplication.CreatedByUserID = CreatedByUserID;

            if (!NewApplication.SaveData()) return null;

            clsLicense RenewLicense = new clsLicense();

            RenewLicense.IssueDate = DateTime.Now;
            RenewLicense.ExpiryDate = DateTime.Now.AddYears(this.LicenseClassInfo.DeafualtValidityLength);
            RenewLicense.IssueReason = clsLicense.enIssueReason.Renew;
            RenewLicense.Notes = Notes;
            RenewLicense.PaidFees = this.LicenseClassInfo.ClassFees;
            RenewLicense.IsActive = true;
            RenewLicense.LicenseClassID = this.LicenseClassID;
            RenewLicense.ApplicationID = NewApplication.ApplicationID;
            RenewLicense.DriverID = this.DriverID;
            RenewLicense.CreatedByUserID = CreatedByUserID;

            if (!RenewLicense.Save()) return null;


            //deactivate old license
            this.DeActivate();
            return RenewLicense;
        }

        public clsLicense ReplaceFroLostOrDamaged(int CreatedByUserID,clsLicense.enIssueReason ApplicationReason)
        {

            
            clsApplication NewApplication = new clsApplication();
            NewApplication.PersonID = this.DriverInfo.PersonID;
            NewApplication.ApplicationDate = DateTime.Now;
            NewApplication.ApplicationTypeID = (IssueReason == enIssueReason.ReplacementForDamaged) ?
              clsApplication.enApplicationType.ReplacementForDamaged :
               clsApplication.enApplicationType.ReplacementForLost;
            NewApplication.ApplicationStaus = clsApplication.enApplicationStatus.Completed;
            NewApplication.LastStatusDate = DateTime.Now;
            NewApplication.ApplicationFees = clsApplicationTypes.Find((int)NewApplication.ApplicationTypeID).Fees;
            NewApplication.CreatedByUserID = CreatedByUserID;

            if (!NewApplication.SaveData()) return null;

            clsLicense RenewLicense = new clsLicense();

            RenewLicense.IssueDate = DateTime.Now;
            RenewLicense.ExpiryDate = DateTime.Now.AddYears(this.LicenseClassInfo.DeafualtValidityLength);

            RenewLicense.IssueReason = ApplicationReason;
              
            RenewLicense.PaidFees = this.LicenseClassInfo.ClassFees;
            RenewLicense.IsActive = true;
            RenewLicense.LicenseClassID = this.LicenseClassID;
            RenewLicense.ApplicationID = NewApplication.ApplicationID;
            RenewLicense.DriverID = this.DriverID;
            RenewLicense.CreatedByUserID = CreatedByUserID;

            if (!RenewLicense.Save()) return null;


            //deactivate old license
            this.DeActivate();
            return RenewLicense;
        }
        public bool DeActivate()
        {
            return clsLicenseDataAccess.DeActivate(this.LicenseID);
        }

        private bool IsLicenseDetained()
        {
            return clsDetainReleaseLicense.IsLicenseDetained(this.LicenseID);
        }

  

        public int Detain(float FineFees,int CreatedByUserID)
        {
            if (this.IsDetained) return -1;

            clsDetainReleaseLicense NewDetain = new clsDetainReleaseLicense();

            NewDetain.LicenseID = LicenseID;
            NewDetain.FineFees = Convert.ToSingle(FineFees);
            NewDetain.CreatedByUserID = CreatedByUserID;

            if (!NewDetain.Save()) return -1;

            return NewDetain.DetainedID;
        }

        public static int GetDetainedIDForLicenseID(int LicenseID)
        {
            return clsDetainReleaseLicense.GetDetainedIDForLicenseID(LicenseID);
        }

        public bool Release(int ReleasedUserID,ref int ApplicationID)
        {
            if (!this.IsDetained) return false;

            clsApplication NewApplication = new clsApplication();
            NewApplication.PersonID = this.DriverInfo.PersonID;
            NewApplication.ApplicationDate = DateTime.Now;
            NewApplication.ApplicationTypeID = clsApplication.enApplicationType.ReleaseDetained;
            NewApplication.ApplicationStaus = clsApplication.enApplicationStatus.Completed;
            NewApplication.LastStatusDate = DateTime.Now;
            NewApplication.ApplicationFees = clsApplicationTypes.Find((int)NewApplication.ApplicationTypeID).Fees;
            NewApplication.CreatedByUserID = ReleasedUserID;

            if (!NewApplication.SaveData()) return false;

            ApplicationID = NewApplication.ApplicationID;
            return this.DetainedInfo.Release(NewApplication.ApplicationID, ReleasedUserID); ;
        }
    }
}
