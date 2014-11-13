using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataGenerator.Helper;
using DataGenerator.DEL;
using DataGenerator.Helper.Country;

namespace DataGenerator
{
    public partial class NameGenerator : Form
    {
        public NameGenerator()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            GenerateByCountry(50,CountryEnum.India,10);
        }

        private void GenerateByCountry(int recordCount, CountryEnum countryEnum,int CharacterLength)
        {
            
            Generator objGenerator = new Generator();

            string[] firstName = objGenerator.RandomFirstName(recordCount,CountryEnum.India);
            string[] lastName = objGenerator.RandomLastName(recordCount, CountryEnum.India);
            string[] userName = objGenerator.RandomName(recordCount, CharacterLength);
            string[] password = objGenerator.RandomAlphaWithSymbols(recordCount, CharacterLength);
            string[] dateOfMonth = objGenerator.RandomNumber(recordCount, DateParts.Months);
            string[] dateOfDay = objGenerator.RandomNumber(recordCount, DateParts.Days);
            string[] dateOfYear = objGenerator.RandomNumber(recordCount, DateParts.Years);
            string[] dateOfGender = objGenerator.RandomNumber(recordCount, DateParts.Gender);
            string[] mobileNo = objGenerator.RandomMobileNo(recordCount, countryEnum);

            India objCountry = (India)objGenerator.GetCountry(CountryEnum.India);

            textBox1.Text = (String.Join(Environment.NewLine, objGenerator.RandomName(recordCount, CharacterLength)));
            textBox2.Text = (String.Join(Environment.NewLine, objGenerator.RandomNumber(recordCount, DateParts.Years)));
            textBox3.Text = (String.Join(Environment.NewLine, objGenerator.RandomMobileNo(recordCount, Helper.Country.CountryEnum.India)));
            textBox4.Text = (String.Join(Environment.NewLine, objGenerator.RandomName(recordCount, CharacterLength)));
            textBox5.Text = (String.Join(Environment.NewLine, objGenerator.RandomName(recordCount, CharacterLength)));

            Connection objConnection = new Connection();

            DataTable dtResult = objConnection.GetAll("Select * from TblUser").Tables[0];
            if (dtResult.Rows.Count > 0)
            {
                objConnection.SaveUpdateDelete("Delete from TblUser");
            }

            for (int i = 0; i < recordCount; i++)
            {
                TblUser objTblUser = new TblUser()
                {
                    FirstName = firstName[i],
                    LastName = lastName[i],
                    UserName = userName[i],
                    UserPwd = password[i],
                    ConfirmPasswords = password[i],
                    DOBMonth = dateOfMonth[i],
                    DOBDay = dateOfDay[i],
                    DOBYear = dateOfYear[i],
                    Gender = dateOfGender[i],
                    MobileNo = mobileNo[i],
                    Locations = objCountry.Locations,
                    ZipCode = objCountry.ZipCode,
                    CountryCode = objCountry.CountyCode
                };

                objConnection.SaveUpdateDelete("Insert into TblUser(FirstName,LastName,UserName,Passwords,ConfirmPasswords,DOBMonth,DOBDay,DOBYear,Gender,MobileNo,Locations,ZipCode,CountryCode) values('" + objTblUser.FirstName + "','" + objTblUser.LastName + "','" + objTblUser.UserName + "','" + objTblUser.UserPwd + "','" + objTblUser.ConfirmPasswords + "','" + objTblUser.DOBMonth + "','" + objTblUser.DOBDay + "','" + objTblUser.DOBYear + "','" + objTblUser.Gender + "','" + objTblUser.MobileNo + "','" + objTblUser.Locations + "','" + objTblUser.ZipCode + "','" + objTblUser.CountryCode + "')");
            }
        }
    }
}