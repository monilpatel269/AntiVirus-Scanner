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
    public partial class Form1 : Form
    {
        public static Form1 instance;
        public Panel pn;
        public Button btn;
        public Button btn1;
        public Label lbl;
        public Form1()
        {
            InitializeComponent();
            instance = this;
            lbl = lblName;
            pn = mainPanel;
            btn = btnLogin;
            btn1 = btnRegister;
            
        }
       public void loadform(object Form)
        {
            if (this.mainPanel.Controls.Count > 0)
                this.mainPanel.Controls.RemoveAt(0);
            Form f = Form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.mainPanel.Controls.Add(f);
            this.mainPanel.Tag = f;
            f.Show();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnStatus_Click(object sender, EventArgs e)
        {
            loadform(new StatusForm());
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            loadform(new ScanForm());
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (btnLogin.Text == "Login")
            {
                loadform(new LoginForm());
            }
            else
            {
                loadform(new LogoutForm());
            }
            
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            loadform(new RegisterForm());
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            loadform(new HomeForm());
        }
    }
}
