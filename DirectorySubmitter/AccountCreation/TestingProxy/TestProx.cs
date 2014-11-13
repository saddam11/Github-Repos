using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using SimpleBrowser;
using AccountCreation.Helper;
using System.Data;
using System.Net.NetworkInformation;
using System.IO;
using System.Globalization;
using System.Diagnostics;
using System.Web;
using System.Web.UI;
using System.Xml.Linq;
using System.Drawing;

namespace AccountCreation.TestingProxy
{
    public class TestProx
    {
        [STAThread]
        static void Main(string[] args)
        {
            //1. Pinging Proxy Server
            string[] proxyServers = { "201.116.227.173", "117.156.8.72", "91.185.215.141", "218.108.170.162", "221.182.75.74", "218.108.170.171", "61.147.67.2", "211.143.146.239", "46.53.184.180", "198.71.51.227" };
            string[] proxyPorts = { "3128", "80", "9179", "82", "8123", "82", "9125", "80", "3128", "80" };
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] 
            { new DataColumn() { ColumnName = "SrNo" } ,new DataColumn() { ColumnName = "ProxyIp" } ,new DataColumn() { ColumnName = "PortNo" } 
            });
            for (int iRow = 0; iRow < proxyServers.Length; iRow++)
            {
                //1.1 Adding Proxies to DataTable for Feature Use...
                dt.Rows.Add(dt.NewRow());
                dt.Rows[iRow][0] = iRow.ToString(); dt.Rows[iRow][1] = proxyServers[iRow].ToString(); dt.Rows[iRow][2] = proxyPorts[iRow].ToString();
            }

