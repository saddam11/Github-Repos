using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Drawing;
using System.Net;

namespace AccountCreation.Helper
{
    public class InsertData
    {
        public static bool blnSuccess { get; set; }

        public static void ToGmail(string[] strAttributeName, string[] strDBValues, WebBrowser wbRegistration)
        {

            List<HtmlElement> MainAttribute = new List<HtmlElement>();
            List<HtmlElement> PlaceHolderAttribute = new List<HtmlElement>();

            foreach (string item in strAttributeName)
            {
                MainAttribute.Add(wbRegistration.Document.GetElementById(item));
                PlaceHolderAttribute.Add(wbRegistration.Document.GetElementById(item + "-placeholder"));
            }

            bool blnCapchaSolve = false;
            for (int i = 0; i < MainAttribute.Count; i++)
            {
                HtmlElement heAttribute = MainAttribute[i]; 
                if (heAttribute != null)
                {
                    if (heAttribute.Name.Equals("BirthMonth"))
                    {
                        heAttribute.OffsetParent.FirstChild.InnerHtml = "<SELECT id=BirthMonth name=BirthMonth> <OPTION selected value=" + strDBValues[i] + ">" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(strDBValues[i])) + "</OPTION> <OPTION value=01>January</OPTION> <OPTION value=02>February</OPTION> <OPTION value=03>March</OPTION> <OPTION value=04>April</OPTION> <OPTION value=05>May</OPTION> <OPTION value=06>June</OPTION> <OPTION value=07>July</OPTION> <OPTION value=08>August</OPTION> <OPTION value=09>September</OPTION> <OPTION value=10>October</OPTION> <OPTION value=11>November</OPTION> <OPTION value=12>December</OPTION></SELECT>";
                    }
                    else if (heAttribute.Id == "Gender")
                    {
                        //wbRegistration.Document.GetElementsByTagName("select")[0].Document.GetElementsByTagName("option")[1].SetAttribute("selected", "selected");
                        HtmlElementCollection hecSelects = wbRegistration.Document.GetElementsByTagName("select");
                        hecSelects[1].Document.GetElementsByTagName("option")[13].SetAttribute("value", strDBValues[i].ToUpper());
                        hecSelects[1].Document.GetElementsByTagName("option")[13].InnerHtml = strDBValues[i].ToString();
                        if (strDBValues[i].ToString().Equals("Female"))
                            hecSelects[1].Document.GetElementsByTagName("option")[13].SetAttribute("aria-posinset", "1");
                        else if (strDBValues[i].ToString().Equals("Male"))
                            hecSelects[1].Document.GetElementsByTagName("option")[13].SetAttribute("aria-posinset", "2");
                        else if (strDBValues[i].ToString().Equals("Other"))
                            hecSelects[1].Document.GetElementsByTagName("option")[13].SetAttribute("aria-posinset", "3");
                    }

                    else if (heAttribute.Id == "CountryCode")
                    {
                        //wbRegistration.Document.GetElementsByTagName("select")[0].Document.GetElementsByTagName("option")[1].SetAttribute("selected", "selected");
                        HtmlElementCollection hecSelects = wbRegistration.Document.GetElementsByTagName("select");
                        //hecSelects[1].Document.GetElementsByTagName("option")[13].SetAttribute("value", strDBValues[i].ToUpper());
                        //hecSelects[1].Document.GetElementsByTagName("option")[13].InnerHtml = strDBValues[i].ToString();
                    }
                    else if (heAttribute.Id == "TermsOfService")
                    {
                        wbRegistration.Document.GetElementById("TermsOfService").SetAttribute("value", "yes");
                        wbRegistration.Document.GetElementById("TermsOfService").SetAttribute("aria-invalid", "true");
                        wbRegistration.Document.GetElementById("TermsOfService").SetAttribute("checked", "True");
                     
                    }
                    else if (heAttribute.Id == "recaptcha_response_field" && !blnCapchaSolve)
                    {
                        HtmlElement heCaptchaImage = wbRegistration.Document.GetElementById("recaptcha_challenge_image");
                        CaptchaInfo.FilePath = wbRegistration.Document.GetElementById("recaptcha_challenge_image").GetAttribute("src");

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
                        wbRegistration.Document.GetElementById("recaptcha_response_field").SetAttribute("value", CaptchaInfo.Code);
                        #endregion

                        blnCapchaSolve = true;
                    }
                    else
                        heAttribute.SetAttribute("value", strDBValues[i]);
                }
            }

