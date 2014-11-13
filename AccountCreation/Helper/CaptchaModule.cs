using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AccountCreation.Helper
{
    public class CaptchaModule
    {
        public static void SaveCaptcha()
        {
            //if (txtImageCode.Text.Trim().Length != 0)
            if (CaptchaInfo.Code.Trim().Length != 0)
            {
                Connection objConnection = new Connection();
                FileStream FS = new FileStream(CaptchaInfo.FilePath, FileMode.Open, FileAccess.Read);
                byte[] img = new byte[FS.Length];
                FS.Read(img, 0, Convert.ToInt32(FS.Length));

                objConnection.OpenConnection();
                if (objConnection.con.State == ConnectionState.Closed)
                    objConnection.con.Open();
                SqlCommand cmd = new SqlCommand("Insert into TblTraning(CaptchaImage,Code) OUTPUT INSERTED.TraningId Values('" + img + "','" + CaptchaInfo.Code + "')", objConnection.con);
                cmd.CommandType = CommandType.Text;
                //cmd.ExecuteNonQuery();
                CaptchaInfo.TraningId = (Int32)cmd.ExecuteScalar();
                
                //_CaptchaInfo.Code = txtImageCode.Text;
                MessageBox.Show("Image Save Successfully!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //CaptchaInfo returnCaptchaInfo = 
            }
            else
            {
                MessageBox.Show("First enter Image Code", "KSoft");
            }
        }

    }
}
