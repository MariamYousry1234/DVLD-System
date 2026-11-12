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
using static System.Net.Mime.MediaTypeNames;

namespace Busniess_Logic
{
    public class clsLocalDrivingApplicationLicense : clsApplication
    {
        public enum enMode { AddNew = 0, Update = 1 }
        enMode Mode;
        public int LocalDrivingApplicationLicenseID { set; get; }
        public int LicenseClassID { set; get; }

        public clsLicenseClass LicenseClasssInfo;

        public clsLocalDrivingApplicationLicense()
        {
            this.LocalDrivingApplicationLicenseID = -1;
            this.LicenseClassID = -1;

            Mode = enMode.AddNew;
        }


        private clsLocalDrivingApplicationLicense(int LocalDrivingApplicationLicenseID,
           int ApplicationID, int PersonID, DateTime ApplicationDate, enApplicationType ApplicationTypeID,
           enApplicationStatus ApplicationStaus, DateTime LastStatusDate, float ApplicationFees,
           int CreatedByUserID, int LicenseClassID) : base(ApplicationID, PersonID, ApplicationDate, ApplicationTypeID,
            ApplicationStaus, LastStatusDate, ApplicationFees,
            CreatedByUserID)
        {

            this.LocalDrivingApplicationLicenseID = LocalDrivingApplicationLicenseID;
            this.LicenseClassID = LicenseClassID;
            this.LicenseClasssInfo = clsLicenseClass.Find(LicenseClassID);

            Mode = enMode.Update;
        }

        private bool _AddNewLocalDrivingLicenseApplication()
        {
            this.LocalDrivingApplicationLicenseID = clsLocalDrivingApplicationLicenseDataAccess.AddNewLocalDrivingApplicationLicense(
                   this.ApplicationID, this.LicenseClassID);

            return (this.LocalDrivingApplicationLicenseID != -1);

        }

        private bool _UpdateLocalDrivingLicenseApplication()
        {
            return clsLocalDrivingApplicationLicenseDataAccess.UpdateLocalDrivingLicenseApplication(
                    this.LocalDrivingApplicationLicenseID, this.ApplicationID, this.LicenseClassID);
        }

        public bool Save()
        {
            base.Mode = (clsApplication.enMode)Mode;
            if (!base.SaveData())
                return false;

            switch (Mode)
            {
                case enMode.Update:
                    {
                        return _UpdateLocalDrivingLicenseApplication();
                    }

                case enMode.AddNew:
                    {
                        if (_AddNewLocalDrivingLicenseApplication())
                        {

                            Mode = enMode.Update;
                            return true;
                        }
                        return false;
                    }

            }
            return false;
        }

        public static clsLocalDrivingApplicationLicense FindByID(int LocalDrivingLicenseApplicationID)
        {

            int ApplicationID = -1, LicenseClassID = -1;
            clsApplication ApplicationInfo;
            if (clsLocalDrivingApplicationLicenseDataAccess.Find(LocalDrivingLicenseApplicationID, ref ApplicationID,
                ref LicenseClassID))
            {
                ApplicationInfo = clsApplication.Find(ApplicationID);

                if (ApplicationInfo != null)
                {
                    return new clsLocalDrivingApplicationLicense(LocalDrivingLicenseApplicationID,
                        ApplicationID, ApplicationInfo.PersonID, ApplicationInfo.ApplicationDate, ApplicationInfo.ApplicationTypeID,
                        ApplicationInfo.ApplicationStaus, ApplicationInfo.LastStatusDate,
                        ApplicationInfo.ApplicationFees, ApplicationInfo.CreatedByUserID,
                        LicenseClassID

                      );
                }
            }

            return null;
        }

        public static clsLocalDrivingApplicationLicense FindByApplicationID(int ApplicationID)
        {

            int LocalDrivingLicenseApplicationID = -1, LicenseClassID = -1;
            clsApplication ApplicationInfo;
            if (clsLocalDrivingApplicationLicenseDataAccess.Find(ref LocalDrivingLicenseApplicationID, ApplicationID,
                ref LicenseClassID))
            {
                ApplicationInfo = clsApplication.Find(ApplicationID);

                if (ApplicationInfo != null)
                {
                    return new clsLocalDrivingApplicationLicense(LocalDrivingLicenseApplicationID,
                        ApplicationID, ApplicationInfo.PersonID, ApplicationInfo.ApplicationDate, ApplicationInfo.ApplicationTypeID,
                        ApplicationInfo.ApplicationStaus, ApplicationInfo.LastStatusDate,
                        ApplicationInfo.ApplicationFees, ApplicationInfo.CreatedByUserID,
                        LicenseClassID
                      );
                }
            }

            return null;
        }
        public static DataTable GetLocalApplicationsList()
        {
            return clsLocalDrivingApplicationLicenseDataAccess.GetLocalApplicationsList();
        }



