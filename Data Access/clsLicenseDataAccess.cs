using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data_Access
{
    public  class clsLicenseDataAccess
    {
 
            public static bool FindLicense(int LicenseID,
                ref DateTime IssueDate,
                ref DateTime ExpiryDate,
                ref byte IssueReason,
                ref string Notes,
                ref float PaidFees,
                ref bool IsActive,
                ref int LicenseClassID,
                ref int ApplicationID,
                ref int DriverID,
                ref int CreatedByUserID)
            {
                bool IsFound = false;

                SqlConnection Connection =
                    new SqlConnection(clsDataSettings.ConnectionString);

                string Query = @"SELECT * FROM Licenses
                             WHERE LicenseID = @LicenseID";

                SqlCommand Command = new SqlCommand(Query, Connection);
                Command.Parameters.AddWithValue("@LicenseID", LicenseID);

                try
                {
                    Connection.Open();

                    SqlDataReader Data = Command.ExecuteReader();

                    if (Data.Read())
                    {
                        IsFound = true;

                        IssueDate = (DateTime)Data["IssueDate"];
                        ExpiryDate = (DateTime)Data["ExpiryDate"];
                        IssueReason = Convert.ToByte(Data["IssueReason"]);

                        Notes = Data["Notes"] == DBNull.Value ? "" : (string)Data["Notes"];

                        PaidFees = Convert.ToSingle(Data["PaidFees"]);
                        IsActive = (bool)Data["IsActive"];

                        LicenseClassID = (int)Data["LicenseClassID"];
                        ApplicationID = (int)Data["ApplicationID"];
                        DriverID = (int)Data["DriverID"];
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


        public static bool FindLicense(ref int LicenseID,
            ref DateTime IssueDate,
            ref DateTime ExpiryDate,
            ref byte IssueReason,
            ref string Notes,
            ref float PaidFees,
            ref bool IsActive,
            ref int LicenseClassID,
            int ApplicationID,
            ref int DriverID,
            ref int CreatedByUserID)
        {
            bool IsFound = false;

            SqlConnection Connection =
                new SqlConnection(clsDataSettings.ConnectionString);

            string Query = @"SELECT * FROM Licenses
                             WHERE ApplicationID = @ApplicationID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                Connection.Open();

                SqlDataReader Data = Command.ExecuteReader();

                if (Data.Read())
                {
                    IsFound = true;

                    LicenseID = (int)Data["LicenseID"];
                    IssueDate = (DateTime)Data["IssueDate"];
                    ExpiryDate = (DateTime)Data["ExpiryDate"];
                    IssueReason = Convert.ToByte(Data["IssueReason"]);

                    Notes = Data["Notes"] == DBNull.Value ? "" : (string)Data["Notes"];

                    PaidFees = Convert.ToSingle(Data["PaidFees"]);
                    IsActive = (bool)Data["IsActive"];

                    LicenseClassID = (int)Data["LicenseClassID"];
                    DriverID = (int)Data["DriverID"];
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


        public static int AddNewLicense(
                DateTime IssueDate,
                DateTime ExpiryDate,
                byte IssueReason,
                string Notes,
                float PaidFees,
                bool IsActive,
                int LicenseClassID,
                int ApplicationID,
                int DriverID,
                int CreatedByUserID)
            {
                int LicenseID = -1;

                SqlConnection Connection =
                    new SqlConnection(clsDataSettings.ConnectionString);

                string Query = @"
                INSERT INTO Licenses
                (
                    IssueDate,
                    ExpiryDate,
                    IssueReason,
                    Notes,
                    PaidFees,
                    IsActive,
                    LicenseClassID,
                    ApplicationID,
                    DriverID,
                    CreatedByUserID
                )
                VALUES
                (
                    @IssueDate,
                    @ExpiryDate,
                    @IssueReason,
                    @Notes,
                    @PaidFees,
                    @IsActive,
                    @LicenseClassID,
                    @ApplicationID,
                    @DriverID,
                    @CreatedByUserID
                );

                SELECT SCOPE_IDENTITY();";

                SqlCommand Command = new SqlCommand(Query, Connection);

                Command.Parameters.AddWithValue("@IssueDate", IssueDate);
                Command.Parameters.AddWithValue("@ExpiryDate", ExpiryDate);
                Command.Parameters.AddWithValue("@IssueReason", IssueReason);

                if (!string.IsNullOrEmpty(Notes))
                    Command.Parameters.AddWithValue("@Notes", Notes);
                else
                    Command.Parameters.AddWithValue("@Notes", DBNull.Value);

                Command.Parameters.AddWithValue("@PaidFees", PaidFees);
                Command.Parameters.AddWithValue("@IsActive", IsActive);
                Command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
                Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                Command.Parameters.AddWithValue("@DriverID", DriverID);
                Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

                try
                {
                    Connection.Open();

                    object Result = Command.ExecuteScalar();

                    if (Result != null &&
                        int.TryParse(Result.ToString(), out int ID))
                    {
                        LicenseID = ID;
                    }
                }
                catch (Exception e)
                {
              
            }
                finally
                {
                    Connection.Close();
                }

                return LicenseID;
            }

  

            public static bool UpdateLicense(
                int LicenseID,
                DateTime IssueDate,
                DateTime ExpiryDate,
                byte IssueReason,
                string Notes,
                float PaidFees,
                bool IsActive,
                int LicenseClassID,
                int ApplicationID,
                int DriverID,
                int CreatedByUserID)
            {
                int RowsAffected = 0;

                SqlConnection Connection =
                    new SqlConnection(clsDataSettings.ConnectionString);

                string Query = @"
                UPDATE Licenses
                SET
                    IssueDate = @IssueDate,
                    ExpiryDate = @ExpiryDate,
                    IssueReason = @IssueReason,
                    Notes = @Notes,
                    PaidFees = @PaidFees,
                    IsActive = @IsActive,
                    LicenseClassID = @LicenseClassID,
                    ApplicationID = @ApplicationID,
                    DriverID = @DriverID,
                    CreatedByUserID = @CreatedByUserID
                WHERE LicenseID = @LicenseID";

                SqlCommand Command = new SqlCommand(Query, Connection);

                Command.Parameters.AddWithValue("@LicenseID", LicenseID);
                Command.Parameters.AddWithValue("@IssueDate", IssueDate);
                Command.Parameters.AddWithValue("@ExpiryDate", ExpiryDate);
                Command.Parameters.AddWithValue("@IssueReason", IssueReason);

                if (!string.IsNullOrEmpty(Notes))
                    Command.Parameters.AddWithValue("@Notes", Notes);
                else
                    Command.Parameters.AddWithValue("@Notes", DBNull.Value);

                Command.Parameters.AddWithValue("@PaidFees", PaidFees);
                Command.Parameters.AddWithValue("@IsActive", IsActive);
                Command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
                Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                Command.Parameters.AddWithValue("@DriverID", DriverID);
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

         
            public static bool IsLicenseExists(int LicenseID)
            {
                bool IsExist = false;

                SqlConnection Connection =
                    new SqlConnection(clsDataSettings.ConnectionString);

                string Query = @"SELECT 1 FROM Licenses
                             WHERE LicenseID = @LicenseID";

                SqlCommand Command = new SqlCommand(Query, Connection);
                Command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                Connection.Open();

                object Result = Command.ExecuteScalar();

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


            public static bool DeleteLicense(int LicenseID)
            {
                int RowsAffected = 0;

                SqlConnection Connection =
                    new SqlConnection(clsDataSettings.ConnectionString);

                string Query = @"DELETE FROM Licenses
                             WHERE LicenseID = @LicenseID";

                SqlCommand Command = new SqlCommand(Query, Connection);
                Command.Parameters.AddWithValue("@LicenseID", LicenseID);

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


           public static bool IsPersonHasActiveLicenseClass(int ApplicationID)
        {
            bool IsHasActiveLicense = false;

            SqlConnection Connection =
                new SqlConnection(clsDataSettings.ConnectionString);

            string Query = @"
                    select 1 from Licenses
                     where ApplicationID=@ApplicationID
                     and Licenses.IsActive=1 ";
                                          

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);


            try
            {
                Connection.Open();

               object Data = Command.ExecuteScalar();

                IsHasActiveLicense = (Data != null);
            }
            catch (Exception e)
            {
              
            }
            finally
            {
                Connection.Close();
            }

            return IsHasActiveLicense;
        }


        public static int GetActiveLicenseClassForDriver(int DriverID,int LicenseClassID)
        {
            int LicenseID = -1;

            SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString);

            string Query = @"
                  select LicenseID from Licenses 
               where DriverID=@DriverID and Licenses.IsActive = 1 
               and LicenseClassID=@LicenseClassID";


            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@DriverID", DriverID);
            Command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);


            try
            {
                Connection.Open();

                object Data = Command.ExecuteScalar();

                if (Data != null && int.TryParse(Data.ToString(), out int ID))
                {
                    LicenseID = ID;
                }
            }
            catch (Exception e)
            {
             
            }
            finally
            {
                Connection.Close();
            }

            return LicenseID;
        }
          public static int GetActiveLicenseClassForPerson(int PersonID,int LicenseClassID)
         {
            int LicenseID = -1;

            SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString);

            string Query = @"
                  select Licenses.LicenseID from Licenses join Drivers
                on Drivers.DriverID=Licenses.DriverID
               where Drivers.PersonID=@PersonID and Licenses.IsActive = 1 
               and Licenses.LicenseClassID=@LicenseClassID";


            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@PersonID", PersonID);
            Command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);


            try
            {
                Connection.Open();

                object Data = Command.ExecuteScalar();

                 if(Data != null && int.TryParse(Data.ToString(),out int ID))
                {
                    LicenseID = ID;
                }
            }
            catch (Exception e)
            {
               
            }
            finally
            {
                Connection.Close();
            }

            return LicenseID;
        }

     
        public static DataTable GetLocalLicensesList(int DriverID)
        {
            DataTable LocalLicensesList = new DataTable();

            SqlConnection Connection =
               new SqlConnection(clsDataSettings.ConnectionString);

            string Query = @"
           select Licenses.LicenseID as 'Lic.ID',
           Licenses.ApplicationID as 'App.ID',
           LicenseClasses.ClassName as 'Class Name' ,
           Licenses.IssueDate as 'Issue Date',
           Licenses.ExpiryDate as 'Expiration Date',
           Licenses.IsActive as 'Is Active'
          from Licenses 
          join LicenseClasses 
          on Licenses.LicenseClassID =LicenseClasses.LicenseClassID
          where Licenses.DriverID = @DriverID
                     ";


            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@DriverID", DriverID);



            try
            {
                Connection.Open();

                SqlDataReader Data = Command.ExecuteReader();

                LocalLicensesList.Load(Data);
            }
            catch (Exception e)
            {
            }
            finally
            {
                Connection.Close();
            }

            return LocalLicensesList;
        }

        public static bool DeActivate(int LicenseID)
        {
            int RowsUpdated = 0;

            SqlConnection Connection =
               new SqlConnection(clsDataSettings.ConnectionString);

            string Query = @"
            update Licenses
            set IsActive=0
            where LicenseID=@LicenseID
                     ";


            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@LicenseID", LicenseID);



            try
            {
                Connection.Open();

                  RowsUpdated = Command.ExecuteNonQuery();

            }
            catch (Exception e)
            {
              
            }
            finally
            {
                Connection.Close();
            }

            return RowsUpdated>0;
        }
    }
}
