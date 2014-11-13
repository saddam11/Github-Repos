namespace AccountCreation.UI
{
    partial class FrmExpressSubmission
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
            this.btnMultiProcess = new System.Windows.Forms.Button();
            this.txtConsoleResult = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnMultiProcess
            // 
            this.btnMultiProcess.Location = new System.Drawing.Point(12, 12);
            this.btnMultiProcess.Name = "btnMultiProcess";
            this.btnMultiProcess.Size = new System.Drawing.Size(267, 23);
            this.btnMultiProcess.TabIndex = 0;
            this.btnMultiProcess.Text = "Multi Process";
            this.btnMultiProcess.UseVisualStyleBackColor = true;
            this.btnMultiProcess.Click += new System.EventHandler(this.btnMultiProcess_Click);
            // 
            // txtConsoleResult
            // 
            this.txtConsoleResult.Location = new System.Drawing.Point(12, 41);
            this.txtConsoleResult.Multiline = true;
            this.txtConsoleResult.Name = "txtConsoleResult";
            this.txtConsoleResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtConsoleResult.Size = new System.Drawing.Size(267, 383);
            this.txtConsoleResult.TabIndex = 1;
            // 
            // FrmExpressSubmission
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 436);
            this.Controls.Add(this.txtConsoleResult);
            this.Controls.Add(this.btnMultiProcess);
            this.Name = "FrmExpressSubmission";
            this.Text = "FrmExpressSubmission";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnMultiProcess;
        private System.Windows.Forms.TextBox txtConsoleResult;
    }
}