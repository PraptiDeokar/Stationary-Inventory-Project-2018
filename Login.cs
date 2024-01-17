using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows;
using InventoryProject.Classes;
using System.Threading;


namespace InventoryProject
{
    public partial class Login : Form
    {
        //ConnectionClass cn = new ConnectionClass();
        clsBackUpDatabase db = new clsBackUpDatabase();
        clsUser objuser =new clsUser();
        public static string user = "";
        public Login()
        {
            Thread t = new Thread(new ThreadStart(frm));
            t.Start();
            Thread.Sleep(5000);
            
            InitializeComponent();
            t.Abort();
        }
        public void frm()
        {
            Application.Run(new splash());
        }
        private void cmdSelect_Click(object sender, EventArgs e)
        {
            DataSet df = new DataSet();
            objuser.Username = uname.Text.ToString();
            user = objuser.Username;
            df = objuser.GetByID();
            string pwd;
             pwd = df.Tables[0].Rows[0]["password"].ToString();
             if (txtpass.Text == pwd)
             {
                 InventoryProject.Forms.MDIParent1 mf = new Forms.MDIParent1();
                 mf.Show();
                 txtpass.Text = "";
                 this.Visible=false;
             }
             else
                 MessageBox.Show("You have entered wrong password..try again...");
             txtpass.Text = "";
             txtpass.Focus();

        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            
             }


        private void restore()
        {

try
            {
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


            
             private void button3_Click(object sender, System.EventArgs e)
             {
                

             }

             private void button2_Click(object sender, System.EventArgs e)
             {
               

             }
             private void GetName()
             {
                 try
                 {
                     DataSet ds = new DataSet();
                     ds = objuser.GetByList();
                     uname.DisplayMember = "Username";
                     uname.ValueMember = "Id";
                      uname.DataSource=ds.Tables[0];



                 }
                 catch (Exception ex)
                 {

                     string msg = ex.Message;
                 }
             }
             private void Form1_Load(object sender, System.EventArgs e)
             {
                 GetName();
        
             }

             private void button4_Click(object sender, EventArgs e)
             {

             }

             private void button5_Click(object sender, EventArgs e)
             {
                 this.Close();
             }

             private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
             {
                 user = uname.Text;
                 InventoryProject.Forms.forgotPassword f= new Forms.forgotPassword();
                 f.Show();
             }

             private void uname_SelectedIndexChanged(object sender, EventArgs e)
             {
               
             }



    }
    }

