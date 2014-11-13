using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Threading;
using System.Threading.Tasks;

using AccountCreation.Helper;
using DirectorySubmitter.CommonControls;
using Microsoft.Win32;
using System.Net;


namespace AccountCreation.UI
{
    public partial class FrmRegister : Form
    {
        SHDocVw.WebBrowser axBrowser;// = (SHDocVw.WebBrowser)this.wbRegistration.ActiveXInstance;
        public bool isAlreadyLoaded { get; set; }
        public FrmRegister()
        {
            InitializeComponent();
            wbRegistration.AllowNavigation = true;
            axBrowser = (SHDocVw.WebBrowser)this.wbRegistration.ActiveXInstance;
            axBrowser.NavigateError +=new SHDocVw.DWebBrowserEvents2_NavigateErrorEventHandler(axBrowser_NavigateError);
            isAlreadyLoaded = false;
        }

        void axBrowser_NavigateError(object pDisp, ref object URL,ref object Frame, ref object StatusCode, ref bool Cancel)
        {
            if (StatusCode.ToString() == "404")
            {
                MessageBox.Show("Page no found");
            }
            else
            {
                MessageBox.Show("Status Code is: "+StatusCode.ToString());
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            wbRegistration.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(wbRegistration_DocumentCompleted);
            wbRegistration.Navigate(txtSearh.Text);
            //var browser = new SimpleBrowser.Browser();
            //browser.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/38.0.2125.111 Safari/537.36";
            //browser.Navigate("http://ifconfig.me/all");
            //browser.SetProxy("221.182.75.74", 8123);
            //browser.Navigate("http://www.lagado.com/proxy-test");
            //wbRegistration.DocumentText=browser.CurrentHtml;
            //browser.SetProxy(

            //Exec("221.182.75.74:8123", "http://www.wikipedia.com/");
            //Exec("221.182.75.74:8123", "https://accounts.google.com/SignUp");
            
            //System.Windows.Forms.WebBrowserBase wbb = new WebBrowserBase();
            
        }
        private void wbRegistration_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //isAlreadyLoaded = true;
            if (isAlreadyLoaded == false)
            {
                //wbRegistration.DocumentCompleted -= new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(wbRegistration_DocumentCompleted);
                //wbRegistration.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(www_DocumentCompleted_logado);
                
                Connection objConnection = new Connection();
                DataTable dtAttribute = new DataTable();

                string strQueryAtttribute = "Select " + txtSearh.Tag + " from TblAccount Where AccountType='" + txtSearh.Text + "' ";
                string strQueryDBVal = "Select " + txtSearh.Tag + " from TblUser ";

                string[] strAttributeName = objConnection.GetAll(strQueryAtttribute).Tables[0].Rows[0].ItemArray.Select(x => x.ToString()).ToArray();
                string[] strDBValues = objConnection.GetAll(strQueryDBVal).Tables[0].Rows[0].ItemArray.Select(x => x.ToString()).ToArray();

                InsertDataToBrowser(strAttributeName, strDBValues, txtSearh.Text);
                isAlreadyLoaded = true;
            }
        }
        void www_DocumentCompleted_logado(object sender, System.Windows.Forms.WebBrowserDocumentCompletedEventArgs e)
        {
            //System.IO.StreamWriter sw = new StreamWriter(@"D:\Projects\DirectorySubmitter\OutputGeneratedWithGmail.txt");
            //sw.Write(wbRegistration.DocumentText);
            //sw.Close();
            //MessageBox.Show(e.Url.AbsolutePath);
        }
        private void SetProxy(string Proxy,bool blnEnable)
        {

            MessageBox.Show("Setting :" + Proxy);
            string key = "Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings";

            RegistryKey RegKey = Registry.CurrentUser.OpenSubKey(key, true);
            if (blnEnable)
            {
                RegKey.SetValue("ProxyServer", Proxy);
                RegKey.SetValue("ProxyEnable", 1);
            }
            else
            {
                RegKey.SetValue("ProxyServer", "");
                RegKey.SetValue("ProxyEnable", 0);
            }

        }
        void Exec(string proxy, string url)
        {
            ClearAllDataBeforeSettingProxy();
            var th = new Thread(() =>
            {
                SetProxy(proxy,false);
                using (wbRegistration = new WebBrowser())
                {
                    wbRegistration.DocumentCompleted += (sndr, e) =>
                    {
                        wbRegistration.Navigate("http://ifconfig.me/all");
                        Application.ExitThread();
                    };
                    wbRegistration.Navigate(url);
                    Application.Run();
                }
            });
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            th.Join();
        }

        private void ClearAllDataBeforeSettingProxy()
        {
            CookieContainer c = new CookieContainer();
            
            var cookies = c.GetCookies(new Uri("https://accounts.google.com/SignUp"));
            var cks = c.GetCookieHeader(new Uri("https://accounts.google.com/SignUp"));
            Console.WriteLine("Cks: "+cks);
            foreach (Cookie co in cookies)
            {
                co.Expires = DateTime.Now.Subtract(TimeSpan.FromDays(1));
                //Response.Cookies("Name") = "" OR Response.Cookies("Name").Expires = now() - 365
            }
        }
        private void InsertDataToBrowser(string[] strAttributeName, string[] strDBValues,string strURL)
        {
            switch (strURL)
            {
                case "https://accounts.google.com/SignUp":
                    InsertData.ToGmail(strAttributeName, strDBValues, wbRegistration);
                    break;
                case "https://signup.live.com":
                    InsertData.ToLive(strAttributeName, strDBValues, wbRegistration);
                    break;
                case "https://na.edit.yahoo.com/registration":
                    InsertData.ToYahoo(strAttributeName, strDBValues,wbRegistration);
                    break;
                default:
                    break;
            }
            if (InsertData.blnSuccess)
            {
                //MessageBox.Show("Account Created Successfully");
            }
            else
            {
                MessageBox.Show("Account Cannot Created");
            }
        }

       
        private void txtSearh_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == System.Windows.Forms.Keys.F2)
                {

                    Connection objConnection = new Connection();

                    System.Data.DataTable dtProduct = objConnection.GetAll("Select Orders,AccountType from TblAccount").Tables[0];
                    FrmSearch objFrmSearch = new FrmSearch(dtProduct);
                    objFrmSearch.ShowDialog();
                    if (objFrmSearch._strReturn != null)
                    {
                        txtSearh.Text = objFrmSearch._strReturn[1].ToString();
                        txtSearh.Tag = objFrmSearch._strReturn[0];
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "KSoft");
            }
        }

        private void FrmRegister_Load(object sender, EventArgs e)
        {
            //if ((this.wbRegistration.Document == null && this.wbRegistration.ActiveXInstance == null))
            //{
            //    throw new ApplicationException("wbRegistration");
            //}
            //// handle NewWindow
            //var activex = (SHDocVw.WebBrowser_V1)this.wbRegistration.ActiveXInstance;

            

        }

        private void wbRegistration_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            
        }

        private void wbRegistration_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
           
        }

        

    }
}
