using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using InventoryProject.Classes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.Windows.Forms;
using CrystalDecisions.ReportSource;

namespace InventoryProject.Forms
{
    public partial class Form1 : Form
    {
        ConnectionClass c = new ConnectionClass();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

          
        }

    //    private void crystalReportViewer1_Load(object sender, EventArgs e)
    //    {
    //        public void ShowCandReport()
    //        {
    //            string sql="";
    //     SqlDataAdapter da = new SqlDataAdapter(sql, ConnectionClass.ConnectionString);
    //    DataSet ds = new  PurchaseDataSet();
    //    da.Fill(ds);        //        ' da.Fill(ds,"DataTableName")

    //    //PubReport.rpt ===> Report file ====>BackEnd class PubReport class

    //    CrystalReport rt = new purCrystalReport();
    //    rt.SetDataSource(ds);

    //    CrystalReportViewer1.ReportSource = rt;


    //        }
    }
    }
