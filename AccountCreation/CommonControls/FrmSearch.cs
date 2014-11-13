using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace DirectorySubmitter.CommonControls
{
    public partial class FrmSearch : Form
    {
        System.Data.DataTable dtSearch;
        public string[] _strReturn;

        #region Code For Validations
        public bool validations()
        {
            return true;
        }
        #endregion

        #region Constructor
        public FrmSearch(DataTable _dtSearch)
        {
            try
            {
                InitializeComponent();
                dtSearch = _dtSearch;
                dgvProduct.DataSource = dtSearch.DefaultView;
                dgvProduct.Columns[0].Visible = false;
                dgvProduct.Columns[1].Width = 90;
            }
            catch (Exception)
            {
                MessageBox.Show("Url Search Constructor Error !", "KSoft");
            }
        }

        #endregion

        #region Code For Save
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (validations())
                {
                    if (SaveUpdate())
                    {
                        btnSave.Text = "Save";
                        MessageBox.Show("Record Save Successfully !", "KSoft");
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "KSoft");
            }
        }
        #endregion

        #region Code For Cancel
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Code For Search
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!txtSearch.Text.Contains("Search"))
                {
                    if (txtSearch.Text.Trim().Length != 0)
                    {
                        ((System.Data.DataView)dgvProduct.DataSource).RowFilter = "" + dtSearch.Columns[1].ColumnName.ToString() + " like '%" + txtSearch.Text.ToString().ToUpper().ToLower() + "%'";
                        if (dgvProduct.Rows.Count == 0)
                        {
                            MessageBox.Show("No Item Found", "KSoft");
                        }
                    }
                    else
                    {
                        ((System.Data.DataView)dgvProduct.DataSource).RowFilter = "" + dtSearch.Columns[1].ColumnName.ToString() + " like '%" + txtSearch.Text.ToString().ToUpper().ToLower() + "%'";
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "KSoft");
            }
        }
        #endregion

        #region Code For SaveUpdate() Function
        public bool SaveUpdate()
        {
            try
            {
                _strReturn = new string[dtSearch.Columns.Count];
                _strReturn[0] = dgvProduct.CurrentRow.Cells[0].Value.ToString();
                _strReturn[1] = dgvProduct.CurrentRow.Cells[1].Value.ToString();
                this.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "KSoft");
                return false;
            }
            return true;
        }
        #endregion

        #region Code For DatagridView MouseDouble Clickc Event
        private void dgvProduct_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                _strReturn = new string[dtSearch.Columns.Count];
                _strReturn[0] = dgvProduct.CurrentRow.Cells[0].Value.ToString();
                _strReturn[1] = dgvProduct.CurrentRow.Cells[1].Value.ToString();
                this.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "KSoft");
            }
        }
        #endregion

    }
}
