using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AccountCreation.Helper
{
    public static class CaptchaInfo
    {
        static int _TraningId;
        static Image _CaptchaImage;
        static string _Code;
        static string _FilePath;
        static string _UserName;
        static string _UserPassword;

        public static string UserPassword
        {
            get { return _UserPassword; }
            set { _UserPassword = value; }
        }

        public static string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        public static int TraningId
        {
            get { return _TraningId; }
            set { _TraningId = value; }
        }

        public static Image CaptchaImage
        {
            get { return _CaptchaImage; }
            set { _CaptchaImage = value; }
        }

        public static string Code
        {
            get { return _Code; }
            set { _Code = value; }
        }

        public static string FilePath
        {
            get { return _FilePath; }
            set { _FilePath = value; }
        }
    }
}
