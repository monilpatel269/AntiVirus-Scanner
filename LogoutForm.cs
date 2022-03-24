using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AntiVirus_Scanner
{
    public partial class LogoutForm : Form
    {
        public LogoutForm()
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
        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (tbxUser.Text == "" && tbxPass.Text != "")
            {
                MessageBox.Show("Please enter Username");
            }
            else if (tbxUser.Text != "" && tbxPass.Text == "")
            {
                MessageBox.Show("please enter Password");
            }
            else if (tbxUser.Text == "" && tbxPass.Text == "")
            {
                MessageBox.Show("Please enter Username and Password");
            }
            else
            {
                MessageBox.Show("Logout Successfully!");
                tbxUser.Clear();
                tbxPass.Clear();
                btnLogout.Text = "Login";
                Form1.instance.btn.Text = "Login";
                Form1.instance.lbl.Text = "Hello User!"; 
                if (btnLogout.Text == "Login")
                {
                    loadform(new LoginForm());
                }
            }
        }
    }
}
