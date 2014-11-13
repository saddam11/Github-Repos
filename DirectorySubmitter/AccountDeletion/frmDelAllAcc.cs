using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AccountDeletion
{
    public partial class frmDelAllAcc : Form
    {
        int TotalCheckBoxes = 0;
        int TotalCheckedCheckBoxes = 0;
        CheckBox HeaderCheckBox = null;
        bool IsHeaderCheckBoxClicked = false;
        public frmDelAllAcc()
        {
            InitializeComponent();
        }
        private void frmDelAllAcc_Load(object sender, EventArgs e)
        {
            AddHeaderCheckBox();

            HeaderCheckBox.KeyUp += new KeyEventHandler(HeaderCheckBox_KeyUp);
            HeaderCheckBox.MouseClick += new MouseEventHandler(HeaderCheckBox_MouseClick);
            dgvDelAllAcc.CellValueChanged += new DataGridViewCellEventHandler(dgvDelAllAcc_CellValueChanged);
            dgvDelAllAcc.CurrentCellDirtyStateChanged += new EventHandler(dgvDelAllAcc_CurrentCellDirtyStateChanged);
            dgvDelAllAcc.CellPainting += new DataGridViewCellPaintingEventHandler(dgvDelAllAcc_CellPainting);

            BindGridView();
        }
        #region General Methods
        private void BindGridView()
        {
            dgvDelAllAcc.DataSource = GetDataSource();
            TotalCheckBoxes = dgvDelAllAcc.RowCount;
            TotalCheckedCheckBoxes = 0;
        }
        
        private DataTable GetDataSource()
        {
            DataTable dTable = new DataTable();

            DataRow dRow = null;
            DateTime dTime;
            Random rnd = new Random();
            dTable.Columns.Add("IsChecked", System.Type.GetType("System.Boolean"));
            dTable.Columns.Add("UserName");
            dTable.Columns.Add("Password");
            dTable.Columns.Add("AccountType");

            for (int n = 0; n < 10000; ++n)
            {
                dRow = dTable.NewRow();
                dTime = DateTime.Now;

                dRow["IsChecked"] = "false";
                dRow["UserName"] = rnd.NextDouble();
                dRow["Password"] = dTime.ToString("MM/dd/yyyy");
                dRow["AccountType"] = dTime.ToString("hh:mm:ss tt");

                dTable.Rows.Add(dRow);
                dTable.AcceptChanges();
            }

            return dTable;
        }
        
        private void AddHeaderCheckBox()
        {
            HeaderCheckBox = new CheckBox();

            HeaderCheckBox.Size = new Size(15, 15);

            //Add the CheckBox into the DataGridView
            this.dgvDelAllAcc.Controls.Add(HeaderCheckBox);
        }

        private void HeaderCheckBoxClick(CheckBox HCheckBox)
        {
            IsHeaderCheckBoxClicked = true;

            foreach (DataGridViewRow Row in dgvDelAllAcc.Rows)
                ((DataGridViewCheckBoxCell)Row.Cells["chkBxSelect"]).Value = HCheckBox.Checked;

            dgvDelAllAcc.RefreshEdit();

            TotalCheckedCheckBoxes = HCheckBox.Checked ? TotalCheckBoxes : 0;

            IsHeaderCheckBoxClicked = false;
        }

        private void RowCheckBoxClick(DataGridViewCheckBoxCell RCheckBox)
        {
            if (RCheckBox != null)
            {
                //Modifiy Counter;            
                if ((bool)RCheckBox.Value && TotalCheckedCheckBoxes < TotalCheckBoxes)
                    TotalCheckedCheckBoxes++;
                else if (TotalCheckedCheckBoxes > 0)
                    TotalCheckedCheckBoxes--;

                //Change state of the header CheckBox.
                if (TotalCheckedCheckBoxes < TotalCheckBoxes)
                    HeaderCheckBox.Checked = false;
                else if (TotalCheckedCheckBoxes == TotalCheckBoxes)
                    HeaderCheckBox.Checked = true;
            }
        }

        private void ResetHeaderCheckBoxLocation(int ColumnIndex, int RowIndex)
        {
            //Get the column header cell bounds
            Rectangle oRectangle = this.dgvDelAllAcc.GetCellDisplayRectangle(ColumnIndex, RowIndex, true);

            Point oPoint = new Point();

            oPoint.X = oRectangle.Location.X + (oRectangle.Width - HeaderCheckBox.Width) / 2 + 1;
            oPoint.Y = oRectangle.Location.Y + (oRectangle.Height - HeaderCheckBox.Height) / 2 + 1;

            //Change the location of the CheckBox to make it stay on the header
            HeaderCheckBox.Location = oPoint;
        }

        #endregion
        #region All Events
        private void HeaderCheckBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
                HeaderCheckBoxClick((CheckBox)sender);
        }

        private void HeaderCheckBox_MouseClick(object sender, MouseEventArgs e)
        {
            HeaderCheckBoxClick((CheckBox)sender);
        }

        private void dgvDelAllAcc_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!IsHeaderCheckBoxClicked)
                RowCheckBoxClick((DataGridViewCheckBoxCell)dgvDelAllAcc[e.ColumnIndex, e.RowIndex]);
        }

        private void dgvDelAllAcc_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvDelAllAcc.CurrentCell is DataGridViewCheckBoxCell)
                dgvDelAllAcc.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dgvDelAllAcc_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex == 0)
                ResetHeaderCheckBoxLocation(e.ColumnIndex, e.RowIndex);
        }
        
        #endregion
    }
    class MyClass
    {
        //private void LoadGrid()
        //{
        //    gridRecords.Columns.Clear();
        //    gridRecords.DataSource = GetDataFromDatabase();
        //    if (gridRecords.Columns.Count > 0) // Add Checkbox column only when records are present.
        //        AddCheckBoxColumn();
        //}
        //private void AddCheckBoxColumn()
        //{
        //    DataGridViewCheckBoxColumn doWork = new DataGridViewCheckBoxColumn();
        //    doWork.Name = "Select";
        //    doWork.HeaderText = "Select";
        //    doWork.FalseValue = 0;
        //    doWork.TrueValue = 1;
        //    gridRecords.Columns.Insert(0, doWork);
        //}

        
    }
}
