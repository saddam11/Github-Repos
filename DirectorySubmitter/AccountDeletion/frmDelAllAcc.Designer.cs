namespace AccountDeletion
{
    partial class frmDelAllAcc
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
            this.dgvDelAllAcc = new System.Windows.Forms.DataGridView();
            this.chkBxSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.txtUserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtPassword = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtAccountType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDelAllAcc)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDelAllAcc
            // 
            this.dgvDelAllAcc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDelAllAcc.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chkBxSelect,
            this.txtUserName,
            this.txtPassword,
            this.txtAccountType});
            this.dgvDelAllAcc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDelAllAcc.Location = new System.Drawing.Point(0, 0);
            this.dgvDelAllAcc.Name = "dgvDelAllAcc";
            this.dgvDelAllAcc.Size = new System.Drawing.Size(447, 478);
            this.dgvDelAllAcc.TabIndex = 0;
            // 
            // chkBxSelect
            // 
            this.chkBxSelect.DataPropertyName = "IsChecked";
            this.chkBxSelect.HeaderText = "";
            this.chkBxSelect.Name = "chkBxSelect";
            this.chkBxSelect.Width = 50;
            // 
            // txtUserName
            // 
            this.txtUserName.DataPropertyName = "UserName";
            this.txtUserName.HeaderText = "User Name";
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.ReadOnly = true;
            this.txtUserName.Width = 150;
            // 
            // txtPassword
            // 
            this.txtPassword.DataPropertyName = "Password";
            this.txtPassword.HeaderText = "Password";
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.ReadOnly = true;
            // 
            // txtAccountType
            // 
            this.txtAccountType.DataPropertyName = "AccountType";
            this.txtAccountType.HeaderText = "AccountType";
            this.txtAccountType.Name = "txtAccountType";
            this.txtAccountType.ReadOnly = true;
            // 
            // frmDelAllAcc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 478);
            this.Controls.Add(this.dgvDelAllAcc);
            this.Name = "frmDelAllAcc";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frmDelAllAcc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDelAllAcc)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDelAllAcc;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chkBxSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtUserName;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtPassword;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtAccountType;
    }
}

