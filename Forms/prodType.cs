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
    public partial class prodType : Form
    {
        static int ID = 0;
        clsProductType b = new clsProductType();
     
        public prodType()
        {
            InitializeComponent();
        }
        public void fill()
        {
            DataSet ds = new DataSet();
            ds = b.GetByListProductType();
            grdProdType.DataSource = ds.Tables[0];


        }
        public void fillcombobox()
        {
            DataSet ds = new DataSet();
            ds = b.getprodName();
            cmbSize.DisplayMember = "Item";
            cmbSize.ValueMember = "ID";
            cmbSize.DataSource = ds.Tables[0];

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                if (ID == 0)
                {
                    b.ID = 0;
                    b.ProdType = txtsubprod.Text;
                    b.Details = txtdetails.Text;
                    b.ProdId = Convert.ToInt32(cmbSize.SelectedValue);
                    b.Flag = 1;
                    b.AddUpdateProductType();


                    MessageBox.Show("Record Saved Successfully......");

                    fill();
                   
                }
                else
                {


                    b.ID = ID;
                    b.ProdType = txtsubprod.Text;
                    b.Details = txtdetails.Text;
                    b.ProdId = Convert.ToInt32(cmbSize.SelectedValue);
                    b.AddUpdateProductType();
                    MessageBox.Show("Record updated Successfully......");

                    fill();
                    ID = 0;
                }
            }

            catch (Exception ex)
            {

                throw;
            }
       
        }
        private void fillID()
        {
            int i = Convert.ToInt32(b.GetMaxID());
            txtcode.Text = i.ToString();
               
        
        }
        private void ClearAll()
        {
            fillcombobox();
            txtdetails.Text = "";
            txtsubprod.Text = "";
            ID = 0;
            fillID();
            fill();
            btnSave.Text = "Save";
        }
        private void MaterialType_Load(object sender, EventArgs e)
        {
            fill();
            fillcombobox();
            ClearAll();
            fillID();
            fillcombobox();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClearAll();
            fillID();
            fillcombobox();
        }

        private void grdProdType_Click(object sender, EventArgs e)
        {
            try
            {
                int i = Convert.ToInt32(grdProdType.CurrentRow.Cells["ID"].Value);
                string type = grdProdType.CurrentRow.Cells["ProdType"].Value.ToString();

                 ID = i;
                b.ID = ID;
                
                b.ProdType= type;
                DataSet ds = new DataSet();
                ds = b.GetByIDProductType();
                cmbSize.Text = ds.Tables[0].Rows[0]["Item"].ToString();
                txtsubprod.Text = ds.Tables[0].Rows[0]["ProdType"].ToString();
                txtdetails.Text = ds.Tables[0].Rows[0]["Details"].ToString();

                txtcode.Text = ds.Tables[0].Rows[0]["ID"].ToString();
                ID = i;
                //Panel4.Enabled = true;
            }
            catch (Exception ex)
            {

                throw;
            }
            btnSave.Text = "Update";
        }

        private void grdProdType_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            b.ID = ID;
            b.DeleteProductType();

            MessageBox.Show("Record deleted successfully...");
            ClearAll();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
