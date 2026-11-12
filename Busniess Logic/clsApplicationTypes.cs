using Data_Access;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Busniess_Logic
{
    public  class clsApplicationTypes
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        
        public int ApplicationTypeID { set; get; }

        public string Title { set; get; }

        public float Fees { set; get; }


        public clsApplicationTypes()
        {
            this.ApplicationTypeID = -1;
            this.Title = "";
            this.Fees = 0;

            Mode = enMode.AddNew;
        }
        private clsApplicationTypes(int ApplicationTypeID, string Title, float Fees)
        {
            this.ApplicationTypeID = ApplicationTypeID;
            this.Title = Title;
            this.Fees = Fees;

            Mode = enMode.Update;
        }

        private bool _Update()
        {
           return clsApplicationTypesDataAccess.Update(this.ApplicationTypeID, this.Title, this.Fees);
        }

        private bool _AddNew()
        {
          
            this.ApplicationTypeID 
                = clsApplicationTypesDataAccess.AddNew(this.Title, this.Fees);


            return (this.ApplicationTypeID != -1);
        }

        public static clsApplicationTypes Find(int ApplicationTypeID )
        {
            string Title = "";
            float Fees = 0;

           if( clsApplicationTypesDataAccess.Find(ApplicationTypeID,ref Title,ref Fees))
            {
                return new clsApplicationTypes(ApplicationTypeID, Title, Fees);
            }

            return null;
        }

        public static clsApplicationTypes Find(string Title)
        {
            int ApplicationTypeID = -1;
            float Fees = 0;

            if (clsApplicationTypesDataAccess.Find(ref ApplicationTypeID,  Title, ref Fees))
            {
                return new clsApplicationTypes(ApplicationTypeID, Title, Fees);
            }

            return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNew())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _Update();

            }

            return false;
        }

        public static DataTable GetApplicationTypesList()
        {
            return clsApplicationTypesDataAccess.GetApplicationTypesList();
        }
    }
}
