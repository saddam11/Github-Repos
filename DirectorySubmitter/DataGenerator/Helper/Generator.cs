using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using DataGenerator.Helper.Country;

namespace DataGenerator.Helper
{
    public class Generator : IUserGenerator
    {
        string characters;
        string numbers;

        public string Characters
        {
            get { return characters; }
            set { characters = value; }
        }

        public string Numbers
        {
            get { return numbers; }
            set { numbers = value; }
        }

        public int MaxNum { get; set; }

        public int MinNum { get; set; }


        public Generator()
        {
            characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            numbers = "0123456789";

        }

        public Generator(int MinDay, int MaxDay)
        {
            characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            numbers = "0123456789";
            MinDay = 1;
            MaxDay = 31;
        }


        /// <summary>
        /// This Function For Generating Random firstname, lastName 
        /// </summary>
        /// <param name="count"></param>
        /// <param name="nameLength"></param>
        /// <returns></returns>
        public string[] RandomName(int count, int nameLength)
        {
            Random random = new Random();
            string[] rndName = new string[count];

            for (int i = 0; i < rndName.Length; i++)
            {
                StringBuilder result = new StringBuilder(nameLength);
                for (int j = 0; j < nameLength; j++)
                {
                    rndName[i] = CultureInfo.GetCultureInfo("en-US").TextInfo.ToTitleCase(result.Append(characters[random.Next(characters.Length)]).ToString().ToLower());
                }
            }
            return rndName;
        }
        /// <summary>
        /// This Function For Generating Random Alphanumeric with Symbols 
        /// </summary>
        /// <param name="count"></param>
        /// <param name="nameLength"></param>
        /// <returns></returns>
        public string[] RandomAlphaWithSymbols(int count, int nameLength)
        {
            characters += "!@#$%^*_1234567890";
            Random random = new Random();
            string[] rndName = new string[count];

            for (int i = 0; i < rndName.Length; i++)
            {
                StringBuilder result = new StringBuilder(nameLength);
                for (int j = 0; j < nameLength; j++)
                {
                    rndName[i] = CultureInfo.GetCultureInfo("en-US").TextInfo.ToTitleCase(result.Append(characters[random.Next(characters.Length)]).ToString().ToLower());
                }
            }
            characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            return rndName;
        }
        /// <summary>
        /// This Function For Generating Random password
        /// </summary>
        /// <param name="count"></param>
        /// <param name="nameLength"></param>
        /// <returns></returns>

        public string[] RandomNumber(int count, DateParts dateParts)
        {
            Random random = new Random();
            string[] rndNumber = new string[count];

            for (int i = 0; i < rndNumber.Length; i++)
            {
                switch (dateParts)
                {
                    case DateParts.Days:
                        MinNum = 1; MaxNum = 30;
                        string day = random.Next(MinNum, MaxNum).ToString();
                        rndNumber[i] = Convert.ToInt32(day) > 9 ? day : "0" + day;
                        break;

                    case DateParts.Months:
                        MinNum = 1; MaxNum = 13;
                        string month = random.Next(MinNum, MaxNum).ToString();
                        rndNumber[i] = Convert.ToInt32(month) > 9 ? month : "0" + month;
                        break;
                    case DateParts.Years:
                        MinNum = 1960; MaxNum = 2005;
                        rndNumber[i] = random.Next(MinNum, MaxNum).ToString();
                        break;

                    case DateParts.Gender:
                        if (i % 2 == 0)
                            rndNumber[i] = "Female";
                        else
                            rndNumber[i] = "Male";
                        break;
                    default:
                        string s = random.Next(MinNum, MaxNum).ToString();
                        rndNumber[i] = s;
                        break;
                }
            }
            return rndNumber;
        }

        /// <summary>
        /// This Function For searches country that matches condtion and provide random mobile no  
        /// </summary>
        /// <param name="count"></param>
        /// <param name="country"></param>
        /// <returns></returns>

        public string[] RandomMobileNo(int count, CountryEnum country)
        {
            string[] rndNumber = new string[count];
            switch (country)
            {
                case CountryEnum.India:
                    India obj = new India();
                    rndNumber = obj.RandomMobileNo(50);
                    break;
                case CountryEnum.China:
                    break;
                case CountryEnum.America:
                    break;
                default:
                    break;
            }
            return rndNumber;
        }

        /// <summary>
        /// This for getting random last name
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>

        public string[] RandomLastName(int count, CountryEnum country)
        {
            string[] rndLastName = new string[count];
            switch (country)
            {
                case CountryEnum.India:
                    India obj = new India();
                    rndLastName = obj.RandomLastName(count);
                    break;
                case CountryEnum.China:
                    break;
                case CountryEnum.America:
                    break;
                default:
                    break;
            }
            return rndLastName;
        }

        /// <summary>
        /// This function for getting random firstname
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public string[] RandomFirstName(int count, CountryEnum country)
        {
            string[] rndFirstName = new string[count];
            switch (country)
            {
                case CountryEnum.India:
                    India obj = new India();
                    rndFirstName = obj.RandomLastName(count);
                    break;
                case CountryEnum.China:
                    break;
                case CountryEnum.America:
                    break;
                default:
                    break;
            }
            return rndFirstName;
        }


        public object GetCountry(CountryEnum countyEnum)
        {
            object obj = new object();
            switch (countyEnum)
            {
                case CountryEnum.India:
                    obj = Activator.CreateInstance(typeof(India));
                    break;
                case CountryEnum.China:
                    break;
                case CountryEnum.America:
                    break;
                default:
                    break;
            }

            return obj;
        }
    }
}
