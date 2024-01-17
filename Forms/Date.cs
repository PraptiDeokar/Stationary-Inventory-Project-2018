using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryProject.Forms
{
    public partial class Date : Form
    {
        public static DateTime  Tod;
        public static DateTime  Fromd;
        public Date()
        {
            InitializeComponent();
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            //Tod = Convert.ToDateTime(dtpToDate.Text);
            //Fromd = Convert.ToDateTime(dtpFromDate.Text);
            //InventoryProject.ReportViewerForm1 F = new ReportViewerForm1();
            //F.showStockReport();
            //F.Show();
        }

        private void Date_Load(object sender, EventArgs e)
        {
            PnlDate.Enabled = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
