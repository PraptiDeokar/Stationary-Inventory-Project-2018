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
    public partial class Bank : Form
    {
        clsBank obj = new clsBank();
        public static int UpdatedId = 0;
        public Bank()
        {
            InitializeComponent();
        }
        public void ClearAll()
        {
            UpdatedId = 0;
        
         
            txtID.Text = "";
            txtName.Text = "";
            GetMaxId();
            GetLoadGrid();
            txtName.Focus();
            btnsave.Text = "Save";
        }
        public void GetLoadGrid()
        {
            DataSet ds = new DataSet();
            ds = obj.GetByList();
            grdDetails.DataSource = ds.Tables[0];
        }
        public void GetMaxId()
        {
            try
            {
                int i = Convert.ToInt32(obj.GetMaxID());
                int b = i + 1;
                txtID.Text = b.ToString();
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void btnnew_Click(object sender, EventArgs e)
        {
            PnlDetails.Enabled = true;
            panel5.Enabled = true;
            ClearAll();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
           
                if (txtName.Text == "")
                {
                    MessageBox.Show("Please Enter The Bank Name...");
                    txtName.Focus();
                }
                else
                {
                    if (UpdatedId == 0)
                    {
                        obj.ID = 0;
                       
                        obj.Name = txtName.Text;
                    
                        // obj.Contact = txtmobno.Text;
                 
                        obj.AddUpdate();
                        MessageBox.Show("Record Inserted....");
                    }
                    else
                    {
                        obj.ID = UpdatedId;
                   
                        obj.Name = txtName.Text;
                   
                        //   obj.Contact = txtmobno.Text;
                
                        obj.AddUpdate();
                        MessageBox.Show("Record UpDated....");
                    }
                    ClearAll();
                }

            
        }

        private void btndel_Click(object sender, EventArgs e)
        {

            DialogResult d = MessageBox.Show("Are you want to delete this Record ?", "Yes/No", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (d == DialogResult.OK)
            {
                obj.ID = UpdatedId;
                obj.Delete();
                MessageBox.Show("Record is Deleted Successfully");
                ClearAll();
            }
        
        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grdDetails_Click(object sender, EventArgs e)
        {

            try
            {
                DataSet ds = new DataSet();
                int i = grdDetails.CurrentRow.Index;
                UpdatedId = Convert.ToInt32(grdDetails.CurrentRow.Cells["ID"].Value);
                obj.ID = Convert.ToInt32(grdDetails.CurrentRow.Cells["ID"].Value);
                obj.Flag = 3;
                ds = obj.GetByID();
                txtID.Text = ds.Tables[0].Rows[0]["ID"].ToString();
                txtName.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                btnsave.Text = "Update";
                //txtAddress.Text = ds.Tables[0].Rows[0]["Branch"].ToString();
                //txtmobno.Text = ds.Tables[0].Rows[0]["Contact"].ToString();
                //txtdiscription.Text = ds.Tables[0].Rows[0]["IFSC Code"].ToString();
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void grdDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Bank_Load(object sender, EventArgs e)
        {
            PnlDetails.Enabled = true;
            panel5.Enabled = true;
            ClearAll();
        }
    }
}