using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data_Access
{
    public class clsLicenseClassDataAccess
    {
        public static bool Find(int LicenseClassID,
            ref string ClassName,
            ref string ClassDescription,
            ref short MinimumAllowedAge,
            ref short DeafualtValidityLength,
            ref float ClassFees)
        {
            bool IsFound = false;

            SqlConnection Connection =
                new SqlConnection(clsDataSettings.ConnectionString);

            string Query = "select * from LicenseClasses where LicenseClassID = @LicenseClassID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                Connection.Open();

                SqlDataReader Data = Command.ExecuteReader();

                if (Data.Read())
                {
                    IsFound = true;

                    ClassName = (string)Data["ClassName"];
                    ClassDescription = (string)Data["ClassDescription"];
                    MinimumAllowedAge = Convert.ToInt16(Data["MinimumAllowedAge"]);
                    DeafualtValidityLength = Convert.ToInt16(Data["DefaultValidityLength"]);
                    ClassFees = Convert.ToSingle(Data["ClassFees"]);
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

        public static bool Find(ref int LicenseClassID,
            string ClassName,
           ref string ClassDescription,
           ref short MinimumAllowedAge,
           ref short DeafualtValidityLength,
           ref float ClassFees)
        {
            bool IsFound = false;

            SqlConnection Connection =
                new SqlConnection(clsDataSettings.ConnectionString);

            string Query = "select * from LicenseClasses where ClassName = @ClassName";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ClassName", ClassName);

            try
            {
                Connection.Open();

                SqlDataReader Data = Command.ExecuteReader();

                if (Data.Read())
                {
                    IsFound = true;

                    LicenseClassID = (int)Data["LicenseClassID"];
                    ClassDescription = (string)Data["ClassDescription"];
                    MinimumAllowedAge = Convert.ToInt16(Data["MinimumAllowedAge"]);
                    DeafualtValidityLength = Convert.ToInt16(Data["DefaultValidityLength"]);
                    ClassFees = Convert.ToSingle(Data["ClassFees"]);
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


        public static bool Update(
            int LicenseClassID,
            string ClassName,
            string ClassDescription,
            short MinimumAllowedAge,
            short DeafualtValidityLength,
            float ClassFees)
        {
            int RowsAffected = 0;

            SqlConnection Connection =
                new SqlConnection(clsDataSettings.ConnectionString);

            string Query = @"UPDATE LicenseClasses
                             SET ClassName = @ClassName,
                                 ClassDescription = @ClassDescription,
                                 MinimumAllowedAge = @MinimumAllowedAge,
                                 DeafualtValidityLength = @DeafualtValidityLength,
                                 ClassFees = @ClassFees
                             WHERE LicenseClassID = @LicenseClassID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            Command.Parameters.AddWithValue("@ClassName", ClassName);
            Command.Parameters.AddWithValue("@ClassDescription", ClassDescription);
            Command.Parameters.AddWithValue("@MinimumAllowedAge", MinimumAllowedAge);
            Command.Parameters.AddWithValue("@DeafualtValidityLength", DeafualtValidityLength);
            Command.Parameters.AddWithValue("@ClassFees", ClassFees);

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

        public static int AddNew(
            string ClassName,
            string ClassDescription,
            short MinimumAllowedAge,
            short DeafualtValidityLength,
            float ClassFees)
        {
            int LicenseClassID = -1;

            SqlConnection Connection =
                new SqlConnection(clsDataSettings.ConnectionString);

            string Query = @"INSERT INTO LicenseClasses
                            (ClassName,
                             ClassDescription,
                             MinimumAllowedAge,
                             DeafualtValidityLength,
                             ClassFees)

                             VALUES
                            (@ClassName,
                             @ClassDescription,
                             @MinimumAllowedAge,
                             @DeafualtValidityLength,
                             @ClassFees);

                             SELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ClassName", ClassName);
            Command.Parameters.AddWithValue("@ClassDescription", ClassDescription);
            Command.Parameters.AddWithValue("@MinimumAllowedAge", MinimumAllowedAge);
            Command.Parameters.AddWithValue("@DeafualtValidityLength", DeafualtValidityLength);
            Command.Parameters.AddWithValue("@ClassFees", ClassFees);

            try
            {
                Connection.Open();

                object Result = Command.ExecuteScalar();

                if (Result != null &&
                    int.TryParse(Result.ToString(), out int InsertedID))
                {
                    LicenseClassID = InsertedID;
                }
            }
            catch (Exception e)
            {
               
            }
            finally
            {
                Connection.Close();
            }

            return LicenseClassID;
        }

        public static DataTable GetLicenseClassesList()
        {
            DataTable dtList = new DataTable();

            SqlConnection Connection =
                new SqlConnection(clsDataSettings.ConnectionString);

            string Query = "select * from LicenseClasses";

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
