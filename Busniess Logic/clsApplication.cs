using Data_Access;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Busniess_Logic
{
    public class clsApplication
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode;
        public enum enApplicationStatus { New = 1, Cancelled = 2, Completed = 3 }

        public enum enApplicationType { NewLocal=1,Renew=2,ReplacementForLost=3,
        ReplacementForDamaged=4,ReleaseDetained=5,NewInternational=6,RetakeTest=7}

        public int ApplicationID { set; get; }

        public int PersonID { set; get; }

        public clsPerson PersonInfo;
        public DateTime ApplicationDate { set; get; }

        public enApplicationType ApplicationTypeID { set; get; }
       

        public clsApplicationTypes ApplicationType;

        public enApplicationStatus ApplicationStaus { set; get; }

        public string StatusText
        {
            get
            {
                switch (ApplicationStaus)
                {
                    case clsApplication.enApplicationStatus.New:
                        return "New";


                    case clsApplication.enApplicationStatus.Cancelled:
                        return "Cancelled";


                    case clsApplication.enApplicationStatus.Completed:
                        return "Completed";

                    default:
                        return "UnKown";
                }
            }
            
      }
       


        public DateTime LastStatusDate { set; get; }

        public float ApplicationFees { set; get; }

        public int CreatedByUserID { set; get; }

        public clsUser CreatedUserInfo;

        public clsApplication()
        {
            this.ApplicationID = -1;
            this.PersonID = -1;
            this.ApplicationDate = DateTime.Now;
            this.ApplicationTypeID = enApplicationType.NewLocal;
            this.ApplicationStaus = enApplicationStatus.New;
            this.LastStatusDate = DateTime.MinValue;
            this.ApplicationFees = 0;
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }

        protected clsApplication(int ApplicationID,int PersonID, DateTime ApplicationDate, enApplicationType ApplicationTypeID,
           enApplicationStatus ApplicationStaus, DateTime LastStatusDate, float ApplicationFees,
           int CreatedByUserID)
        {
            this.ApplicationID = ApplicationID;
            this.PersonID = PersonID;
            this.PersonInfo = clsPerson.Find(PersonID);
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationType = clsApplicationTypes.Find((int) ApplicationTypeID);
            this.ApplicationStaus = ApplicationStaus;
            this.LastStatusDate = LastStatusDate;
            this.ApplicationFees = ApplicationFees;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedUserInfo = clsUser.Find(CreatedByUserID);

            Mode = enMode.Update;
        }


        protected bool _AddNewApplication()
        {
            this.ApplicationID = clsApplicationDataAccess.AddNewApplication(this.PersonID,this.ApplicationDate,(int) this.ApplicationTypeID,
                  (int)this.ApplicationStaus, this.LastStatusDate, this.ApplicationFees, this.CreatedByUserID);

            return this.ApplicationID != -1;
        }


        protected bool _UpdateApplication()
        {
            return clsApplicationDataAccess.UpdateApplication(this.ApplicationID,this.PersonID, this.ApplicationDate,(int) this.ApplicationTypeID,
                 (int)this.ApplicationStaus, this.LastStatusDate, this.ApplicationFees, this.CreatedByUserID);
        }

        public static clsApplication Find(int ApplicationID)
        {

            DateTime ApplicationDate = DateTime.Now, LastStatusDate = DateTime.MinValue;
            int ApplicationTypeID = -1, CreatedByUserID = -1,PersonID=-1;
            int ApplicationStatus = 1;
            float ApplicationFees = 0;


            bool IsFound = clsApplicationDataAccess.Find(ApplicationID,ref PersonID, ref ApplicationDate, ref ApplicationTypeID,
                ref ApplicationStatus, ref LastStatusDate, ref ApplicationFees, ref CreatedByUserID);


            if (IsFound)
            {
                return new clsApplication(ApplicationID, PersonID, ApplicationDate, (enApplicationType)ApplicationTypeID,
                    (enApplicationStatus)ApplicationStatus, LastStatusDate, ApplicationFees,
                    CreatedByUserID);
            }

            return null;
        }


        public bool SaveData()
        {
            switch (Mode)
            {
                case enMode.Update:
                    {
                        return _UpdateApplication();
                    }

                case enMode.AddNew:
                    {
                        if (_AddNewApplication())
                        {

                            Mode = enMode.Update;
                            return true;
                        }
                        return false;
                    }

            }
            return false;
        }


        public  bool DeleteApplication()
        {
           return clsApplicationDataAccess.DeleteApplication(this.ApplicationID);
        }

       
        public int GetActiveApplicationID(clsApplication.enApplicationType ApplicationType)
        {
            return clsApplicationDataAccess.GetActiveApplicationForPerson(this.PersonID,(int) ApplicationType);
        }

        public bool DoesPersonHaveactiveApplication(clsApplication.enApplicationType ApplicationType)
        {
            return GetActiveApplicationID(ApplicationType) != -1;
        }

        public static int CheckApplicationActivation(int PersonID, clsApplication.enApplicationType ApplicationType, int ClassID)
        {
            return clsApplicationDataAccess.CheckApplicationActivation(PersonID, (int)ApplicationType, ClassID);
        }


        private bool UpdateStatus(clsApplication.enApplicationStatus NewStatus)
        {
            return clsApplicationDataAccess.UpdateStatus((short)NewStatus, this.ApplicationID);
        }

        public bool SetCompleted()
        {
            return  UpdateStatus(enApplicationStatus.Completed);
        }

        public bool SetCancelled()
        {
            return UpdateStatus(enApplicationStatus.Cancelled);
        }
    }
}
