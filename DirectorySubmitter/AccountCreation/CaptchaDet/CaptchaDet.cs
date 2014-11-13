using System;
using System.Collections.Generic;
using System.Net;
using DeathByCaptcha;
using AccountCreation.Helper;
using AccountCreation.UI;


namespace CaptchaDet
{
    public class CaptchaDet
    {
        public static void ApplyCaptcha()
        {
            #region 1.Configure CaptchaInfo
            //Client client = (Client)new HttpClient(dbcUsername, dbcPassword);
            Client client = (Client)new SocketClient( CaptchaInfo.UserName,  CaptchaInfo.UserPassword);
            #endregion
            #region 1.Find Balance Get Captcha Code...
            //Console.WriteLine("Your balance is {0:F2} US cents", client.Balance);
            if (client.Balance > 0)
            {
                //3.Automatically
                SolveMyCaptchaByDBCaptcha(client);
            }
            else
            {
                //3.Call FrmAddManually for Manually Entry
                (new FrmAddCaptchaManually()).ShowDialog();
            }

            Console.WriteLine("Your balance is {0:F2} US cents", client.Balance);

            #endregion
        }

        private static void SolveMyCaptchaByDBCaptcha(Client client)
        {
            for (int i = 2, l = CaptchaInfo.FilePath.Length; i < l; i++)
            {
                Console.WriteLine("Solving CAPTCHA {0}", CaptchaInfo.FilePath);

                // Upload a CAPTCHA and poll for its status.  Put the CAPTCHA
                // image file name, file object, stream, or a vector of bytes,
                // and desired solving timeout (in seconds) here.  If solved,
                // you'll receive a DeathByCaptcha.Captcha object.
                Captcha captcha = client.Decode(CaptchaInfo.FilePath, Client.DefaultTimeout);
                if (null != captcha)
                {
                    Console.WriteLine("CAPTCHA {0:D} solved: {1}",
                                      captcha.Id, captcha.Text);

                    // Report an incorrectly solved CAPTCHA.  Make sure the
                    // CAPTCHA was in fact incorrectly solved, do not just
                    // report them all or at random, or you might be banned
                    // as abuser.
                    if (false /* put your CAPTCHA correctness check here */)
                    {
                        if (client.Report(captcha))
                        {
                            Console.WriteLine("Reported as incorrectly solved");
                        }
                        else
                        {
                            Console.WriteLine("Failed reporting as incorrectly solved");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("CAPTCHA was not solved");
                }
            }
        }
    }


    public class ExampleFull
    {
        static public Client client = null;


        static public void Decode(object o)
        {
            string captchaFileName = (string)o;

            Console.WriteLine("Solving {0}", captchaFileName);

            // Put your CAPTCHA image file name, file object, stream or vector
            // of bytes here:
            Captcha captcha = ExampleFull.client.Upload(captchaFileName);
            if (null != captcha) {
                Console.WriteLine("CAPTCHA {0} uploaded: {1}",
                                  captchaFileName, captcha.Id);

                // Poll for the CAPTCHA status until it's solved.
                // Wait at least a few seconds between poll or you'll get
                // banned as abuser.
                while (captcha.Uploaded && !captcha.Solved) {
                    System.Threading.Thread.Sleep(Client.PollsInterval * 1000);
                    captcha = ExampleFull.client.GetCaptcha(captcha.Id);
                }

                if (captcha.Solved) {
                    Console.WriteLine("CAPTCHA {0} solved: {1}",
                                      captchaFileName, captcha.Text);

                    // Report an incorrectly solved CAPTCHA.  Make sure the
                    // CAPTCHA was in fact incorrectly solved, do not just
                    // report them all or at random, or you might be banned
                    // as abuser.
                    if (false /* put your CAPTCHA correctness check here */) {
                        if (ExampleFull.client.Report(captcha)) {
                            Console.WriteLine("CAPTCHA {0} reported as incorrectly solved",
                                              captchaFileName);
                        } else {
                            Console.WriteLine("Failed reporting as incorrectly solved");
                        }
                    }
                } else {
                    Console.WriteLine("CAPTCHA was not solved");
                }
            } else {
                Console.WriteLine("CAPTCHA was not uploaded");
            }
        }


        static public void MainXX(string[] argv)
        {
            string dbcUsername = argv[0], dbcPassword = argv[1];

            // Put your DBC username & password here.
            //ExampleFull.client = (Client)new HttpClient(dbcUsername, dbcPassword);
            ExampleFull.client = (Client)new SocketClient(dbcUsername, dbcPassword);

            Console.WriteLine("Your balance is {0:F2} US cents",
                              ExampleFull.client.Balance);

            List<System.Threading.Thread> threads =
                new List<System.Threading.Thread>();
            for (int i = 2, l = argv.Length; i < l; i++) {
                System.Threading.Thread t =
                    new System.Threading.Thread(ExampleFull.Decode);
                threads.Add(t);
                t.Start(argv[i]);
            }
            foreach (System.Threading.Thread t in threads) {
                t.Join();
            }

            Console.WriteLine("Your balance is {0:F2} US cents",
                              ExampleFull.client.Balance);
        }
    }


    public class ExampleAsync
    {
        static protected System.Threading.ManualResetEvent _ready =
            new System.Threading.ManualResetEvent(false);


        static protected void Decoded(Captcha captcha)
        {
            if (null != captcha) {
                Console.WriteLine("CAPTCHA {0:D} solved: {1}",
                                  captcha.Id, captcha.Text);
            } else {
                Console.WriteLine("CAPTCHA was not solved");
            }
            ExampleAsync._ready.Set();
        }


        static public void MainX(string[] argv)
        {
            string dbcUsername = argv[0], dbcPassword = argv[1];

            // Put your DBC username & password here.
            //Client client = (Client)new HttpClient(dbcUsername, dbcPassword);
            Client client = (Client)new SocketClient(dbcUsername, dbcPassword);

            Console.WriteLine("Your balance is {0:F2} US cents", client.Balance);

            for (int i = 2, l = argv.Length; i < l; i++) {
                Console.WriteLine("Solving CAPTCHA {0}", argv[i]);

                // Upload a CAPTCHA and poll for its status.  Put the CAPTCHA
                // image file name, file object, stream, or a vector of bytes,
                // and desired solving timeout (in seconds) here:
                client.Decode(new DecodeDelegate(ExampleAsync.Decoded),
                              argv[i],
                              Client.DefaultTimeout);
                ExampleAsync._ready.WaitOne();
            }

            Console.WriteLine("Your balance is {0:F2} US cents", client.Balance);
        }
    }
}
