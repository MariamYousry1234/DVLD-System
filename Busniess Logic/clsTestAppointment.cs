using Data_Access;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Busniess_Logic
{
    public class clsTestAppointment
    {
        private enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;

        public int TestAppointmentID { set; get; }

        public DateTime AppointmentDate { set; get; }

        public float PaidFees { set; get; }

        public int LocalDrivingLicenseApplicationID { set; get; }
        public clsTestTypes.enTestType TestTypeID { set; get; }
        public string TestText
        {
            get
            {
                switch (TestTypeID)
                {
                    case clsTestTypes.enTestType.VisionTest:
                           return "Vision Test";
                    
  
                    case clsTestTypes.enTestType.WrittenTest:
                           return  "Written Test";
                      


                    case clsTestTypes.enTestType.PracticalTest:           
                           return "Street Test";

                    default:
                        return "UnKnown";
                 
                }

            }
        }

       
        public int CreatedByUserID { set; get; }
        public clsUser UserInfo;
        public bool IsLocked { set; get; }

        public int ReTakeTestApplicationID { set; get; }
        public  clsApplication TeTakeTestApplicationInfo;

        public int TestID
        { 
            get 
            { 
                return GetTestIDForAppointment(); 
            } 
        }

        public clsTestAppointment()
        {
            this.TestAppointmentID = -1;
            this.AppointmentDate = DateTime.Now;
            this.PaidFees = 0;
            this.LocalDrivingLicenseApplicationID = -1;
            this.TestTypeID = clsTestTypes.enTestType.VisionTest;
            this.CreatedByUserID = -1;
            this.IsLocked = false;
            this.ReTakeTestApplicationID = -1;

            _Mode = enMode.AddNew;
        }



        private clsTestAppointment(
            int TestAppointmentID,
            DateTime AppointmentDate,
            float PaidFees,
            int LocalDrivingLicenseApplicationID,
            clsTestTypes.enTestType TestTypeID,
            int CreatedByUserID,
            bool IsLocked, int ReTakeTestApplicationID)
        {
            this.TestAppointmentID = TestAppointmentID;
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.TestTypeID = TestTypeID;
            this.CreatedByUserID = CreatedByUserID;
            this.UserInfo = clsUser.Find(CreatedByUserID);
            this.IsLocked = IsLocked;
            this.ReTakeTestApplicationID = ReTakeTestApplicationID;
            this.TeTakeTestApplicationInfo = clsApplication.Find(ReTakeTestApplicationID);

            _Mode = enMode.Update;
        }



        private bool _AddNewTestAppointment()
        {
            this.TestAppointmentID =
                clsTestAppointmentDataAccess.AddNewTestAppointment(
                    (int)this.TestTypeID,
                    this.LocalDrivingLicenseApplicationID,
                    this.AppointmentDate,
                    this.PaidFees,
                    this.CreatedByUserID,
                    this.IsLocked,this.ReTakeTestApplicationID);

            return (this.TestAppointmentID != -1);
        }



        private bool _UpdateTestAppointment()
        {
            return clsTestAppointmentDataAccess.UpdateTestAppointment(
                this.TestAppointmentID,
                (int)this.TestTypeID,
                this.LocalDrivingLicenseApplicationID,
                this.AppointmentDate,
                this.PaidFees,
                this.CreatedByUserID,
                this.IsLocked,this.ReTakeTestApplicationID);
        }



        public static clsTestAppointment Find(int TestAppointmentID)
        {
            int TestTypeID = -1;
            int LocalDrivingLicenseApplicationID = -1;
            DateTime AppointmentDate = DateTime.Now;
            float PaidFees = 0;
            int CreatedByUserID = -1;
            bool IsLocked = false;
            int ReTakeTestApplicationID = -1;

            if (clsTestAppointmentDataAccess.FindTestAppointment(
                TestAppointmentID,
                ref TestTypeID,
                ref LocalDrivingLicenseApplicationID,
                ref AppointmentDate,
                ref PaidFees,
                ref CreatedByUserID,
                ref IsLocked,ref ReTakeTestApplicationID))
            {

                return new clsTestAppointment(
                    TestAppointmentID,
                    AppointmentDate,
                    PaidFees,
                    LocalDrivingLicenseApplicationID,
                    (clsTestTypes.enTestType) TestTypeID,
                    CreatedByUserID,
                    IsLocked, ReTakeTestApplicationID);
            }

            return null;
        }



        public bool Save()
        {
            switch (_Mode)
            {

                case enMode.AddNew:

                    if (_AddNewTestAppointment())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }

                    return false;



                case enMode.Update:

                    return _UpdateTestAppointment();

            }

            return false;
        }



        public static bool DeleteTestAppointment(int TestAppointmentID)
        {
            return clsTestAppointmentDataAccess
                .DeleteTestAppointment(TestAppointmentID);
        }



        public static bool IsTestAppointmentExist(int TestAppointmentID)
        {
            return clsTestAppointmentDataAccess
                .IsTestAppointmentExist(TestAppointmentID);
        }



        public static DataTable GetAllTestAppointments()
        {
            return clsTestAppointmentDataAccess
                .GetAllTestAppointments();
        }


        public static bool IsHasActiveTestAppointment(int LocalDrivingApplicationID, clsTestTypes.enTestType TestType)
        {
           return  clsTestAppointmentDataAccess.IsHasActiveTestAppointment(LocalDrivingApplicationID,(int) TestType);
        }

       
        public static int NumberOfTrailsForTest(int LocalDrivingLicenseID, clsTestTypes.enTestType TestTypeID)
        {
            return clsTestAppointmentDataAccess.NumberOfTrailsForTest(LocalDrivingLicenseID,(int) TestTypeID);
        }


        public static DataTable GetAllAppointmentsForTest(int LocalDrivingapplicationID,
         clsTestTypes.enTestType TestTypeID)
        {
           return  clsTestAppointmentDataAccess.GetAllAppointmentsForTest(LocalDrivingapplicationID,
               (int) TestTypeID);
        }

        public static bool DoesAttentTestBefore(int LocalDrivingLicenseApplicationID,clsTestTypes.enTestType TestType)
        {
            return clsTestAppointmentDataAccess.DoesAttentTestBefore(LocalDrivingLicenseApplicationID,(int) TestType);
        }


        public int GetTestIDForAppointment()
        {
            return clsTestAppointmentDataAccess.GetTestIDForAppointment(this.TestAppointmentID);
        }
    }
}
