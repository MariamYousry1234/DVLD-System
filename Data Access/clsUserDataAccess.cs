using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Data_Access
{
    public class clsUserDataAccess
    {
        public static bool FindUser(int UserID,
          ref int PersonID,
          ref string Username,
          ref string Password,
          ref bool IsActive)
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString);

            string Query = "SELECT * FROM Users WHERE UserID = @UserID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                Connection.Open();

                SqlDataReader Data = Command.ExecuteReader();

                if (Data.Read())
                {
                    IsFound = true;

                    PersonID = (int)Data["PersonID"];
                    Username = (string)Data["Username"];
                    Password = (string)Data["Password"];
                    IsActive = (bool)Data["IsActive"];
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


        public static bool FindUserByUsername(ref int UserID,
           ref int PersonID,
           string Username,
           ref string Password,
           ref bool IsActive)
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString);

            string Query = "SELECT * FROM Users WHERE Username = @Username";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@Username", Username);

            try
            {
                Connection.Open();

                SqlDataReader Data = Command.ExecuteReader();

                if (Data.Read())
                {
                    IsFound = true;

                    UserID = (int)Data["UserID"];
                    PersonID = (int)Data["PersonID"];
                    Password = (string)Data["Password"];
                    IsActive = (bool)Data["IsActive"];
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


        public static bool FindUserByUsernameAndPassword(ref int UserID,
           ref int PersonID,
           string Username,
           string Password,
           ref bool IsActive)
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString);

            string Query = "SELECT * FROM Users WHERE Username = @Username and Password = @Password";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@Username", Username);
            Command.Parameters.AddWithValue("@Password", Password);

            try
            {
                Connection.Open();

                SqlDataReader Data = Command.ExecuteReader();

                if (Data.Read())
                {
                    IsFound = true;

                    UserID = (int)Data["UserID"];
                    PersonID = (int)Data["PersonID"];
                    IsActive = (bool)Data["IsActive"];
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
        public static int AddNewUser(int PersonID,
           string Username,
           string Password,
           bool IsActive)
        {
            int UserID = -1;

            SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString);

            string Query = @"
                INSERT INTO Users(PersonID, Username, Password, IsActive)
                VALUES(@PersonID, @Username, @Password, @IsActive);
                SELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@PersonID", PersonID);
            Command.Parameters.AddWithValue("@Username", Username);
            Command.Parameters.AddWithValue("@Password", Password);
            Command.Parameters.AddWithValue("@IsActive", IsActive);

            try
            {
                Connection.Open();

                object Result = Command.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int Id))
                    UserID = Id;
            }
            catch (Exception e)
            {
              
            }
            finally
            {
                Connection.Close();
            }

            return UserID;
        }

        public static bool UpdateUser(int UserID,
          int PersonID,
          string Username,
          string Password,
          bool IsActive)
        {
            int RowsAffected = 0;

            SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString);

            string Query = @"
                UPDATE Users
                SET PersonID = @PersonID,
                    Username = @Username,
                    Password = @Password,
                    IsActive = @IsActive
                WHERE UserID = @UserID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@UserID", UserID);
            Command.Parameters.AddWithValue("@PersonID", PersonID);
            Command.Parameters.AddWithValue("@Username", Username);
            Command.Parameters.AddWithValue("@Password", Password);
            Command.Parameters.AddWithValue("@IsActive", IsActive);

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

        public static bool IsUserExists(int UserID)
        {
            bool IsExist = false;

            SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString);

            string Query = "SELECT 1 FROM Users WHERE UserID = @UserID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@UserID", UserID);

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

        public static bool IsUsernameExists(string Username)
        {
            bool IsExist = false;

            SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString);

            string Query = "SELECT 1 FROM Users WHERE Username = @Username";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@Username", Username);

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

        public static bool IsPersonIDExists(int PersonID)
        {
            bool IsExist = false;

            SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString);

            string Query = "SELECT 1 FROM Users WHERE PersonID = @PersonID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@PersonID", PersonID);

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
        public static bool DeleteUser(int UserID)
        {
            int RowsAffected = 0;

            SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString);

            string Query = "DELETE FROM Users WHERE UserID = @UserID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@UserID", UserID);

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

        public static DataTable GetUsersList()
        {
            DataTable UsersList = new DataTable();

            SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString);

            string Query = "\r\nselect Users.UserID,Users.PersonID,FullName=\r\nPeople.FirstName+' '+People.SecondName+' '+People.ThirdName+\r\n' '+People.LastName ,Users.Username,\r\nUsers.IsActive\r\nfrom Users join People\r\non Users.PersonID =People.PersonID";


            SqlCommand Command = new SqlCommand(Query, Connection);

            try
            {
                Connection.Open();

                SqlDataReader Reader = Command.ExecuteReader();
                UsersList.Load(Reader);
                Reader.Close();
            }
            catch (Exception e)
            {
             
            }
            finally
            {
                Connection.Close();
            }

            return UsersList;
        }


        public static bool IsUsernameAndPasswordValid(string Username, string Password)
        {

            bool IsExist = false;

            SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString);

            string Query = "SELECT 1 FROM Users WHERE Username = @Username and Password = @Password";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@Username", Username);
            Command.Parameters.AddWithValue("@Password", Password);

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

        public static bool ChangePassword(int UserID,string NewPassword)
        {

            int RowsAffected = 0;

            SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString);

            string Query = "update Users\r\nset Password=@NewPassword\r\nwhere UserID =@UserID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@NewPassword", NewPassword);
            Command.Parameters.AddWithValue("@UserID", UserID);

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