        public  bool DeletLocalDrivingLicenseApplication()
        {     
            if(clsLocalDrivingApplicationLicenseDataAccess.DeleteLocalDrivingApplication(this.LocalDrivingApplicationLicenseID))
            {
                return base.DeleteApplication();          
            }

            return false;
        }

        public static bool CancelLocalDrivingLicenseApplication(int LocalDrivingLicenseID)
        {
            return clsLocalDrivingApplicationLicenseDataAccess.CancelLocalDrivingLicenseApplication(LocalDrivingLicenseID);
        }


        public byte GetNumberOfPassedTests()
        {
            return clsTest.GetNumberOfPassedTests(this.LocalDrivingApplicationLicenseID);
        }


        public  bool IsPassedAllTests()
        {
            return clsTest.IsPassedAllTests(this.LocalDrivingApplicationLicenseID);
        }

        public bool IsHaveActiveLicenseClass()
        {
            return GetActiveLicenseClassForPerson() != -1;
        }
        public int GetActiveLicenseClassForPerson()
        {
            return  clsLicense.GetActiveLicenseClassForPerson(this.PersonID, this.LicenseClassID);
        }

        public DataTable GetAllAppointmentsForTest(clsTestTypes.enTestType TestType)
        {
            return clsTestAppointment.GetAllAppointmentsForTest(this.LocalDrivingApplicationLicenseID, TestType);
        }

        public bool IsHasActiveTestAppointment(clsTestTypes.enTestType TestType)
        {
            return clsTestAppointment.IsHasActiveTestAppointment(this.LocalDrivingApplicationLicenseID, TestType);
        }

        public bool IsPassedThisTest(clsTestTypes.enTestType TestType)
        {
            return clsTest.IsPassedThisTest(this.LocalDrivingApplicationLicenseID, TestType);
        }

        public int NumberOfTrailsForTest(clsTestTypes.enTestType TestType)
        {
            return clsTestAppointment.NumberOfTrailsForTest(this.LocalDrivingApplicationLicenseID, TestType);
        }

        public clsTest GetLastTest(int TestTypeID)
        {
            return clsTest.GetLastTest(this.LocalDrivingApplicationLicenseID, TestTypeID);
        }

        public bool DoesAttentTestBefore( clsTestTypes.enTestType TestType)
        {
            return clsTestAppointment.DoesAttentTestBefore(this.LocalDrivingApplicationLicenseID, TestType);
        }

        public int IssueLicenseForFirstTime(string Notes,int CreatedByUserID)
        {
            int DriverID = -1;

            //create driver if not exists

            if(!clsDriver.IsDriverExistsByPersonID(this.PersonID))
            {
                clsDriver Driver = new clsDriver();
                Driver.PersonID = this.PersonID;
                Driver.CreatedbyUserID = CreatedByUserID;
                Driver.CreatedDate = DateTime.Now;

                if (Driver.Save())
                {
                    DriverID = Driver.DriverID;
                }
                else
                    return -1;
            }

            clsLicense License = new clsLicense();

            License.DriverID = DriverID;
            License.ApplicationID = this.ApplicationID;
            License.LicenseClassID = this.LicenseClassID;
            License.IssueDate = DateTime.Now;
            License.ExpiryDate = DateTime.Now.AddYears(this.LicenseClasssInfo.DeafualtValidityLength);
            License.IssueReason = clsLicense.enIssueReason.FirstTime;
            License.IsActive = true;
            License.PaidFees = this.LicenseClasssInfo.ClassFees;
            License.Notes = Notes;
            License.CreatedByUserID = CreatedByUserID;

            if (License.Save())
            {
                //change application status from new to completed
                this.SetCompleted();

                return License.LicenseID;
            }
            else
                return -1;

        }
    }
}
