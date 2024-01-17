using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Data.SqlClient;
//using Microsoft.SqlServer.Management.Smo;
//using Microsoft.SqlServer.Management.Common;
using Microsoft.Win32;
using System.IO;
using System.Windows.Forms;
using InventoryProject.Classes;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.CrystalReports;
using CrystalDecisions;
using CrystalDecisions.Web;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using System.Management;

/// <summary>
/// Summary description for clsUtility
/// </summary>
public class UtilityClass
{
    public UtilityClass()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static string Username;
    // variable declaration
    public class clsVariables
    {
        //STRING VARIABLES
        public static string sMSGBOX = "Inventory Management System".ToUpper();
        public static string sINSTITUTION;
        public static string sCONTACTNAME;
        public static string sADDRESSS;
        public static string sPHONENUMBER;
        public static string sFAXNUMBER;
        public static string sEMAILADDRESS;
        public static string sWEBSITE;
        public static string sLibrarianID;
        public static string sLibrarianName;
        public static string sUserID;
        public static string sTimeLogin;
        public static int UserID;
        public static int ID;
        public static bool isAlert;
        public static string ReportPath;
        public static string ImgPath;
        public static string ErrPath;
        public static string PhotoPath;
        public static string GalleryPath;
        public static string logoPath;
        public static string logoPath1;
        public static int GlobalBranchID;
        public static int GlobalRoleID;
        public static int GlobalStaffID;
        public static string GlobalLoginName;
        public static int StaffID;
        public static string Name;
        public static int branchID;
        public static string BranchName;
        public static string UserName;
        public static string Password;
        public static int RoleID;
        //ublic static int UserID;
        public static int DeptID;
        public static string DeptName;
        //public static string ReportPath;
    }

    public class clsReportPath
    {

    }
    // this class store all Records ID's 
    public class clsGlobalValues
    {
        public static bool isAction = false;
    }

    public class clsMessageBox
    {
        //STRING VARIABLES
        public static string msgTitle = "";
        public static string msgText = "";
        public static bool YesNo;
        public static int flag = 0;
        public static string button1 = "";
        public static string button2 = "";

    }
    //public void showMSG(string argTitle, string argText, string argButton1, string argButton2, int argFlag)
    //{
    //    clsMessageBox.msgTitle = System.Configuration.ConfigurationSettings.AppSettings["MessageName"].ToString();
    //    clsMessageBox.msgText = argText;
    //    clsMessageBox.button2 = argButton2;
    //    clsMessageBox.button1 = argButton1;
    //    clsMessageBox.flag = argFlag;

    //}
    //set institution setup information
    public class clsInstitutionSetup
    {
        public static void setINSTITUTION()
        {
            //long totalRow = 0;
            ////Set the Data Adapter
            //OleDbDataAdapter da = new OleDbDataAdapter("SELECT tblLibrarySetup.InstitutionName, tblLibrarySetup.ContactName, " +
            //    "[tblLibrarySetup.StreetAddr] & ', ' & [tblZipCodeList.CityTown] & ',  ' & [tblZipCodeList.Province] & ', ' & [tblZipCodeList.ZipCode] AS Address," +
            //    "tblLibrarySetup.PhoneNumber, tblLibrarySetup.FaxNumber, tblLibrarySetup.EmailAddr, tblLibrarySetup.Website FROM tblZipCodeList RIGHT JOIN tblLibrarySetup " +
            //    "ON tblZipCodeList.ZipCode = tblLibrarySetup.ZipCode", clsConnections.CN);

            //DataSet ds = new DataSet();
            //da.Fill(ds, "tblLibrarySetup");

            //totalRow = ds.Tables["tblLibrarySetup"].Rows.Count - 1;

            //clsVariables.sINSTITUTION = ds.Tables["tblLibrarySetup"].Rows[0].ItemArray.GetValue(0).ToString();
            //clsVariables.sCONTACTNAME = ds.Tables["tblLibrarySetup"].Rows[0].ItemArray.GetValue(1).ToString();
            //clsVariables.sADDRESSS = ds.Tables["tblLibrarySetup"].Rows[0].ItemArray.GetValue(2).ToString();
            //clsVariables.sPHONENUMBER = ds.Tables["tblLibrarySetup"].Rows[0].ItemArray.GetValue(3).ToString();
            //clsVariables.sFAXNUMBER = ds.Tables["tblLibrarySetup"].Rows[0].ItemArray.GetValue(4).ToString();
            //clsVariables.sEMAILADDRESS = ds.Tables["tblLibrarySetup"].Rows[0].ItemArray.GetValue(5).ToString();
            //clsVariables.sWEBSITE = ds.Tables["tblLibrarySetup"].Rows[0].ItemArray.GetValue(6).ToString();
        }

    }

