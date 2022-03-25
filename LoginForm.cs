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
        bool startflag = false;
        public LoginForm()
        {
            InitializeComponent();
        }
        public void loadform(object Form)
        {
            if (Form1.instance.pn.Controls.Count > 0)
                Form1.instance.pn.Controls.RemoveAt(0);
            Form f = Form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            Form1.instance.pn.Controls.Add(f);
            Form1.instance.pn.Tag = f;
            f.Show();
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
                if (startflag == false)
                {
                    startflag = true;
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
                        if (user == tbUser.Text && password == tbpass.Text)
                        {
                            MessageBox.Show("Successfully Login");
                            Form1.instance.lbl.Text = "Hello " + tbUser.Text;
                            Form1.instance.btn.Text = "Logout";
                            Form1.instance.btn1.Visible = false;
                            tbUser.Clear();
                            tbpass.Clear();
                            goto here;
                        }
                    }
                    MessageBox.Show("Invalid Login details!");
                here:
                    btnLogin.Text = "Logout";
                    if (btnLogin.Text == "Logout")
                    {
                        loadform(new HomeForm());
                    }
                    
                }
                else
                {
                    startflag = false;
                    btnLogin.Text = "Login";
                }
                
            }
        }
    }
}
