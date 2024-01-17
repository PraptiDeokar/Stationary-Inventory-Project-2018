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
    public partial class ChangePasswordForm : Form
    {
        clsUser objuser = new clsUser();
        public static string pwd = "";
        public static string Newpwd = "";
        
        public ChangePasswordForm()
        {
            InitializeComponent();
        }

        private void ChangePasswordForm_Load(object sender, EventArgs e)
        {
            usernameLabel.Text = InventoryProject.Login.user.ToString();
            DataSet df = new DataSet();
            objuser.Username = usernameLabel.Text;
            df = objuser.GetByID();
            
            pwd = df.Tables[0].Rows[0]["password"].ToString();
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void usernameLabel_Click(object sender, EventArgs e)
        {

        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }

        private void Label4_Click(object sender, EventArgs e)
        {

        }

        private void Label5_Click(object sender, EventArgs e)
        {

        }

        private void ChangeButton_Click(object sender, EventArgs e)
        {
            if (newTextBox.Text == ConfirmTextBox.Text)
            {
                Newpwd = ConfirmTextBox.Text;
                objuser.password = Newpwd;
            
                objuser.Username = usernameLabel.Text;
                objuser.Flag = 6;
                objuser.Update();
                MessageBox.Show("Password changed successfully...");

            }
            else
            {
                label6.Visible = true;
                label6.Text = "New Password and confirm password must match";
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OldTextBox_Leave(object sender, EventArgs e)
        {
            if (OldTextBox.Text == pwd)
            {
                panel1.Enabled = true;
                label6.Visible = false;
                ChangeButton.Enabled = true;

            }
            else
            {
                label6.Text = "Enter correct Password";
                label6.Visible = true;

            }
        }
    }
}
