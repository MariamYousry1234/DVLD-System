using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data_Access
{
    public class clsDetainReleaseLicenseDataAccess
    {

        public static bool FindDetainedLicense(
    int DetainID,
    ref float FineFees,
    ref DateTime DetainDate,
    ref int LicenseID,
    ref int CreatedByUserID,
    ref bool IsReleased,
    ref int ReleaseApplicationID,
    ref DateTime ReleaseDate,
    ref int ReleasedByUserID)
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString);

            string Query = @"select * from DetainedLicenses
                     where DetainedID=@DetainID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@DetainID", DetainID);

            try
            {
                Connection.Open();

                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    IsFound = true;

                    FineFees = Convert.ToSingle(Reader["FineFees"]);
                    DetainDate = (DateTime)Reader["DetainedDate"];
                    LicenseID = (int)Reader["LicenseID"];
                    CreatedByUserID = (int)Reader["CreatedByUserID"];
                    IsReleased = (bool)Reader["IsReleased"];

                    ReleaseApplicationID =
                        Reader["ReleaseApplicationID"] == DBNull.Value
                        ? -1
                        : (int)Reader["ReleaseApplicationID"];

                    ReleaseDate =
                        Reader["ReleaseDate"] == DBNull.Value
                        ? DateTime.MinValue
                        : (DateTime)Reader["ReleaseDate"];

                    ReleasedByUserID =
                        Reader["ReleasedByUserID"] == DBNull.Value
                        ? -1
                        : (int)Reader["ReleasedByUserID"];
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
        public static bool GetDetainedInfoByLicenseID(
              ref int DetainID,
    ref float FineFees,
    ref DateTime DetainDate,
     int LicenseID,
    ref int CreatedByUserID,
    ref bool IsReleased,
    ref int ReleaseApplicationID,
    ref DateTime ReleaseDate,
    ref int ReleasedByUserID)
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString);

            string Query = @"select top 1 * from DetainedLicenses
            where LicenseID=@LicenseID and IsReleased=0
                  order by DetainedID desc";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                Connection.Open();

                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    IsFound = true;

                    DetainID = (int)Reader["DetainedID"];
                    FineFees = Convert.ToSingle(Reader["FineFees"]);
                    DetainDate = (DateTime)Reader["DetainedDate"];
                    CreatedByUserID = (int)Reader["CreatedByUserID"];
                    IsReleased = (bool)Reader["IsReleased"];

                    ReleaseApplicationID =
                        Reader["ReleaseApplicationID"] == DBNull.Value
                        ? -1
                        : (int)Reader["ReleaseApplicationID"];

                    ReleaseDate =
                        Reader["ReleaseDate"] == DBNull.Value
                        ? DateTime.MinValue
                        : (DateTime)Reader["ReleaseDate"];

                    ReleasedByUserID =
                        Reader["ReleasedByUserID"] == DBNull.Value
                        ? -1
                        : (int)Reader["ReleasedByUserID"];
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

        public static int GetDetainedIDForLicenseID(int LicenseID)
        {
            int DetainedID = -1;

            SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString);

            string Query = @"
                  
            select top 1 DetainedID from DetainedLicenses
              where LicenseID=@LicenseID and IsReleased=0
              order by DetainedID desc";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                Connection.Open();

                object Data = Command.ExecuteScalar();

                if (Data != null)
                    DetainedID = Convert.ToInt32(Data);
            }
            catch (Exception e)
            {
                
            }
            finally
            {
                Connection.Close();
            }


            return DetainedID;
        }
        public static int AddNewDetainedLicense(
     float FineFees,
     DateTime DetainDate,
     int LicenseID,
     int CreatedByUserID,
     bool IsReleased,
     int ReleaseApplicationID,
     DateTime ReleaseDate,
     int ReleasedByUserID)
        {
            int DetainID = -1;

            SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString);

            string Query = @"INSERT INTO DetainedLicenses
                    (
                        FineFees,
                        DetainedDate,
                        LicenseID,
                        CreatedByUserID,
                        IsReleased,
                        ReleaseApplicationID,
                        ReleaseDate,
                        ReleasedByUserID
                    )
                    VALUES
                    (
                        @FineFees,
                        @DetainedDate,
                        @LicenseID,
                        @CreatedByUserID,
                        @IsReleased,
                        @ReleaseApplicationID,
                        @ReleaseDate,
                        @ReleasedByUserID
                    );
                    SELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@FineFees", FineFees);
            Command.Parameters.AddWithValue("@DetainedDate", DetainDate);
            Command.Parameters.AddWithValue("@LicenseID", LicenseID);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            Command.Parameters.AddWithValue("@IsReleased", IsReleased);

            if (ReleaseApplicationID != -1)
                Command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
            else
                Command.Parameters.AddWithValue("@ReleaseApplicationID", DBNull.Value);

            if (ReleaseDate != DateTime.MinValue)
                Command.Parameters.AddWithValue("@ReleaseDate", ReleaseDate);
            else
                Command.Parameters.AddWithValue("@ReleaseDate", DBNull.Value);

            if (ReleasedByUserID != -1)
                Command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
            else
                Command.Parameters.AddWithValue("@ReleasedByUserID", DBNull.Value);

            try
            {
                Connection.Open();

                object Result = Command.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int ID))
                    DetainID = ID;
            }
            catch (Exception e)
            {
                
            }
            finally
            {
                Connection.Close();
            }

            return DetainID;
        }


        public static bool IsDetainedLicenseExists(int DetainID)
        {
            bool IsExist = false;

            SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString);

            string Query = @"select Found=1
                     from DetainedLicenses
                     where DetainedID=@DetainID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@DetainedID", DetainID);

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

        public static bool DeleteDetainedLicense(int DetainID)
        {
            int RowsAffected = 0;

            SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString);

            string Query = @"delete from DetainedLicenses
                     where DetainedID=@DetainID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@DetainedID", DetainID);

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


        public static bool UpdateDetainedLicense(
   int DetainID,
   float FineFees,
   DateTime DetainDate,
   int LicenseID,
   int CreatedByUserID,
   bool IsReleased,
   int ReleaseApplicationID,
   DateTime ReleaseDate,
   int ReleasedByUserID)
        {
            int RowsAffected = 0;

            SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString);

            string Query = @"UPDATE DetainedLicenses
                     SET
                        FineFees=@FineFees,
                        DetainedDate=@DetainedDate,
                        LicenseID=@LicenseID,
                        CreatedByUserID=@CreatedByUserID,
                        IsReleased=@IsReleased,
                        ReleaseApplicationID=@ReleaseApplicationID,
                        ReleaseDate=@ReleaseDate,
                        ReleasedByUserID=@ReleasedByUserID
                     WHERE DetainedID=@DetainID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@DetainID", DetainID);
            Command.Parameters.AddWithValue("@FineFees", FineFees);
            Command.Parameters.AddWithValue("@DetainedDate", DetainDate);
            Command.Parameters.AddWithValue("@LicenseID", LicenseID);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            Command.Parameters.AddWithValue("@IsReleased", IsReleased);

            if (ReleaseApplicationID != -1)
                Command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
            else
                Command.Parameters.AddWithValue("@ReleaseApplicationID", DBNull.Value);

            if (ReleaseDate != DateTime.MinValue)
                Command.Parameters.AddWithValue("@ReleaseDate", ReleaseDate);
            else
                Command.Parameters.AddWithValue("@ReleaseDate", DBNull.Value);

            if (ReleasedByUserID != -1)
                Command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
            else
                Command.Parameters.AddWithValue("@ReleasedByUserID", DBNull.Value);

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

        public static DataTable GetDetainedLicensesList()
        {
            DataTable dt = new DataTable();

            SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString);

            string Query = @"select * from DetainedInfoView
                ORDER BY IsReleased, DetainedID DESC ";
             
            SqlCommand Command = new SqlCommand(Query, Connection);

            try
            {
                Connection.Open();

                SqlDataReader Reader = Command.ExecuteReader();

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


        public static bool IsLicenseDetained(int LicenseID)
        {
            bool IsDetained = false;

            SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString);

            string Query = @"
               select top 1 Found=1 from DetainedLicenses
             where LicenseID=@LicenseID and IsReleased=0
             order by DetainedID desc";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                Connection.Open();

                object IsDetain = Command.ExecuteScalar();

                if (IsDetain != null)
                    IsDetained = true;
            }
            catch (Exception e)
            {
               
            }
            finally
            {
                Connection.Close();
            }

            return IsDetained;
        }

        public static bool ReleaseDetainedLicense(int DetainedID,int ReleasedUserID,int ApplicationID)
        {
            int  NumberOfRows = 0;

            SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString);

            string Query = @"
               update DetainedLicenses
            set IsReleased=1,
              ReleaseApplicationID=@ApplicationID,
               ReleaseDate=@ReleaseDate,
               ReleasedByUserID=@ReleasedUserID
               where DetainedID=@DetainedID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            Command.Parameters.AddWithValue("@ReleasedUserID", ReleasedUserID);
            Command.Parameters.AddWithValue("@DetainedID", DetainedID);
            Command.Parameters.AddWithValue("@ReleaseDate", DateTime.Now);

            try
            {
                Connection.Open();

                NumberOfRows = Command.ExecuteNonQuery();

            }
            catch (Exception  e)
            {
               
            }
            finally
            {
                Connection.Close();
            }

            return NumberOfRows>0;
        }
    }
}
