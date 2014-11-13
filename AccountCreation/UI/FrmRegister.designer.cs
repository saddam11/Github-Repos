namespace AccountCreation.UI
{
    partial class FrmRegister
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
            this.txtSearh = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.wbRegistration = new System.Windows.Forms.WebBrowser();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtSearh
            // 
            this.txtSearh.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearh.Location = new System.Drawing.Point(173, 12);
            this.txtSearh.Name = "txtSearh";
            this.txtSearh.Size = new System.Drawing.Size(229, 22);
            this.txtSearh.TabIndex = 4;
            this.txtSearh.Text = "Press F2 For Url Search";
            this.txtSearh.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearh_KeyDown);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(408, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
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
            this.splitContainer1.Panel1.Controls.Add(this.btnStart);
            this.splitContainer1.Panel1.Controls.Add(this.txtSearh);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.wbRegistration);
            this.splitContainer1.Size = new System.Drawing.Size(861, 573);
            this.splitContainer1.SplitterDistance = 67;
            this.splitContainer1.TabIndex = 5;
            // 
            // wbRegistration
            // 
            this.wbRegistration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbRegistration.Location = new System.Drawing.Point(0, 0);
            this.wbRegistration.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbRegistration.Name = "wbRegistration";
            this.wbRegistration.Size = new System.Drawing.Size(861, 502);
            this.wbRegistration.TabIndex = 1;
            this.wbRegistration.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.wbRegistration_DocumentCompleted);
            this.wbRegistration.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.wbRegistration_Navigated);
            this.wbRegistration.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.wbRegistration_Navigating);
            // 
            // FrmRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(861, 573);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FrmRegister";
            this.Text = "FrmRegister";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmRegister_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtSearh;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.WebBrowser wbRegistration;
    }
}