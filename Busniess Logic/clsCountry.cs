using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access;

namespace Busniess_Logic
{
    public class clsCountry
    {
        public int CountryID { set; get; }
        public string CountryName { set; get; }

        public clsCountry()
        {
            CountryID = -1;
            CountryName = "";
        }
        private clsCountry(int CountryID, string CountryName)
        {
            this.CountryID = CountryID;
            this.CountryName = CountryName;
        }


        public static clsCountry Find(int CountryID)
        {
            string CountryName = "";
            if (clsCountryDataAccess.FindCountryByID(CountryID, ref CountryName))
            {
                return new clsCountry(CountryID, CountryName);
            }

            return null;
        }

        public static clsCountry Find(string CountryName)
        {
            int CountryID = -1;
            if (clsCountryDataAccess.FindCountryByName(ref CountryID, CountryName))
            {
                return new clsCountry(CountryID, CountryName);
            }

            return null;
        }

        public static DataTable GetAllCountries()
        {
            return clsCountryDataAccess.GetAllCountries();
        }
    }
}
