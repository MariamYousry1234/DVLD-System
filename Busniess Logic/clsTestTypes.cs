using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access;
using System.Data;

namespace Busniess_Logic
{
    public class clsTestTypes
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public enum enTestType { VisionTest=1,WrittenTest=2,PracticalTest=3}

        public enTestType ID { set; get; }
        //public int TestTypeID { set; get; }
        public string Title { set; get; }
        public string Description { set; get; }
        public float Fees { set; get; }

        public clsTestTypes()
        {
            //this.TestTypeID = -1;
            this.ID = enTestType.VisionTest;
            this.Title = "";
            this.Description = "";
            this.Fees = 0;

            Mode = enMode.AddNew;
        }

        private clsTestTypes(enTestType ID, string Title, string Description, float Fees)
        {
            //this.TestTypeID = TestTypeID;
            this.ID = ID;
            this.Title = Title;
            this.Description = Description;
            this.Fees = Fees;

            Mode = enMode.Update;
        }

        private bool _Update()
        {
            return clsTestTypesDataAccess.Update(
                (int) this.ID,
                this.Title,
                this.Description,
                this.Fees
            );
        }

        private bool _AddNew()
        {
            this.ID =
             (enTestType)clsTestTypesDataAccess.AddNew(this.Title, this.Description, this.Fees);

            return (this.Title != "");
        }

        public static clsTestTypes Find(enTestType TestTypeID)
        {
            string Title = "";
            string Description = "";
            float Fees = 0;

            if (clsTestTypesDataAccess.Find(
                (int)TestTypeID,
                ref Title,
                ref Description,
                ref Fees))
            {
                return new clsTestTypes(
                    TestTypeID,
                    Title,
                    Description,
                    Fees
                );
            }

            return null;
        }

        public static clsTestTypes Find(string Title)
        {
            int TestTypeID = -1;
            string Description = "";
            float Fees = 0;

            if (clsTestTypesDataAccess.Find(
                ref TestTypeID,
                Title,
                ref Description,
                ref Fees))
            {
                return new clsTestTypes(
                     (enTestType) TestTypeID,
                    Title,
                    Description,
                    Fees
                );
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

        public static DataTable GetTestTypesList()
        {
            return clsTestTypesDataAccess.GetTestTypesList();
        }
    }
}
