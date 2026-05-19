using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DVLD_Project.GlobalClasses
{
    internal class clsUtil
    {
        public static string NewGUId()
        {
            return Guid.NewGuid().ToString();
        }

        public static bool CreateFolder(string FolderName)
        {
            if (!Directory.Exists(FolderName))
            {
                try
                {
                    Directory.CreateDirectory(FolderName);
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        public static string GetNewImagePath(string FolderName,string OriginPath)
        {
            FileInfo FI = new FileInfo(OriginPath);

            return FolderName + NewGUId() + FI.Extension;
        }

        public static bool AddNewImageToFolder(ref string OriginPath)
        {
            string FolderName =
            "C:\\Users\\Admin\\Desktop\\DVLD Images\\";

            if (!CreateFolder(FolderName)) return false;

            string NewPath = GetNewImagePath(FolderName,OriginPath);

            try
            {
                File.Copy(OriginPath, NewPath, true);
            }
            catch (Exception)
            {
                return false;
            }
            OriginPath = NewPath;
            return true;
        }
    }
}
