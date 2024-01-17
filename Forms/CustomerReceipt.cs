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
    public partial class CustomerReceipt : Form
    {

        clsCustomer objcust = new clsCustomer();
        clsReciept obj = new clsReciept();
        clsBank objbank = new clsBank();
        clsBill objBill = new clsBill();
        public static int UpdatdID = 0;

        public CustomerReceipt()
        {
            InitializeComponent();
        }

        private void rdCheque_CheckedChanged(object sender, EventArgs e)
        {
            panel5.Visible =true ;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
           
        }

        private void rdCheque_Click(object sender, EventArgs e)
        {
            panel5.Visible = true;
            panel5.Enabled = true;

        }
        public void loadTotal()
        {


            try
            {
                objBill.ID = Convert.ToInt32(objBill.GetMaxID());

                DataSet ds = new DataSet();

                ds = objBill.GetByID();
                txtTotal.Text = ds.Tables[0].Rows[0]["FinalTotal"].ToString();
                cmbCustomer.Text = ds.Tables[0].Rows[0]["Name"].ToString();

            }
            catch (Exception ex)
            {
                string sg = ex.Message;

                throw;
            }
        }

        private void CustomerReceipt_Load(object sender, EventArgs e)
        {
            this.Top = 65;
          
            pnlDetails.Enabled = true;
            ClearALL();
            loadTotal();
            GetBankName();
        }

        private void ClearALL()
        {
            GetMaxID();
            GetCustmoer();
            GetCustomerSearch();
            GetBankName();
            cmbCustomer.Text = "Select";
            cmbCustomerSearch.Text = "Select";
            txtTotal.Text = "0";
            
            txtdescription.Text = "";
            rdCash.Checked = true;
            panel5.Visible = false;
            cmbCustomerSearch.Text = "";
            LoadAllData();
            UpdatdID = 0;
            btnSave.Text = "Save";
        }

        private void GetBankName()
        {
            try
            {
                DataSet ds = new DataSet();
                ds = objbank.GetByList();
                cmbBank.DisplayMember = "Name";
                cmbBank.ValueMember = "ID";
                cmbBank.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {

                string msg = ex.Message;
            }
        }

        private void LoadAllData()
        {
            try
            {
                DataSet ds = new DataSet();
                ds = obj.GetByList();
                grdDetails.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {

                string msg = ex.Message;
            }
        }

        private void GetMaxID()
        {
            txtID.Text = Convert.ToString(obj.GetCode());
        }

        private void GetCustomerSearch()
        {
            DataSet ds1 = new DataSet();
            ds1 = objcust.GetByList();
            cmbCustomerSearch.DisplayMember = "Name";
            cmbCustomerSearch.ValueMember = "ID";
            cmbCustomerSearch.DataSource = ds1.Tables[0];

        }

        private void GetCustmoer()
        {
            try
            {
                DataSet ds = new DataSet();
                ds = objcust.GetByList();
                cmbCustomer.DisplayMember = "Name";
                cmbCustomer.ValueMember = "ID";
                cmbCustomer.DataSource = ds.Tables[0];

            }
            catch (Exception ex)
            {

                string msg = ex.Message;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (UpdatdID == 0)
                {
                    obj.ID = 0;
                    InsertData();
                    MessageBox.Show("Insert Record Successfully...........");

                }
                else
                {
                    obj.ID = UpdatdID;
                    InsertData();
                    MessageBox.Show("Updated Record Successfully...........");
                }
                //GetPreviousAmt();
                ClearALL();

            }
            catch (Exception ex)
            {

                string msg = ex.Message;
            }
        }
        //private void GetPreviousAmt()
        //{
        //    try
        //    {
        //        double a = 0;
        //        double c = 0;
        //        a = (float)Convert.ToDouble(txtRemainig.Text);
        //        objcust.ID = Convert.ToInt32(cmbCustomer.SelectedValue);
        //        c = a;
        //        objcust.Type = (float)Convert.ToDouble(c);
        //       // objcust.AddPreviousCredit();
        //    }
        //    catch (Exception ex)
        //    {

        //        string msg = ex.Message;
        //    }
        //}

        private void InsertData()
        {
            obj.Code = Convert.ToString(obj.GetCode());
            obj.Date = Convert.ToDateTime(dtpDate.Text);
            obj.CustomerId = Convert.ToInt32(cmbCustomer.SelectedValue);
            obj.AmtToPay = (float)Convert.ToDouble(txtTotal.Text);
           
            obj.Details = txtdescription.Text;
            if (rdCash.Checked == true)
            {
                obj.Mode = "Cash";
                obj.ChkDate = Convert.ToDateTime(dtpchequedate.Text);
                
                obj.Bankname = 0;
                obj.ChequeNum = "";
                obj.ChkDate = Convert.ToDateTime(dtpDate.Text);

            }
            if (rdCheque.Checked == true)
            {
                obj.Mode = "Chaque";
                obj.Bankname = Convert.ToInt32(cmbBank.SelectedValue);
                obj.ChequeNum = txtchequeno.Text;
                obj.ChkDate = Convert.ToDateTime(dtpchequedate.Text);
            }
            obj.AddUpdate();
        }

        //private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        DataSet ds = new DataSet();
        //        objcust.ID = Convert.ToInt32(cmbCustomer.SelectedValue);
        //        ds = objcust.GetByID();
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            txtTotal.Text = ds.Tables[0].Rows[0]["PreviousCredit"].ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        string msg = ex.Message;
        //    }
        //}

        //private void txtPaid_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        double a = 0;
        //        double b = 0;
        //        double c = 0;
        //        if (txtTotal.Text != "")
        //        {
        //            a = (float)Convert.ToDouble(txtTotal.Text);
        //        }
        //        if (txtPaid.Text != "")
        //        {
        //            b = (float)Convert.ToDouble(txtPaid.Text);
        //        }
        //        c = a - b;
        //        txtRemainig.Text = c.ToString();
        //    }
        //    catch (Exception ex)
        //    {

        //        string msg = ex.Message;

        //    }


        //}

        private void grdDetails_Click(object sender, EventArgs e)
        {
            try
            {

                DataSet ds = new DataSet();
                int i = grdDetails.CurrentRow.Index;
                UpdatdID = Convert.ToInt32(grdDetails.CurrentRow.Cells["ID"].Value);
                obj.ID = Convert.ToInt32(grdDetails.CurrentRow.Cells["ID"].Value);
                obj.Flag = 3;
                ds = obj.GetByID();
                txtID.Text = ds.Tables[0].Rows[0]["Code"].ToString();
                GetCustmoer();

                cmbCustomer.SelectedValue = ds.Tables[0].Rows[0]["CustomerId"].ToString();
                txtTotal.Text = ds.Tables[0].Rows[0]["Total"].ToString();
                
                txtdescription.Text = ds.Tables[0].Rows[0]["Details"].ToString();
                if (ds.Tables[0].Rows[0]["Mode"].ToString() == "Cash")
                {
                    rdCash.Checked = true;
                }
                if (ds.Tables[0].Rows[0]["Mode"].ToString() == "Chaque")
                {
                    rdCheque.Checked = true;
                    panel5.Visible = true;
                    cmbBank.Text = ds.Tables[0].Rows[0]["BName"].ToString();
                    dtpchequedate.Text = ds.Tables[0].Rows[0]["ChkDate"].ToString(); 
                  txtchequeno.Text= ds.Tables[0].Rows[0]["ChequeNum"].ToString(); 
             
                }
            }
            catch (Exception ex)
            {

                string msg = ex.Message;
            }
            btnSave.Text = "Update";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                obj.CustomerId = Convert.ToInt32(cmbCustomerSearch.SelectedValue);
                ds = obj.GetBy_custIDSearch();
                grdDetails.DataSource = ds.Tables[0];

            }
            catch (Exception ex)
            {

                string msg = ex.Message;
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Are you want to delete this Record ?", "Yes/No", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (d == DialogResult.OK)
            {
                obj.ID = UpdatdID;
                obj.Delete();
                MessageBox.Show("Record is Deleted Successfully");
                ClearALL();
            }
        
        }

        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void rdCash_CheckedChanged(object sender, EventArgs e)
        {
            panel5.Visible = false;
        }

        private void grdDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InventoryProject.Forms.Bank p = new InventoryProject.Forms.Bank();
            p.Show();
           
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GetBankName();
            btnSave.Text = "Save";
        }
    }
}
