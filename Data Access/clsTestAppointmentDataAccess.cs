using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access
{
    public class clsTestAppointmentDataAccess
    {
        public static bool FindTestAppointment(
           int TestAppointmentID,
           ref int TestTypeID,
           ref int LocalDrivingLicenseApplicationID,
           ref DateTime AppointmentDate,
           ref float PaidFees,
           ref int CreatedByUserID,
           ref bool IsLocked,ref int ReTakeTestApplicationID)
        {
            bool IsFound = false;

            SqlConnection Connection =
                new SqlConnection(clsDataSettings.ConnectionString);

            string Query =
                "SELECT * FROM TestAppointments " +
                "WHERE TestAppointmentID=@TestAppointmentID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue(
                "@TestAppointmentID",
                TestAppointmentID);

            try
            {
                Connection.Open();

                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    IsFound = true;

                    TestTypeID =
                        (int)Reader["TestTypeID"];

                    LocalDrivingLicenseApplicationID =
                        (int)Reader["LocalDrivingLicenseApplicationID"];

                    AppointmentDate =
                        (DateTime)Reader["AppointmentDate"];

                    PaidFees =
                        Convert.ToSingle(Reader["PaidFees"]);

                    CreatedByUserID =
                        (int)Reader["CreatedByUserID"];

                    IsLocked =
                        (bool)Reader["IsLocked"];

                    ReTakeTestApplicationID =
                        Reader["ReTakeTestApplicationID"] == DBNull.Value ? -1 : (int)Reader["ReTakeTestApplicationID"];
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



        public static int AddNewTestAppointment(
            int TestTypeID,
            int LocalDrivingLicenseApplicationID,
            DateTime AppointmentDate,
            float PaidFees,
            int CreatedByUserID,
            bool IsLocked,int ReTakeTestApplicationID)
        {
            int TestAppointmentID = -1;

            SqlConnection Connection =
                new SqlConnection(clsDataSettings.ConnectionString);

            string Query =
                @"INSERT INTO [dbo].[TestAppointments]
           ([AppointmentDate]
           ,[PaidFees]
           ,[IsLocked]
           ,[LocalDrivingLicenseApplicationID]
           ,[TestTypeID]
           ,[CreatedByUserID]
           ,[RetakeTestApplicationID])
            VALUES
           (@AppointmentDate
           ,@PaidFees
           ,@IsLocked
           ,@LocalDrivingLicenseApplicationID
           ,@TestTypeID
           ,@CreatedByUserID
           ,@RetakeTestApplicationID)

            SELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            Command.Parameters.AddWithValue(
                "@LocalDrivingLicenseApplicationID",
                LocalDrivingLicenseApplicationID);

            Command.Parameters.AddWithValue(
                "@AppointmentDate",
                AppointmentDate);

            Command.Parameters.AddWithValue(
                "@PaidFees",
                PaidFees);

            Command.Parameters.AddWithValue(
                "@CreatedByUserID",
                CreatedByUserID);

            Command.Parameters.AddWithValue(
                "@IsLocked",
                IsLocked);

            if(ReTakeTestApplicationID != -1)
            {
                Command.Parameters.AddWithValue(
                     "@ReTakeTestApplicationID",
                   ReTakeTestApplicationID);
            }
            else
            {
                Command.Parameters.AddWithValue(
                     "@ReTakeTestApplicationID",
                      DBNull.Value);
            }

                try
                {
                    Connection.Open();

                    object Result = Command.ExecuteScalar();

                    if (Result != null &&
                        int.TryParse(Result.ToString(), out int ID))
                    {
                        TestAppointmentID = ID;
                    }
                }
                catch (Exception e)
                {
                
            }
                finally
                {
                    Connection.Close();
                }

            return TestAppointmentID;
        }



        public static bool UpdateTestAppointment(
            int TestAppointmentID,
            int TestTypeID,
            int LocalDrivingLicenseApplicationID,
            DateTime AppointmentDate,
            float PaidFees,
            int CreatedByUserID,
            bool IsLocked, int ReTakeTestApplicationID )
        {
            int RowsAffected = 0;

            SqlConnection Connection =
                new SqlConnection(clsDataSettings.ConnectionString);

            string Query =
                @"UPDATE TestAppointments
                  SET
                  TestTypeID=@TestTypeID,
                  LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID,
                  AppointmentDate=@AppointmentDate,
                  PaidFees=@PaidFees,
                  CreatedByUserID=@CreatedByUserID,
                  IsLocked=@IsLocked,
                  ReTakeTestApplicationID=@ReTakeTestApplicationID
                  WHERE TestAppointmentID=@TestAppointmentID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue(
                "@TestAppointmentID",
                TestAppointmentID);

            Command.Parameters.AddWithValue(
                "@TestTypeID",
                TestTypeID);

            Command.Parameters.AddWithValue(
                "@LocalDrivingLicenseApplicationID",
                LocalDrivingLicenseApplicationID);

            Command.Parameters.AddWithValue(
                "@AppointmentDate",
                AppointmentDate);

            Command.Parameters.AddWithValue(
                "@PaidFees",
                PaidFees);

            Command.Parameters.AddWithValue(
                "@CreatedByUserID",
                CreatedByUserID);

            Command.Parameters.AddWithValue(
                "@IsLocked",
                IsLocked);

            if (ReTakeTestApplicationID != -1)
            {
                Command.Parameters.AddWithValue(
                     "@ReTakeTestApplicationID",
                   ReTakeTestApplicationID);
            }
            else
            {
                Command.Parameters.AddWithValue(
                     "@ReTakeTestApplicationID",
                      DBNull.Value);
            }


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



        public static bool DeleteTestAppointment(
            int TestAppointmentID)
        {
            int RowsAffected = 0;

            SqlConnection Connection =
                new SqlConnection(clsDataSettings.ConnectionString);

            string Query =
                @"DELETE FROM TestAppointments
                  WHERE TestAppointmentID=@TestAppointmentID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue(
                "@TestAppointmentID",
                TestAppointmentID);

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



        public static bool IsTestAppointmentExist(int TestAppointmentID)
        {
            bool IsExist = false;

            SqlConnection Connection =
                new SqlConnection(clsDataSettings.ConnectionString);

            string Query =
                @"SELECT Found=1
                  FROM TestAppointments
                  WHERE TestAppointmentID=@TestAppointmentID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue(
                "@TestAppointmentID",
                TestAppointmentID);

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



        public static DataTable GetAllTestAppointments()
        {
            DataTable dt = new DataTable();

            SqlConnection Connection =
                new SqlConnection(clsDataSettings.ConnectionString);

            string Query =
                @"SELECT *
                  FROM TestAppointments";

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

        public static DataTable GetAllAppointmentsForTest(int LocalDrivingapplicationID,
            int TestTypeID)
        {
            DataTable dt = new DataTable();

            SqlConnection Connection =
                new SqlConnection(clsDataSettings.ConnectionString);

            string Query =
                @"
              select TestAppointmentID,AppointmentDate,
              PaidFees,IsLocked from TestAppointments
              where LocalDrivingLicenseApplicationID =@LocalDrivingapplicationID
              and TestTypeID=@TestTypeID 
             order by TestAppointmentID desc";

            SqlCommand Command =
                new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@LocalDrivingapplicationID", LocalDrivingapplicationID);
            Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);


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
        
       
        public static bool IsHasActiveTestAppointment(int LocalDrivingApplicationID, int TestType)
        {
            bool IsHasappointment = false;
         
            SqlConnection Connection =
                new SqlConnection(clsDataSettings.ConnectionString);

            string Query =
                @"select top 1 1 from TestAppointments
                where LocalDrivingLicenseApplicationID=@LocalDrivingApplicationID
                and TestTypeID=@TestType and IsLocked=0
                order by TestAppointments.TestAppointmentID desc";

            SqlCommand Command =new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@LocalDrivingApplicationID", LocalDrivingApplicationID);
            Command.Parameters.AddWithValue("@TestType", TestType);

            try
            {
                Connection.Open();

                object Reader =Command.ExecuteScalar();

                IsHasappointment = (Reader != null);
            }
            catch (Exception e)
            {
              

            }
            finally
            {
                Connection.Close();
            }

            return IsHasappointment;

        }


        public static int NumberOfTrailsForTest(int LocalDrivingLicenseID, int TestTypeID)
        {
             int NumberOfTrails = 0;

            SqlConnection Connection =
                new SqlConnection(clsDataSettings.ConnectionString);

            string Query = @"
                 select count(Tests.TestID) 
                 from TestAppointments join Tests
                 ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                 where TestAppointments.LocalDrivingLicenseApplicationID=@LocalDrivingLicenseID 
                  and TestAppointments.TestTypeID=@TestTypeID";


            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@LocalDrivingLicenseID", LocalDrivingLicenseID);
            Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);


            try
            {
                Connection.Open();

                object Data = Command.ExecuteScalar();

                NumberOfTrails = (int)Data;

            }
            catch (Exception e)
            {
              
            }
            finally
            {
                Connection.Close();
            }

            return NumberOfTrails;

        }

       public static  bool DoesAttentTestBefore(int LocalDrivingLicenseApplicationID, int TestType)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataSettings.ConnectionString);

            string query = @"     
                   select top 1 Tests.TestID from TestAppointments join Tests
                   on TestAppointments.TestAppointmentID=Tests.TestAppointmentID
                   where TestAppointments.LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID
                   and TestAppointments.TestTypeID=@TestType
                   order by TestAppointments.TestAppointmentID desc";
              
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestType", TestType);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                {
                    IsFound = true;
                }
            }

            catch (Exception e)
            {
              
            }

            finally
            {
                connection.Close();
            }

            return IsFound;
        }

        public static int GetTestIDForAppointment(int AppointmentID)
        {
            int TestID = -1;

            SqlConnection Connection =
                new SqlConnection(clsDataSettings.ConnectionString);

            string Query = @"select TestID
                   from Tests  
                   where TestAppointmentID=@AppointmentID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@AppointmentID", AppointmentID);


            try
            {
                Connection.Open();

                object Data = Command.ExecuteScalar();

                if (Data != null && int.TryParse(Data.ToString(), out int Result))
                    TestID = Result;

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
    }
}
