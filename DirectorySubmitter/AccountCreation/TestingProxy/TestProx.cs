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

            //4. Navigate to gmail
            strURL = "https://accounts.google.com/SignUp";
            //strURL = "http://localhost/SignupGmail.htm";
            wbRegistration.Navigate(strURL);
            if (LastRequestFailed(wbRegistration)) return; // always check the last request in case the page failed to load
            #endregion

            List<HtmlResult> MainAttribute = new List<HtmlResult>();
            List<HtmlResult> PlaceHolderAttribute = new List<HtmlResult>();

            Browser br = wbRegistration;

            for (int i = 0; i < dtAttributeName.Columns.Count; i++ )
            {
                MainAttribute.Add(wbRegistration.Find(dtAttributeName.Rows[0][i].ToString()));
                PlaceHolderAttribute.Add(wbRegistration.Find(dtAttributeName.Rows[0][i].ToString() + "-placeholder"));
            }

            bool blnCapchaSolve = false;
            string column="";
            for (int i = 0; i < MainAttribute.Count; i++)
            {

                if (MainAttribute[i].XElement.Name.LocalName.ToString().Equals("input"))
                {
                    column = dtAttributeName.Rows[0][i].ToString();
                    if (MainAttribute[i].Exists)
                        br.Find("input", FindBy.Name, column).Value = dtDBValues.Rows[0][i].ToString();
                }
                else
                {
                    if (MainAttribute[i].XElement.Name.LocalName.ToString().Equals("select"))
                        column = dtAttributeName.Rows[0][i].ToString();
                    #region ComboBox Code...
                    if (MainAttribute[i].Exists)
                    {
                        switch (column)
                        {
                            case "BirthMonth":
                                br.Find("select", FindBy.Name, column).XElement.SetValue("<SELECT id=BirthMonth name=BirthMonth> <OPTION selected value=" + dtDBValues.Rows[0][i] + ">" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(dtDBValues.Rows[0][i])) + "</OPTION> <OPTION value=01>January</OPTION> <OPTION value=02>February</OPTION> <OPTION value=03>March</OPTION> <OPTION value=04>April</OPTION> <OPTION value=05>May</OPTION> <OPTION value=06>June</OPTION> <OPTION value=07>July</OPTION> <OPTION value=08>August</OPTION> <OPTION value=09>September</OPTION> <OPTION value=10>October</OPTION> <OPTION value=11>November</OPTION> <OPTION value=12>December</OPTION></SELECT>");
                                break;
                            case "Gender":
                                if (dtDBValues.Rows[0][column].ToString().Equals("Female"))
                                {
                                    br.Find("select", FindBy.Name, column).XElement.SetAttributeCI("aria-posinset", "1");
                                    break;
                                }
                                else if (dtDBValues.Rows[0][column].ToString().Equals("Male"))
                                {
                                    br.Find("select", FindBy.Name, column).XElement.SetAttributeCI("aria-posinset", "2");
                                    break;
                                }
                                else if (dtDBValues.Rows[0][column].ToString().Equals("Other"))
                                {
                                    br.Find("select", FindBy.Name, column).XElement.SetAttributeCI("aria-posinset", "3");
                                }
                                break;
                            case "CountryCode":
                            default:
                                break;
                        }

                    }
                    #endregion
                }

            }
            var loginLink = br.Find("input", FindBy.Name, "submitbutton");
            if (loginLink.Exists)
            {
                var Result = loginLink.Click();
                if (ClickResult.SucceededNavigationComplete == Result)
                {
                    Console.WriteLine("===========================================================================");
                    (new frmShowOutput(br.CurrentHtml)).ShowDialog();
                    Console.WriteLine(br.CurrentHtml);
                    Console.WriteLine("===========================================================================");
                }
                else
                {
                    Console.WriteLine("===========================================================================");
                    (new frmShowOutput(br.CurrentHtml)).ShowDialog();
                    Console.WriteLine(br.CurrentHtml);
                    Console.WriteLine("===========================================================================");
                }
            }
            
            //        if (hrAttribute.Value.Equals("BirthMonth"))
            //        {

            //            //hrAttribute.OffsetParent.FirstChild.InnerHtml = "<SELECT id=BirthMonth name=BirthMonth> <OPTION selected value=" + strDBValues[i] + ">" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(strDBValues[i])) + "</OPTION> <OPTION value=01>January</OPTION> <OPTION value=02>February</OPTION> <OPTION value=03>March</OPTION> <OPTION value=04>April</OPTION> <OPTION value=05>May</OPTION> <OPTION value=06>June</OPTION> <OPTION value=07>July</OPTION> <OPTION value=08>August</OPTION> <OPTION value=09>September</OPTION> <OPTION value=10>October</OPTION> <OPTION value=11>November</OPTION> <OPTION value=12>December</OPTION></SELECT>";
            //        }
            //        else if (hrAttribute.Id == "Gender")
            //        {
            //            HtmlElementCollection hecSelects = wbRegistration.Document.GetElementsByTagName("select");
            //            hecSelects[1].Document.GetElementsByTagName("option")[13].SetAttribute("value", strDBValues[i].ToUpper());
            //            hecSelects[1].Document.GetElementsByTagName("option")[13].InnerHtml = strDBValues[i].ToString();
            //            if (strDBValues[i].ToString().Equals("Female"))
            //                hecSelects[1].Document.GetElementsByTagName("option")[13].SetAttribute("aria-posinset", "1");
            //            else if (strDBValues[i].ToString().Equals("Male"))
            //                hecSelects[1].Document.GetElementsByTagName("option")[13].SetAttribute("aria-posinset", "2");
            //            else if (strDBValues[i].ToString().Equals("Other"))
            //                hecSelects[1].Document.GetElementsByTagName("option")[13].SetAttribute("aria-posinset", "3");
            //        }

            //        else if (hrAttribute.Id == "CountryCode")
            //        {
            //            HtmlElementCollection hecSelects = wbRegistration.Document.GetElementsByTagName("select");

            //        }
            //        else if (hrAttribute.Id == "TermsOfService")
            //        {
            //            wbRegistration.Document.GetElementById("TermsOfService").SetAttribute("value", "yes");
            //            wbRegistration.Document.GetElementById("TermsOfService").SetAttribute("aria-invalid", "true");
            //            wbRegistration.Document.GetElementById("TermsOfService").SetAttribute("checked", "True");

            //        }
            //        else if (hrAttribute.Id == "recaptcha_response_field" && !blnCapchaSolve)
            //        {
            //            HtmlElement heCaptchaImage = wbRegistration.Document.GetElementById("recaptcha_challenge_image");
            //            CaptchaInfo.FilePath = wbRegistration.Document.GetElementById("recaptcha_challenge_image").GetAttribute("src");

            //            #region Convert URL to Image Object
            //            System.Net.WebClient MyWebClient = new System.Net.WebClient();
            //            byte[] ImageInBytes = MyWebClient.DownloadData(CaptchaInfo.FilePath);
            //            System.IO.MemoryStream ImageStream = new System.IO.MemoryStream(ImageInBytes);
            //            Image img = new System.Drawing.Bitmap(ImageStream);
            //            #endregion

            //            #region Account Details
            //            CaptchaInfo.CaptchaImage = img;
            //            CaptchaInfo.UserName = "shivajik";
            //            CaptchaInfo.UserPassword = "admin12!";
            //            CaptchaDet.CaptchaDet.ApplyCaptcha();
            //            wbRegistration.Document.GetElementById("recaptcha_response_field").SetAttribute("value", CaptchaInfo.Code);
            //            #endregion

            //            blnCapchaSolve = true;
            //        }
            //        else
            //            hrAttribute.SetAttribute("value", strDBValues[i]);
            //    }
            //}

            //foreach (HtmlElement placehd in PlaceHolderAttribute)
            //{
            //    if (placehd != null)
            //    {
            //        placehd.SetAttribute("InnerHtml", "");
            //    }
            //}

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
