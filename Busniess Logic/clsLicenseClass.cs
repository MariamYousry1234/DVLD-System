using Data_Access;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Busniess_Logic
{
    public class clsLicenseClass
    {
        public enum enMode { AddNew = 0, Update = 1 }

        public enMode Mode = enMode.AddNew;

      
        public int LicenseClassID { set; get; }

        public string ClassName { set; get; }

        public string ClassDescription { set; get; }

        public short MinimumAllowedAge { set; get; }

        public short DeafualtValidityLength { set; get; }

        public float ClassFees { set; get; }

        public clsLicenseClass()
        {
            this.LicenseClassID = -1;
            this.ClassName = "";
            this.ClassDescription = "";
            this.MinimumAllowedAge = 0;
            this.DeafualtValidityLength = 0;
            this.ClassFees = 0;

            Mode = enMode.AddNew;
        }

        private clsLicenseClass(
            int LicenseClassID,
            string ClassName,
            string ClassDescription,
            short MinimumAllowedAge,
            short DeafualtValidityLength,
            float ClassFees)
        {
            this.LicenseClassID = LicenseClassID;
            this.ClassName = ClassName;
            this.ClassDescription = ClassDescription;
            this.MinimumAllowedAge = MinimumAllowedAge;
            this.DeafualtValidityLength = DeafualtValidityLength;
            this.ClassFees = ClassFees;

            Mode = enMode.Update;
        }

        private bool _Update()
        {
            return clsLicenseClassDataAccess.Update(
                (int)this.LicenseClassID,
                this.ClassName,
                this.ClassDescription,
                this.MinimumAllowedAge,
                this.DeafualtValidityLength,
                this.ClassFees
            );
        }

        private bool _AddNew()
        {
            this.LicenseClassID =
            clsLicenseClassDataAccess.AddNew(
                    this.ClassName,
                    this.ClassDescription,
                    this.MinimumAllowedAge,
                    this.DeafualtValidityLength,
                    this.ClassFees
                );

            return (this.ClassDescription != "");
        }

        public static clsLicenseClass Find(int LicenseClassID)
        {
            string ClassName = "";
            string ClassDescription = "";
            short MinimumAllowedAge = 0;
            short DeafualtValidityLength = 0;
            float ClassFees = 0;

            if (clsLicenseClassDataAccess.Find(
                LicenseClassID,
                ref ClassName,
                ref ClassDescription,
                ref MinimumAllowedAge,
                ref DeafualtValidityLength,
                ref ClassFees))
            {
                return new clsLicenseClass(
                   LicenseClassID,
                    ClassName,
                    ClassDescription,
                    MinimumAllowedAge,
                    DeafualtValidityLength,
                    ClassFees
                );
            }

            return null;
        }


        public static clsLicenseClass Find(string ClassName)
        {
            int LicenseClassID = -1;
            string ClassDescription = "";
            short MinimumAllowedAge = 0;
            short DeafualtValidityLength = 0;
            float ClassFees = 0;

            if (clsLicenseClassDataAccess.Find(
                ref LicenseClassID,
                ClassName,
                ref ClassDescription,
                ref MinimumAllowedAge,
                ref DeafualtValidityLength,
                ref ClassFees))
            {
                return new clsLicenseClass(
                  LicenseClassID,
                    ClassName,
                    ClassDescription,
                    MinimumAllowedAge,
                    DeafualtValidityLength,
                    ClassFees
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

        public static DataTable GetLicenseClassesList()
        {
            return clsLicenseClassDataAccess.GetLicenseClassesList();
        }


    }
}
