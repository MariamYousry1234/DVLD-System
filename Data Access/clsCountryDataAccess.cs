
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data_Access
{
    public class clsCountryDataAccess
    {

        public static bool FindCountryByID(int CountryID, ref string CountryName)
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString);
            string Query = "select * from Countries where CountryID =@CountryID";
            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@CountryID", CountryID);

            try
            {
                Connection.Open();

                SqlDataReader Data = Command.ExecuteReader();

                if (Data.Read())
                {
                    IsFound = true;
                    CountryName = (string)Data["CountryName"];
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

        public static bool FindCountryByName(ref int CountryID, string CountryName)
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString);
            string Query = "select * from Countries where CountryName =@CountryName";
            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@CountryName", CountryName);

            try
            {
                Connection.Open();

                SqlDataReader Data = Command.ExecuteReader();

                if (Data.Read())
                {
                    IsFound = true;
                    CountryID = (int)Data["CountryID"];
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

        public static DataTable GetAllCountries()
        {

            DataTable AllCountries = new DataTable();
            SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString);
            string Query = "select * from Countries";
            SqlCommand Command = new SqlCommand(Query, Connection);

            try
            {
                Connection.Open();

                SqlDataReader Data = Command.ExecuteReader();

                AllCountries.Load(Data);

                Data.Close();
            }
            catch (Exception e)
            {
             
            }
            finally
            {
                Connection.Close();
            }

            return AllCountries;
        }

    }
}
