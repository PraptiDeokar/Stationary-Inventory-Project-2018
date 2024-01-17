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
    public partial class Expense : Form
    {
        clsExpences obj = new clsExpences();
        clsBank objBank = new clsBank();
        public static int UpdateId;
    
        public Expense()
        {
            InitializeComponent();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
        private void fillId()
        {

            txtID.Text = obj.GetExpenceCode();
            
        
        }
        private void LoadGrid()
        {
            try
            {
                obj.Flag = 3;
                DataSet ds = obj.GetByList_Expences();
                grdDetails.DataSource = ds.Tables[0];
                if (ds.Tables[0].Rows.Count > 0)
                {
                    double a = 0;
                    double b = 0;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        a = Convert.ToDouble(ds.Tables[0].Rows[i]["PaidAmount"].ToString());
                        b = b + a;
                    }
                    //       TXTdATEwISEtOTAL.Text = b.ToString();
                }
            }
            catch (Exception ex)
            {
                string sg = ex.Message;
                //clsErrHandler.WriteError(ex, this.Text);
            }
  
        
        }
        public void LoadBank()
        {
            try
            {
                DataSet ds = objBank.GetByList();//this is for supplier name
                ddlBank.DisplayMember = "Name";
                ddlBank.ValueMember = "ID";
                ddlBank.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                string sg = ex.Message;
                //clsErrHandler.WriteError(ex, this.Text);
            }

        }
       
        private void clearAll()
        {
            txtAmount.Text = "";
           txtchequeNo.Text  = "";
           txtexpence.Text  = "";
            txtFrom.Text = "";
           txtID.Text  = "";
            txtReson.Text = "";
            txtTo.Text = "";
            fillId();
            LoadGrid();
            LoadBank();
            UpdateId = 0;
        }
        private void Expense_Load(object sender, EventArgs e)
        {
            clearAll();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PnlBank.Enabled = false;
            PnlDetails.Enabled = true;
            panel6.Enabled = true;
            clearAll();
        }
        public void EditExpences()
        {
            try
            {
                DataSet ds = new DataSet();
                ds = obj.GetByID_Expences();
                txtID.Text = ds.Tables[0].Rows[0]["ID"].ToString();
                cmbSite.Text = ds.Tables[0].Rows[0]["Expence"].ToString();
                txtAmount.Text = ds.Tables[0].Rows[0]["PaidAmount"].ToString();
                txtFrom.Text = ds.Tables[0].Rows[0]["FromExpence"].ToString();
                txtTo.Text = ds.Tables[0].Rows[0]["ToExpence"].ToString();
                txtReson.Text = ds.Tables[0].Rows[0]["Reson"].ToString();
                dtpDate.Text = ds.Tables[0].Rows[0]["Date"].ToString();
                //    txtPaidAmount.Text = ds.Tables[0].Rows[0]["PaidAmount"].ToString();
                if (ds.Tables[0].Rows[0]["Mode"].ToString() == "Cheque")
                {
                    //     panel5.Visible = true;
                    rdCheque.Checked = true;
                    dtpChequeDate.Text = ds.Tables[0].Rows[0]["ChequeDate"].ToString();
                    txtchequeNo.Text = ds.Tables[0].Rows[0]["ChequeNo"].ToString();
                    LoadBank();
                    ddlBank.SelectedValue = ds.Tables[0].Rows[0]["BankID"].ToString();
                }
                else
                {
                    rdCash.Checked = true;
                    //    panel5.Visible = false;
                }

            }
            catch (Exception ex)
            {
                string sg = ex.Message;
                //clsErrHandler.WriteError(ex, this.Text);
            }

        }

        private void rdCheque_CheckedChanged(object sender, EventArgs e)
        {
            if (rdCheque.Checked == true)
            {
                PnlBank.Enabled = true;
                PnlBank.Visible = true;
            }
        }
        public void InsertExpences()
        {
            try
            {
                //  obj.Code = txtID.Text;
                obj.Date = Convert.ToDateTime(dtpDate.Text);
                obj.Expence = cmbSite.Text;
                obj.FromExpence = txtFrom.Text;
                obj.ToExpence = txtTo.Text;
                obj.PaidAmount = (float)Convert.ToDouble(txtAmount.Text);
                obj.Reason  = txtReson.Text;
                if (rdCheque.Checked == true)
                {
                    obj.Mode = "Cheque";
                    obj.ChequeDate = Convert.ToDateTime(dtpChequeDate.Text);
                    obj.BankID = Convert.ToInt32(ddlBank.SelectedValue);
                    obj.ChequeNo = txtchequeNo.Text;
                }
                else
                {
                    obj.Mode = "Cash";
                    obj.ChequeDate = Convert.ToDateTime("01/01/1900");
                }
                obj.AddUpdateExpences();
            }
            catch (Exception ex)
            {
                string sg = ex.Message;
                //clsErrHandler.WriteError(ex, this.Text);
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtAmount.Text == "0" || txtTo.Text == "")
                {
                    MessageBox.Show("Please Make All Entry......");
                    return;
                }
                else
                {
                    if (UpdateId == 0)
                    {
                        obj.ID = 0;
                    }
                    else
                    {
                        obj.ID = UpdateId;
                    }
                    InsertExpences();
                  
                    MessageBox.Show("Record Saved Succesfully !!");
               

                }
                clearAll();

            }
            catch (Exception ex)
            {

                string msg = ex.Message;

            }
            // panel4.Enabled = false;
       
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            
              DialogResult d = MessageBox.Show("Are you want to delete this Record ?", "Yes/No", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
              if (d == DialogResult.OK)
              {
                  obj.ID = UpdateId;
                  obj.DeleteExpences();
              }
            clearAll();
        }

        private void grdDetails_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(grdDetails.CurrentRow.Cells["ID"].Value);
            obj.ID = ID;
            UpdateId = ID;
            EditExpences();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            obj.Date = Convert.ToDateTime(dtpExpenceDate.Text);
            ds = obj.GetSearched();
            if (ds.Tables[0].Rows.Count > 0)
            {
                grdDetails.DataSource = ds.Tables[0];
            }
            else
            {
                MessageBox.Show("There is no record on selected date !!");
            }
       
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdCash_CheckedChanged(object sender, EventArgs e)
        {
            if (rdCash.Checked == true)
            { 
            PnlBank.Enabled=false ;
            
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grdDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
