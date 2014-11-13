using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AccountCreation.TestingProxy
{
    public partial class frmShowOutput : Form
    {
        public string BrowserHTML { get; set; }
        public frmShowOutput(string strHTML)
        {
            InitializeComponent();
            BrowserHTML = strHTML;
            
        }
        private void btnLoadHTML_Click(object sender, EventArgs e)
        {
            wbShowOutput.DocumentText = BrowserHTML;
        }
    }
}
