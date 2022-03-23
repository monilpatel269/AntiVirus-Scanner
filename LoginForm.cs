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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (tbUser.Text == "" && tbpass.Text != "")
            {
                MessageBox.Show("Please enter Username");
            }
            else if (tbUser.Text != "" && tbpass.Text == "")
            {
                MessageBox.Show("please enter Password");
            }
            else if (tbUser.Text == "" && tbpass.Text == "")
            {
                MessageBox.Show("Please enter Username and Password");
            }
            else
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
                con.Open();
                SqlCommand cmd = new SqlCommand("exec LoginDetails", con);
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                   string user = dr["User"].ToString();
                   string password = dr["Password"].ToString();
                    if (user == tbUser.Text && password==tbpass.Text)
                    {
                        MessageBox.Show("Successfully Login");
                        Form1.instance.lbl.Text = "Hello " + tbUser.Text;
                        goto here;
                    }
                }
                MessageBox.Show("Invalid Login details!");
            here:
                con.Close();
            }
        }
    }
}
