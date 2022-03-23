using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AntiVirus_Scanner
{
    public partial class ScanForm : Form
    {
        int viruses;
        string folder;
        public ScanForm()
        {
            InitializeComponent();
        }

        private void btnFullScan_Click(object sender, EventArgs e)
        {
            try
            {
                string[] search = Directory.GetFiles(@"D:\STUDY", "*.*");
                progressBar1.Maximum = search.Length;
                foreach (string item in search)
                {

                    StreamReader stream = new StreamReader(item);
                    string read = stream.ReadToEnd();
                    string[] virus = new string[] { "trojan", "virus", "hacker", "malware", "spyware", "freeware", "RAT" };
                    foreach (string st in virus)
                    {
                        if (Regex.IsMatch(read, st))
                        {

                            viruses += 1;
                            label4.Text = "Viruses Detected:" + viruses.ToString();
                            listBox1.Items.Add(item);
                        }
                        progressBar1.Increment(1);

                    }
                }
                MessageBox.Show("Virus Detected!");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                ErrorLogging(ex);
                ReadError();
            }

        }

        public static void ErrorLogging(Exception ex)
        {
            string strPath = @"D:\ASP.NET project\AntiVirus Scanner\Log\log.txt";
            if (!File.Exists(strPath))
            {
                File.Create(strPath).Dispose();
            }
            using (StreamWriter sw = File.AppendText(strPath))
            {
                sw.WriteLine("=============Error Logging ===========");
                sw.WriteLine("===========Start============= " + DateTime.Now);
                sw.WriteLine("Error Message: " + ex.Message);
                sw.WriteLine("Stack Trace: " + ex.StackTrace);
                sw.WriteLine("===========End============= " + DateTime.Now);

            }
        }
        public static void ReadError()
        {
            string strPath = @"D:\ASP.NET project\AntiVirus Scanner\Log\log.txt";
            using (StreamReader sr = new StreamReader(strPath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
        }


        private void btnFullDel_Click(object sender, EventArgs e)
        {
            try
            {
                string removex = listBox1.Text;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                File.Delete(this.listBox1.Text);
                listBox1.Items.Remove(listBox1.SelectedItem);
                MessageBox.Show("Selected Virus file is deleted!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ErrorLogging(ex);
                ReadError();
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                folder = folderBrowserDialog1.SelectedPath;
                label7.Text += folder;
            }
            viruses = 0;
            label8.Text = "Viruses Detected:" + viruses.ToString();
            progressBar2.Value = 0;
            listBox1.Items.Clear();
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            try
            {
                string[] search = Directory.GetFiles(folderBrowserDialog1.SelectedPath, "*.*");
                progressBar2.Maximum = search.Length;
                foreach (string item in search)
                {

                    StreamReader stream = new StreamReader(item);
                    string read = stream.ReadToEnd();
                    string[] virus = new string[] { "trojan", "virus", "hacker", "malware", "spyware", "freeware", "RAT" };
                    foreach (string st in virus)
                    {
                        if (Regex.IsMatch(read, st))
                        {

                            viruses += 1;
                            label8.Text = "Viruses Detected:" + viruses.ToString();
                            listBox1.Items.Add(item);
                        }
                        progressBar2.Increment(1);

                    }
                }
                MessageBox.Show("Virus Detected!");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                ErrorLogging(ex);
                ReadError();
            }
        }


        private void btnScanDel_Click(object sender, EventArgs e)
        {
            try
            {
                string removex = listBox1.Text;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                File.Delete(this.listBox1.Text);
                listBox1.Items.Remove(listBox1.SelectedItem);
                MessageBox.Show("Selected Virus file is deleted!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ErrorLogging(ex);
                ReadError();
            }
        }

        private void ScanForm_Load(object sender, EventArgs e)
        {

        }

    }
}

