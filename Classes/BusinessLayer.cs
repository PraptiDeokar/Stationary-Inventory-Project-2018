using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Xml;
using System.Windows;
using System.IO;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using InventoryProject.Classes;

namespace InventoryProject.Classes
{
    class BusinessLayer
    {
        ConnectionClass con = new ConnectionClass();
        int i = 0;
    }
    public class clsErrHandler
    {
        public static void WriteError(Object errorObj, string FormTitle)
        {
            try
            {
                string path = UtilityClass.clsVariables.ErrPath + DateTime.Today.ToString("dd-MM-yyyy") + "_" + DateTime.Today.ToString("HH-MM-ss") + ".txt";
                //path = path.Replace(":","");
                if (!File.Exists(path))
                {
                    File.Create(path).Close();
                }

                using (StreamWriter w = File.AppendText(path))
                {
                    Exception ex = (Exception)errorObj;
                    StackTrace objStackTrace = new StackTrace(ex, true);
                    w.WriteLine("\r\nLog Entry :");
                    w.WriteLine("{0}", DateTime.Now.ToString(CultureInfo.InvariantCulture));
                    string err = "Error in :" + FormTitle + Environment.NewLine +
                                 "From Method: " + Convert.ToString(objStackTrace.GetFrame(objStackTrace.FrameCount - 1).GetMethod()) + Environment.NewLine +
                                 "At Line Number:" + Convert.ToString(objStackTrace.GetFrame(objStackTrace.FrameCount - 1).GetFileColumnNumber()) + Environment.NewLine +
                                 "Erro Msg : " + ex.Message + Environment.NewLine;
                    w.WriteLine(err);
                    w.WriteLine("_______________________________");
                    w.Flush();
                    w.Close();
                }
            }
            catch (Exception ex)
            {
                WriteError(ex.Message, FormTitle);
            }
        }
    }

    public class clsBank
    {
        int id;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }


        int flag;
        public int Flag
        {
            get { return flag; }
            set { flag = value; }
        }
        public void AddUpdate()
        {
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@ID", SqlDbType.Int);
            p[0].Value = ID;
            p[1] = new SqlParameter("@Name", SqlDbType.NVarChar);
            p[1].Value = Name;
            p[2] = new SqlParameter("@Flag", SqlDbType.Int);
            p[2].Value = 1;
            SqlHelper.ExecuteNonQuery(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_Bank]", p);

        }
        public int Delete()
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@Flag", SqlDbType.Int);
            p[0].Value = 2;
            p[1] = new SqlParameter("@ID", SqlDbType.Int);
            p[1].Value = ID;
            int i = Convert.ToInt32(SqlHelper.ExecuteNonQuery(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_Bank]", p));
            return i;
        }

        public DataSet GetByID()
        {
            try
            {
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@ID", SqlDbType.Int);
                p[0].Value = ID;
                p[1] = new SqlParameter("@Flag", SqlDbType.Int);
                p[1].Value = 3;   // 2 for getting particular customer

                DataSet ds = SqlHelper.ExecuteDataset(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_Bank]", p);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetByList()
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@Flag", SqlDbType.Int);
            p[0].Value = 4;
            DataSet ds = SqlHelper.ExecuteDataset(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_Bank]", p);
            return ds;
        }
        public string GetMaxID()
        {
            SqlDataAdapter ad = new SqlDataAdapter("SELECT CASE WHEN MAX(ID) IS NULL THEN 0 ELSE MAX(ID) END AS ID FROM tblBank", ConnectionClass.ConnectionString);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            return Convert.ToString(ds.Tables[0].Rows[0].ItemArray.GetValue(0));
        }
    }
    public class clsProfile
    {
        int iD;
        public int Id
        {
            get { return iD; }
            set { iD = value; }
        }

        string name;
        public string ShopName
        {
            get { return name; }
            set { name = value; }
        }
        string nam;
        public string adddr
        {
            get { return nam; }
            set { nam = value; }
        }
        string na;
        public string proprieter
        {
            get { return na; }
            set { na = value; }
        }
        string n;
        public string details
        {
            get { return n; }
            set { n = value; }
        }
        
        int flag;
        public int Flag
        {
            get { return flag; }
            set { flag = value; }
        }
        public void AddUpdate()
        {
            SqlParameter[] p = new SqlParameter[6];
           
            p[0] = new SqlParameter("@Flag", SqlDbType.Int);
            p[0].Value =1;
            p[1] = new SqlParameter("@Id", SqlDbType.Int);
            p[1].Value = 1;
            p[2] = new SqlParameter("@ShopName ", SqlDbType.NVarChar);
            p[2].Value = ShopName;
            p[3] = new SqlParameter("@adddr ", SqlDbType.NVarChar);
            p[3].Value = adddr;
            p[4] = new SqlParameter("@proprieter", SqlDbType.NVarChar);
            p[4].Value = proprieter;
            p[5] = new SqlParameter("@details ", SqlDbType.NVarChar);
            p[5].Value = details;
            SqlHelper.ExecuteNonQuery(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_ShopProfile]", p);

        }
        public int Delete()
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@Flag", SqlDbType.Int);
            p[0].Value = 2;

            int i = Convert.ToInt32(SqlHelper.ExecuteNonQuery(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_ShopProfile]", p));
            return i;
        }

        //public DataSet GetByID()
        //{
        //    try
        //    {
        //        SqlParameter[] p = new SqlParameter[1];
               
        //        p[0] = new SqlParameter("@Flag", SqlDbType.Int);
        //        p[0].Value = 4;   

