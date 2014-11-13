using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using AccountCreation.Helper;
using System.Drawing.Imaging;

namespace AccountCreation.UI
{
    public partial class FrmAddCaptchaManually : Form
    {
        public string strImagePath { get; set; }

        public FrmAddCaptchaManually()
        {
            InitializeComponent();
            LoadImage();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            SaveCaptcha();
        }

        private void SaveCaptcha()
        {
            if (txtImageCode.Text.Trim().Length != 0)
            {
                Connection objConnection = new Connection();
                FileStream FS = new FileStream(strImagePath, FileMode.Open, FileAccess.Read);
                byte[] img = new byte[FS.Length];
                FS.Read(img, 0, Convert.ToInt32(FS.Length));

                objConnection.OpenConnection();
                if (objConnection.con.State == ConnectionState.Closed)
                    objConnection.con.Open();
                SqlCommand cmd = new SqlCommand("Insert into TblTraning(CaptchaImage,Code) OUTPUT INSERTED.TraningId Values('" + img + "','" + txtImageCode.Text + "')", objConnection.con);
                cmd.CommandType = CommandType.Text;
                //cmd.ExecuteNonQuery();
                CaptchaInfo.TraningId = (Int32)cmd.ExecuteScalar();
                CaptchaInfo.Code = txtImageCode.Text;

                if (MessageBox.Show("Image Save Successfully!!", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    this.Close();
                }
                else
                {
                    txtImageCode.Clear();
                }
                //CaptchaInfo returnCaptchaInfo = 
            }
            else
            {
                MessageBox.Show("First enter Image Code", "KSoft");
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            LoadImage();
            
        }
        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
        private void LoadImage()
        {
            try
            {
                string strFilePath = CaptchaInfo.FilePath;
                //pictureBox1.ImageLocation = objCaptchaInfo.FilePath;
                //pictureBox1.Image = byteArrayToImage(ZetaLongPaths.ZlpIOHelper.ReadAllBytes(objCaptchaInfo.FilePath));
                pictureBox1.Image = CaptchaInfo.CaptchaImage;// new Bitmap(objCaptchaInfo.FilePath);
                //strImagePath = @"D:\old-data\Projects\AccountCreation\Images\CurrentImage.jpg";
                strImagePath = Application.StartupPath + @"\Images\CurrentImage.jpg";
                pictureBox1.Image.Save(strImagePath, ImageFormat.Jpeg);
                CaptchaInfo.FilePath = strImagePath;
                CaptchaInfo.CaptchaImage = pictureBox1.Image;
                CaptchaInfo.Code= txtImageCode.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading image" + ex.Message);
            }
        }
        
        public void Retrive()
        {
            //if (cmbImageID.SelectedValue != null)
            //{
            //    if (picImage.Image != null)
            //        picImage.Image.Dispose();

            //    SqlConnection con = new SqlConnection(DBHandler.GetConnectionString());
            //    SqlCommand cmd = new SqlCommand("ReadImage", con);
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@imgId", SqlDbType.Int).Value =
            //              Convert.ToInt32(cmbImageID.SelectedValue.ToString());
            //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
            //    DataTable dt = new DataTable();
            //    try
            //    {
            //        if (con.State == ConnectionState.Closed)
            //            con.Open();
            //        adp.Fill(dt);
            //        if (dt.Rows.Count > 0)
            //        {
            //            MemoryStream ms = new MemoryStream((byte[])dt.Rows[0]["ImageData"]);
            //            picImage.Image = Image.FromStream(ms);
            //            picImage.SizeMode = PictureBoxSizeMode.StretchImage;
            //            picImage.Refresh();
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message, "Error",
            //              MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //    finally
            //    {
            //        if (con.State == ConnectionState.Open)
            //            con.Close();
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Please Select a Image ID to Display!!",
            //       "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        //private CaptchaInfo RetriveCaptcha()
        //{
        //Connection objConnection = new Connection();
        //SqlCommand cmd = new SqlCommand("Select * from TblTraning Where ", con);
        //cmd.CommandType = CommandType.StoredProcedure;
        //cmd.Parameters.Add("@imgId", SqlDbType.Int).Value =
        //          Convert.ToInt32(cmbImageID.SelectedValue.ToString());
        //SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //DataTable dt = new DataTable();
        //try
        //{
        //    if (con.State == ConnectionState.Closed)
        //        con.Open();
        //    adp.Fill(dt);
        //    if (dt.Rows.Count > 0)
        //    {
        //        MemoryStream ms = new MemoryStream((byte[])dt.Rows[0]["ImageData"]);
        //        picImage.Image = Image.FromStream(ms);
        //        picImage.SizeMode = PictureBoxSizeMode.StretchImage;
        //        picImage.Refresh();
        //    }
        //}
        //catch (Exception ex)
        //{
        //    MessageBox.Show(ex.Message, "Error",
        //          MessageBoxButtons.OK, MessageBoxIcon.Error);
        //}
        //finally
        //{
        //    if (con.State == ConnectionState.Open)
        //        con.Close();
        //}
        //    }
    }
}