            try
            {
                for (int iRow = 0; iRow < proxyServers.Length; iRow++)
                {
                    strResult += "Fetching ProxyLists From Database..." + Environment.NewLine;
                    //1.2.Finding Whether Proxy Work or Not    and then If Success Then CreateAccount
                    strPingingResult = "Success"; // ((new Ping()).Send(dt.Rows[iRow][1].ToString())).Status.ToString();
                    if (strPingingResult == "Success")
                    {
                        //2. Clear All Data
                        //MultipleBrowsers objMultipleBrowsers = new MultipleBrowsers();
                        //objMultipleBrowsers.ClearCache();

                        //3. SetProxy with Success 



                        //5. Fill the Information From Database
                        #region Database Values Fetching...
                        Connection objConnection = new Connection();
                        DataTable dtAttribute = new DataTable();
                        strURL = "https://accounts.google.com/SignUp";
                        string strOrders = "FirstName,LastName,UserName,Passwords,ConfirmPasswords,DOBMonth,DOBDay,DOBYear,Gender,CurrentEmail,Captchas,Locations,AgreeTerms";
                        string strQueryAtttribute = "Select " + strOrders + " from TblAccount Where AccountType='" + strURL + "' ";
                        string strQueryDBVal = "Select " + strOrders + " from TblUser";
                        dtAttribute = objConnection.GetAll(strQueryAtttribute).Tables[0];
                        string[] strAttributeName = dtAttribute.Rows[0].ItemArray.Select(x => x.ToString()).ToArray();

                        DataTable dtDBValues = new DataTable();
                        dtDBValues = objConnection.GetAll(strQueryDBVal).Tables[0];
                        string[] strDBValues = dtDBValues.Rows[0].ItemArray.Select(x => x.ToString()).ToArray();
                        #endregion


                        FillTheInfoFromDB(dtAttribute.Rows[0].Table, dtDBValues.Rows[0].Table);


                        //7. Display Result on Browser
                    }
                    else
                    {
                        strResult = "Proxy Not Working " + dt.Rows[iRow][1].ToString();
                    }
                }//for Loop for Each Proxy Server Closed
            }
            catch (Exception exp)
            {
                wbRegistration.Log(exp.Message, LogMessageType.Error);
                wbRegistration.Log(exp.StackTrace, LogMessageType.StackTrace);

                MessageBox.Show("Error: " + exp.Message);
            }
            finally
            {
                var path = WriteFile("log-" + DateTime.UtcNow.Ticks + ".html", wbRegistration.RenderHtmlLogFile("SimpleBrowser Sample - Request Log"));
                //Process.Start(path);
            }
            //Browser browser = new Browser();
            //string strURL = "http://www.lagado.com/proxy-test";
            //browser.SetProxy("221.182.75.74", 8123);
            //browser.Navigate(strURL);
            //(new frmShowOutput(browser.CurrentHtml)).ShowDialog();

        }



        private static void FillTheInfoFromDB(DataTable dtAttributeName, DataTable dtDBValues)
        {
            #region Browser Configurations
            wbRegistration.RequestLogged += OnBrowserRequestLogged;
            wbRegistration.MessageLogged += new Action<Browser, string>(OnBrowserMessageLogged);
            wbRegistration.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US) AppleWebKit/534.10 (KHTML, like Gecko) Chrome/8.0.552.224 Safari/534.10";

            //browser.SetProxy(dt.Rows[iRow][1].ToString(), Convert.ToInt32(dt.Rows[iRow][2].ToString()));
            try
            {
                //4. Navigate to gmail
                strURL = "https://accounts.google.com/SignUp";
                wbRegistration.Navigate(strURL);
                if (LastRequestFailed(wbRegistration)) return; // always check the last request in case the page failed to load
            #endregion

                List<HtmlResult> MainAttribute = new List<HtmlResult>();
                List<HtmlResult> PlaceHolderAttribute = new List<HtmlResult>();

                Browser br = wbRegistration;

                for (int i = 0; i < dtAttributeName.Columns.Count; i++)
                {
                    MainAttribute.Add(wbRegistration.Find(dtAttributeName.Rows[0][i].ToString()));
                    PlaceHolderAttribute.Add(wbRegistration.Find(dtAttributeName.Rows[0][i].ToString() + "-placeholder"));
                }

                bool blnCapchaSolve = false;
                string column = "";
                for (int i = 0; i < MainAttribute.Count; i++)
                {
                    string strTagType = MainAttribute[i].XElement.Name.LocalName.ToString();
                    if (strTagType.Equals("input"))
                    {
                        column = dtAttributeName.Rows[0][i].ToString();
                        if (MainAttribute[i].Exists)
                        {
                            if (column == "TermsOfService")
                            {
                                MainAttribute[i].XElement.Add(new XAttribute("checked", "true"));
                            }
                            else
                            {
                                br.Find("input", FindBy.Name, column).Value = dtDBValues.Rows[0][i].ToString();
                            }
                            if (!blnCapchaSolve && column.Equals("recaptcha_response_field"))
                            {
                                SolveCaptcha(br);
                                blnCapchaSolve = true;
                            }
                        }
                    }
                    else if (strTagType.Equals("select"))
                    {
                        #region Code For dropdown Selecction
                        column = dtAttributeName.Rows[0][i].ToString();
                        if (MainAttribute[i].Exists)
                        {
                            XDocument doc;
                            doc = br.XDocument;
                            var penItemValue = from opt in doc.Descendants("option")
                                               where opt.Attribute("value").Value == ""
                                               select opt;
                            switch (column)
                            {
                                case "BirthMonth":

                                    //XElement result = doc.Descendants("option").First();
                                    XElement xelement = ((IEnumerable<System.Xml.Linq.XElement>)penItemValue).ToList()[0];
                                    xelement.Attribute("value").Value = xelement.Attribute("value").Value + "" + dtDBValues.Rows[0][i].ToString() + "";
                                    xelement.Value = "" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(dtDBValues.Rows[0][i])).ToString() + "";
                                    xelement.Add(new XAttribute("selected", "true"));
                                    //br.Find("select", FindBy.Name, column).XElement.SetValue(System.Web.HttpUtility.HtmlDecode("<SELECT id=BirthMonth name=BirthMonth> <OPTION selected value=" + dtDBValues.Rows[0][i] + ">" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(dtDBValues.Rows[0][i])) + "</OPTION> <OPTION value=01>January</OPTION> <OPTION value=02>February</OPTION> <OPTION value=03>March</OPTION> <OPTION value=04>April</OPTION> <OPTION value=05>May</OPTION> <OPTION value=06>June</OPTION> <OPTION value=07>July</OPTION> <OPTION value=08>August</OPTION> <OPTION value=09>September</OPTION> <OPTION value=10>October</OPTION> <OPTION value=11>November</OPTION> <OPTION value=12>December</OPTION></SELECT>"));
                                    //br.XDocument.Save(doc); 
                                    break;
                                case "Gender":
                                    xelement = ((IEnumerable<System.Xml.Linq.XElement>)penItemValue).ToList()[0];
                                    if (dtDBValues.Rows[0][column].ToString().Equals("Female"))
                                    {
                                        xelement.Attribute("value").Value = xelement.Attribute("value").Value + "" + dtDBValues.Rows[0][i].ToString().ToUpper() + "";
                                        xelement.Value = "" + CultureInfo.GetCultureInfo("en-Us").TextInfo.ToTitleCase(dtDBValues.Rows[0][i].ToString()) + "";
                                        //br.Find("select", FindBy.Name, column).XElement.SetAttributeCI("aria-posinset", "1");
                                        break;
                                    }
                                    else if (dtDBValues.Rows[0][column].ToString().Equals("Male"))
                                    {
                                        xelement.Attribute("value").Value = xelement.Attribute("value").Value + "" + dtDBValues.Rows[0][i].ToString().ToUpper() + "";
                                        xelement.Value = "" + CultureInfo.GetCultureInfo("en-Us").TextInfo.ToTitleCase(dtDBValues.Rows[0][i].ToString()) + "";
                                        //br.Find("select", FindBy.Name, column).XElement.SetAttributeCI("aria-posinset", "2");
                                        break;
                                    }
                                    else if (dtDBValues.Rows[0][column].ToString().Equals("Other"))
                                    {
                                        xelement.Attribute("value").Value = xelement.Attribute("value").Value + "" + dtDBValues.Rows[0][i].ToString().ToUpper() + "";
                                        xelement.Value = "" + CultureInfo.GetCultureInfo("en-Us").TextInfo.ToTitleCase(dtDBValues.Rows[0][i].ToString()) + "";
                                        //br.Find("select", FindBy.Name, column).XElement.SetAttributeCI("aria-posinset", "3");
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                Console.ReadLine();
            }
            finally
            {

                //Process.Start(s);
            }

            #region code For Submit Button
            //HtmlElement btnSubmit = wbRegistration.Document.GetElementById("submitbutton");
            //if (btnSubmit != null)
            //{
            //    //"theNode.onclick = function(){ alert('You clicked a link with href:' + this.href); };";
            //    //wbRegistration.InvokeScript
            //    //wbRegistration.BeginInvoke(
            //    //Explorer is Object of SHDocVw.WebBrowserClass
            //    //HtmlDocument htmlDoc = wbRegistration.Document;//(HtmlDocument)this.Explorer.IWebBrowser_Document;

            //    ////inject Script
            //    //htmlDoc.InvokeScript("alert('Welcome to KSoft !!')");
            //    //htmlDoc।parentWindow.execScript("alert('hello world !!')", "javascript");


            //    //6. Invoke Submit Button


            //    //form.InvokeMember("submitbutton");
            //    btnSubmit.Focus();
            //    btnSubmit.InvokeMember("click");

            //    blnSuccess = true;
            //}
            //else
            //{
            //    blnSuccess = false;
            //}
            //return (wbRegistration);
            #endregion
        }

        private static void SolveCaptcha(Browser br)
        {
            var penItemValue = from opt in br.XDocument.Document.Descendants("img")
                                               where opt.Attribute("src").Value.Contains("www.google.com/recaptcha/api/image")
                                               select opt;
            XElement xelement = ((IEnumerable<System.Xml.Linq.XElement>)penItemValue).ToList()[0];
            CaptchaInfo.FilePath = xelement.GetAttribute("src");
            HtmlResult hrCaptcha = br.FindAll("img");

            #region Convert URL to Image Object
            System.Net.WebClient MyWebClient = new System.Net.WebClient();
            byte[] ImageInBytes = MyWebClient.DownloadData(CaptchaInfo.FilePath);
            System.IO.MemoryStream ImageStream = new System.IO.MemoryStream(ImageInBytes);
            Image img = new System.Drawing.Bitmap(ImageStream);
            #endregion

            #region Account Details
            CaptchaInfo.CaptchaImage = img;
            CaptchaInfo.UserName = "shivajik";
            CaptchaInfo.UserPassword = "admin12!";
            CaptchaDet.CaptchaDet.ApplyCaptcha();
            br.Find("input", FindBy.Id, "recaptcha_response_field").Value = CaptchaInfo.Code;
            #endregion
        }


        #region Event Handling for Browser Class

        static void OnBrowserRequestLogged(Browser req, HttpRequestLog log)
        {
            Console.WriteLine(" -> " + log.Method + " request to " + log.Url);
            Console.WriteLine("request Details are:");
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

        #endregion

        public static string strResult { get; set; }

        public static string strPingingResult { get; set; }

        public static string strURL { get; set; }

        public static Browser wbRegistration=new Browser();
        
    }
}
