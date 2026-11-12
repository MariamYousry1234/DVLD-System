using Data_Access;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Busniess_Logic
{
    public class clsUser
    {
        private enum enMode { AddNew = 0, Update = 1 };
        enMode _Mode;
        public int UserID { set; get; }

        public int PersonID { set; get; }

        public string Username { set; get; }

        public string Password { set; get; }

        public bool IsActive { set; get; }

        public clsPerson PersonInfo;

        public clsUser()
        {
            this.UserID = -1;
            this.PersonID = -1;
            this.Username = "";
            this.Password = "";
            this.IsActive = false;

            this.PersonInfo = null;

            _Mode = enMode.AddNew;
        }

        private clsUser(int UserID, int PersonID, string Username,
                      string Password, bool IsActive)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.Username = Username;
            this.Password = Password;
            this.IsActive = IsActive;

            this.PersonInfo = clsPerson.Find(PersonID);

            _Mode = enMode.Update;
        }
        private bool _AddNewUser()
        {
            this.UserID = clsUserDataAccess.AddNewUser(
                this.PersonID,
                this.Username,
               this.Password,
                this.IsActive
            );

            return (this.UserID != -1);
        }

        private bool _UpdateUser()
        {
         

            return clsUserDataAccess.UpdateUser(
                this.UserID,
                this.PersonID,
                this.Username,
                this.Password,
                this.IsActive
            );
        }
        public static clsUser Find(int UserID)
        {
            int PersonID = -1;
            string Username = "", Password = "";
            bool IsActive = false;

            if (clsUserDataAccess.FindUser(UserID, ref PersonID, ref Username, ref Password, ref IsActive))
            {
                return new clsUser(UserID, PersonID, Username, Password, IsActive);
            }

            return null;
        }

        public static clsUser Find(string Username)
        {
            int UserID = -1;
            int PersonID = -1;
            string Password = "";
            bool IsActive = false;

            if (clsUserDataAccess.FindUserByUsername(ref UserID, ref PersonID, Username, ref Password, ref IsActive))
            {
                return new clsUser(UserID, PersonID, Username, Password, IsActive);
            }

            return null;
        }

        public static clsUser Find(string Username,string Password)
        {
            int UserID = -1;
            int PersonID = -1;
            bool IsActive = false;
          
            if (clsUserDataAccess.FindUserByUsernameAndPassword(ref UserID, ref PersonID, Username, Password, ref IsActive))
            {
                return new clsUser(UserID, PersonID, Username, Password, IsActive);
            }

            return null;
        }
        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.Update:
                    return _UpdateUser();

                case enMode.AddNew:
                    if (_AddNewUser())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    return false;
            }

            return false;
        }

        public static bool IsUserExists(int UserID)
        {
            return clsUserDataAccess.IsUserExists(UserID);
        }

        public static bool IsUsernameExists(string Username)
        {
            return clsUserDataAccess.IsUsernameExists(Username);
        }

       

        public static bool DeleteUser(int UserID)
        {
            return clsUserDataAccess.DeleteUser(UserID);
        }

        public static DataTable GetUsersList()
        {
            return clsUserDataAccess.GetUsersList();
        }

        public static bool IsPersonIDExists(int PersonID)
        {
            return clsUserDataAccess.IsPersonIDExists(PersonID);
        }


        public static bool IsUsernameAndPasswordValid(string Username, string Password)
        {
        
            return clsUserDataAccess.IsUsernameAndPasswordValid(Username, Password);
        }


        public bool ChangePassword(string NewPassword)
        {
            if (clsUserDataAccess.ChangePassword(this.UserID, Password))
            {
                this.Password = NewPassword;
                return true;
            }

            return false;
        }
    }
}
