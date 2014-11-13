namespace AccountCreation.TestingProxy
{
    partial class frmShowOutput
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.wbShowOutput = new System.Windows.Forms.WebBrowser();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnLoadHTML = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // wbShowOutput
            // 
            this.wbShowOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbShowOutput.Location = new System.Drawing.Point(0, 0);
            this.wbShowOutput.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbShowOutput.Name = "wbShowOutput";
            this.wbShowOutput.Size = new System.Drawing.Size(642, 385);
            this.wbShowOutput.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnLoadHTML);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.wbShowOutput);
            this.splitContainer1.Size = new System.Drawing.Size(642, 429);
            this.splitContainer1.SplitterDistance = 40;
            this.splitContainer1.TabIndex = 3;
            // 
            // btnLoadHTML
            // 
            this.btnLoadHTML.Location = new System.Drawing.Point(3, 12);
            this.btnLoadHTML.Name = "btnLoadHTML";
            this.btnLoadHTML.Size = new System.Drawing.Size(75, 23);
            this.btnLoadHTML.TabIndex = 0;
            this.btnLoadHTML.Text = "Load HTML";
            this.btnLoadHTML.UseVisualStyleBackColor = true;
            this.btnLoadHTML.Click += new System.EventHandler(this.btnLoadHTML_Click);
            // 
            // frmShowOutput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 429);
            this.Controls.Add(this.splitContainer1);
            this.Name = "frmShowOutput";
            this.Text = "frmShowOutput";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser wbShowOutput;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnLoadHTML;
    }
}