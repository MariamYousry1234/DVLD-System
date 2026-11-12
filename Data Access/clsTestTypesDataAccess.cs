using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace Data_Access
{
    public  class clsTestTypesDataAccess
    {
        public static bool Find(int TestTypeID, ref string Title, ref string Description, ref float Fees)
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString);

            string Query = "select * from TestTypes where TestTypeID = @TestTypeID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                Connection.Open();

                SqlDataReader Data = Command.ExecuteReader();

                if (Data.Read())
                {
                    IsFound = true;

                    Title = (string)Data["Title"];
                    Description = (string)Data["Description"];
                    Fees = Convert.ToSingle(Data["Fees"]);
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

        public static bool Find(ref int TestTypeID, string Title, ref string Description, ref float Fees)
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString);

            string Query = "select * from TestTypes where Title = @Title";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@Title", Title);

            try
            {
                Connection.Open();

                SqlDataReader Data = Command.ExecuteReader();

                if (Data.Read())
                {
                    IsFound = true;

                    TestTypeID = (int)Data["TestTypeID"];
                    Description = (string)Data["Description"];
                    Fees = Convert.ToSingle(Data["Fees"]);
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

        public static bool Update(int TestTypeID, string Title, string Description, float Fees)
        {
            int RowsAffected = 0;

            SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString);

            string Query = @"UPDATE [dbo].[TestTypes]
                     SET [Title] = @Title,
                         [Description] = @Description,
                         [Fees] = @Fees
                     WHERE TestTypeID = @TestTypeID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            Command.Parameters.AddWithValue("@Title", Title);
            Command.Parameters.AddWithValue("@Description", Description);
            Command.Parameters.AddWithValue("@Fees", Fees);

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

        public static int AddNew(string Title, string Description, float Fees)
        {
            int TestTypeID = -1;

            SqlConnection connection = new SqlConnection(clsDataSettings.ConnectionString);

            string query = @"Insert Into TestTypes (Title,Description,Fees)
                            Values (@Title,@Description,@Fees)
                            where TestTypeID = @TestTypeID;
                            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@itle", Title);
            command.Parameters.AddWithValue("@Description", Description);
            command.Parameters.AddWithValue("@Fees", Fees);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    TestTypeID = insertedID;
                }
            }

            catch (Exception e)
            {
            

            }

            finally
            {
                connection.Close();
            }


            return TestTypeID;

        }


        public static DataTable GetTestTypesList()
        {
            DataTable dtList = new DataTable();

            SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString);

            string Query = "select * from TestTypes";

            SqlCommand Command = new SqlCommand(Query, Connection);

            try
            {
                Connection.Open();

                SqlDataReader Data = Command.ExecuteReader();

                dtList.Load(Data);
            }
            catch (Exception e)
            {
             
            }
            finally
            {
                Connection.Close();
            }

            return dtList;
        }



    }
}
