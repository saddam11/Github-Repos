using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;


namespace DataGenerator.Helper
{
    public class Connection
    {

        private static string ConnectionString = ConfigurationManager.ConnectionStrings["Connectionstring"].ConnectionString;
        public SqlConnection con = new SqlConnection(ConnectionString);
        public void OpenConnection()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
        }

        public void CloseConnection()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }

        public DataSet GetAll(string text)
        {
            this.OpenConnection();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = text;
            cmd.Connection = this.con;
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            ds.Clear();
            da.Fill(ds);
            this.CloseConnection();
            return ds;

        }

        public bool SaveUpdateDelete(string text)
        {
            this.OpenConnection();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = text;
            cmd.Connection = this.con;
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            this.CloseConnection();
            return true;

        }


        #region Code For IsNumeric Function

        public bool isNumeric(string val, System.Globalization.NumberStyles NumberStyle)
        {
            Double result;
            return Double.TryParse(val, NumberStyle, System.Globalization.CultureInfo.CurrentCulture, out result);
        }

        #endregion

    }
}
