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
    public partial class Customer : Form
    {
        clsCustomer obj = new clsCustomer();
        public static int UpdatedId = 0;
        
        public Customer()
        {
            InitializeComponent();
        }

        private void clearAll()
        {
            txtAddress.Text = "";
            txtdetails.Text = "";
            txtEmailID.Text = "";
            txtID.Text = "";
            txtmobno.Text = "";
                txtName.Text= "";
            txtPreviousCre.Text= "";
            GetLoadGrid();
            FillmaxId();
            UpdatedId = 0;
            btnSave.Text = "Save";
        }
        private void FillmaxId()
        {
            try
            {
                int i = Convert.ToInt32(obj.GetMaxID());
                int b = i + 1;
                txtID.Text ="c" +b.ToString();
            }
            catch (Exception)
            {

                throw;
            }
        
        
        }
        public void GetLoadGrid()
        {
            DataSet ds = new DataSet();
            ds = obj.GetByList();
            grdDetails.DataSource = ds.Tables[0];
        }
        
        private void btnNew_Click(object sender, EventArgs e)
        {
            PnlDetails.Enabled = true;
            panel1.Enabled = true;
            panel5.Enabled = true;
            clearAll();
            //FillmaxId();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           
            try
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("Please Enter Ther Customer Name...");
                    txtName.Focus();
                }
                else
                {
                    if (UpdatedId == 0)
                    {
                        obj.ID = 0;
                        obj.Code = txtID.Text;
                        obj.Name = txtName.Text;
                        obj.Address = txtAddress.Text;
                        obj.Contact = txtmobno.Text;
                        obj.Email = txtEmailID.Text;
                        obj.Type = txtPreviousCre.Text;
                        obj.Details = txtdetails.Text;
                        obj.AddUpdate();
                        MessageBox.Show("Record Inserted....");
                    }
                    else
                    {
                        obj.ID = UpdatedId;
                        obj.Code = txtID.Text;
                        obj.Name = txtName.Text;
                        obj.Address = txtAddress.Text;
                        obj.Contact = txtmobno.Text;
                        obj.Email = txtEmailID.Text;
                        obj.Type =txtPreviousCre.Text;
                        obj.Details = txtdetails.Text;
                        obj.AddUpdate();
                        MessageBox.Show("Record UpDated....");

                    }
                   
                }
            }
            catch (Exception)
            {

                throw;
            }
       
                       
            
            clearAll();
         
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Are you want to delete this Record ?", "Yes/No", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (d == DialogResult.OK)
            {
                obj.ID = UpdatedId;
                obj.Delete();
                MessageBox.Show("Record is Deleted Successfully");
               
            }
      
            clearAll();
        }

        private void Customer_Load(object sender, EventArgs e)
        {
            clearAll();
            panel1.Enabled = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
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
               // obj.Flag = 3;
                ds = obj.GetByID();
                txtID.Text = ds.Tables[0].Rows[0]["Code"].ToString();
                txtName.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                txtAddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                txtmobno.Text = ds.Tables[0].Rows[0]["Contact"].ToString();
                txtEmailID.Text = ds.Tables[0].Rows[0]["Email"].ToString();
                txtdetails.Text = ds.Tables[0].Rows[0]["Details"].ToString();
                txtPreviousCre.Text = ds.Tables[0].Rows[0]["Type"].ToString();
                btnSave.Text = "Update";
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void PnlDetails_Paint(object sender, PaintEventArgs e)
        {

        }

        private void grdDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char ch = e.KeyChar;
            if (Char.IsDigit(ch)== true)
            {
                e.Handled = true;
                toolTip1.Show("Please enter only Characters", txtName, 2000);
            
            }
        }

        private void txtmobno_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char ch = e.KeyChar;
            if (Char.IsLetter(ch) == true)
            {
                e.Handled = true;
                toolTip1.Show("Please enter only numbers", txtmobno, 2000);

            }
        }
    }
}
