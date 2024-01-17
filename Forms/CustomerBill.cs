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
    public partial class CustomerBill : Form
    {
        clsCustomer objcust = new clsCustomer();
        clsItemMaster ObjItem = new clsItemMaster();
        clsProductType ObjType = new clsProductType();
       
        clsBill obj = new clsBill();
        clsBillDetails objdetails = new clsBillDetails();
        public static int UpdatedId = 0;
        public static int idx = 0;
        public static int qty = 0;
        public static double amount = 0;
        public static double OnlyVatAmount = 0;
        public static double VatAmount = 0;
        public static double otherAmount = 0;
        public static double GrandTotal = 0;
        public CustomerBill()
        {
            InitializeComponent();
        }

        private void CustomerBill_Load(object sender, EventArgs e)
        {
            GrandTotal = 0;
            panel3.Enabled = true;
            panel4.Enabled = true;
            cmbType.Text = "select";
            btnSave.Text = "Save";
            UpdatedId = 0;
            clearAll();
        }
        private void GetTotalAmount()
        {
            try
            {
                double a = 0;
                double b = 0;
                 double c =0;
                 if (txtGrandTot.Text != "")
                    a=Convert.ToDouble(txtGrandTot.Text);
                 if (txtOthAmt.Text != "")
                b = Convert.ToDouble(txtOthAmt.Text);
                 if (txtVatAmount.Text != "")
                c = Convert.ToDouble(txtVatAmount.Text);
                txtFinalTot.Text = (a + b + c).ToString();
                //txtRem.Text = txtGrandTot.Text;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }
        private void  clearAll()
        {
            txtAddress.Text = "";
            txtGrandTot.Text = "";
            txtID.Text = "";
            txtMobile.Text = "";
            txtOthAmt.Text = "";
           
            txtQuantity.Text = "";
            txtRate.Text = "";
            txtFinalTot.Text = "";
            txtTotal.Text = "";
            txtVatAmount.Text = "";
            loadAllData();
            LoadAddress();
            GetCustomerName();
            
            GetItemName();
            ClearDetails();
            GetMaxID();
            fillEmptyGrid();
            UpdatedId = 0;
            cmbType.Text = "select";
            comboBox1.Text = "select";
            btnSave.Text = "Save";
            
            
        }
        private void GetMaxID()
        {
            try
            {
                txtID.Text = Convert.ToString(obj.GetCode());
            }
            catch (Exception ex)
            {

                string msg = ex.Message;
            }
        }

        private void loadAllData()
        {
            try
            {
                DataSet ds = new DataSet();
                ds = obj.GetByList();
                grdData.DataSource = ds.Tables[0];

            }
            catch (Exception ex)
            {

                string msg = ex.Message;
            }
        }
        public DataTable GetEmptyTable()
        {
            DataTable dt = new DataTable();
            try
            {
                dt.TableName = "Sale Details";
                DataRow dr = dt.NewRow();
                DataColumn dcol0 = new DataColumn();
                dcol0.ColumnName = "Item";
                dt.Columns.Add(dcol0);
                DataColumn dcol1 = new DataColumn();
                dcol1.ColumnName = "Quantity";
                dt.Columns.Add(dcol1);
                DataColumn dco2 = new DataColumn();
                dco2.ColumnName = "Rate";
                dt.Columns.Add(dco2);
                DataColumn dcol3 = new DataColumn();
                dcol3.ColumnName = "Total";
                dt.Columns.Add(dcol3);
                DataColumn dcol4 = new DataColumn();
                dcol4.ColumnName = "Delete";
                dt.Columns.Add(dcol4);
                DataColumn dcol5 = new DataColumn();
                dcol5.ColumnName = "ID";
                dt.Columns.Add(dcol5);
            }
            catch (Exception ex)
            {
                string sg = ex.Message;
                // clsErrHandler.WriteError(ex, this.Text);
            }
            return dt;


        }


        private void ClearDetails()
        {
            cmbSize.Text = "Select";
            txtQuantity.Text = "0";
            txtRate.Text = "0";
            txtTotal.Text = "0";

        }

        private void GetCustomerName()
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
        private void GetCustName()
        {
            try
            {
                DataSet ds = new DataSet();
                ds = objcust.GetByList();
               cmbCustSearch.DisplayMember = "Name";
               cmbCustSearch.ValueMember = "ID";
               cmbCustSearch.DataSource = ds.Tables[0];

            }
            catch (Exception ex)
            {

                string msg = ex.Message;
            }
        }

        private void GetItemName()
        {
            try
            {
                DataSet ds = new DataSet();
                ds = ObjItem.GetByListItem();
                cmbSize.DisplayMember  = "Item";
                cmbSize.ValueMember = "ID";
                cmbSize.DataSource = ds.Tables[0];

            }
            catch (Exception ex)
            {

                string msg = ex.Message;
            }
        }
        
        private void GetTotal()
        {
            try
            {
                double quantity = 0;
                double rate = 0;
                double total = 0;
                if (txtQuantity.Text != "")
                {
                    quantity = Convert.ToDouble(txtQuantity.Text);
                }
                if (txtRate.Text != "")
                {
                    rate = Convert.ToDouble(txtRate.Text);
                }
                total = quantity * rate;
                txtTotal.Text = total.ToString();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }
        private void addnewrow()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = (DataTable)grdDetails.DataSource;
                DataRow dr = dt.NewRow();
                dr["Item"] = cmbSize.Text;
                dr["Quantity"] = txtQuantity.Text;
                dr["Rate"] = txtRate.Text;
                dr["Total"] = txtTotal.Text;
                dr["Delete"] = "Delete";
                dr["ID"] = lbl_ID.Text;
                dt.Rows.Add(dr);
                dt.AcceptChanges();
                grdDetails.DataSource = dt;
                CountTotal();
                ClearDetails();
            }
            catch (Exception)
            {

                throw;
            }
        }



        //private void GetPreviousAmt()
        //{
        //    try
        //    {
        //        double a = 0;
        //        double c = 0;
        //        double e = 0;
        //        a = (float)Convert.ToDouble(txtFinalTot.Text);
        //        e = (float)Convert.ToDouble(lbl_PrewBalances.Text);
        //        objcust.ID = Convert.ToInt32(cmbCustomer.SelectedValue);
        //        c = a + e;
        //       // objcust.Type = (float)Convert.ToDouble(c);
        //        //objcust.AddPreviousCredit();
        //    }
        //    catch (Exception ex)
        //    {

        //        string msg = ex.Message;
        //    }
        //}

        private void CountTotal()
        {
            Double Total = 0;
            Double amount = 0;
            DataTable dt = (DataTable)grdDetails.DataSource;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Total = Convert.ToDouble(dt.Rows[i]["Total"]);
                amount = Total + amount;

            }
            txtGrandTot.Text = amount.ToString();

            
            
        }

        
        private void LoadAddress()
        {
            try
            {

                DataSet ds = new DataSet();
                objcust.ID = Convert.ToInt32(cmbCustomer.SelectedValue);
                ds = objcust.GetByID();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtAddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                    txtMobile.Text = ds.Tables[0].Rows[0]["Contact"].ToString();

                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public void deleteSaleDetail()
        {
            try
            {
                objdetails.BID = UpdatedId;
                objdetails.Delete();
            }
            catch (Exception ex)
            {
                string sg = ex.Message;
                // clsErrHandler.WriteError(ex, this.Text);
            }
        }

        private void InsertDetails()
        {
            try
            {
                int i = 1;
                foreach (DataGridViewRow r in grdDetails.Rows)
                {
                    if (i < grdDetails.Rows.Count)
                    {
                        // objdetails.ItemName = Convert.ToInt32(r.Cells["ItemName"].Value);
                        objdetails.Item = Convert.ToInt32(r.Cells["ID"].Value);
                        objdetails.Quantity = (float)Convert.ToDouble(r.Cells["Quantity"].Value);
                        objdetails.Rate = (float)Convert.ToDouble(r.Cells["Rate"].Value);
                        objdetails.Total = (float)Convert.ToDouble(r.Cells["Total"].Value);
                        objdetails.AddUpdate();
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {

                string msg = ex.Message;
            }
        }

        private void grdData_Click(object sender, EventArgs e)
        {
            btnSave.Text = "Update";
            try
            {
                int i = Convert.ToInt32(grdData.CurrentRow.Cells["ID"].Value);
                DataSet ds = new DataSet();
                obj.ID = i;
                UpdatedId = i;
                ds = obj.GetByID();
                GetCustomerName();
                cmbCustomer.SelectedValue = ds.Tables[0].Rows[0]["CustomerId"].ToString();
                
                dtpDate.Text = ds.Tables[0].Rows[0]["Date"].ToString();
                cmbType.SelectedText = ds.Tables[0].Rows[0]["Mode"].ToString();
           
                double T = Convert.ToDouble(ds.Tables[0].Rows[0]["Vat"]);
                cmbGstRate.Text = Convert.ToString(Math.Round(T, 2));
                double R = Convert.ToDouble(ds.Tables[0].Rows[0]["VatAmt"]);
                txtVatAmount.Text = Convert.ToString(Math.Round(R, 2));

                double A = Convert.ToDouble(ds.Tables[0].Rows[0]["Total"]);
                txtGrandTot.Text = Convert.ToString(Math.Round(A, 2));

                double B = Convert.ToDouble(ds.Tables[0].Rows[0]["FinalTotal"]);
                txtFinalTot.Text = Convert.ToString(Math.Round(B, 2));
                double C = Convert.ToDouble(ds.Tables[0].Rows[0]["Other"]);
                txtOthAmt.Text = Convert.ToString(Math.Round(C, 2));

                txtID.Text = ds.Tables[0].Rows[0]["Code"].ToString();
                panel5.Visible = false;
                panel1.Visible = true;
                LoadAddress();
               
                getSaleDetailEdit();
               
            
            }
            catch (Exception ex)
            {

                string msg = ex.Message;
            }
        }
       

        private void InsertMaster()
        {
            obj.Code = txtID.Text;
            obj.Mode = cmbType.Text;
            obj.Date = Convert.ToDateTime(dtpDate.Text);
            obj.CustomerId = Convert.ToInt32(cmbCustomer.SelectedValue);
            
            obj.Vat = (float)Convert.ToDouble(cmbGstRate.Text);
            obj.VatAmt = (float)Convert.ToDouble(txtVatAmount.Text);
            obj.Total = (float)Convert.ToDouble(txtGrandTot.Text);
            obj.Other = (float)Convert.ToDouble(txtOthAmt.Text);
            obj.FinalTotal = (float)Convert.ToDouble(txtFinalTot.Text);
            obj.AddUpdate();
        }

        

        private void fillEmptyGrid()
        {
            try
            {

                grdDetails.DataSource = GetEmptyTable();
                grdDetails.Columns[5].DefaultCellStyle.ForeColor = Color.Blue;
                grdDetails.Columns[5].CellTemplate.ToolTipText = "Doubble Click to Delete";
                // grdDetails.Columns[11].Width = 50;
                grdDetails.Columns[1].Width = 100;
                grdDetails.Columns[2].Width = 100;
                grdDetails.Columns[3].Width = 100;
                grdDetails.Columns[4].Width = 100;
                //s grdDetails.Columns[5].Width = 100;

            }
            catch (Exception ex)
            {

                string msg = ex.Message;
            }
        }

        private void LoadCmbCustData()
        {
            try
            {
                DataSet ds = new DataSet();
                ds = objcust.GetByList();
                cmbCustSearch.DisplayMember = "Name";
                cmbCustSearch.ValueMember = "ID";
                cmbCustSearch.DataSource = ds.Tables[0];

            }
            catch (Exception ex)
            {

                string msg = ex.Message;
            }
        }
       
        private void btnnew_Click(object sender, EventArgs e)
        {

        }

        private void label45_Click(object sender, EventArgs e)
        {

        }

        private void btnnew_Click_1(object sender, EventArgs e)
        {
            GrandTotal = 0;
            panel3.Enabled = true;
            panel4.Enabled = true;
            cmbType.Text = "select";
            UpdatedId = 0;
            clearAll();
            btnSave.Text = "Save";

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (UpdatedId == 0)
                {
                    obj.ID = 0;
                    InsertMaster();
                    objdetails.BID = Convert.ToInt32(obj.GetMaxID());
                    InsertDetails();
                    MessageBox.Show("Insert Record Successfully..........");
                }
                else
                {
                    obj.ID = UpdatedId;
                    InsertMaster();
                    objdetails.BID = UpdatedId;
                    deleteSaleDetail();
                    InsertDetails();
                    MessageBox.Show("Update  Record Successfully..........");

                }
                GrandTotal = 0;
                UpdatedId = 0;
                btnSave.Text = "Save";
               
            }
            catch (Exception ex)
            {

                string msg = ex.Message;
            }
            InventoryProject.Forms.CustomerReceipt f = new CustomerReceipt();
            f.Show();
            obj.ID = UpdatedId;

           
            clearAll();
            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel5.Visible = true;
            LoadCmbCustData();
        }

     
     

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (cmbSize.Text == "Select" || txtQuantity.Text == "0" || txtRate.Text == "0")
            {
                MessageBox.Show("Please Enter the  All Details......");
                return;
            }
            else
            {
                GrandTotal = 0;
                if (idx == 0)       // for Update row
                {
                    addnewrow();
                    GetTotalAmount();
                }
                else
                {
                    //grdEditRow();
                    idx = 0;
                }
                
            }
       
        }

        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAddress();
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            try
            {
                GetTotal();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
       
        }

        private void txtRate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                GetTotal();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
       
        }

        private void cmbvat_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                double a = 0;
                double b = 0;

                if (cmbGstRate.Text != "")
                {
                    a = Convert.ToDouble(cmbGstRate.Text);
                }
                if (txtGrandTot.Text != "")
                {
                    b = Convert.ToDouble(txtGrandTot.Text);
                }
                OnlyVatAmount = (a/100)*b;
                txtVatAmount.Text = OnlyVatAmount.ToString();
                VatAmount = (float)Convert.ToDouble(txtVatAmount.Text);
                GetTotalAmount();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }
        private void getSaleDetailEdit()
        {
            try
            {
                DataSet ds = new DataSet();
                objdetails.BID = UpdatedId;
                //  ds = objdetails.GetByID();
                ds = objdetails.GetByIDSearch();

                DataTable dt = new DataTable();
                dt = (DataTable)grdDetails.DataSource;

                if (dt.Rows.Count == 1)
                {
                    if (Convert.ToString(dt.Rows[0].ItemArray.GetValue(0)) == "")
                    {
                        dt.Rows.Clear();
                    }
                }

                dt.Rows.Clear();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["Item"] = ds.Tables[0].Rows[i]["Item"].ToString();
                    dr["ID"] = ds.Tables[0].Rows[i]["ItemId"].ToString();
                    dr["Quantity"] = ds.Tables[0].Rows[i]["Quantity"].ToString();
                    double A = Convert.ToDouble(ds.Tables[0].Rows[i]["Rate"]);
                    dr["Rate"] = Convert.ToString(Math.Round(A, 2));
                    double B = Convert.ToDouble(ds.Tables[0].Rows[i]["Total"]);
                    dr["Total"] = Convert.ToString(Math.Round(B, 2));
                    dr["Delete"] = "Delete";
                    dt.Rows.Add(dr);
                    dt.AcceptChanges();
                    grdDetails.DataSource = dt;

                }
            }
            catch (Exception ex)
            {

                string msg = ex.Message;
            }
        }

        private void txtPaid_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double a = 0;
                if (txtOthAmt.Text != "")
                {
                    a = Convert.ToDouble(txtOthAmt.Text);
                }
                otherAmount = (float)Convert.ToDouble(a);
                GetTotalAmount();

            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
      
        }

        private void grdData_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel5.Visible = false;
            panel1.Visible = true;
       
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void grdData_Click_2(object sender, EventArgs e)
        {
            try
            {
                int i = Convert.ToInt32(grdData.CurrentRow.Cells["ID"].Value);
                DataSet ds = new DataSet();
                obj.ID = i;
                UpdatedId = i;
                ds = obj.GetByID();
                GetCustomerName();
                cmbCustomer.SelectedValue = ds.Tables[0].Rows[0]["CustomerId"].ToString();
                
                LoadAddress();
               
                dtpDate.Text = ds.Tables[0].Rows[0]["Date"].ToString();
                cmbType.Text = "";
                cmbType.SelectedText = ds.Tables[0].Rows[0]["Mode"].ToString();
         
                double A = Convert.ToDouble(ds.Tables[0].Rows[0]["Total"]);
                txtGrandTot.Text = Convert.ToString(Math.Round(A, 2));
                               
                double T = Convert.ToDouble(ds.Tables[0].Rows[0]["Vat"]);
                cmbGstRate.Text = Convert.ToString(Math.Round(T, 2));
                double R = Convert.ToDouble(ds.Tables[0].Rows[0]["VatAmt"]);
                txtVatAmount.Text = Convert.ToString(Math.Round(R, 2));
                double C = Convert.ToDouble(ds.Tables[0].Rows[0]["Other"]);
                txtOthAmt.Text = Convert.ToString(Math.Round(C, 2));

                double B = Convert.ToDouble(ds.Tables[0].Rows[0]["FinalTotal"]);
                txtFinalTot.Text = Convert.ToString(Math.Round(B, 2));
               
                
                txtID.Text = ds.Tables[0].Rows[0]["Code"].ToString();
                panel5.Visible = false;
                panel1.Visible = true;
               

                getSaleDetailEdit();
                CountTotal();
            }
            catch (Exception ex)
            {

                string msg = ex.Message;
            }
        
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
          
            try
            {
                if (cmbCustSearch.Text != "")
                {
                    DataSet ds = new DataSet();

                    obj.CustomerId = Convert.ToInt32(cmbCustSearch.SelectedValue);
                    ds = obj.GetBySearchID();
                    grdData.DataSource = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {

                string msg = ex.Message;
            }
       
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            panel5.Visible = false;
            panel1.Visible = true;
            panel1.Enabled = true;

        }

        private void cmbSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl_ID.Text = Convert.ToString(cmbSize.SelectedValue);
            int i = Convert.ToInt32(cmbSize.SelectedValue);
            ObjType.ProdId = i;
            DataSet ds = new DataSet();
            ds = ObjType.getProdType();
            comboBox1.DisplayMember = "ProdType";
            comboBox1.ValueMember = "ID";
            comboBox1.DataSource = ds.Tables[0];
        }

        private void grdData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void grdDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string cellText = Convert.ToString(grdDetails.CurrentCell.Value);
                int i = Convert.ToInt16(grdDetails.CurrentCell.RowIndex);

                if (cellText == "Delete")
                {
                    DataTable dt = (DataTable)grdDetails.DataSource;
                    dt.Rows[i].Delete();
                    dt.AcceptChanges();
                    grdDetails.DataSource = dt;
                }
                else
                {
                    DataTable dt = (DataTable)grdDetails.DataSource;
                    string ItemName = Convert.ToString(dt.Rows[i]["Item"]);
                    string Quantity = dt.Rows[i]["Quantity"].ToString();
                    string Rate = dt.Rows[i]["Rate"].ToString();
                    string Total = dt.Rows[i]["Total"].ToString();
                    cmbSize.Text = ItemName;
                    txtQuantity.Text = Quantity;
                    txtRate.Text = Rate;
                    txtTotal.Text = Total;
                    idx = 2;
                }
                CountTotal();
                GrandTotal = 0;
                GetTotalAmount();
            }
            catch (Exception ex)
            {

                string msg = ex.Message;
            }
        
        }

        private void txtGrandtotal_TextChanged(object sender, EventArgs e)
        {
            GetTotalAmount();
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtVatAmount_TextChanged(object sender, EventArgs e)
        {
            //double j = 0;
            //double m = 0;
            //j = Convert.ToDouble(txtVatAmount.Text);
            //m = Convert.ToDouble(txtGrandTot.Text);
            //double t = j + m;
            //txtGrandTot.Text = t.ToString();



            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            InventoryProject.Forms.Customer cust = new Customer();
            cust.Show();
            GetCustomerName();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = Convert.ToInt32(comboBox1.SelectedValue);
            ObjType.ID  = i;
            DataSet ds = new DataSet();
            ds = ObjType.getProdTypeRate();
            txtRate.Text = ds.Tables[0].Rows[0]["Details"].ToString(); ;
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            obj.ID  = UpdatedId;

            InventoryProject.ReportViewerForm1 F = new ReportViewerForm1();
            F.showBillReport();
            F.Show();


        }

        private void cmbSize_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            lbl_ID.Text = Convert.ToString(cmbSize.SelectedValue);
            int i = Convert.ToInt32(cmbSize.SelectedValue);
            ObjType.ProdId = i;
            DataSet ds = new DataSet();
            ds = ObjType.getProdType();
            comboBox1.DisplayMember = "ProdType";
            comboBox1.ValueMember = "ID";
            comboBox1.DataSource = ds.Tables[0];
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            DataSet dd = new DataSet();
            ObjType.ID = Convert.ToInt32(comboBox1.SelectedValue);
            dd=ObjType.getProdTypeRate();
            txtRate.Text = dd.Tables[0].Rows[0]["Rate"].ToString();
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            DataSet dd = new DataSet();
            ObjType.ID = Convert.ToInt32(comboBox1.SelectedValue);
            dd = ObjType.getProdTypeRate();
            txtRate.Text = dd.Tables[0].Rows[0]["Rate"].ToString();
        }

        private void comboBox1_TextUpdate(object sender, EventArgs e)
        {

            DataSet dd = new DataSet();
            ObjType.ID = Convert.ToInt32(comboBox1.SelectedValue);
            dd = ObjType.getProdTypeRate();
            txtRate.Text = dd.Tables[0].Rows[0]["Rate"].ToString();
        }

        private void comboBox1_Enter(object sender, EventArgs e)
        {
            DataSet dd = new DataSet();
            ObjType.ID = Convert.ToInt32(comboBox1.SelectedValue);
            dd = ObjType.getProdTypeRate();
            txtRate.Text = dd.Tables[0].Rows[0]["Rate"].ToString();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (cmbSize.Text == "Select" || txtQuantity.Text == "0" || txtRate.Text == "0")
            {
                MessageBox.Show("Please Enter the  All Details......");
                return;
            }
            else
            {
                GrandTotal = 0;
                if (idx == 0)       // for Update row
                {
                  //  chkstk();
                    addnewrow();
                    qty = 0;
                    GetTotalAmount();
                }
                else
                {
                    //grdEditRow();
                    idx = 0;
                }

            }
       
        }
        public void chkstk()
        {

            ObjItem.ID = Convert.ToInt32(cmbSize.SelectedValue);
            ObjItem.Flag = 2;
            DataSet ds1 = new DataSet();
            ds1 = ObjItem.GetStockReport();
             qty  = Convert.ToInt32(ds1.Tables[0].Rows[0]["Purchase Qty"]);
             int qqty = Convert.ToInt32(txtQuantity.Text);
             if (qty < qqty)
             {
                 toolTip1.Show("Item is out of stock", txtQuantity, 2000);
             
             }
        
        }
        private void txtQuantity_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                GetTotal();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
       
        }

        private void txtRate_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                GetTotal();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
       
        }
    }
}