        //        DataSet ds = SqlHelper.ExecuteDataset(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_Bank]", p);
        //        return ds;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        public DataSet GetByList()
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@Flag", SqlDbType.Int);
            p[0].Value = 4;
            DataSet ds = SqlHelper.ExecuteDataset(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_ShopProfile]", p);
            return ds;
        }
        //public string GetMaxID()
        //{
        //    SqlDataAdapter ad = new SqlDataAdapter("SELECT CASE WHEN MAX(ID) IS NULL THEN 0 ELSE MAX(ID) END AS ID FROM tblBank", ConnectionClass.ConnectionString);
        //    DataSet ds = new DataSet();
        //    ad.Fill(ds);
        //    return Convert.ToString(ds.Tables[0].Rows[0].ItemArray.GetValue(0));
        //}
    }
    public class clsUser
    {
        int iD;
        public int Id
        {
            get { return iD; }
            set { iD = value; }
        }

        string name;
        public string Username
        {
            get { return name; }
            set { name = value; }
        }

        string code;
        public string password
        {
            get { return code; }
            set { code = value; }
        }

        string details;
        public string secQues
        {
            get { return details; }
            set { details = value; }
        }
        string address;
        public string secAns
        {
            get { return address; }
            set { address = value; }
        }

        int flag;
        public int Flag
        {
            get { return flag; }
            set { flag = value; }
        }
        public void AddUpdate()
        {
            SqlParameter[] p = new SqlParameter[6];
            p[0] = new SqlParameter("@Id", SqlDbType.Int);
            p[0].Value = Id;
            p[1] = new SqlParameter("@Username", SqlDbType.NVarChar);
            p[1].Value = Username;
            p[2] = new SqlParameter("@password ", SqlDbType.NVarChar);
            p[2].Value = password;
            p[3] = new SqlParameter("@Flag", SqlDbType.Int);
            p[3].Value = 1;
            p[4] = new SqlParameter("@secQues", SqlDbType.NVarChar);
            p[4].Value = secQues;
            p[5] = new SqlParameter("@secAns", SqlDbType.NVarChar);
            p[5].Value = secAns;

            SqlHelper.ExecuteNonQuery(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_UserLogin]", p);

        }
        public void Update()
        {
            SqlParameter[] p = new SqlParameter[3];
            
            p[0] = new SqlParameter("@Username", SqlDbType.NVarChar);
            p[0].Value = Username;
            p[1] = new SqlParameter("@password ", SqlDbType.NVarChar);
            p[1].Value = password;
            p[2] = new SqlParameter("@Flag", SqlDbType.Int);
            p[2].Value = 6;
            
            SqlHelper.ExecuteNonQuery(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_UserLogin]", p);

        }
        public int Delete()
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@Flag", SqlDbType.Int);
            p[0].Value = 2;
            p[1] = new SqlParameter("@Username", SqlDbType.NVarChar);
            p[1].Value = Username;
            int i = Convert.ToInt32(SqlHelper.ExecuteNonQuery(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_UserLogin]", p));
            return i;
        }

        public DataSet GetByID()
        {
            try
            {
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@Username", SqlDbType.NVarChar);
                p[0].Value = Username;
                p[1] = new SqlParameter("@Flag", SqlDbType.Int);
                p[1].Value = 3;   // 2 for getting particular customer

                DataSet ds = SqlHelper.ExecuteDataset(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_UserLogin]", p);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetByUser()
        {
            try
            {
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@Username", SqlDbType.NVarChar);
                p[0].Value = Username;
                p[1] = new SqlParameter("@Flag", SqlDbType.Int);
                p[1].Value = 6;   // 2 for getting particular customer

                DataSet ds = SqlHelper.ExecuteDataset(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_UserLogin]", p);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetByList()
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@Flag", SqlDbType.NVarChar);
            p[0].Value = 5;
            DataSet ds = SqlHelper.ExecuteDataset(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_UserLogin]", p);
            return ds;
        }
        public string GetMaxID()
        {
            SqlDataAdapter ad = new SqlDataAdapter("SELECT CASE WHEN MAX(Id) IS NULL THEN 0 ELSE MAX(Id) END AS ID FROM tblUserLogin", ConnectionClass.ConnectionString);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            return Convert.ToString(ds.Tables[0].Rows[0].ItemArray.GetValue(0));
        }
    }
    public class clsUnit
    {
        int id;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        string name;
        public string Unit
        {
            get { return name; }
            set { name = value; }
        }

        int flag;
        public int Flag
        {
            get { return flag; }
            set { flag = value; }
        }
        public void AddUpdate()
        {
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@ID", SqlDbType.Int);
            p[0].Value = ID;
            p[1] = new SqlParameter("@Unit", SqlDbType.NVarChar);
            p[1].Value = Unit;
            p[2] = new SqlParameter("@Flag", SqlDbType.Int);
            p[2].Value = 1;
            SqlHelper.ExecuteNonQuery(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_Unit]", p);

        }
        public int Delete()
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@Flag", SqlDbType.Int);
            p[0].Value = 2;
            p[1] = new SqlParameter("@ID", SqlDbType.Int);
            p[1].Value = ID;
            int i = Convert.ToInt32(SqlHelper.ExecuteNonQuery(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_Unit]", p));
            return i;
        }

        public DataSet GetByID()
        {
            try
            {
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@ID", SqlDbType.Int);
                p[0].Value = ID;
                p[1] = new SqlParameter("@Flag", SqlDbType.Int);
                p[1].Value = 3;   // 2 for getting particular customer

                DataSet ds = SqlHelper.ExecuteDataset(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_Unit]", p);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetByList()
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@Unit", SqlDbType.NVarChar);
            p[0].Value = Unit;
            p[1] = new SqlParameter("@Flag", SqlDbType.Int);
            p[1].Value = 4;
            DataSet ds = SqlHelper.ExecuteDataset(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_Unit]", p);
            return ds;
        }
        public string GetMaxID()
        {
            SqlDataAdapter ad = new SqlDataAdapter("SELECT CASE WHEN MAX(ID) IS NULL THEN 1 ELSE MAX(ID)+1 END AS ID FROM tblUnit", ConnectionClass.ConnectionString);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            return Convert.ToString(ds.Tables[0].Rows[0].ItemArray.GetValue(0));
        }
    }

    public class clsItemMaster
    {
        int Id;
        public int ID
        {
            get { return Id; }
            set { Id = value; }
        }
        string Name;
        public string Item
        {
            get { return Name; }
            set { Name = value; }
        }
        string detail;
        public string Details
        {
            get { return detail; }
            set { detail = value; }
        }

        int flag;
        public int Flag
        {
            get { return flag; }
            set { flag = value; }
        }
        DateTime fromDate;
        public DateTime FromDate
        {
            get { return fromDate; }
            set { fromDate = value; }
        }
        DateTime toDate;
        public DateTime ToDate
        {
            get { return toDate; }
            set { toDate = value; }
        }



        public void AddUpdateItem()
        {
            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter("@ID", SqlDbType.Int);
            p[0].Value = ID;
            p[1] = new SqlParameter("@Item", SqlDbType.NVarChar);
            p[1].Value = Item;
            p[2] = new SqlParameter("@Details", SqlDbType.NVarChar);
            p[2].Value = Details;
            p[3] = new SqlParameter("@Flag", SqlDbType.Int);
            p[3].Value = Flag;
            SqlHelper.ExecuteNonQuery(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_ItemMaster]", p);

        }
        public DataSet getItembyID()
        {
            DataSet ds = new DataSet();
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@ID", SqlDbType.Int);
            p[0].Value = ID;
            p[1] = new SqlParameter("@Flag", SqlDbType.Int);
            p[1].Value = 6;
            p[2] = new SqlParameter("@Item", SqlDbType.NVarChar);
            p[2].Value = Item;
            ds = SqlHelper.ExecuteDataset(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_ItemMaster]", p);
            return ds;
        }
        public int DeleteItem()
        {
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@Flag", SqlDbType.Int);
            p[0].Value = 4;
            p[1] = new SqlParameter("@ID", SqlDbType.Int);
            p[1].Value = ID;
            p[2] = new SqlParameter("@Item", SqlDbType.NVarChar);
            p[2].Value = Item;
            int i = Convert.ToInt32(SqlHelper.ExecuteNonQuery(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_ItemMaster]", p));
            return i;
        }

        public DataSet GetByIDItem()
        {
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@Flag", SqlDbType.Int);
            p[0].Value = 2;
            p[1] = new SqlParameter("@ID", SqlDbType.Int);
            p[1].Value = ID;
            p[2] = new SqlParameter("@Item", SqlDbType.NVarChar);
            p[2].Value = Item;

            DataSet ds = SqlHelper.ExecuteDataset(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_ItemMaster]", p);

            return ds;
        }
        public DataSet GetByListItem()
        {
            SqlDataAdapter ad = new SqlDataAdapter("SELECT * FROM tblItem", ConnectionClass.ConnectionString);

            DataSet ds = new DataSet();
            ad.Fill(ds);
            return ds;
        }
        public string GetForReport()
        {

            SqlDataAdapter ad = new SqlDataAdapter("SELECT CASE WHEN MAX(ID) IS NULL THEN 1 ELSE MAX(ID)+1  END AS ID FROM tblItem", ConnectionClass.ConnectionString);
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

            SqlDataAdapter ad = new SqlDataAdapter("SELECT CASE WHEN MAX(ID) IS NULL THEN 1 ELSE MAX(ID)+1  END AS ID FROM dbo.tblItem", ConnectionClass.ConnectionString);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            return Convert.ToString(ds.Tables[0].Rows[0].ItemArray.GetValue(0));
        }
        public string Get(string si)
        {

            SqlDataAdapter ad = new SqlDataAdapter("select ID from tblItem where Item =" + "'" + si + "'", ConnectionClass.ConnectionString);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            return Convert.ToString(ds.Tables[0].Rows[0].ItemArray.GetValue(0));
        }

        public DataSet GetStockReport()
        {
            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter("@Flag", SqlDbType.Int);
            p[0].Value = Flag;
            p[1] = new SqlParameter("@ID", SqlDbType.Int);
            p[1].Value = ID;
            p[2] = new SqlParameter("@FromDate", SqlDbType.DateTime);
            p[2].Value = FromDate;
            p[3] = new SqlParameter("@ToDate", SqlDbType.DateTime);
            p[3].Value = ToDate;
            DataSet ds = SqlHelper.ExecuteDataset(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_GetStockNew]", p);
            return ds;
        }



    }
    public class clsSupplier
    {
        int id;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        string code;
        public string Code
        {
            get { return code; }
            set { code = value; }
        }
        string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        string address;
        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        string email;
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        string details;
        public string Details
        {
            get { return details; }
            set { details = value; }
        }
        string gstno;
        public string GstNo
        {
            get { return gstno; }
            set { gstno = value; }
        }
        string contact;
        public string Contact
        {
            get { return contact; }
            set { contact = value; }
        }

        string previousCredit;
        public string Type
        {
            get { return previousCredit; }
            set { previousCredit = value; }
        }

        int flag;
        public int Flag
        {
            get { return flag; }
            set { flag = value; }
        }
        public void AddUpdate()
        {
            SqlParameter[] p = new SqlParameter[10];
            p[0] = new SqlParameter("@ID", SqlDbType.Int);
            p[0].Value = ID;
            p[1] = new SqlParameter("@Name", SqlDbType.NVarChar);
            p[1].Value = Name;
            p[2] = new SqlParameter("@GstNo", SqlDbType.NVarChar);
            p[2].Value = GstNo;
            p[3] = new SqlParameter("@Address", SqlDbType.NVarChar);
            p[3].Value = Address;
            p[4] = new SqlParameter("@Contact", SqlDbType.NVarChar);
            p[4].Value = Contact;
            p[5] = new SqlParameter("@Email", SqlDbType.NVarChar);
            p[5].Value = Email;
            p[6] = new SqlParameter("@Details", SqlDbType.NVarChar);
            p[6].Value = Details;
            p[7] = new SqlParameter("@Flag", SqlDbType.Int);
            p[7].Value = 1;
            p[8] = new SqlParameter("@Type", SqlDbType.NVarChar);
            p[8].Value = Type;
            p[9] = new SqlParameter("@Code", SqlDbType.NVarChar);
            p[9].Value = Code;

            SqlHelper.ExecuteNonQuery(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_Supplier]", p);

        }
        public int Delete()
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@Flag", SqlDbType.Int);
            p[0].Value = 2;
            p[1] = new SqlParameter("@ID", SqlDbType.Int);
            p[1].Value = ID;
            int i = Convert.ToInt32(SqlHelper.ExecuteNonQuery(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_Supplier]", p));
            return i;
        }

        //public void AddPreviousCredit()
        //{
        //    try
        //    {
        //        SqlParameter[] p = new SqlParameter[3];
        //        p[0] = new SqlParameter("@ID", SqlDbType.Int);
        //        p[0].Value = ID;
        //        p[1] = new SqlParameter("@Flag", SqlDbType.Int);
        //        p[1].Value = 5;
        //        p[2] = new SqlParameter("@OpeningAmt", SqlDbType.Float);
        //        p[2].Value = OpeningAmt ;
        //        SqlHelper.ExecuteNonQuery(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_Supplier]", p);

        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        //public DataSet GetAmount()
        //{
        //    try
        //    {
        //        SqlParameter[] p = new SqlParameter[2];
        //        p[0] = new SqlParameter("@ID", SqlDbType.Int);
        //        p[0].Value = ID;
        //        p[1] = new SqlParameter("@Flag", SqlDbType.Int);
        //        p[1].Value = 3;

        //        DataSet ds = SqlHelper.ExecuteDataset(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_Supplier]", p);
        //        return ds;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public DataSet GetByList()
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@Flag", SqlDbType.Int);
            p[0].Value = 4;
            DataSet ds = SqlHelper.ExecuteDataset(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_Supplier]", p);
            return ds;
        }


        public DataSet GetByID()
        {
            try
            {
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@Flag", SqlDbType.Int);
                p[0].Value = 3;
                p[1] = new SqlParameter("@ID", SqlDbType.Int);
                p[1].Value = ID;
                // 2 for getting particular customer

                DataSet ds = SqlHelper.ExecuteDataset(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_Supplier]", p);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string GetMaxID()
        {

            SqlDataAdapter ad = new SqlDataAdapter("SELECT CASE WHEN MAX(ID) IS NULL THEN 1 ELSE MAX(ID)+1   END AS ID FROM tblSupplier", ConnectionClass.ConnectionString);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            return Convert.ToString(ds.Tables[0].Rows[0].ItemArray.GetValue(0));
        }
    }


    public class clsPurchaseDetails
    {
        int iD;
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        int pID;
        public int PID
        {
            get { return pID; }
            set { pID = value; }
        }

        int itemName;
        public int ItemName
        {
            get { return itemName; }
            set { itemName = value; }
        }
        int itemtype;
        public int ItemType
        {
            get { return itemtype; }
            set { itemtype = value; }
        }
        float rate;
        public float Rate
        {
            get { return rate; }
            set { rate = value; }
        }

        float quantity;
        public float Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
        string unit;
        public string Unit
        {
            get { return unit; }
            set { unit = value; }
        }

        float total;
        public float Total
        {
            get { return total; }
            set { total = value; }
        }
        public void AddUpdate()
        {
            SqlParameter[] p = new SqlParameter[9];
            p[0] = new SqlParameter("ID", SqlDbType.Int);
            p[0].Value = ID;
            p[1] = new SqlParameter("PID", SqlDbType.Int);
            p[1].Value = PID;
            p[2] = new SqlParameter("ItemName", SqlDbType.Int);
            p[2].Value = ItemName;
            p[3] = new SqlParameter("Rate", SqlDbType.Float);
            p[3].Value = Rate;
            p[4] = new SqlParameter("Quantity", SqlDbType.Float);
            p[4].Value = Quantity;
            p[5] = new SqlParameter("Total", SqlDbType.Float);
            p[5].Value = Total;
            p[6] = new SqlParameter("Flag", SqlDbType.Int);
            p[6].Value = 1;
            p[7] = new SqlParameter("ItemType", SqlDbType.Int);
            p[7].Value = ItemType;
            p[8] = new SqlParameter("Unit", SqlDbType.NVarChar);
            p[8].Value = Unit;
            //p[9] = new SqlParameter("ItemType", SqlDbType.Int);
            //p[9].Value = ItemType;

            SqlHelper.ExecuteNonQuery(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_PurchaseDetails]", p);
        }
        public string GetMaxID()
        {
            SqlDataAdapter ad = new SqlDataAdapter("SELECT CASE WHEN MAX(ID) IS NULL THEN '1' ELSE MAX(ID)+1 END FROM tblPurchaseDetails", ConnectionClass.ConnectionString);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            return Convert.ToString(ds.Tables[0].Rows[0].ItemArray.GetValue(0));
        }
        public void delete()
        {
            SqlParameter[] p = new SqlParameter[2];

            p[0] = new SqlParameter("Flag", SqlDbType.Int);
            p[0].Value = 2;

            p[1] = new SqlParameter("PID", SqlDbType.Int);
            p[1].Value = PID;

            SqlHelper.ExecuteNonQuery(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_PurchaseDetails]", p);

        }
        public DataSet getByList()
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@Flag", SqlDbType.Int);
            p[0].Value = 4;
            DataSet ds = SqlHelper.ExecuteDataset(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_PurchaseDetails]", p);
            return ds;
        }


        public DataSet getById()
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@Flag", SqlDbType.Int);
            p[0].Value = 3;

            p[1] = new SqlParameter("PID", SqlDbType.Int);
            p[1].Value = PID;
            DataSet ds = SqlHelper.ExecuteDataset(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "Proc_PurchaseDetails", p);
            return ds;

        }
        public DataSet getByStock()
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@Flag", SqlDbType.Int);
            p[0].Value = 5;

            p[1] = new SqlParameter("@ItemName", SqlDbType.Int);
            p[1].Value = ItemName;

            DataSet ds = SqlHelper.ExecuteDataset(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "Proc_PurchaseDetails", p);
            return ds;

        }
        public void updateStock()
        {
            try
            {
                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("@ID", SqlDbType.Int);
                p[0].Value = ID;
                p[1] = new SqlParameter("@Flag", SqlDbType.Int);
                p[1].Value = 6;
                p[2] = new SqlParameter("@Quantity", SqlDbType.Float);
                p[2].Value = Quantity;
                SqlHelper.ExecuteNonQuery(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_PurchaseDetails]", p);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetByIDSearch()
        {
            try
            {
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@Flag", SqlDbType.Int);
                p[0].Value = 7;
                p[1] = new SqlParameter("@PID", SqlDbType.Int);
                p[1].Value = PID;
                // 2 for getting particular customer

                DataSet ds = SqlHelper.ExecuteDataset(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_PurchaseDetails]", p);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }

    public class clsPurchase
    {
        int iD;
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        string code;
        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        DateTime date;
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        string transactionMode;
        public string TransactionMode
        {
            get { return transactionMode; }
            set { transactionMode = value; }
        }

        int supplierId;
        public int SupplierId
        {
            get { return supplierId; }
            set { supplierId = value; }
        }

        float vat;
        public float Gst
        {
            get { return vat; }
            set { vat = value; }
        }

        float vatAmt;
        public float GstAmt
        {
            get { return vatAmt; }
            set { vatAmt = value; }
        }

        float otherAmt;
        public float OtherAmt
        {
            get { return otherAmt; }
            set { otherAmt = value; }
        }

        float total;
        public float Total
        {
            get { return total; }
            set { total = value; }
        }
        float tot;
        public float FinalTotal
        {
            get { return tot; }
            set { tot = value; }
        }

        int flag;
        public int Flag
        {
            get { return flag; }
            set { flag = value; }
        }

        string search;
        public string Search
        {
            get { return search; }
            set { search = value; }
        }

        public void AddUpdate()
        {
            SqlParameter[] p = new SqlParameter[11];
            p[0] = new SqlParameter("ID", SqlDbType.Int);
            p[0].Value = ID;
            p[1] = new SqlParameter("Code", SqlDbType.NVarChar);
            p[1].Value = Code;
            p[2] = new SqlParameter("Date", SqlDbType.DateTime);
            p[2].Value = Date;
            p[3] = new SqlParameter("TransactionMode", SqlDbType.NVarChar);
            p[3].Value = TransactionMode;
            p[4] = new SqlParameter("SupplierId", SqlDbType.Int);
            p[4].Value = SupplierId;
            p[5] = new SqlParameter("Gst", SqlDbType.Float);
            p[5].Value = Gst;
            p[6] = new SqlParameter("GstAmt", SqlDbType.Float);
            p[6].Value = GstAmt;
            p[7] = new SqlParameter("OtherAmt", SqlDbType.Float);
            p[7].Value = OtherAmt;
            p[8] = new SqlParameter("Total", SqlDbType.Float);
            p[8].Value = Total;
            p[9] = new SqlParameter("Flag", SqlDbType.Int);
            p[9].Value = 1;
            p[10] = new SqlParameter("FinalTotal", SqlDbType.Float);
            p[10].Value = FinalTotal;

            SqlHelper.ExecuteNonQuery(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_PurchaseMaster]", p);
        }

        public void delete()
        {
            SqlParameter[] p = new SqlParameter[2];

            p[0] = new SqlParameter("Flag", SqlDbType.Int);
            p[0].Value = 2;

            p[1] = new SqlParameter("ID", SqlDbType.Int);
            p[1].Value = ID;

            SqlHelper.ExecuteNonQuery(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_PurchaseMaster]", p);

        }

        public DataSet getByList()
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@Flag", SqlDbType.Int);
            p[0].Value = 4;
            DataSet ds = SqlHelper.ExecuteDataset(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_PurchaseMaster]", p);
            return ds;

        }
        public DataSet GetByDate()
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@Flag", SqlDbType.Int);
            p[0].Value = 6;

            p[1] = new SqlParameter("@Date", SqlDbType.DateTime);
            p[1].Value = Date;
            DataSet ds = SqlHelper.ExecuteDataset(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "Proc_PurchaseMaster", p);
            return ds;

        }
        public DataSet getBySupplierId()
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@Flag", SqlDbType.Int);
            p[0].Value = 5;

            p[1] = new SqlParameter("@SupplierId", SqlDbType.Int);
            p[1].Value = SupplierId;
            DataSet ds = SqlHelper.ExecuteDataset(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "Proc_PurchaseMaster", p);
            return ds;

        }
        public DataSet getById()
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@Flag", SqlDbType.Int);
            p[0].Value = 3;

            p[1] = new SqlParameter("ID", SqlDbType.Int);
            p[1].Value = ID;
            DataSet ds = SqlHelper.ExecuteDataset(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "Proc_PurchaseMaster", p);
            return ds;

        }

        public string GetCode()
        {
            SqlDataAdapter ad = new SqlDataAdapter("SELECT CASE WHEN MAX(Code) IS NULL THEN '1' ELSE MAX(Code)+1 END FROM tblPurchaseMaster", ConnectionClass.ConnectionString);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            return Convert.ToString(ds.Tables[0].Rows[0].ItemArray.GetValue(0));
        }
        public string GetMaxID()
        {
            SqlDataAdapter ad = new SqlDataAdapter("SELECT CASE WHEN MAX(ID) IS NULL THEN '0' ELSE MAX(ID) END FROM tblPurchaseMaster", ConnectionClass.ConnectionString);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            return Convert.ToString(ds.Tables[0].Rows[0].ItemArray.GetValue(0));
        }
        public DataSet GetPurReport()
        {
            SqlParameter[] p = new SqlParameter[4];

            p[0] = new SqlParameter("@ID", SqlDbType.Int);
            p[0].Value = ID;
            DataSet ds = SqlHelper.ExecuteDataset(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Get_PurReport]", p);
            return ds;
        }
    }

    public class clsPayment
    {

        int id;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        int supplierId;
        public int SupplierId
        {
            get { return supplierId; }
            set { supplierId = value; }
        }
        float amtToPay;
        public float AmtToPay
        {
            get { return amtToPay; }
            set { amtToPay = value; }
        }

        string details;
        public string Details
        {
            get { return details; }
            set { details = value; }
        }

        string mode;
        public string Mode
        {
            get { return mode; }
            set { mode = value; }
        }
        int bankName;
        public int BankName
        {
            get { return bankName; }
            set { bankName = value; }
        }
        string code;
        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        string chequeNum;
        public string ChequeNum
        {
            get { return chequeNum; }
            set { chequeNum = value; }
        }
        int flag;
        public int Flag
        {
            get { return flag; }
            set { flag = value; }
        }
        DateTime date;
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
        DateTime chkDate;
        public DateTime ChkDate
        {
            get { return chkDate; }
            set { chkDate = value; }
        }
        public void AddUpdate()
        {
            SqlParameter[] p = new SqlParameter[11];
            p[0] = new SqlParameter("ID", SqlDbType.Int);
            p[0].Value = ID;

            p[1] = new SqlParameter("Details", SqlDbType.NVarChar);
            p[1].Value = Details;

            p[2] = new SqlParameter("Mode", SqlDbType.NVarChar);
            p[2].Value = Mode;

            p[3] = new SqlParameter("BankName", SqlDbType.Int);
            p[3].Value = BankName;

            p[4] = new SqlParameter("Flag", SqlDbType.Int);
            p[4].Value = 1;

            p[5] = new SqlParameter("ChequeNum", SqlDbType.NVarChar);
            p[5].Value = ChequeNum;

            p[6] = new SqlParameter("SupplierId", SqlDbType.Int);
            p[6].Value = SupplierId;

            p[7] = new SqlParameter("Date", SqlDbType.Date);
            p[7].Value = Date;

            p[8] = new SqlParameter("ChkDate", SqlDbType.Date);
            p[8].Value = ChkDate;

            p[9] = new SqlParameter("AmtToPay", SqlDbType.Float);
            p[9].Value = AmtToPay;


            p[10] = new SqlParameter("Code", SqlDbType.NVarChar);
            p[10].Value = Code;

            SqlHelper.ExecuteNonQuery(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_Payment]", p);
        }
        public DataSet GetSearch()
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@Flag", SqlDbType.Int);
            p[0].Value = 6;
            p[1] = new SqlParameter("@BankName", SqlDbType.NVarChar);
            p[1].Value = BankName;


            DataSet ds = SqlHelper.ExecuteDataset(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_Payment]", p);
            return ds;

        }
        public void delete()
        {
            SqlParameter[] p = new SqlParameter[2];

            p[0] = new SqlParameter("Flag", SqlDbType.Int);
            p[0].Value = 2;

            p[1] = new SqlParameter("ID", SqlDbType.Int);
            p[1].Value = ID;

            SqlHelper.ExecuteNonQuery(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_Payment]", p);

        }

        public DataSet getByList()
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@Flag", SqlDbType.Int);
            p[0].Value = 4;


            DataSet ds = SqlHelper.ExecuteDataset(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_Payment]", p);
            return ds;

        }
        public DataSet GetAmount()
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@Flag", SqlDbType.Int);
            p[0].Value = 5;

            p[1] = new SqlParameter("SupplierId", SqlDbType.Int);
            p[1].Value = SupplierId;
            DataSet ds = SqlHelper.ExecuteDataset(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_Payment]", p);
            return ds;

        }
        public DataSet getById()
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@Flag", SqlDbType.Int);
            p[0].Value = 3;

            p[1] = new SqlParameter("ID", SqlDbType.Int);
            p[1].Value = ID;
            DataSet ds = SqlHelper.ExecuteDataset(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_Payment]", p);
            return ds;

        }
        public DataSet getBy_supId()
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@Flag", SqlDbType.Int);
            p[0].Value = 5;

            p[1] = new SqlParameter("@SupplierId", SqlDbType.Int);
            p[1].Value = SupplierId;
            DataSet ds = SqlHelper.ExecuteDataset(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_Payment]", p);
            return ds;

        }
        public string GetCode()
        {
            SqlDataAdapter ad = new SqlDataAdapter("SELECT CASE WHEN MAX(Code) IS NULL THEN '1' ELSE MAX(Code)+1 END FROM tblPayment", ConnectionClass.ConnectionString);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            return Convert.ToString(ds.Tables[0].Rows[0].ItemArray.GetValue(0));
        }
        public string GetMaxID()
        {
            SqlDataAdapter ad = new SqlDataAdapter("SELECT CASE WHEN MAX(ID) IS NULL THEN '0' ELSE MAX(ID) END FROM tblPayment", ConnectionClass.ConnectionString);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            return Convert.ToString(ds.Tables[0].Rows[0].ItemArray.GetValue(0));
        }
    }

    public class clsBill
    {
        int iD;
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        int customerId;
        public int CustomerId
        {
            get { return customerId; }
            set { customerId = value; }
        }


        string code;
        public string Code
        {
            get { return code; }
            set { code = value; }
        }
        string mode;
        public string Mode
        {
            get { return mode; }
            set { mode = value; }
        }
        int flag;
        public int Flag
        {
            get { return flag; }
            set { flag = value; }
        }
        DateTime date;
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        float vat;
        public float Vat
        {
            get { return vat; }
            set { vat = value; }
        }

        float vatAmt;
        public float VatAmt
        {
            get { return vatAmt; }
            set { vatAmt = value; }
        }

        float total;
        public float Total
        {
            get { return total; }
            set { total = value; }
        }

        float advance;
        public float Other
        {
            get { return advance; }
            set { advance = value; }
        }
        float remaining;
        public float FinalTotal
        {
            get { return remaining; }
            set { remaining = value; }
        }
        public string GetCode()
        {
            SqlDataAdapter ad = new SqlDataAdapter("SELECT CASE WHEN MAX(Code) IS NULL THEN '1' ELSE MAX(Code)+1 END FROM tblBillMaster", ConnectionClass.ConnectionString);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            return Convert.ToString(ds.Tables[0].Rows[0].ItemArray.GetValue(0));
        }
        public string GetMaxID()
        {
            SqlDataAdapter ad = new SqlDataAdapter("SELECT CASE WHEN MAX(ID) IS NULL THEN '0' ELSE MAX(ID) END FROM tblBillMaster", ConnectionClass.ConnectionString);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            return Convert.ToString(ds.Tables[0].Rows[0].ItemArray.GetValue(0));
        }
        public void AddUpdate()
        {
            SqlParameter[] p = new SqlParameter[11];
            p[0] = new SqlParameter("@ID", SqlDbType.Int);
            p[0].Value = ID;
            p[1] = new SqlParameter("@Code", SqlDbType.NVarChar);
            p[1].Value = Code;
            p[2] = new SqlParameter("@Date", SqlDbType.DateTime);
            p[2].Value = Date;
            p[3] = new SqlParameter("@Mode", SqlDbType.NVarChar);
            p[3].Value = Mode;
            p[4] = new SqlParameter("@CustomerId", SqlDbType.Int);
            p[4].Value = CustomerId;
            p[5] = new SqlParameter("@Vat", SqlDbType.Float);
            p[5].Value = Vat;
            p[6] = new SqlParameter("@Flag", SqlDbType.Int);
            p[6].Value = 1;
            p[7] = new SqlParameter("@VatAmt", SqlDbType.Float);
            p[7].Value = VatAmt;
            p[8] = new SqlParameter("@Total", SqlDbType.Float);
            p[8].Value = Total;
            p[9] = new SqlParameter("@Other", SqlDbType.Float);
            p[9].Value = Other;
            p[10] = new SqlParameter("@FinalTotal", SqlDbType.Float);
            p[10].Value = FinalTotal;
            SqlHelper.ExecuteNonQuery(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_BillMaster]", p);

        }
        public DataSet GetByList()
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@Flag", SqlDbType.Int);
            p[0].Value = 4;
            DataSet ds = SqlHelper.ExecuteDataset(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_BillMaster]", p);
            return ds;
        }
        public DataSet GetByID()
        {
            try
            {
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@Flag", SqlDbType.Int);
                p[0].Value = 3;
                p[1] = new SqlParameter("@ID", SqlDbType.Int);
                p[1].Value = ID;
                // 2 for getting particular customer

                DataSet ds = SqlHelper.ExecuteDataset(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_BillMaster]", p);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetBySearchID()
        {
            try
            {
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@Flag", SqlDbType.Int);
                p[0].Value = 5;
                p[1] = new SqlParameter("@CustomerId", SqlDbType.Int);
                p[1].Value = CustomerId;
                // 2 for getting particular customer

                DataSet ds = SqlHelper.ExecuteDataset(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_BillMaster]", p);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int Delete()
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@Flag", SqlDbType.Int);
            p[0].Value = 2;
            p[1] = new SqlParameter("@ID", SqlDbType.Int);
            p[1].Value = ID;
            int i = Convert.ToInt32(SqlHelper.ExecuteNonQuery(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_BillMaster]", p));
            return i;
        }
    }
    public class clsReciept
    {
        int iD;
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        string code;
        public string Code
        {
            get { return code; }
            set { code = value; }
        }
        DateTime date;
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        int customerId;
        public int CustomerId
        {
            get { return customerId; }
            set { customerId = value; }
        }

        float amtToPay;
        public float AmtToPay
        {
            get { return amtToPay; }
            set { amtToPay = value; }
        }

        string details;
        public string Details
        {
            get { return details; }
            set { details = value; }
        }
        string mode;
        public string Mode
        {
            get { return mode; }
            set { mode = value; }
        }
        int bankname;
        public int Bankname
        {
            get { return bankname; }
            set { bankname = value; }
        }
        string chequeNum;
        public string ChequeNum
        {
            get { return chequeNum; }
            set { chequeNum = value; }
        }
        DateTime chkDate;
        public DateTime ChkDate
        {
            get { return chkDate; }
            set { chkDate = value; }
        }
        int flag;
        public int Flag
        {
            get { return flag; }
            set { flag = value; }
        }
        public void AddUpdate()
        {
            SqlParameter[] p = new SqlParameter[11];
            p[0] = new SqlParameter("@ID", SqlDbType.Int);
            p[0].Value = ID;
            p[1] = new SqlParameter("@Code", SqlDbType.NVarChar);
            p[1].Value = Code;
            p[2] = new SqlParameter("@Date", SqlDbType.DateTime);
            p[2].Value = Date;
            p[3] = new SqlParameter("@CustomerId", SqlDbType.Int);
            p[3].Value = CustomerId;
            p[4] = new SqlParameter("@AmtToPay", SqlDbType.Float);
            p[4].Value = AmtToPay;

            p[5] = new SqlParameter("@Details", SqlDbType.NVarChar);
            p[5].Value = Details;
            p[6] = new SqlParameter("@Mode", SqlDbType.NVarChar);
            p[6].Value = Mode;
            p[7] = new SqlParameter("@Bankname", SqlDbType.Int);
            p[7].Value = Bankname;
            p[8] = new SqlParameter("@ChequeNum", SqlDbType.NVarChar);
            p[8].Value = ChequeNum;
            p[9] = new SqlParameter("@ChkDate", SqlDbType.DateTime);
            p[9].Value = ChkDate;
            p[10] = new SqlParameter("@Flag", SqlDbType.Int);
            p[10].Value = 1;

            SqlHelper.ExecuteNonQuery(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_Receipt]", p);

        }
        public DataSet GetByList()
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@Flag", SqlDbType.Int);
            p[0].Value = 4;
            DataSet ds = SqlHelper.ExecuteDataset(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_Receipt]", p);
            return ds;
        }


        public DataSet GetByID()
        {
            try
            {
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@Flag", SqlDbType.Int);
                p[0].Value = 3;
                p[1] = new SqlParameter("@ID", SqlDbType.Int);
                p[1].Value = ID;
                // 2 for getting particular customer

                DataSet ds = SqlHelper.ExecuteDataset(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_Receipt]", p);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetBy_custIDSearch()
        {
            try
            {
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@Flag", SqlDbType.Int);
                p[0].Value = 5;
                p[1] = new SqlParameter("@CustomerId", SqlDbType.Int);
                p[1].Value = CustomerId;
                // 2 for getting particular customer

                DataSet ds = SqlHelper.ExecuteDataset(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_Receipt]", p);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Delete()
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@Flag", SqlDbType.Int);
            p[0].Value = 2;
            p[1] = new SqlParameter("@ID", SqlDbType.Int);
            p[1].Value = ID;
            int i = Convert.ToInt32(SqlHelper.ExecuteNonQuery(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_Receipt]", p));
            return i;
        }
        public string GetCode()
        {
            SqlDataAdapter ad = new SqlDataAdapter("SELECT CASE WHEN MAX(Code) IS NULL THEN '1' ELSE MAX(Code)+1 END FROM tblReceipt", ConnectionClass.ConnectionString);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            return Convert.ToString(ds.Tables[0].Rows[0].ItemArray.GetValue(0));
        }
        public string GetMaxID()
        {
            SqlDataAdapter ad = new SqlDataAdapter("SELECT CASE WHEN MAX(ID) IS NULL THEN '0' ELSE MAX(ID) END FROM tblReceipt", ConnectionClass.ConnectionString);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            return Convert.ToString(ds.Tables[0].Rows[0].ItemArray.GetValue(0));
        }




    }
    public class clsBillDetails
    {
        int iD;
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        int bID;
        public int BID
        {
            get { return bID; }
            set { bID = value; }
        }

        int workDetailsId;
        public int Item
        {
            get { return workDetailsId; }
            set { workDetailsId = value; }
        }
        float rate;
        public float Rate
        {
            get { return rate; }
            set { rate = value; }
        }
        float quantity;
        public float Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
        float total;
        public float Total
        {
            get { return total; }
            set { total = value; }
        }
        int flag;
        public int Flag
        {
            get { return flag; }
            set { flag = value; }
        }
        public void AddUpdate()
        {
            SqlParameter[] p = new SqlParameter[7];
            p[0] = new SqlParameter("@ID", SqlDbType.Int);
            p[0].Value = ID;
            p[1] = new SqlParameter("@BID", SqlDbType.Int);
            p[1].Value = BID;
            p[2] = new SqlParameter("@Item", SqlDbType.Int);
            p[2].Value = Item;
            p[3] = new SqlParameter("@Rate", SqlDbType.Float);
            p[3].Value = Rate;
            p[4] = new SqlParameter("@Quantity", SqlDbType.Float);
            p[4].Value = Quantity;
            p[5] = new SqlParameter("@Total", SqlDbType.Float);
            p[5].Value = Total;
            p[6] = new SqlParameter("@Flag", SqlDbType.Int);
            p[6].Value = 1;

            SqlHelper.ExecuteNonQuery(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_BillDetails]", p);

        }
        public DataSet GetByList()
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@Flag", SqlDbType.Int);
            p[0].Value = 4;
            DataSet ds = SqlHelper.ExecuteDataset(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_BillDetails]", p);
            return ds;
        }


        public DataSet GetByID()
        {
            try
            {
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@Flag", SqlDbType.Int);
                p[0].Value = 3;
                p[1] = new SqlParameter("@BID", SqlDbType.Int);
                p[1].Value = BID;
                // 2 for getting particular customer

                DataSet ds = SqlHelper.ExecuteDataset(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_BillDetails]", p);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetByIDSearch()
        {
            try
            {
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@Flag", SqlDbType.Int);
                p[0].Value = 5;
                p[1] = new SqlParameter("@BID", SqlDbType.Int);
                p[1].Value = BID;
                // 2 for getting particular customer

                DataSet ds = SqlHelper.ExecuteDataset(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_BillDetails]", p);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int Delete()
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@Flag", SqlDbType.Int);
            p[0].Value = 2;
            p[1] = new SqlParameter("@BID", SqlDbType.Int);
            p[1].Value = BID;
            int i = Convert.ToInt32(SqlHelper.ExecuteNonQuery(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_BillDetails]", p));
            return i;
        }


    }
    public class clsCustomer
    {
        int id;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        string code;
        public string Code
        {
            get { return code; }
            set { code = value; }
        }
        string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        string address;
        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        string email;
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        string details;
        public string Details
        {
            get { return details; }
            set { details = value; }
        }
        string contact;
        public string Contact
        {
            get { return contact; }
            set { contact = value; }
        }

        string previousCredit;
        public string Type
        {
            get { return previousCredit; }
            set { previousCredit = value; }
        }

        int flag;
        public int Flag
        {
            get { return flag; }
            set { flag = value; }
        }
        public void AddUpdate()
        {
            SqlParameter[] p = new SqlParameter[9];
            p[0] = new SqlParameter("@ID", SqlDbType.Int);
            p[0].Value = ID;
            p[1] = new SqlParameter("@Name", SqlDbType.NVarChar);
            p[1].Value = Name;
            p[2] = new SqlParameter("@Address", SqlDbType.NVarChar);
            p[2].Value = Address;
            p[3] = new SqlParameter("@Contact", SqlDbType.NVarChar);
            p[3].Value = Contact;
            p[4] = new SqlParameter("@Email", SqlDbType.NVarChar);
            p[4].Value = Email;
            p[5] = new SqlParameter("@Details", SqlDbType.NVarChar);
            p[5].Value = Details;
            p[6] = new SqlParameter("@Flag", SqlDbType.Int);
            p[6].Value = 1;
            p[7] = new SqlParameter("@Type", SqlDbType.NVarChar);
            p[7].Value = Type;
            p[8] = new SqlParameter("@Code", SqlDbType.NVarChar);
            p[8].Value = Code;
            SqlHelper.ExecuteNonQuery(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_Customer]", p);

        }
        public int Delete()
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@Flag", SqlDbType.Int);
            p[0].Value = 2;
            p[1] = new SqlParameter("@ID", SqlDbType.Int);
            p[1].Value = ID;
            int i = Convert.ToInt32(SqlHelper.ExecuteNonQuery(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_Customer]", p));
            return i;
        }

        //public void AddPreviousCredit()
        //{
        //    try
        //    {
        //        SqlParameter[] p = new SqlParameter[3];
        //        p[0] = new SqlParameter("@ID", SqlDbType.Int);
        //        p[0].Value = ID;
        //        p[1] = new SqlParameter("@Flag", SqlDbType.Int);
        //        p[1].Value = 5;
        //        p[2] = new SqlParameter("@PreviousCredit", SqlDbType.Float);
        //        p[2].Value = Type;
        //        SqlHelper.ExecuteNonQuery(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_Customer]", p);

        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}


        public DataSet GetByList()
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@Flag", SqlDbType.Int);
            p[0].Value = 4;
            DataSet ds = SqlHelper.ExecuteDataset(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_Customer]", p);
            return ds;
        }


        public DataSet GetByID()
        {
            try
            {
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@Flag", SqlDbType.Int);
                p[0].Value = 3;
                p[1] = new SqlParameter("@ID", SqlDbType.Int);
                p[1].Value = ID;
                // 2 for getting particular customer

                DataSet ds = SqlHelper.ExecuteDataset(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_Customer]", p);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string GetMaxID()
        {

            SqlDataAdapter ad = new SqlDataAdapter("SELECT CASE WHEN MAX(ID) IS NULL THEN 0 ELSE MAX(ID)   END AS ID FROM tblCustomer", ConnectionClass.ConnectionString);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            return Convert.ToString(ds.Tables[0].Rows[0].ItemArray.GetValue(0));
        }
        public DataSet GetAmount()
        {
            try
            {
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@ID", SqlDbType.Int);
                p[0].Value = ID;
                p[1] = new SqlParameter("@Flag", SqlDbType.Int);
                p[1].Value = 3;

                DataSet ds = SqlHelper.ExecuteDataset(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_Customer]", p);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }



    public class clsProductType
    {
        int productId;
        public int ID
        {
            get { return productId; }
            set { productId = value; }
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

            SqlDataAdapter ad = new SqlDataAdapter("SELECT CASE WHEN MAX(ID) IS NULL THEN 1 ELSE MAX(ID)+1  END AS ID FROM tblType", ConnectionClass.ConnectionString);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            return Convert.ToString(ds.Tables[0].Rows[0].ItemArray.GetValue(0));
        }
        public DataSet getProdType()
        {
            try
            {
                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("@ID", SqlDbType.Int);
                p[0].Value = ID;
                p[1] = new SqlParameter("@ProdId", SqlDbType.Int);
                p[1].Value = ProdId;
                p[2] = new SqlParameter("@Flag", SqlDbType.Int);
                p[2].Value = 5;   // 2 for getting particular customer
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "[Proc_ProdType]", p);
                return ds;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public DataSet getProdTypeRate()
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
        public DataSet getprodName()
        {

            SqlDataAdapter ad = new SqlDataAdapter("select ID,Item from tblItem group by ID,Item", ConnectionClass.ConnectionString);
            DataSet ds = new DataSet();

            ad.Fill(ds);
            return ds;
        }
    }
}
public class clsBackUpDatabase // Class backup For back up Setting 
{
    int flag;
    public int Flag
    {
        get { return flag; }
        set { flag = value; }
    }
    string destinationPath;
    public string DestinationPath
    {
        get { return destinationPath; }
        set { destinationPath = value; }
    }
    string sourcePath;
    public string SourcePath
    {
        get { return sourcePath; }
        set { sourcePath = value; }
    }
    string databaseName;
    public string DatabaseName
    {
        get { return databaseName; }
        set { databaseName = value; }
    }

    public void BackupDB()
    {

        SqlParameter[] p = new SqlParameter[3];
        p[0] = new SqlParameter("@Flag", SqlDbType.Int);
        p[0].Value = Flag;
        p[1] = new SqlParameter("@BackupFile", SqlDbType.NVarChar);
        p[1].Value = DestinationPath;
        p[2] = new SqlParameter("@DataBaseName", SqlDbType.NVarChar);
        p[2].Value = "Inventory";
        int i = SqlHelper.ExecuteNonQuery(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "Proc_DatabaseBackup", p);
    }
    public void RestoreDB(string s)
    {
        SqlConnection cn = new SqlConnection();
        cn.ConnectionString = ConnectionClass.ConnectionString;
        cn.Open();
        string database = "Inventory";
        try
        {
            string sql1 = string.Format("ALTER DATABASE[" + database + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
            SqlCommand cmd1 = new SqlCommand(sql1, cn);
            cmd1.ExecuteNonQuery();
            string sql2 = string.Format("USE MASTER RESTORE DATABASE [" + database + "] FROM DISK='" + s + "' WITH REPLACE;");
            SqlCommand cmd2 = new SqlCommand(sql2, cn);
            cmd2.ExecuteNonQuery();
            string sql3 = string.Format("ALTER DATABASE[" + database + "] SET MULTI_USER");
            SqlCommand cmd3 = new SqlCommand(sql3, cn);
            cmd3.ExecuteNonQuery();


        }
        catch
        {

        }
    }













        //SqlParameter[] p = new SqlParameter[3];
        //p[0] = new SqlParameter("@Flag", SqlDbType.Int);
        //p[0].Value = Flag;
        //p[1] = new SqlParameter("@BackupFile", SqlDbType.NVarChar);
        //p[1].Value = SourcePath;
        //p[2] = new SqlParameter("@DataBaseName", SqlDbType.NVarChar);
        //p[2].Value = DatabaseName;
        //SqlHelper.ExecuteNonQuery(ConnectionClass.ConnectionString, CommandType.StoredProcedure, "Proc_DatabaseBackup", p);
   // }
}






