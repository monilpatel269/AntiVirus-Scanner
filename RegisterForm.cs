using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AntiVirus_Scanner
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (TbUser.Text == "" && TbPass.Text != "")
            {
                MessageBox.Show("Please enter Username");
            }
            else if (TbUser.Text != "" && TbPass.Text == "")
            {
                MessageBox.Show("please enter Password");
            }
            else if (TbUser.Text == "" && TbPass.Text == "")
            {
                MessageBox.Show("Please enter Username and Password");
            }
            else
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
                con.Open();
                string user = TbUser.Text;
                string password = TbPass.Text;
                SqlCommand cmd = new SqlCommand("exec Register '" + user + "','" + password + "' ", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully Registered!");
                TbUser.Clear();
                TbPass.Clear();
                con.Close();
            }
        }
    }
}
