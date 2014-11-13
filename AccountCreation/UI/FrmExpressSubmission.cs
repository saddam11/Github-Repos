using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using SimpleBrowser;
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.IO;
using AccountCreation.Helper;
using System.Net;

namespace AccountCreation.UI
{
    public partial class FrmExpressSubmission : Form
    {
        public FrmExpressSubmission()
        {
            InitializeComponent();
        }
        public string strSubnetMask { get; set; }
        public static int iStatusCode { get; set; }
        private void btnMultiProcess_Click(object sender, EventArgs e)
        {
            var browser = new Browser();
            string strPingingResult = "";

            ThreadStart childref = null;
            int iProxyCounter = 0;
            string[] proxyServers = { "201.116.227.173", "117.156.8.72", "91.185.215.141", "218.108.170.162", "221.182.75.74", "218.108.170.171", "61.147.67.2", "211.143.146.239", "46.53.184.180", "198.71.51.227" };
            string[] proxyPorts = { "3128", "80", "9179", "82", "8123", "82", "9125", "80", "3128", "80" };
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] 
            { new DataColumn() { ColumnName = "SrNo" } ,new DataColumn() { ColumnName = "ProxyIp" } ,new DataColumn() { ColumnName = "PortNo" } 
            });
            for (int iRow = 0; iRow < proxyServers.Length; iRow++)
            {
                //1. Adding Proxies to DataTable for Feature Use...
                dt.Rows.Add(dt.NewRow()); 
                dt.Rows[iRow][0] = iRow.ToString(); dt.Rows[iRow][1] = proxyServers[iRow].ToString(); dt.Rows[iRow][2] = proxyPorts[iRow].ToString();
            }
            for (int iRow = 0; iRow < proxyServers.Length; iRow++)
            {
                txtConsoleResult.Text += "Fetching ProxyLists From Database..." + Environment.NewLine;
                //2.Finding Whether Proxy Work or Not    and then If Success Then CreateAccount
                strPingingResult = ((new Ping()).Send(dt.Rows[iRow][1].ToString())).Status.ToString();

                if (strPingingResult == "Success")
                {
                    txtConsoleResult.Text += "Pinging Successfull..." + Environment.NewLine;
                    //NetworkManagement objNetworkManagement= new NetworkManagement();
                    //strSubnetMask = objNetworkManagement.getSubnetMaskByPublicIP(dt.Rows[iRow][1].ToString());
                    //objNetworkManagement.setIP(dt.Rows[iRow][1].ToString(), strSubnetMask);

                    //if (iProxyCounter >= 5)
                    {
                        browser.SetProxy(dt.Rows[iRow][1].ToString(), Convert.ToInt32(dt.Rows[iRow][2].ToString()));
                        txtConsoleResult.Text += "Proxy Applied to Browser Control: " + iStatusCode.ToString() + Environment.NewLine;
                        if (browser.Navigate("http://www.wikipedia.org"))
                        {
                            txtConsoleResult.Text += "Navigation Success Code: " + iStatusCode.ToString() + Environment.NewLine;
                            //txtConsoleResult.Text += "Response CurrentHTML Response"+browser.CurrentHtml + Environment.NewLine;
                            browser.LogRequestData();
                            HttpRequestLog req= browser.RequestData();
                            //var y = req.RequestHeaders();
                            //WebHeaderCollection whcRequest = req.RequestHeaders();
                            //var z = req.ResponseHeaders();
                            //txtConsoleResult.Text += "Request Headers: " + req.RequestHeaders[0].ToString() +Environment.NewLine;
                            
                        }
                        else
                        {
                            txtConsoleResult.Text += "Navigation Error Code...: " + iStatusCode.ToString() + Environment.NewLine;
                        }
                    }
                    //childref = new ThreadStart(() => CreateAccount(browser));
                }
                else
                {
                    txtConsoleResult.Text += strPingingResult+"Logging..." + Environment.NewLine;
                }
            }
        }//Close btnMultiProcess_Click
        public void CreateAccount(Browser browser)
        {

            try
            {
                txtConsoleResult.Text += "Account Creation Started..." + Environment.NewLine;
                
                #region Code For AccountCreation using WebBrowserControl or With Custom Browser
                //// log the browser request/response data to files so we can interrogate them in case of an issue with our scraping
                //browser.RequestLogged += OnBrowserRequestLogged;
                //browser.MessageLogged += new Action<Browser, string>(OnBrowserMessageLogged);


                //// we'll fake the user agent for websites that alter their content for unrecognised browsers
                //browser.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US) AppleWebKit/534.10 (KHTML, like Gecko) Chrome/8.0.552.224 Safari/534.10";

                //// browse to Wikipedia
                //browser.Navigate("http://www.wikipedia.org/");

                ////string strMac = browser.readMAC();
                ////strMac = "7C-DD-90-61-3C-1F";
                ////browser.writeMAC(strMac);

                //if (LastRequestFailed(browser)) return; // always check the last request in case the page failed to load

                //// click the login link and click it
                //browser.Log("First we need to log in, so browse to the login page, fill in the login details and submit the form.");
                ////var loginLink = browser.Find("input", FindBy.Id, "searchInput").Value;
                //browser.Find("input", FindBy.Id, "searchInput").Value = "Shaktimaan";
                //var loginLink = browser.Find("input", FindBy.Name, "go");
                //if (loginLink.Exists)
                //{
                //    loginLink.Click();
                //    Console.WriteLine("Value Inserted: ");
                //    browser.Log("Can't find the login link! Perhaps the site is down for maintenance?");
                //}
                //else
                //{
                //    browser.Log("Can't find the login link! Perhaps the site is down for maintenance?");
                //    Console.WriteLine("Value Not Inserted In Web Browser");
                //}
                #endregion
                txtConsoleResult.Text += "Account Creation Stopped..." + Environment.NewLine;
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
        }//Close CreateAccount Function
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
            iStatusCode = log.StatusCode;
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
    }//Close Class
}//Close Namespace
