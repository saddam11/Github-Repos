using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DirectorySubmitterDB.DEL;
using System.Data;
using System.Data.SqlClient;
using AccountCreation.Helper;

namespace DAL.Helper
{
    public class UserService
    {
        Connection objConnection;
        
        #region Constructor
        public UserService()
        {
            objConnection = new Connection();
        }
        #endregion

        #region Code For ValidateUser
        public TblUser ValidateUser(TblUser objTblUser )
        {
            try
            {
                DataSet ds = new DataSet();
                objConnection.OpenConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Select * from tbl_User where UserName='" + objTblUser.UserName + "' and UserPwd='"+ objTblUser.Passwords +"'";
                cmd.Connection = objConnection.con;
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    objTblUser.UserId= Convert.ToInt32(ds.Tables[0].Rows[i][0].ToString());
                    objTblUser.CurrentEmail= ds.Tables[0].Rows[i][1].ToString();
                    objTblUser.MobileNo= ds.Tables[0].Rows[i][2].ToString();
                    objTblUser.UserName= ds.Tables[0].Rows[i][3].ToString();
                    objTblUser.Passwords = ds.Tables[0].Rows[i][4].ToString();
                }
                return objTblUser;
            }
            catch (Exception)
            {
                throw new Exception("UserServices.ValidateUser Not Executed Successfully");
            }
        }
        #endregion
    }
}
