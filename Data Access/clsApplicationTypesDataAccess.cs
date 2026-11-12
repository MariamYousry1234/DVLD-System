
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data_Access
{
    public  class clsApplicationTypesDataAccess
    {

        public static bool Find(int ApplicationTypeID,ref string Title,ref float Fees)
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString);

            string Query = "select * from ApplicationTypes where ApplicationTypeID =@ApplicationTypeID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

            try
            {
                Connection.Open();

               SqlDataReader Data= Command.ExecuteReader();

                if(Data.Read())
                {
                    IsFound = true;

                    Title = (string)Data["Title"];
                    Fees =Convert.ToSingle(Data["Fees"]);
                }
            }
            catch(Exception e)
            {
               
            }
            finally
            {
                Connection.Close();
            }

            return IsFound;
        }

        public static bool Find(ref int ApplicationTypeID,  string Title, ref float Fees)
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString);

            string Query = "select * from ApplicationTypes where Title =@Title";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@Title", Title);

            try
            {
                Connection.Open();

                SqlDataReader Data = Command.ExecuteReader();

                if (Data.Read())
                {
                    IsFound = true;

                    ApplicationTypeID = (int) Data["ApplicationTypeID"];
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

        public static bool Update(int ApplicationTypeID, string Title, float Fees)
        {
            int RowsAffected = 0;

            SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString);

            string Query = "UPDATE [dbo].[ApplicationTypes]\r\n   SET [Title] = @Title\r\n      ,[Fees] = @Fees\r\n WHERE ApplicationTypeID=@ApplicationTypeID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@Title", Title);
            Command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
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

            return RowsAffected>0;
        }
        public static int AddNew(string Title, float Fees)
        {
            int ApplicationTypeID = -1;

            SqlConnection connection = new SqlConnection(clsDataSettings.ConnectionString);

            string query = @"Insert Into ApplicationTypes (Title,Fees)
                            Values (@Title,@Fees)
                            
                            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationTypeTitle", Title);
            command.Parameters.AddWithValue("@ApplicationFees", Fees);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    ApplicationTypeID = insertedID;
                }
            }

            catch (Exception e)
            {
              
            }

            finally
            {
                connection.Close();
            }


            return ApplicationTypeID;

        }
        public static DataTable GetApplicationTypesList()
        {
            DataTable dtList=new DataTable();

            SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString);

            string Query = "select * from ApplicationTypes";

            SqlCommand Command = new SqlCommand(Query, Connection);


            try
            {
                Connection.Open();


               SqlDataReader Data= Command.ExecuteReader();

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
