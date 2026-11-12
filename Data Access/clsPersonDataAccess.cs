using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;


namespace Data_Access
{
    public class clsPersonDataAccess
    {
        public static bool FindPerson(int PersonID, ref string NationalNo, ref string FirstName, ref string SecondName,
         ref string ThirdName, ref string LastName, ref short Gender, ref DateTime DateOfBirth,
         ref string Phone, ref string Email, ref string Address, ref int NationalityID, ref string ImagePath)
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString);

            string Query = "select * from People where PersonID=@PersonID";
            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                Connection.Open();

                SqlDataReader Data = Command.ExecuteReader();

                if (Data.Read())
                {
                    IsFound = true;
                    NationalNo = (string)Data["NationalNo"];
                    FirstName = (string)Data["FirstName"];
                    SecondName = (string)Data["SecondName"];
                    ThirdName = Data["ThirdName"] == DBNull.Value ? "" : (string)Data["ThirdName"];
                    LastName = (string)Data["LastName"];
                    Gender = Convert.ToInt16((Data["Gender"]));
                    DateOfBirth = (DateTime)Data["DateOfBirth"];
                    Phone = (string)Data["Phone"];
                    Email = Data["Email"] == DBNull.Value ? "" : (string)Data["Email"];
                    Address = (string)Data["Address"];
                    NationalityID = (int)Data["NationalityID"];
                    ImagePath = Data["ImagePath"] == DBNull.Value ? "" : (string)Data["ImagePath"];
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

        public static bool FindPersonByNationalNo(ref int PersonID, string NationalNo, ref string FirstName, ref string SecondName,
        ref string ThirdName, ref string LastName, ref short Gender, ref DateTime DateOfBirth,
        ref string Phone, ref string Email, ref string Address, ref int NationalityID, ref string ImagePath)
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString);

            string Query = "select * from People\r\nwhere NationalNo=@NationalNo";
            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                Connection.Open();

                SqlDataReader Data = Command.ExecuteReader();

