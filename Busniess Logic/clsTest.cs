using Data_Access;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Busniess_Logic
{
    public class clsTest
    {
        private enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;
        public int TestID { set; get; }

        public bool TestResult { set; get; }

        public string Notes { set; get; }

        public int TestAppointmentID { set; get; }
        public clsTestAppointment TestAppointmentInfo;

        public int CreatedByUserID { set; get; }
        public clsUser UserInfo;

        public clsTest()
        {
            this.TestID = -1;
            this.TestResult = false;
            this.Notes = "";
            this.TestAppointmentID = -1;
            this.CreatedByUserID = -1;

            _Mode = enMode.AddNew;
        }



        private clsTest(
            int TestID,
            bool TestResult,
            string Notes,
            int TestAppointmentID,
            int CreatedByUserID)
        {
            this.TestID = TestID;
            this.TestResult = TestResult;
            this.Notes = Notes;
            this.TestAppointmentID = TestAppointmentID;
            this.TestAppointmentInfo =
                clsTestAppointment.Find(TestAppointmentID);

            this.CreatedByUserID = CreatedByUserID;
            this.UserInfo =
                clsUser.Find(CreatedByUserID);

            _Mode = enMode.Update;
        }



        private bool _AddNewTest()
        {
            this.TestID =
                clsTestDataAccess.AddNewTest(
                    this.TestResult,
                    this.Notes,
                    this.TestAppointmentID,
                    this.CreatedByUserID);

            return (this.TestID != -1);
        }



        private bool _UpdateTest()
        {
            return clsTestDataAccess.UpdateTest(
                this.TestID,
                this.TestResult,
                this.Notes,
                this.TestAppointmentID,
                this.CreatedByUserID);
        }



        public static clsTest Find(int TestID)
        {
            bool TestResult = false;
            string Notes = "";
            int TestAppointmentID = -1;
            int CreatedByUserID = -1;

            if (clsTestDataAccess.FindTest(
                TestID,
                ref TestResult,
                ref Notes,
                ref TestAppointmentID,
                ref CreatedByUserID))
            {

                return new clsTest(
                    TestID,
                    TestResult,
                    Notes,
                    TestAppointmentID,
                    CreatedByUserID);
            }

            return null;
        }


        public static clsTest GetLastTest(int LocalDrivingLicenseApplicationID,int TestTypeID)
        {
            int TestID = -1;
            bool TestResult = false;
            string Notes = "";
            int TestAppointmentID = -1;
            int CreatedByUserID = -1;

            if (clsTestDataAccess.FindLastTest(LocalDrivingLicenseApplicationID, TestTypeID,
                ref  TestID,
                ref TestResult,
                ref Notes,
                ref TestAppointmentID,
                ref CreatedByUserID))
            {

                return new clsTest(
                    TestID,
                    TestResult,
                    Notes,
                    TestAppointmentID,
                    CreatedByUserID);
            }

            return null;
        }
        public bool Save()
        {
            switch (_Mode)
            {

                case enMode.AddNew:

                    if (_AddNewTest())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }

                    return false;



                case enMode.Update:

                    return _UpdateTest();

            }

            return false;
        }


        public static bool DeleteTest(int TestID)
        {
            return clsTestDataAccess.DeleteTest(TestID);
        }


        public static bool IsTestExist(int TestID)
        {
            return clsTestDataAccess.IsTestExist(TestID);
        }


        public static DataTable GetAllTests()
        {
            return clsTestDataAccess.GetAllTests();
        }


        public static byte GetNumberOfPassedTests(int LocalApplicationID)
        {
            return clsTestDataAccess.GetNumberOfPassedTests(LocalApplicationID);
        }

        public static bool IsPassedAllTests(int LocalApplicationID)
        {
            return (GetNumberOfPassedTests(LocalApplicationID) == 3);
        }

        public static bool IsPassedThisTest(int LDLAppID, clsTestTypes.enTestType TestTypeID)
        {
           return  clsTestDataAccess.IsPassedThisTest(LDLAppID, (int)TestTypeID);
        }


     }
}
