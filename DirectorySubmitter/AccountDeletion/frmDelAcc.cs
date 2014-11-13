using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SimpleBrowser;
using System.IO;

namespace AccountDeletion
{
    public partial class frmDelAcc : Form
    {
        Browser browser;
        public static string strResult { get; set; }
        public static int Http_StatusCode { get; set; }

        public frmDelAcc()
        {
            InitializeComponent();
            browser = new Browser();
            browser.RequestLogged += OnBrowserRequestLogged;
            browser.MessageLogged += new Action<Browser, string>(OnBrowserMessageLogged);
            browser.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US) AppleWebKit/534.10 (KHTML, like Gecko) Chrome/8.0.552.224 Safari/534.10";
        }

        private void btnLoginCheck_Click(object sender, EventArgs e)
        {
            bool blnResult = false;
            blnResult = CheckLoginInfo(cboAccountType.SelectedItem.ToString(), txtUserName.Text.Trim(), txtPassword.Text.Trim());
            if (blnResult == false)
            {
                MessageBox.Show("Try Again");
            }
        }

        private bool CheckLoginInfo(string strAccountType,string strUserName, string strPassword)
        {
            bool blnResult=false;
            switch(strAccountType)
            {
                case "https://accounts.google.com/ServiceLogin":
                    browser.Navigate(strAccountType);
                    GmailAccDel(strUserName,strPassword);
                        break;
                    default:
                        break;
                }
                
                
            return(blnResult);
        }

        private void GmailAccDel(string strUserName,string strPassword)
        {
            try
            {
                browser.Find("input", FindBy.Id, "Email").Value = strUserName;
                browser.Find("input", FindBy.Id, "Passwd").Value = strPassword;
                var loginLink = browser.Find("input", FindBy.Name, "signIn");
                if (loginLink.Exists)
                {
                    var Result=loginLink.Click();
                    if(Http_StatusCode>=200 && Http_StatusCode <=399)
                    {
                        strResult = "OK";
                        browser.Log("RED");
                        browser.UserAgent="";
                        browser.Navigate("https://www.google.com/settings/datatools");
                        if (Http_StatusCode <= 399)
                        {
                            if (MessageBox.Show("Login Success! Are You Sure you want to Delete Account")==System.Windows.Forms.DialogResult.Yes)
                            {
                                if (DeleteAccount())
                                {
                                    MessageBox.Show("Account Deleted!!!");
                                }
                            }
                            
                        }

                    }
                    if (ClickResult.SucceededNavigationComplete == Result)
                    {
                        strResult = "Clicked";
                        browser.Log("Clicked");
                    }
                    else
                    {
                        strResult = "Response Code "+Result.ToString();
                        browser.Log("Can't find the login link! Perhaps the site is down for maintenance?");
                    }
                }
                else
                {
                    browser.Log("Can't find the login link! Perhaps the site is down for maintenance?");
                    Console.WriteLine("Value Not Inserted In Web Browser");
                }
            }
            catch (Exception ex)
            {
                browser.Log(ex.Message, LogMessageType.Error);
                browser.Log(ex.StackTrace, LogMessageType.StackTrace);
            }
            finally
            {
                //var path = WriteFile("log-" + DateTime.UtcNow.Ticks + ".html", browser.RenderHtmlLogFile("SimpleBrowser Sample - Request Log"));
                //Process.Start(path);
            }
}

        private bool DeleteAccount()
        {
            bool blnResult = false;
            var VwMngAcc=browser.Find("a", FindBy.Text, "Delete account and data");
            if (VwMngAcc.Exists)
            {
                VwMngAcc.Click();
                if (Http_StatusCode <= 399)
                {
                    MessageBox.Show("Working Account Deletion Process");
                    blnResult = true;
                }
            }
            else
            {
                MessageBox.Show("NOT Working Account Deletion Process");
            }

            return(blnResult);
        }
        static bool LastRequestFailed(Browser browser)
        {
            if (browser.LastWebException != null)
            {
                browser.Log("There was an error loading the page: " + browser.LastWebException.Message);
                return true;
            }
            return false;
        }

        static void OnBrowserMessageLogged(Browser browser, string log)
        {
            Console.WriteLine(log);
        }

        static void OnBrowserRequestLogged(Browser req, HttpRequestLog log)
        {
            Console.WriteLine(" -> " + log.Method + " request to " + log.Url);
            Console.WriteLine("request Details are:");
            Http_StatusCode = log.StatusCode;
            Console.WriteLine(" <- Response status code: " + log.StatusCode);
        }

        static string WriteFile(string filename, string text)
        {
            var dir = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs"));
            if (!dir.Exists) dir.Create();
            var path = Path.Combine(dir.FullName, filename);
            File.WriteAllText(path, text);
            return path;
        }

        
    }
}
