using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InventoryProject.Classes;
namespace InventoryProject.Forms
{
    public partial class AddItem : Form
    {
        clsItemMaster ItemObj = new clsItemMaster();
        public static int UpdatedID = 0;
        
        public AddItem()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtItem.Text == "")
                {
                    MessageBox.Show("Please Enter Ther Item Name...");
                }
                else
                {
                    if (UpdatedID == 0)
                    {
                        ItemObj.ID = 0;
                       ItemObj.Item= txtItem.Text;
                        ItemObj.Details = txtDescription.Text;
                       
                        ItemObj.Flag = 1;
                        ItemObj.AddUpdateItem();
                        MessageBox.Show("Record Saved Successfully......");
                       
                    }
                    else
                    {
                        btnSave.Text = "Update";
                        ItemObj.ID  = UpdatedID;
                        ItemObj.Item= txtItem.Text;
                        ItemObj.Details = txtDescription.Text;
                       
                        ItemObj.Flag = 3;
                        ItemObj.AddUpdateItem();

                        MessageBox.Show("Record Updated Successfully.......");
                        btnSave.Text = "Save";
                    }
                    LoadGrid();
                    ClearAll();
                   
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        
        

        private void btnNew_Click(object sender, EventArgs e)
        {
           btnSave.Text = "Save";
            fillID();
          LoadGrid();
            btnSave.Enabled = true;
            btnDelete.Enabled = true;
            txtItem.Focus();
            PnlDetails.Enabled = true;
            grdDetails.Enabled = true;
            panel3.Enabled = true;
            txtItem.Text  = "";
            txtDescription.Text = "";
            ClearAll();
            }
        public void fillID()
        {
            int i = Convert.ToInt32(ItemObj.GetMaxID());
            txtID.Text = i.ToString();
               
        }
        public void ClearAll()
        {
            try
            {
                fillID ();
                LoadGrid();
                txtItem.Text = "";
                txtDescription.Text = "";
                UpdatedID = 0;

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void LoadGrid()
        {
            try
            {
                DataSet ds = new DataSet();
                ds = ItemObj.GetByListItem();
                grdDetails.DataSource = ds.Tables[0];
                grdDetails.Columns[0].Width = 100;
                grdDetails.Columns[1].Width = 170;
                grdDetails.Columns[2].Width = 200;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ItemObj.ID = UpdatedID;
            ItemObj.DeleteItem();

            MessageBox.Show("Record deleted successfully...");
            ClearAll();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MaterialEntry_Load(object sender, EventArgs e)
        {
            btnSave.Text = "Save";
            fillID();
            LoadGrid();
            btnSave.Enabled = true;
            btnDelete.Enabled = true;
            txtItem.Focus();
            PnlDetails.Enabled = true;
            grdDetails.Enabled = true;
            panel3.Enabled = true;
            txtItem.Text = "";
            txtDescription.Text = "";
            ClearAll();
        }

        private void grdDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void grdDetails_Click(object sender, EventArgs e)
        {
            try
            {
                int i = Convert.ToInt32(grdDetails.CurrentRow.Cells["ID"].Value);
                string  item = grdDetails.CurrentRow.Cells["Item"].Value.ToString();
               
                UpdatedID = i;
                ItemObj.ID = UpdatedID;
                ItemObj.Item = item;
                DataSet ds = new DataSet();
                ds = ItemObj.GetByIDItem();
                txtItem.Text = ds.Tables[0].Rows[0]["Item"].ToString();
                txtDescription.Text = ds.Tables[0].Rows[0]["Details"].ToString();

                txtID.Text = ds.Tables[0].Rows[0]["ID"].ToString();
                PnlDetails.Enabled = true;
                btnSave.Text = "Update";
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void grdDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
