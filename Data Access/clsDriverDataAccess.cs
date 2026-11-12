using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;


namespace Data_Access
{
    public  class clsDriverDataAccess
    {

        public static bool FindDriver(int DriverID,
           ref int PersonID,
           ref int CreatedByUserID,
           ref DateTime CreatedDate)
        {
            bool IsFound = false;

            SqlConnection Connection =
                new SqlConnection(clsDataSettings.ConnectionString);

            string Query = "select * from Drivers where DriverID=@DriverID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                Connection.Open();

                SqlDataReader Data = Command.ExecuteReader();

                if (Data.Read())
                {
                    IsFound = true;

                    PersonID = (int)Data["PersonID"];
                    CreatedByUserID = (int)Data["CreatedByUserID"];
                    CreatedDate = Convert.ToDateTime(Data["CreatedDate"]);
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

        public static bool FindDriverByPersonID(
            ref int DriverID,
            int PersonID,
            ref int CreatedByUserID,
            ref DateTime CreatedDate)
        {
            bool IsFound = false;

            SqlConnection Connection =
                new SqlConnection(clsDataSettings.ConnectionString);

            string Query = "select * from Drivers where PersonID=@PersonID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                Connection.Open();

                SqlDataReader Data = Command.ExecuteReader();

                if (Data.Read())
                {
                    IsFound = true;

                    DriverID = (int)Data["DriverID"];
                    CreatedByUserID = (int)Data["CreatedByUserID"];
                    CreatedDate = Convert.ToDateTime(Data["CreatedDate"]);
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

        public static int AddNewDriver(int PersonID,
            int CreatedByUserID)
        {
            int DriverID = -1;

            SqlConnection Connection =
                new SqlConnection(clsDataSettings.ConnectionString);

            string Query =
            @"INSERT INTO Drivers
            (
                PersonID,
                CreatedByUserID,
                CreatedDate
            )
            VALUES
            (
                @PersonID,
                @CreatedByUserID,
                @CreatedDate
            );

            SELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@PersonID", PersonID);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            Command.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

            try
            {
                Connection.Open();

                object AutoNumber = Command.ExecuteScalar();

                if (AutoNumber != null &&
                    int.TryParse(AutoNumber.ToString(), out int ID))
                {
                    DriverID = ID;
                }
            }
            catch (Exception e)
            {
                
            }
            finally
            {
                Connection.Close();
            }

            return DriverID;
        }

        public static bool UpdateDriver(int DriverID,
            int PersonID,
            int CreatedByUserID,
            DateTime CreatedDate)
        {
            int RowsAffected = 0;

            SqlConnection Connection =
                new SqlConnection(clsDataSettings.ConnectionString);

            string Query =
            @"UPDATE Drivers
              SET PersonID=@PersonID,
                  CreatedByUserID=@CreatedByUserID,
                  CreatedDate=@CreatedDate
              WHERE DriverID=@DriverID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@DriverID", DriverID);
            Command.Parameters.AddWithValue("@PersonID", PersonID);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            Command.Parameters.AddWithValue("@CreatedDate", CreatedDate);

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

   
        public static bool IsDriverExists(int DriverID)
        {
            bool IsExist = false;

            SqlConnection Connection =
                new SqlConnection(clsDataSettings.ConnectionString);

            string Query =
                "select Found=1 from Drivers where DriverID=@DriverID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@DriverID", DriverID);

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

        public static bool IsDriverExistsByPersonID(int PersonID)
        {
            bool IsExist = false;

            SqlConnection Connection =
                new SqlConnection(clsDataSettings.ConnectionString);

            string Query =
                "select Found=1 from Drivers where PersonID=@PersonID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@PersonID", PersonID);

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

        public static DataTable GetDriversList()
        {
            DataTable DriversList = new DataTable();

            SqlConnection Connection =
                new SqlConnection(clsDataSettings.ConnectionString);

            string Query =
            @"select * from DriversInfoView";

            SqlCommand Command = new SqlCommand(Query, Connection);

            try
            {
                Connection.Open();

                SqlDataReader Data = Command.ExecuteReader();

                DriversList.Load(Data);

                Data.Close();
            }
            catch (Exception e)
            {
                
            }
            finally
            {
                Connection.Close();
            }

            return DriversList;
        }

    }
}