    public void CreateDirectory(string sFolder)
    {
        if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + sFolder) == false)
        {

            Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + sFolder);
        }
    }
    public static bool APP_CONNECTED = false;

    public static bool havePrevInstance()
    {
        try
        {
            if (System.Diagnostics.Process.GetProcessesByName(System.Diagnostics.Process.GetCurrentProcess().ProcessName).Length > 1)
            {
                return true;
            }
            else { return false; }
        }
        catch (Exception ex)
        {
            return false;

        }
    }

    public static void shell(string strShell, string sText)
    {
        try { System.Diagnostics.Process.Start(strShell); }
        catch (Exception ex)
        {
            MessageBox.Show("No " + sText + " installed in the system.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    // for user login and logout

    public static void record_login(string sLogin, string sLibrarian_ID)
    {
        //try
        //{
        //    //OLEDB COMMAND VARIABLES
        //    OleDbCommand cmdLogin;

        //    cmdLogin = new OleDbCommand("INSERT INTO tblUsersLog(LibrarianID,Login) VALUES(@getLibrarianID,@getLogin)", CN);
        //    cmdLogin.Parameters.Add("@getLibrarianID", OleDbType.VarChar);
        //    cmdLogin.Parameters.Add("@getLogin", OleDbType.Date);
        //    cmdLogin.Parameters["@getLibrarianID"].Value = sLibrarian_ID;
        //    cmdLogin.Parameters["@getLogin"].Value = sLogin;

        //    cmdLogin.ExecuteNonQuery();

        //}
        //catch (Exception ex) { }
    }

    public static void record_logout(string sLogout, string sLibrarian_ID)
    {

        //try
        //{
        //    //OLEDB COMMAND VARIABLES
        //    OleDbCommand cmdLogout;

        //    cmdLogout = new OleDbCommand("UPDATE tblUsersLog SET Logout =@getLogout WHERE LibrarianID LIKE '" + sLibrarian_ID + "' AND Logout Is Null", clsConnections.CN);
        //    cmdLogout.Parameters.Add("@getLogout", OleDbType.Date);
        //    cmdLogout.Parameters["@getLogout"].Value = sLogout;

        //    cmdLogout.ExecuteNonQuery();
        //}
        //catch (Exception ex) { }

    }

    public void fillgrid(string qry, int Flag, DataGridView dg)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@Flag", SqlDbType.Int);
            p[0].Value = Flag;
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(ConnectionClass.ConnectionString, CommandType.StoredProcedure, qry, p);
            dg.DataSource = ds.Tables[0];

        }
        catch (Exception ex)
        {
            clsErrHandler.WriteError(ex, this.ToString());

        }
    }
    public string getDate(string strdate, string formatdate)
    {
        if (strdate != string.Empty)
        {
            string[] myDateArray = strdate.Split(Convert.ToChar("/"));

            switch (formatdate)
            {

                case "MDY": return myDateArray[1].ToString() + "/" + myDateArray[0].ToString() + "/" + myDateArray[2].ToString();
                case "DMY": return myDateArray[0].ToString() + "/" + myDateArray[1].ToString() + "/" + myDateArray[2].ToString();
                case "YMD": return myDateArray[2].ToString() + "/" + myDateArray[0].ToString() + "/" + myDateArray[1].ToString();

                default: return "1/1/1900";

            }
        }
        return "1/1/1900";
    }

    // Clear All Controls on Form - Supriya
    public void clearAll(Control.ControlCollection ctrl)
    {
        for (int i = 0; i < ctrl.Count; i++)
        {
            if (ctrl[i].GetType() == typeof(System.Windows.Forms.TextBox))
            {
                TextBox txt = (TextBox)ctrl[i];
                txt.Text = "";
            }
            if (ctrl[i].GetType() == typeof(System.Windows.Forms.ComboBox))
            {
                ComboBox ddl = (ComboBox)ctrl[i];
                ddl.Text = "--Select--";
            }
            if (ctrl[i].GetType() == typeof(System.Windows.Forms.RichTextBox))
            {
                RichTextBox rchTxt = (RichTextBox)ctrl[i];
                rchTxt.Text = "";
            }
            if (ctrl[i].HasChildren)
            {
                clearAll(ctrl[i].Controls);
            }
        }
    }

    public void BtnValidate(int b1, int b2, int b3, int b4, Button add, Button save, Button delete, Button exit)
    {
        if (b1 == 0)
        {
            add.Enabled = true;
        }
        else
            add.Enabled = false;
        if (b2 == 0)
        {
            save.Enabled = true;
        }
        else
            save.Enabled = false;
        if (b3 == 0)
        {
            delete.Enabled = true;
        }
        else
            delete.Enabled = false;
        if (b4 == 0)
        {
            exit.Enabled = true;
        }
        else
            exit.Enabled = false;

    }
    public void NumValidate(KeyPressEventArgs e, TextBox t)
    {
        t.MaxLength = 12;
        if (char.IsLetter(e.KeyChar))
        {
            e.Handled = true;
        }
        else
            e.Handled = false;
    }
    public void LetterValidate(KeyPressEventArgs e)
    {
        if (char.IsNumber(e.KeyChar))
        {
            e.Handled = true;
        }
        else
            e.Handled = false;
    }
    public int textvalidate(Panel pnl)
    {
        foreach (Control ctrl in pnl.Controls)
        {
            if (ctrl.GetType().ToString() == typeof(TextBox).ToString())
            {
                if (ctrl.Text == "")
                {
                    ctrl.Focus();
                    MessageBox.Show("Plz fill full Information");
                    return 0;
                }
                else
                {
                    return 1;
                }
            }

        }
        return 2;
    }
    public void clear(Panel pnl)
    {
        for (int i = 0; i < pnl.Controls.Count; i++)
        {
            if (pnl.Controls[i].GetType() == typeof(System.Windows.Forms.TextBox))
            {
                TextBox txt = (TextBox)pnl.Controls[i];
                txt.Text = "";
            }
            if (pnl.Controls[i].GetType() == typeof(System.Windows.Forms.ComboBox))
            {
                ComboBox ddl = (ComboBox)pnl.Controls[i];
                ddl.Text = "--Select--";
            }
            if (pnl.Controls[i].GetType() == typeof(System.Windows.Forms.RichTextBox))
            {
                RichTextBox rchTxt = (RichTextBox)pnl.Controls[i];
                rchTxt.Text = "";
            }
            //if (pnl.Controls[i].HasChildren)
            //{
            //    clearAll(pnl.Controls[i].Controls);
            //}
            if (pnl.Controls[i].GetType() == typeof(System.Windows.Forms.DataGrid))
            {
                DataGrid dg = (DataGrid)pnl.Controls[i];
                dg.DataSource = null;
            }
        }
    }

    public int autogenerateid(string qry)
    {
        int i = 0;
        try
        {

            object o = SqlHelper.ExecuteScalar(ConnectionClass.ConnectionString, CommandType.Text, qry);
            if (o is DBNull)
            {
                i = 1;
            }
            else
            {
                i = Convert.ToInt32(o) + 1;
            }
        }
        catch (Exception ex)
        {

            clsErrHandler.WriteError(ex, this.ToString());
        }
        return i;
    }


    public void filcombo(string qry, ComboBox cmb)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = SqlHelper.ExecuteDataset(ConnectionClass.ConnectionString, CommandType.Text, qry);
            cmb.ValueMember = ds.Tables[0].Columns[0].ToString();
            cmb.DisplayMember = ds.Tables[0].Columns[1].ToString();
            cmb.DataSource = ds.Tables[0];

        }
        catch (Exception ex)
        {
            clsErrHandler.WriteError(ex, this.ToString());

        }
    }
    public void fillcombo(string qry, ComboBox cmb)
    {
        try
        {
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(ConnectionClass.ConnectionString, CommandType.Text, qry);
            cmb.ValueMember = ds.Tables[0].Columns[0].ToString();
            cmb.DisplayMember = ds.Tables[0].Columns[1].ToString();
            cmb.DataSource = ds.Tables[0];
        }
        catch (Exception ex)
        { }
    }
    public void EnabledControls(Control.ControlCollection ctrl)
    {
        for (int i = 0; i < ctrl.Count; i++)
        {
            if (ctrl[i].GetType() == typeof(System.Windows.Forms.TextBox))
            {
                TextBox txt = (TextBox)ctrl[i];
                txt.Enabled = true;
            }
            if (ctrl[i].GetType() == typeof(System.Windows.Forms.ComboBox))
            {
                ComboBox ddl = (ComboBox)ctrl[i];
                ddl.Enabled = true;
            }
            if (ctrl[i].GetType() == typeof(System.Windows.Forms.DateTimePicker))
            {
                DateTimePicker dtp = (DateTimePicker)ctrl[i];
                dtp.Enabled = true;
            }

            if (ctrl[i].HasChildren)
            {
                EnabledControls(ctrl[i].Controls);
            }
        }
    }
    public void DisableControls(Control.ControlCollection ctrl)
    {
        for (int i = 0; i < ctrl.Count; i++)
        {
            if (ctrl[i].GetType() == typeof(System.Windows.Forms.TextBox))
            {
                TextBox txt = (TextBox)ctrl[i];
                txt.Enabled = false; ;
            }
            if (ctrl[i].GetType() == typeof(System.Windows.Forms.ComboBox))
            {
                ComboBox ddl = (ComboBox)ctrl[i];
                ddl.Enabled = false;
                ddl.Text = "--Select--";
            }
            if (ctrl[i].GetType() == typeof(System.Windows.Forms.DateTimePicker))
            {
                DateTimePicker dtp = (DateTimePicker)ctrl[i];
                dtp.Enabled = false;
            }

            if (ctrl[i].HasChildren)
            {
                DisableControls(ctrl[i].Controls);
            }
        }
    }
    public class clsProductType
    {
        int id;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        int productName;
        public int ProdId
        {
            get { return productName; }
            set { productName = value; }
        }
        string ProductType;
        public string ProdType
        {
            get { return ProductType; }
            set { ProductType = value; }
        }
        string details;
        public string Details
        {
            get { return details; }
            set { details = value; }
        }

        int flag;
        public int Flag
        {
            get { return flag; }
            set { flag = value; }
        }
        public void AddUpdateProductType()
        {
            SqlParameter[] p = new SqlParameter[5];
            p[0] = new SqlParameter("@ID", SqlDbType.Int);
            p[0].Value = ID;
            p[1] = new SqlParameter("@ProdId", SqlDbType.Int);
            p[1].Value = ProdId;
            p[2] = new SqlParameter("@ProdType", SqlDbType.NVarChar);
            p[2].Value = ProdType;
            p[3] = new SqlParameter("@Details", SqlDbType.NVarChar);
            p[3].Value = Details;

            p[4] = new SqlParameter("@Flag", SqlDbType.Int);
            p[4].Value = 1;
            SqlHelper.ExecuteNonQuery(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_ProdType]", p);

        }
        public int DeleteProductType()
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@Flag", SqlDbType.Int);
            p[0].Value = 2;
            p[1] = new SqlParameter("@ID", SqlDbType.Int);
            p[1].Value = ID;
            int i = Convert.ToInt32(SqlHelper.ExecuteNonQuery(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_ProdType]", p));
            return i;
        }

        public DataSet GetByIDProductType()
        {
            try
            {
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@ID", SqlDbType.Int);
                p[0].Value = ID;
                p[1] = new SqlParameter("@Flag", SqlDbType.Int);
                p[1].Value = 3;   // 2 for getting particular customer

                DataSet ds = SqlHelper.ExecuteDataset(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_ProdType]", p);
                return ds;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public DataSet GetByListProductType()
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@Flag", SqlDbType.Int);
            p[0].Value = 4;
            DataSet ds = SqlHelper.ExecuteDataset(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_ProdType]", p);
            return ds;
        }
        //public DataSet GetByListBank1()
        //{
        //    SqlParameter[] p = new SqlParameter[1];
        //    p[0] = new SqlParameter("@Flag", SqlDbType.Int);
        //    p[0].Value = 4;
        //    DataSet ds = SqlHelper.ExecuteDataset(clsConnection.ConnectionString, CommandType.StoredProcedure, "[Proc_Frmbank]", p);
        //    return ds;
        //}
        public string GetForReport()
        {

            SqlDataAdapter ad = new SqlDataAdapter("SELECT CASE WHEN MAX(ID) IS NULL THEN 1 ELSE MAX(ID)+1  END AS ID FROM dbo.tblType", ConnectionClass.ConnectionString);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            return Convert.ToString(ds.Tables[0].Rows[0].ItemArray.GetValue(0));
        }
        public DataSet LoadGrid_Reminder() // create for get all records from Company
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@Flag", SqlDbType.Int);
                p[0].Value = 1;
                ds = SqlHelper.ExecuteDataset(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[GetReminder_ExpiryDate]", p);
                return ds;
            }
            catch (Exception ex)
            {
                //  clsErrHandler.WriteError(ex, this.ToString());
            }
            return ds;
        }

        public DataSet LoadGrid_ReminderByID() // create for get all records from Company
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@Flag", SqlDbType.Int);
                p[0].Value = 2;
                p[1] = new SqlParameter("@ID", SqlDbType.Int);
                p[1].Value = ID;
                ds = SqlHelper.ExecuteDataset(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[GetReminder_ExpiryDate]", p);
                return ds;
            }
            catch (Exception ex)
            {
                //  clsErrHandler.WriteError(ex, this.ToString());
            }
            return ds;
        }

        public string GetMaxID()
        {

            SqlDataAdapter ad = new SqlDataAdapter("SELECT CASE WHEN MAX(ID) IS NULL THEN 1 ELSE MAX(ID)+1  END AS ID FROM dbo.tblType", ConnectionClass.ConnectionString);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            return Convert.ToString(ds.Tables[0].Rows[0].ItemArray.GetValue(0));
        }
        public DataSet getProdType()
        {
            try
            {
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@ID", SqlDbType.Int);
                p[0].Value = ID;
                p[1] = new SqlParameter("@Flag", SqlDbType.Int);
                p[1].Value = 5;   // 2 for getting particular customer
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_ProdType]", p);
                return ds;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public DataSet getprodName()
        {
            try
            {
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@ID", SqlDbType.Int);
                p[0].Value = ID;
                p[1] = new SqlParameter("@Flag", SqlDbType.Int);
                p[1].Value = 6;   // 2 for getting particular customer
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_ProdType]", p);
                return ds;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
    

}

