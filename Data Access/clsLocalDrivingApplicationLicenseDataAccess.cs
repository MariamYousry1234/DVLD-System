using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;


namespace Data_Access
{
    public  class clsLocalDrivingApplicationLicenseDataAccess
    {
        public static int AddNewLocalDrivingApplicationLicense(int ApplicationID, int LicenseClassID)
        {
            int AutoNumber = -1;

            SqlConnection Connection =
               new SqlConnection(clsDataSettings.ConnectionString);

            string Query = @"INSERT INTO [dbo].[LocalDrivingLicenseApplications]
           ([ApplicationID]
           ,[LicenseClassID])
         VALUES
           (@ApplicationID
           ,@LicenseClassID);

                             SELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            Command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            

            try
            {
                Connection.Open();

                object Result = Command.ExecuteScalar();

                if (Result != null &&
                    int.TryParse(Result.ToString(), out int InsertedID))
                {
                    AutoNumber = InsertedID;
                }
            }
            catch (Exception e)
            {
                
            }
            finally
            {
                Connection.Close();

             
            }

            return AutoNumber;
        }


        public static bool UpdateLocalDrivingLicenseApplication(int LocalDrivingApplicationLicenseID,
           int ApplicationID, int LicenseClassID)
        {

            int RowsAffected = 0;

            SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString);

            string Query = "UPDATE [dbo].[LocalDrivingLicenseApplications]\r\n " +
                "  SET [ApplicationID] = @ApplicationID\r\n      " +
                ",[LicenseClassID] =@LicenseClassID\r\n" +
                " WHERE LocalDrivingApplicationLicenseID=@LocalDrivingApplicationLicenseID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@LocalDrivingApplicationLicenseID", LocalDrivingApplicationLicenseID);
            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            Command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);


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


        public static bool Find(int LocalDrivingApplicationLicenseID,
           ref int ApplicationID,ref int LicenseClassID)
        {
            bool IsFound = false;

            SqlConnection Connection =
                new SqlConnection(clsDataSettings.ConnectionString);

            string Query = "select * from LocalDrivingLicenseApplications where LocalDrivingLicenseApplicationID= @LocalDrivingApplicationLicenseID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@LocalDrivingApplicationLicenseID", LocalDrivingApplicationLicenseID);

            try
            {
                Connection.Open();

                SqlDataReader Data = Command.ExecuteReader();

                if (Data.Read())
                {
                    IsFound = true;

                    ApplicationID = (int)Data["ApplicationID"];
                    LicenseClassID = (int)Data["LicenseClassID"];
                }
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

        public static bool Find(ref int LocalDrivingApplicationLicenseID,
         int ApplicationID, ref int LicenseClassID)
        {
            bool IsFound = false;

            SqlConnection Connection =
                new SqlConnection(clsDataSettings.ConnectionString);

            string Query = @"select * from LocalDrivingLicenseApplications
               where ApplicationID= @ApplicationID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                Connection.Open();

                SqlDataReader Data = Command.ExecuteReader();

                if (Data.Read())
                {
                    IsFound = true;

                    LocalDrivingApplicationLicenseID = (int)Data["LocalDrivingLicenseApplicationID"];
                    LicenseClassID = (int)Data["LicenseClassID"];
                }
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


        public static DataTable GetLocalApplicationsList()
        {
            DataTable ApplicationsList = new DataTable();

            SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString);

            string Query = "select * from  LocalDrivingLicenseApplicationView ";

            SqlCommand Command = new SqlCommand(Query, Connection);

            try
            {
                Connection.Open();

                SqlDataReader Data = Command.ExecuteReader();
                ApplicationsList.Load(Data);
                Data.Close();
            }
            catch (Exception e)
            {
            
            }
            finally
            {
                Connection.Close();
            }

            return ApplicationsList;
        }

 
        public static bool DeleteLocalDrivingApplication(int LocalDrivingApplicationID)
        {
            int RowAffected = 0;

            SqlConnection Connection =
                new SqlConnection(clsDataSettings.ConnectionString);

            string Query = @"
               DELETE FROM [dbo].[LocalDrivingLicenseApplications]
                WHERE LocalDrivingLicenseApplicationID=@LocalDrivingApplicationID";
            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@LocalDrivingApplicationID", LocalDrivingApplicationID);

            try
            {
                Connection.Open();

                RowAffected = Command.ExecuteNonQuery();

              
            }
            catch (Exception e)
            {
               
            }
            finally
            {
                Connection.Close();
            }

            return RowAffected>0;

        }

        public static bool CancelLocalDrivingLicenseApplication(int LocalDrivingLicenseID)
        {
            int RowAffected = 0;

            SqlConnection Connection =
                new SqlConnection(clsDataSettings.ConnectionString);

            string Query = @"
              update Applications
             set ApplicationStatus=2 , LastStatusDate=GetDate()
             from LocalDrivingLicenseApplications join Applications
             on LocalDrivingLicenseApplications.ApplicationID=Applications.ApplicationID
              where LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID=@LocalDrivingLicenseID
             ";
            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@LocalDrivingLicenseID", LocalDrivingLicenseID);

            try
            {
                Connection.Open();

                RowAffected = Command.ExecuteNonQuery();

            }
            catch (Exception e)
            {
               
            }
            finally
            {
                Connection.Close();
            }

            return RowAffected > 0;

        }


    }
}