                if (Data.Read())
                {
                    IsFound = true;

                    PersonID = (int)Data["PersonID"];
                    FirstName = (string)Data["FirstName"];
                    SecondName = (string)Data["SecondName"];
                    ThirdName = Data["ThirdName"] == DBNull.Value ? "" : (string)Data["ThirdName"];
                    LastName = (string)Data["LastName"];
                    Gender = Convert.ToInt16(Data["Gender"]);
                    DateOfBirth = (DateTime)Data["DateOfBirth"];
                    Phone = (string)Data["Phone"];
                    Email = Data["Email"] == DBNull.Value ? "" : (string)Data["Email"];
                    Address = (string)Data["Address"];
                    NationalityID = (int)Data["NationalityID"];
                    ImagePath = Data["ImagePath"] == DBNull.Value ? "" : (string)Data["ImagePath"];
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
        public static bool UpdatePerson(int PersonID, string NationalNo, string FirstName, string SecondName,
            string ThirdName, string LastName, short Gender, DateTime DateOfBirth,
            string Phone, string Email, string Address, int NationalityID, string ImagePath)
        {
            int RowsAffected = 0;

            SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString);

            string Query = "UPDATE People\r\nSET \r\n    NationalNo= @NationalNo,\r\n    FirstName = @FirstName,\r\n    SecondName = @SecondName,\r\n    ThirdName = @ThirdName,\r\n    LastName = @LastName,\r\n    Gender = @Gender,\r\n    DateOfBirth = @DateOfBirth,\r\n    Phone = @Phone,\r\n    Email = @Email,\r\n    Address = @Address,\r\n    NationalityID = @NationalityID,\r\n    ImagePath = @ImagePath\r\nWHERE PersonID = @PersonID;";
            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@PersonID", PersonID);
            Command.Parameters.AddWithValue("@NationalNo", NationalNo);
            Command.Parameters.AddWithValue("@FirstName", FirstName);
            Command.Parameters.AddWithValue("@SecondName", SecondName);

            if (!string.IsNullOrEmpty(ThirdName))
                Command.Parameters.AddWithValue("@ThirdName", ThirdName);
            else
                Command.Parameters.AddWithValue("@ThirdName", DBNull.Value);

            Command.Parameters.AddWithValue("@LastName", LastName);
            Command.Parameters.AddWithValue("@Gender", Gender);
            Command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            Command.Parameters.AddWithValue("@Phone", Phone);


            if (!string.IsNullOrEmpty(Email))
                Command.Parameters.AddWithValue("@Email", Email);
            else
                Command.Parameters.AddWithValue("@Email", DBNull.Value);

            Command.Parameters.AddWithValue("@Address", Address);
            Command.Parameters.AddWithValue("@NationalityID", NationalityID);

            if (!string.IsNullOrEmpty(ImagePath))
                Command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                Command.Parameters.AddWithValue("@ImagePath", DBNull.Value);

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

        public static int AddNewPerson(string NationalNo, string FirstName, string SecondName,
            string ThirdName, string LastName, short Gender, DateTime DateOfBirth,
            string Phone, string Email, string Address, int NationalityID, string ImagePath)
        {

            int PersonID = -1;

            SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString);

            string Query = "INSERT INTO [dbo].[People]\r\n(\r\n    NationalNo,\r\n    FirstName,\r\n    SecondName,\r\n    ThirdName,\r\n    LastName,\r\n    Gender,\r\n    DateOfBirth,\r\n    [Phone],\r\n    [Email],\r\n    [Address],\r\n    [NationalityID],\r\n    [ImagePath]\r\n)\r\nVALUES\r\n(\r\n    @NationalNo,\r\n    @FirstName,\r\n    @SecondName,\r\n    @ThirdName,\r\n    @LastName,\r\n    @Gender,\r\n    @DateOfBirth,\r\n    @Phone,\r\n    @Email,\r\n    @Address,\r\n    @NationalityID,\r\n    @ImagePath\r\n);" +
                "SELECT SCOPE_IDENTITY()";
            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@NationalNo", NationalNo);
            Command.Parameters.AddWithValue("@FirstName", FirstName);
            Command.Parameters.AddWithValue("@SecondName", SecondName);

            if (!string.IsNullOrEmpty(ThirdName))
                Command.Parameters.AddWithValue("@ThirdName", ThirdName);
            else
                Command.Parameters.AddWithValue("@ThirdName", DBNull.Value);

            Command.Parameters.AddWithValue("@LastName", LastName);
            Command.Parameters.AddWithValue("@Gender", Gender);
            Command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            Command.Parameters.AddWithValue("@Phone", Phone);

            if (!string.IsNullOrEmpty(Email))
                Command.Parameters.AddWithValue("@Email", Email);
            else
                Command.Parameters.AddWithValue("@Email", DBNull.Value);

            Command.Parameters.AddWithValue("@Address", Address);
            Command.Parameters.AddWithValue("@NationalityID", NationalityID);

            if (!string.IsNullOrEmpty(ImagePath))
                Command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                Command.Parameters.AddWithValue("@ImagePath", DBNull.Value);

            try
            {
                Connection.Open();

                object AutoNumber = Command.ExecuteScalar();

                if (AutoNumber != null && int.TryParse(AutoNumber.ToString(), out int Id))

                    PersonID = Id;

            }
            catch (Exception e)
            {
            }
            finally
            {
                Connection.Close();
            }


            return PersonID;
        }


        public static bool IsPersonExists(int PersonID)
        {
            bool IsExist = false;

            SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString);

            string Query = "\r\nselect Found=1 from People\r\nwhere PersonID=@PersonID;\r\n";

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

        public static bool IsNationalNoExists(string NationalNo)
        {
            bool IsExist = false;

            SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString);

            string Query = "\r\nselect Found=1 from People\r\nwhere NationalNo=@NationalNo;\r\n";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@NationalNo", NationalNo);

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

        public static bool DeletePerson(int PersonID)
        {
            int RowsAffected = 0;

            SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString);

            string Query = "\r\nDELETE FROM [dbo].[People]\r\n      WHERE PersonID=@PersonID;\r\n";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@PersonID", PersonID);

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


        public static DataTable GetPeopleList()
        {
            DataTable PeopleList = new DataTable();

            SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString);

            string Query = "select People.PersonID ,People.NationalNo ,People.FirstName,\r\nPeople.SecondName,People.ThirdName,People.LastName,\r\nGender=\r\ncase when Gender=1 then 'Male'\r\nelse 'Female'\r\nend\r\n,People.DateOfBirth,People.Phone,People.Email,People.Address,Countries.CountryName as Nationality,People.ImagePath\r\nfrom People join Countries\r\non People.NationalityID =Countries.CountryID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            try
            {
                Connection.Open();

                SqlDataReader Data = Command.ExecuteReader();
                PeopleList.Load(Data);
                Data.Close();
            }
            catch (Exception e)
            {
               
            }
            finally
            {
                Connection.Close();
            }

            return PeopleList;
        }

    }
}
