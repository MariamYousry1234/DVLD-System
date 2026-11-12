using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data_Access
{
    public class clsTestDataAccess
    {
       
            public static bool FindTest(
                int TestID,
                ref bool TestResult,
                ref string Notes,
                ref int TestAppointmentID,
                ref int CreatedByUserID)
            {
                bool IsFound = false;

                SqlConnection Connection =
                    new SqlConnection(clsDataSettings.ConnectionString);

                string Query =
                    @"SELECT *
                  FROM Tests
                  WHERE TestID=@TestID";

                SqlCommand Command =
                    new SqlCommand(Query, Connection);

                Command.Parameters.AddWithValue("@TestID", TestID);

                try
                {
                    Connection.Open();

                    SqlDataReader Reader =
                        Command.ExecuteReader();

                    if (Reader.Read())
                    {
                        IsFound = true;

                        TestResult =
                            (bool)Reader["TestResult"];

                        Notes =
                            Reader["Notes"] == DBNull.Value ?
                            "" : (string)Reader["Notes"];

                        TestAppointmentID =
                            (int)Reader["TestAppointmentID"];

                        CreatedByUserID =
                            (int)Reader["CreatedByUserID"];
                    }

                    Reader.Close();
                }
                catch (Exception e)
                {
                
            }
                finally
                {
                    Connection.Close();
                }

                return IsFound;
            }

        public static bool FindLastTest(int LocalDrivingLicenseApplicationID,int TestTypeID,
             ref int TestID,
             ref bool TestResult,
             ref string Notes,
             ref int TestAppointmentID,
             ref int CreatedByUserID)
        {
            bool IsFound = false;

            SqlConnection Connection =
                new SqlConnection(clsDataSettings.ConnectionString);

            string Query = @"SELECT  top 1 Tests.TestID, 
                Tests.TestAppointmentID, Tests.TestResult, 
			    Tests.Notes, Tests.CreatedByUserID
                FROM        
                                         Tests INNER JOIN
                                         TestAppointments ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID 
                                     
                WHERE       TestAppointments.LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID
                        AND ( TestAppointments.TestTypeID=@TestTypeID)
                ORDER BY Tests.TestAppointmentID DESC";
            SqlCommand Command =
                new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                Connection.Open();

                SqlDataReader Reader =
                    Command.ExecuteReader();

                if (Reader.Read())
                {
                    IsFound = true;

                    TestID = (int)Reader["TestID"];

                    TestResult =
                        (bool)Reader["TestResult"];

                    Notes =
                        Reader["Notes"] == DBNull.Value ?
                        "" : (string)Reader["Notes"];

                    TestAppointmentID =
                        (int)Reader["TestAppointmentID"];

                    CreatedByUserID =
                        (int)Reader["CreatedByUserID"];
                }

                Reader.Close();
            }
            catch (Exception e)
            {
              
            }
            finally
            {
                Connection.Close();
            }

            return IsFound;
        }


        public static int AddNewTest(
                bool TestResult,
                string Notes,
                int TestAppointmentID,
                int CreatedByUserID)
            {
                int TestID = -1;

                SqlConnection Connection =
                    new SqlConnection(clsDataSettings.ConnectionString);

                string Query =
                    @"INSERT INTO Tests
                (
                    TestResult,
                    Notes,
                    TestAppointmentID,
                    CreatedByUserID
                )
                VALUES
                (
                    @TestResult,
                    @Notes,
                    @TestAppointmentID,
                    @CreatedByUserID
                );

               
               update TestAppointments
               set IsLocked=1
               where TestAppointmentID=@TestAppointmentID

                SELECT SCOPE_IDENTITY();";

                SqlCommand Command =
                    new SqlCommand(Query, Connection);

                Command.Parameters.AddWithValue(
                    "@TestResult",
                    TestResult);

                if (!string.IsNullOrEmpty(Notes))
                    Command.Parameters.AddWithValue(
                        "@Notes",
                        Notes);
                else
                    Command.Parameters.AddWithValue(
                        "@Notes",
                        DBNull.Value);

                Command.Parameters.AddWithValue(
                    "@TestAppointmentID",
                    TestAppointmentID);

                Command.Parameters.AddWithValue(
                    "@CreatedByUserID",
                    CreatedByUserID);

                try
                {
                    Connection.Open();

                    object Result =
                        Command.ExecuteScalar();

                    if (Result != null &&
                        int.TryParse(Result.ToString(), out int ID))
                    {
                        TestID = ID;
                    }
                }
                catch (Exception e)
                {
              
            }
                finally
                {
                    Connection.Close();
                }

                return TestID;
            }



            public static bool UpdateTest(
                int TestID,
                bool TestResult,
                string Notes,
                int TestAppointmentID,
                int CreatedByUserID)
            {
                int RowsAffected = 0;

                SqlConnection Connection =
                    new SqlConnection(clsDataSettings.ConnectionString);

                string Query =
                    @"UPDATE Tests
                  SET
                  TestResult=@TestResult,
                  Notes=@Notes,
                  TestAppointmentID=@TestAppointmentID,
                  CreatedByUserID=@CreatedByUserID
                  WHERE TestID=@TestID";

                SqlCommand Command =
                    new SqlCommand(Query, Connection);

                Command.Parameters.AddWithValue(
                    "@TestID",
                    TestID);

                Command.Parameters.AddWithValue(
                    "@TestResult",
                    TestResult);

                if (!string.IsNullOrEmpty(Notes))
                    Command.Parameters.AddWithValue(
                        "@Notes",
                        Notes);
                else
                    Command.Parameters.AddWithValue(
                        "@Notes",
                        DBNull.Value);

                Command.Parameters.AddWithValue(
                    "@TestAppointmentID",
                    TestAppointmentID);

                Command.Parameters.AddWithValue(
                    "@CreatedByUserID",
                    CreatedByUserID);

                try
                {
                    Connection.Open();

                    RowsAffected =
                        Command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
             
            }
                finally
                {
                    Connection.Close();
                }

                return (RowsAffected > 0);
            }



            public static bool DeleteTest(int TestID)
            {
                int RowsAffected = 0;

                SqlConnection Connection =
                    new SqlConnection(clsDataSettings.ConnectionString);

                string Query =
                    @"DELETE FROM Tests
                  WHERE TestID=@TestID";

                SqlCommand Command =
                    new SqlCommand(Query, Connection);

                Command.Parameters.AddWithValue(
                    "@TestID",
                    TestID);

                try
                {
                    Connection.Open();

                    RowsAffected =
                        Command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
               
            }
                finally
                {
                    Connection.Close();
                }

                return (RowsAffected > 0);
            }



            public static bool IsTestExist(int TestID)
            {
                bool IsExist = false;

                SqlConnection Connection =
                    new SqlConnection(clsDataSettings.ConnectionString);

                string Query =
                    @"SELECT Found=1
                  FROM Tests
                  WHERE TestID=@TestID";

                SqlCommand Command =
                    new SqlCommand(Query, Connection);

                Command.Parameters.AddWithValue(
                    "@TestID",
                    TestID);

                try
                {
                    Connection.Open();

                    object Result =
                        Command.ExecuteScalar();

                    IsExist = (Result != null);
                }
                catch (Exception e)
               {
                

                }
                finally
                {
                    Connection.Close();
                }

                return IsExist;
            }



            public static DataTable GetAllTests()
            {
                DataTable dt = new DataTable();

                SqlConnection Connection =
                    new SqlConnection(clsDataSettings.ConnectionString);

                string Query =
                    @"SELECT *
                  FROM Tests";

                SqlCommand Command =
                    new SqlCommand(Query, Connection);

                try
                {
                    Connection.Open();

                    SqlDataReader Reader =
                        Command.ExecuteReader();

                    dt.Load(Reader);

                    Reader.Close();
                }
                catch (Exception e)
                {
               
            }
                finally
                {
                    Connection.Close();
                }

                return dt;
            }


        public static byte GetNumberOfPassedTests(int LocalApplicationID)
        {
            byte NumberOfPassedTests = 0;

            SqlConnection Connection =
                new SqlConnection(clsDataSettings.ConnectionString);

            string Query = "select count(*) as NumberOfPassedTests\r\n" +
               "from  TestAppointments\r\n" +
               "join Tests\r\n" +
               "on TestAppointments.TestAppointmentID=Tests.TestAppointmentID\r\n" +
               "where TestAppointments.LocalDrivingLicenseApplicationID=@LocalApplicationID and\r\n" +
               "Tests.TestResult =1";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@LocalApplicationID", LocalApplicationID);

            try
            {
                Connection.Open();

                object Data = Command.ExecuteScalar();

                NumberOfPassedTests = Convert.ToByte(Data);

            }
            catch (Exception e)
            {
              
            }
            finally
            {
                Connection.Close();
            }

            return NumberOfPassedTests;
        }

        public static bool IsPassedThisTest(int LDLAppID,int TestTypeID)
        {
            bool IsPassed = false;

            SqlConnection Connection =
                new SqlConnection(clsDataSettings.ConnectionString);

            string Query = @"select top 1 Tests.TestResult from testAppointments join Tests
                on Testappointments.TestAppointmentID=Tests.TestAppointmentID
               where TestAppointments.LocalDrivingLicenseApplicationID=@LDLAppID
               and TestAppointments.TestTypeID =@TestTypeID 
               order by Testappointments.TestAppointmentID desc";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@LDLAppID", LDLAppID);
            Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                Connection.Open();

                object Data = Command.ExecuteScalar();

                if(Data != null && bool.TryParse(Data.ToString(),out bool Result))
                      IsPassed =Result;

            }
            catch (Exception e)
            {
              
            }
            finally
            {
                Connection.Close();
            }

            return IsPassed;
        }

    
       
    }
 }
