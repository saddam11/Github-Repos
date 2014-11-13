using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using DirectorySubmitterDB.DEL;
using AccountCreation.UI;

namespace AccountCreation
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmRegister());
            //Application.Run(new FrmExpressSubmission());
        }//Main Method Closed...
    }
}
