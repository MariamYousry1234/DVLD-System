using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data_Access
{
    public  class clsInternationalLicenseDataAccess
    {
        public static bool FindInternationalLicense(
    int InternationalLicenseID,
    ref DateTime IssueDate,
    ref DateTime ExpiryDate,
    ref bool IsActive,
    ref int ApplicationID,
    ref int DriverID,
    ref int LocalLicenseID,
    ref int CreatedByUserID)
        {
            bool IsFound = false;

            SqlConnection Connection =
                new SqlConnection(clsDataSettings.ConnectionString);

            string Query =
                @"select * from InternationalLicenses
          where InternationalLicenseID=@InternationalLicenseID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@InternationalLicenseID",
                InternationalLicenseID);

            try
            {
                Connection.Open();

                SqlDataReader Data = Command.ExecuteReader();

                if (Data.Read())
                {
                    IsFound = true;

          
                    IssueDate = Convert.ToDateTime(Data["IssueDate"]);
                    ExpiryDate = Convert.ToDateTime(Data["ExpiryDate"]);
                    IsActive = (bool)Data["IsActive"];
                    ApplicationID = (int)Data["ApplicationID"];
                    DriverID = (int)Data["DriverID"];
                    LocalLicenseID = (int)Data["LocalLicenseID"];
                    CreatedByUserID = (int)Data["CreatedByUserID"];
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

        public static int AddNewInternationalLicense(

     DateTime IssueDate,
     DateTime ExpiryDate,
     bool IsActive,
     int ApplicationID,
     int DriverID,
     int LocalLicenseID,
     int CreatedByUserID)
        {
            int InternationalLicenseID = -1;

            SqlConnection Connection =
                new SqlConnection(clsDataSettings.ConnectionString);

            string Query =
            @"update InternationalLicenses
           set IsActive=0
        where DriverID=@DriverID 

     INSERT INTO InternationalLicenses
    (
       
        IssueDate,
        ExpiryDate,
        IsActive,
        ApplicationID,
        DriverID,
        LocalLicenseID,
        CreatedByUserID
    )
    VALUES
    (
     
        @IssueDate,
        @ExpiryDate,
        @IsActive,
        @ApplicationID,
        @DriverID,
        @LocalLicenseID,
        @CreatedByUserID
    );

    SELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(Query, Connection);

       
            Command.Parameters.AddWithValue("@IssueDate", IssueDate);
            Command.Parameters.AddWithValue("@ExpiryDate", ExpiryDate);
            Command.Parameters.AddWithValue("@IsActive", IsActive);
            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            Command.Parameters.AddWithValue("@DriverID", DriverID);
            Command.Parameters.AddWithValue("@LocalLicenseID", LocalLicenseID);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                Connection.Open();

                object Result = Command.ExecuteScalar();

                if (Result != null &&
                    int.TryParse(Result.ToString(), out int ID))
                {
                    InternationalLicenseID = ID;
                }
            }
            catch (Exception e)
            {
              
            }
            finally
            {
                Connection.Close();
            }

            return InternationalLicenseID;
        }

        public static bool UpdateInternationalLicense(
             int InternationalLicenseID,
   
            DateTime IssueDate,
            DateTime ExpiryDate,
           bool IsActive,
          int ApplicationID,
          int DriverID,
            int LocalLicenseID,
            int CreatedByUserID)
        {
            int RowsAffected = 0;

            SqlConnection Connection =
                new SqlConnection(clsDataSettings.ConnectionString);

            string Query =
            @"UPDATE InternationalLicenses
      SET
     
        IssueDate=@IssueDate,
        ExpiryDate=@ExpiryDate,
        IsActive=@IsActive,
        ApplicationID=@ApplicationID,
        DriverID=@DriverID,
        LocalLicenseID=@LocalLicenseID,
        CreatedByUserID=@CreatedByUserID
      WHERE InternationalLicenseID=@InternationalLicenseID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);
  
            Command.Parameters.AddWithValue("@IssueDate", IssueDate);
            Command.Parameters.AddWithValue("@ExpiryDate", ExpiryDate);
            Command.Parameters.AddWithValue("@IsActive", IsActive);
            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            Command.Parameters.AddWithValue("@DriverID", DriverID);
            Command.Parameters.AddWithValue("@LocalLicenseID", LocalLicenseID);
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

            return RowsAffected > 0;
        }

        public static bool IsInternationalLicenseExists(int InternationalLicenseID)
        {
            bool IsExist = false;

            SqlConnection Connection =
                new SqlConnection(clsDataSettings.ConnectionString);

            string Query =
                @"select Found=1
          from InternationalLicenses
          where InternationalLicenseID=@InternationalLicenseID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@InternationalLicenseID",
                InternationalLicenseID);

            try
            {
                Connection.Open();

                object Data = Command.ExecuteScalar();

                IsExist = (Data != null);
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

        public static bool DeleteInternationalLicense(int InternationalLicenseID)
        {
            int RowsAffected = 0;

            SqlConnection Connection =
                new SqlConnection(clsDataSettings.ConnectionString);

            string Query =
                @"DELETE FROM InternationalLicenses
          WHERE InternationalLicenseID=@InternationalLicenseID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@InternationalLicenseID",
                InternationalLicenseID);

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

        public static DataTable GetAllInternationalLicenses()
        {
            DataTable dt = new DataTable();

            SqlConnection Connection =
                new SqlConnection(clsDataSettings.ConnectionString);

            string Query = "select * from InternationalLicenseView";

            SqlCommand Command = new SqlCommand(Query, Connection);

            try
            {
                Connection.Open();

                SqlDataReader Data = Command.ExecuteReader();

                dt.Load(Data);

                Data.Close();
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

        public static int GetActiveInternationalLicenseID(int DriverID)
        {
            int InternationalLicenseID = -1;

            SqlConnection Connection =
             new SqlConnection(clsDataSettings.ConnectionString);

            string Query =
                @"
       
           select top 1 InternationalLicenseID
            from InternationalLicenses
           where DriverID=@DriverID and 
           GetDate() between IssueDate and ExpiryDate 
           order by InternationalLicenseID desc";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@DriverID",
                DriverID);

            try
            {
                Connection.Open();

                object ILID = Command.ExecuteScalar();

                if(ILID !=null && int.TryParse(ILID.ToString(),out int ID))
                    {
                    InternationalLicenseID = ID;
                }
            }
            catch (Exception e)
            {
               
            }
            finally
            {
                Connection.Close();
            }

            return InternationalLicenseID;
        }

        public static DataTable GetInternationaLicenses(int DriverID)
        {
            DataTable ILicenses = new DataTable();

            SqlConnection Connection =
         new SqlConnection(clsDataSettings.ConnectionString);

            string Query =
                @"
               select InternationalLicenseID as 'Int LicenseID'
              ,ApplicationID as 'Application ID' ,LocalLicenseID as 'L LicenseID'
             ,IssueDate as 'Issue Date',
             ExpiryDate as 'Expiration Date',
              IsActive as 'Is Active'
             from InternationalLicenses
             where DriverID=@DriverID
              order by InternationalLicenseID desc ";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@DriverID",
                DriverID);

            try
            {
                Connection.Open();

                SqlDataReader Data = Command.ExecuteReader();

                ILicenses.Load(Data);

            }
            catch (Exception e)
            {
              
            }
            finally
            {
                Connection.Close();
            }

            return ILicenses;
        }
    }
}
