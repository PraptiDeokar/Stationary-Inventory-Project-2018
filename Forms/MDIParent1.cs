using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
////using CrystalDecisions.CrystalReports;
////using CrystalDecisions.ReportSource;
////using CrystalDecisions.Shared;
////using CrystalDecisions.CrystalReports.Engine;
using System.Data.SqlClient;



using InventoryProject.Classes;

namespace InventoryProject.Forms
{
    public partial class MDIParent1 : Form
    {
        private int childFormNumber = 0;

        public MDIParent1()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }




        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void productToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InventoryProject.Forms.AddItem m = new InventoryProject.Forms.AddItem();
            m.Show();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            InventoryProject.Forms.prodType m = new InventoryProject.Forms.prodType();
            m.Show();
        }

        private void supplierDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InventoryProject.Forms.Supplier s = new InventoryProject.Forms.Supplier();
            s.Show();
        }

        private void purchaseDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void bankInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            }

        private void paymentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InventoryProject.Forms.SupplierPayment p = new InventoryProject.Forms.SupplierPayment();
            p.Show();
        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {

            InventoryProject.Forms.Customer p = new InventoryProject.Forms.Customer();
            p.Show();
        }

        private void employeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void expenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void paymentToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            
        }

        private void MDIParent1_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel3.Text = "Welcome To Sai Stationery";
            toolStripStatusLabel1.Text = "              ";

            toolStripStatusLabel4.Text = System.DateTime.Now.ToString("dd MMM yyyy");
            toolStripStatusLabel2.Text = System.DateTime.Now.ToString("hh:mm:ss tt");

        }

        private void billInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            }

        private void receiptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InventoryProject.Forms.CustomerReceipt p = new InventoryProject.Forms.CustomerReceipt();
            p.Show();
        }

        private void usedItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void stockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InventoryProject.Forms.Stock p = new InventoryProject.Forms.Stock();
            p.Show();

        }

        private void customerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //ObjPur.ID = UpdatedId;

            InventoryProject.ReportViewerForm1 F = new ReportViewerForm1();
            F.showAllPurReport();
            F.Show();

     
            
            
            
            
            
            //    try
            //    {
            //        ReportDocument rp;
            //        DataSet ds = new DataSet();
            //        rp = new ReportDocument();

            //        SqlConnection con = new SqlConnection(ConnectionClass.ConnectionString);
            //        con.Open();
            //        SqlDataAdapter myadp = new SqlDataAdapter("[Get_OutstandingReport]", con);
            //        myadp.SelectCommand.CommandType = CommandType.StoredProcedure;
            //        myadp.Fill(ds, "TempReport");
            //        string relpath = UtilityClass.clsVariables.ReportPath + "CrptOutstanding.rpt";
            //        rp.Load(relpath);
            //        rp.SetDataSource(ds.Tables[0]);
            //        ExportOptions CrExportOptions;
            //        DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
            //        PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();

            //        string date = DateTime.Today.ToString("dd-MM-yyyy");

            //        string DirPath = UtilityClass.clsVariables.ReportPath;
            //        if (!Directory.Exists(DirPath))
            //        {
            //            Directory.CreateDirectory(DirPath);
            //        }
            //        CrDiskFileDestinationOptions.DiskFileName = DirPath + "CrptOutstanding.doc";
            //        CrExportOptions = rp.ExportOptions;
            //        {
            //            CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            //            CrExportOptions.ExportFormatType = ExportFormatType.WordForWindows;
            //            CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
            //            CrExportOptions.FormatOptions = CrFormatTypeOptions;
            //        }
            //        rp.Export();
            //        System.Diagnostics.Process.Start(DirPath + "CrptOutstanding.doc");
            //    }
            //    catch (Exception ex)
            //    {
            //        //  clsErrHandler.WriteError(ex, this.Text);
            //        string msg = ex.Message;
            //    }

            //}
        }

        private void unitMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InventoryProject.Forms.forgotPassword u = new forgotPassword();
            u.Show();
        }

        private void calculatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
           
        

        
        }

        private void mToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ContactMenu_Click(object sender, EventArgs e)
        {

        }

        private void userManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InventoryProject.Forms.UserManagmentForm f = new UserManagmentForm();
            f.Show();
        }

        private void HomeMenu_Click(object sender, EventArgs e)
        {

        }

        private void billInformationToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            InventoryProject.Forms.CustomerBill p = new InventoryProject.Forms.CustomerBill();
            p.Show();
        
        }

        private void purchaseDetailToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            InventoryProject.Forms.PurchaseMaterial  p = new InventoryProject.Forms.PurchaseMaterial ();
            p.Show();
        
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InventoryProject.Forms.ChangePasswordForm f = new ChangePasswordForm();
            f.Show();
        }

        private void exitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void companyProfileToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            InventoryProject.Forms.ShopProfile f = new ShopProfile();
            f.Show();
        }

        private void stockToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            InventoryProject.ReportViewerForm1 F = new ReportViewerForm1();
            F.showStockReport();
            F.Show();
        }

        private void transationsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void reportsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void changePasswordToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            InventoryProject.Forms.ChangePasswordForm f = new ChangePasswordForm();
            f.Show();
        }
    }
}
