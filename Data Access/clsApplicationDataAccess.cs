using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data_Access
{
    public class clsApplicationDataAccess
    {
     
        public static bool Find(int ApplicationID,ref int PersonID,ref DateTime ApplicationDate,
            ref int ApplicationTypeID, ref int ApplicationStaus,
            ref  DateTime LastStatusDate,ref float ApplicationFees,
            ref int CreatedByUserID)
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString);

            string Query = "select * from Applications where ApplicationID=@ApplicationID";
            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);


            try
            {
                Connection.Open();

                SqlDataReader Data = Command.ExecuteReader();

                if (Data.Read())
                {
                    IsFound = true;

                    PersonID =(int) Data["PersonID"];
                    ApplicationDate = Convert.ToDateTime(Data["ApplicationDate"]);
                    ApplicationTypeID = (int)Data["ApplicationTypeID"];
                    ApplicationStaus = Convert.ToInt32(Data["ApplicationStatus"]);
                    LastStatusDate = Convert.ToDateTime(Data["LastStatusDate"]);
                    ApplicationFees = Convert.ToSingle(Data["ApplicationFees"]);
                    CreatedByUserID = (int) Data["CreatedByUserID"];
                }
                Data.Close();
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


        public static int AddNewApplication(int PersonID,DateTime ApplicationDate,
            int ApplicationTypeID,
           int ApplicationStaus, DateTime LastStatusDate, float ApplicationFees,
           int CreatedByUserID)
        {

            int ApplicationID = -1;

            SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString);

            string Query = "INSERT INTO [dbo].[Applications]\r\n          " +
                " ([PersonID],[ApplicationDate]\r\n         " +
                "  ,[ApplicationTypeID]\r\n     " +
                "      ,[ApplicationStatus]\r\n        " +
                "   ,[LastStatusDate]\r\n    " +
                "       ,[ApplicationFees]\r\n        " +
                "   ,[CreatedByUserID])\r\n     " +
                "VALUES (@PersonID,@ApplicationDate, @ApplicationTypeID, @ApplicationStaus ,@LastStatusDate,@ApplicationFees ,@CreatedByUserID )" +
                "SELECT SCOPE_IDENTITY();";
            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@PersonID", PersonID);
            Command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            Command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            Command.Parameters.AddWithValue("@ApplicationStaus", ApplicationStaus);

            Command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
          

            Command.Parameters.AddWithValue("@ApplicationFees", ApplicationFees);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);


            try
                {
                    Connection.Open();

                    object AutoNumber = Command.ExecuteScalar();

                    if (AutoNumber != null && int.TryParse(AutoNumber.ToString(), out int Id))
                        ApplicationID = Id;

                }
                catch (Exception e)
                {
               
            }
                finally
                {
                    Connection.Close();
                }


            return ApplicationID;
        }


        public static bool UpdateApplication(int ApplicationID,int PersonID,
            DateTime ApplicationDate, int ApplicationTypeID,
           int ApplicationStaus, DateTime LastStatusDate, 
           float ApplicationFees,
           int CreatedByUserID)
        {
            int RowsAffected = 0;

            SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString);

            string Query = "UPDATE [dbo].[Applications]\r\n  " +
                " SET [PersonID] =@PersonID" +
                ",[ApplicationDate] = @ApplicationDate\r\n  " +
                "    ,[ApplicationTypeID] = @ApplicationTypeID\r\n    " +
                "  ,[ApplicationStatus] = @ApplicationStaus\r\n    " +
                "  ,[LastStatusDate] = @LastStatusDate\r\n  " +
                "    ,[ApplicationFees] = @ApplicationFees\r\n   " +
                "   ,[CreatedByUserID] = @CreatedByUserID\r\n " +
                "WHERE ApplicationID=@ApplicationID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@PersonID", PersonID);
            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            Command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            Command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            Command.Parameters.AddWithValue("@ApplicationStaus", ApplicationStaus);

          
             Command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
         

            Command.Parameters.AddWithValue("@ApplicationFees", ApplicationFees);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);


            try
            {
                Connection.Open();

                RowsAffected = Command.ExecuteNonQuery();

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

        public static bool DeleteApplication(int ApplicationID)
        {
            int RowsAffected = 0;

            SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString);

            string Query = "\r\nDELETE FROM [dbo].[Applications]\r\n      WHERE ApplicationID=@ApplicationID;\r\n";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                Connection.Open();

                RowsAffected = Command.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                
            }
            finally
            {
                Connection.Close();
            }

            return RowsAffected > 0;
        }



        public static int GetActiveApplicationForPerson(int PersonID, int ApplicationTypeID)
        {

            int FoundActiveApplicationID = -1;

            SqlConnection Connection =
                new SqlConnection(clsDataSettings.ConnectionString);

            string Query = @"
             select Applications.ApplicationTypeID 
               from Applications
              where Applications.PersonID = @PersonID and
              Applications.ApplicationTypeID=@ApplicationTypeID and      
            and Applications.ApplicationStatus = 1";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@PersonID", PersonID);
            Command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

            try
            {
                Connection.Open();

                object Result = Command.ExecuteScalar();

                if (Result != null &&
                    int.TryParse(Result.ToString(), out int InsertedID))
                {
                    FoundActiveApplicationID = InsertedID;
                }
            }
            catch (Exception e)
            {
              
            }
            finally
            {
                Connection.Close();
            }

            return FoundActiveApplicationID;

        }
        public static int CheckApplicationActivation(int PersonID, int ApplicationTypeID, int ClassID)
        {
            int FoundActiveApplicationID = -1;

            SqlConnection Connection =
                new SqlConnection(clsDataSettings.ConnectionString);

            string Query = @"
             select LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID 
               from LocalDrivingLicenseApplications join Applications
             on LocalDrivingLicenseApplications.ApplicationID=Applications.ApplicationID
             where Applications.PersonID = @PersonID and
              Applications.ApplicationTypeID=@ApplicationTypeID and
                 LocalDrivingLicenseApplications.LicenseClassID=@ClassID
            and Applications.ApplicationStatus = 1";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@PersonID", PersonID);
            Command.Parameters.AddWithValue("@ClassID", ClassID);
            Command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

            try
            {
                Connection.Open();

                object Result = Command.ExecuteScalar();

                if (Result != null &&
                    int.TryParse(Result.ToString(), out int InsertedID))
                {
                    FoundActiveApplicationID = InsertedID;
                }
            }
            catch (Exception e)
            {
               
            }
            finally
            {
                Connection.Close();
            }

            return FoundActiveApplicationID;

        }

        public static bool UpdateStatus(int NewStatus,int ApplicationID)
        {
            int RowsAffected = 0;

            SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString);

            string Query = @"update Applications
                 set ApplicationStatus=@NewStatus,
                    LastStatusDate = @LastStatusDate
                   where ApplicationID=@ApplicationID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@NewStatus", NewStatus);
            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            Command.Parameters.AddWithValue("@LastStatusDate", DateTime.Now);


            try
            {
                Connection.Open();

                RowsAffected = Command.ExecuteNonQuery();

            }
            catch (Exception e)
            {
               
            }
            finally
            {
                Connection.Close();
            }

            return RowsAffected > 0;

        }
     }
}