            foreach (HtmlElement placehd in PlaceHolderAttribute)
            {
                if (placehd != null)
                {
                    placehd.SetAttribute("InnerHtml", "");
                }
            }

            HtmlElement btnSubmit = wbRegistration.Document.GetElementById("submitbutton");
            if (btnSubmit != null)
            {
                //"theNode.onclick = function(){ alert('You clicked a link with href:' + this.href); };";
                //wbRegistration.InvokeScript
                //wbRegistration.BeginInvoke(
                //Explorer is Object of SHDocVw.WebBrowserClass
                //HtmlDocument htmlDoc = wbRegistration.Document;//(HtmlDocument)this.Explorer.IWebBrowser_Document;

                ////inject Script
                //htmlDoc.InvokeScript("alert('Welcome to KSoft !!')");
                //htmlDoc।parentWindow.execScript("alert('hello world !!')", "javascript");


                //form.InvokeMember("submitbutton");
                btnSubmit.Focus();
                btnSubmit.InvokeMember("click");

                blnSuccess = true;
            }
            else
            {
                blnSuccess = false;
            }
        }
        
        internal static  void ToLive(string[] strAttributeName, string[] strDBValues, WebBrowser wbRegistration)
        {
            throw new NotImplementedException();
        }

        internal static void ToYahoo(string[] strAttributeName, string[] strDBValues, WebBrowser wbRegistration)
        {
            throw new NotImplementedException();
        }


        //private void Form1_Load(System.Object sender, System.EventArgs e)
        //{
        //    WebBrowser1.Navigate("https://secure.diigo.com/sign-up?referInfo=http%3A%2F%2Fwww.diigo.com");
        //    //Website that has the ReCaptcha
        //}

        //private void WebBrowser1_DocumentCompleted(System.Object sender, System.Windows.Forms.WebBrowserDocumentCompletedEventArgs e)
        //{
        //    if (WebBrowser1.DocumentTitle.Contains("Emk"))
        //    {
        //        Button1.Enabled = true;
        //        //Enables DoCaptcha button
        //        Button2.Enabled = true;
        //        //Enables Get New Captcha button
        //        Button3.Enabled = true;
        //        // Enables Play button
        //        getcaptcha();
        //        //Runs the sub getcaptcha()
        //    }
        //    if (WebBrowser1.Url.AbsolutePath.Contains("reload"))
        //    {
        //        getcaptcha();
        //        //Runs the sub getcaptcha()
        //    }
        //}

        //private void Button1_Click(System.Object sender, System.EventArgs e)
        //{
        //    WebBrowser1.Document.GetElementById("recaptcha_response_field").InnerText = TextBox1.Text;
        //    //Sets the Captcha text box
        //    //With info you put in the textbox1
        //}

        //private void Button2_Click(System.Object sender, System.EventArgs e)
        //{
        //    WebBrowser1.Navigate("javascript:Recaptcha.reload ();");
        //    //Runs the Captcha Reload function
        //    PictureBox1.ImageLocation = "A";
        //    //Changes the ImageLocation
        //    Interaction.MsgBox("Captcha changed successfully.");
        //    //Uses the messagebox because without it, it has some problems on timing. Could use system.threading.thread.sleep(7000) instead.
        //    getcaptcha();
        //    //Runs the sub getcaptcha()
        //}

        //public void getcaptcha()
        //{
        //    string str = WebBrowser1.Document.GetElementById("recaptcha_image").InnerHtml;
        //    //Gets the html code for the recaptcha_image element
        //    string img = str.Remove(0, 33).Replace("\" width=300 height=57>", "");
        //    //Deletes all the info around the link because the height and width will never change
        //    PictureBox1.ImageLocation = img;
        //    //Sets the ImageLocation to the URL of the ReCaptcha Image
        //}

        //private void Button3_Click(System.Object sender, System.EventArgs e)
        //{
        //    WebBrowser1.Navigate("javascript:Recaptcha.switch_type('audio');");
        //    //Plays the audio of the ReCaptcha
        //}
    }
}
