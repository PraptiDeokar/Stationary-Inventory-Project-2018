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
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using InventoryProject.Report;

namespace InventoryProject
{
    public partial class ReportViewerForm1 : Form
    {
        clsPurchase ObjPur = new clsPurchase();
        clsBill ObjBill = new clsBill();
        static int UpdatedId=0;
        public ReportViewerForm1()
        {
            InitializeComponent();
        }
        public void showpurReport()
        {
            UpdatedId = ObjPur.ID;
            UpdatedId = InventoryProject.Forms.PurchaseMaterial.UpdatedId;
            try
            {
                DataSet ds = new DataSet();
            
                //ds = ObjPur.GetPurReport();
                ReportClass rp; ;

                rp = new purCrystalReport1();

                SqlConnection con = new SqlConnection(ConnectionClass.ConnectionString);
                con.Open();

                SqlDataAdapter myadp = new SqlDataAdapter("[Get_PurReport]", con);
                myadp.SelectCommand.CommandType = CommandType.StoredProcedure;
                myadp.SelectCommand.Parameters.AddWithValue("@ID", Convert.ToInt32(UpdatedId));
                myadp.Fill(ds, "TempReport");
                string relpath = "D:\\AA\\print\\" + "purCrystalReport1.rpt";

                rp.Load(relpath);
                rp.SetDataSource(ds.Tables[0]);
                crystalReportViewer1.ReportSource = rp;
                ExportOptions CrExportOptions;
                DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();

                string rptdate = DateTime.Today.ToString("dd-MM-yyyy");
                String DirPath = "D:\\AA\\print\\" + rptdate;
                //if (!Directory.Exists(DirPath))
                //{
                //    Directory.CreateDirectory(DirPath);

                //}
                CrDiskFileDestinationOptions.DiskFileName = DirPath + "\\PurReport.pdf";
                CrExportOptions = rp.ExportOptions;
                {
                    CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                    CrExportOptions.FormatOptions = CrFormatTypeOptions;
                }
                rp.Export();
                System.Diagnostics.Process.Start(DirPath + "\\PurReport.pdf");
            }
            catch (Exception ex)
            {
                // clsErrHandler.WriteError(ex, this.Text);
            }


        
        
        }

        public void showBillReport()
        {
            UpdatedId = ObjBill.ID;
            UpdatedId = InventoryProject.Forms.CustomerBill.UpdatedId ;
            try
            {
                DataSet ds = new DataSet();

                //ds = ObjPur.GetPurReport();
                ReportClass rp; ;

                rp = new CustBillReport();

                SqlConnection con = new SqlConnection(ConnectionClass.ConnectionString);
                con.Open();

                SqlDataAdapter myadp = new SqlDataAdapter("[Get_BillReport]", con);
                myadp.SelectCommand.CommandType = CommandType.StoredProcedure;
                myadp.SelectCommand.Parameters.AddWithValue("@ID", Convert.ToInt32(UpdatedId));
                myadp.Fill(ds, "TempReport");
                string relpath = "D:\\AA\\print\\" + "CustBillReport.rpt";

                rp.Load(relpath);
                rp.SetDataSource(ds.Tables[0]);
                crystalReportViewer1.ReportSource = rp;
                ExportOptions CrExportOptions;
                DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();

                string rptdate = DateTime.Today.ToString("dd-MM-yyyy");
                String DirPath = "D:\\AA\\print\\" + rptdate;
                //if (!Directory.Exists(DirPath))
                //{
                //    Directory.CreateDirectory(DirPath);

                //}
                CrDiskFileDestinationOptions.DiskFileName = DirPath + "\\CustBillReport.pdf";
                CrExportOptions = rp.ExportOptions;
                {
                    CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                    CrExportOptions.FormatOptions = CrFormatTypeOptions;
                }
                rp.Export();
                System.Diagnostics.Process.Start(DirPath + "\\CustBillReport.pdf");
            }
            catch (Exception ex)
            {
                // clsErrHandler.WriteError(ex, this.Text);
            }




        }


        public void showStockReport()
        {
            //DateTime td = InventoryProject.Forms.Date.Tod;
            //DateTime fd = InventoryProject.Forms.Date.Fromd ;
           
            try
            {
                DataSet ds = new DataSet();

                //ds = ObjPur.GetPurReport();
                ReportClass rp; ;

                rp = new Stock();

                SqlConnection con = new SqlConnection(ConnectionClass.ConnectionString);
                con.Open();

                SqlDataAdapter myadp = new SqlDataAdapter("[Proc_GetStockReport]", con);
                myadp.SelectCommand.CommandType = CommandType.StoredProcedure;
                //myadp.SelectCommand.Parameters.AddWithValue("@FromDate", Convert.ToDateTime(fd));
                //myadp.SelectCommand.Parameters.AddWithValue("@ToDate", Convert.ToDateTime(td));
                myadp.Fill(ds, "TempReport");
                string relpath = "D:\\AA\\print\\" + "Stock.rpt";

                rp.Load(relpath);
                rp.SetDataSource(ds.Tables[0]);
                crystalReportViewer1.ReportSource = rp;
                ExportOptions CrExportOptions;
                DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();

                string rptdate = DateTime.Today.ToString("dd-MM-yyyy");
                String DirPath = "D:\\AA\\print\\" + rptdate;
                //if (!Directory.Exists(DirPath))
                //{
                //    Directory.CreateDirectory(DirPath);

                //}
                CrDiskFileDestinationOptions.DiskFileName = DirPath + "\\StockReport.pdf";
                CrExportOptions = rp.ExportOptions;
                {
                    CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                    CrExportOptions.FormatOptions = CrFormatTypeOptions;
                }
                rp.Export();
                System.Diagnostics.Process.Start(DirPath + "\\StockReport.pdf");
            }
            catch (Exception ex)
            {
                // clsErrHandler.WriteError(ex, this.Text);
            }




        }
        public void showAllPurReport()
        {
            

            try
            {
                DataSet ds = new DataSet();

                //ds = ObjPur.GetPurReport();
                ReportClass rp; ;

                rp = new rReport();

                SqlConnection con = new SqlConnection(ConnectionClass.ConnectionString);
                con.Open();

                SqlDataAdapter myadp = new SqlDataAdapter("[Get_AllPurReport]", con);
                myadp.SelectCommand.CommandType = CommandType.StoredProcedure;
               
                myadp.Fill(ds, "TempReport");
                string relpath = "D:\\AA\\print\\" + "rReport.rpt";

                rp.Load(relpath);
                rp.SetDataSource(ds.Tables[0]);
                crystalReportViewer1.ReportSource = rp;
                ExportOptions CrExportOptions;
                DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();

                string rptdate = DateTime.Today.ToString("dd-MM-yyyy");
                String DirPath = "D:\\AA\\print\\" + rptdate;
                //if (!Directory.Exists(DirPath))
                //{
                //    Directory.CreateDirectory(DirPath);

                //}
                CrDiskFileDestinationOptions.DiskFileName = DirPath + "\\rReport.pdf";
                CrExportOptions = rp.ExportOptions;
                {
                    CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                    CrExportOptions.FormatOptions = CrFormatTypeOptions;
                }
                rp.Export();
                System.Diagnostics.Process.Start(DirPath + "\\rReport.pdf");
            }
            catch (Exception ex)
            {
                // clsErrHandler.WriteError(ex, this.Text);
            }




        }
        
    }



      
}
