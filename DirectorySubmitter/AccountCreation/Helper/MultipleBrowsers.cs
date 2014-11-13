using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AccountCreation.Helper
{
    class MultipleBrowsers
    {

        public void ClearCache()
        {
            ClearAllLocalCache();
        }

        public void ClearAllLocalCache()
        {
            string GooglePath = Environment.GetEnvironmentVariable("USERPROFILE") + @"\AppData\Local\Google\Chrome\User Data\Default\";
            string MozilaPath = "";// Environment.GetEnvironmentVariable("USERPROFILE") + @"\AppData\Roaming\Mozilla\Firefox\";
            string Opera1 = Environment.GetEnvironmentVariable("USERPROFILE") + @"\AppData\Local\Opera\Opera";
            string Opera2 = Environment.GetEnvironmentVariable("USERPROFILE") + @"\AppData\Roaming\Opera\Opera";
            string Safari1 = Environment.GetEnvironmentVariable("USERPROFILE") + @"\AppData\Local\Apple Computer\Safari";
            string Safari2 = Environment.GetEnvironmentVariable("USERPROFILE") + @"\AppData\Roaming\Apple Computer\Safari";
            string IE1 = "";// Environment.GetEnvironmentVariable("USERPROFILE") + @"\AppData\Local\Microsoft\Intern~1";
            string IE2 = "";// Environment.GetEnvironmentVariable("USERPROFILE") + @"\AppData\Local\Microsoft\Windows\History";
            string IE3 = "";// Environment.GetEnvironmentVariable("USERPROFILE") + @"\AppData\Local\Microsoft\Windows\Tempor~1";
            string IE4 = "";// Environment.GetEnvironmentVariable("USERPROFILE") + @"\AppData\Roaming\Microsoft\Windows\Cookies";
            string Flash = Environment.GetEnvironmentVariable("USERPROFILE") + @"\AppData\Roaming\Macromedia\Flashp~1";

            //Call This Method ClearAllSettings and Pass String Array Param
            ClearAllSettings(new string[] { GooglePath, MozilaPath, Opera1, Opera2, Safari1, Safari2, IE1, IE2, IE3, IE4, Flash });

        }
        /// <summary>
        /// Clear All Settings History and Folder by Specified Browser Name Location
        /// </summary>
        /// <param name="ClearPath">String Array To Delete Complete Files and Folders</param>
        public void ClearAllSettings(string[] ClearPath)
        {
            foreach (string HistoryPath in ClearPath)
            {
                if (Directory.Exists(HistoryPath))
                {
                    DoDelete(new DirectoryInfo(HistoryPath));
                }

            }
        }

        /// <summary>
        /// Require Folder Name to Delete All Files and Sub Directory
        /// </summary>
        /// <param name="folder">DirectoryInfo Object Require here...</param>
        public void DoDelete(DirectoryInfo folder)
        {
            try
            {
                foreach (FileInfo file in folder.GetFiles())
                {
                    try
                    {
                        
                        file.Delete();
                    }
                    catch
                    { }

                }
                foreach (DirectoryInfo subfolder in folder.GetDirectories())
                {
                    DoDelete(subfolder);
                }
            }
            catch
            {
            }
        }

        void clearIECache()
        {
            DoDelete(new DirectoryInfo(Environment.GetFolderPath
            (Environment.SpecialFolder.InternetCache)));
        }
    }
}
