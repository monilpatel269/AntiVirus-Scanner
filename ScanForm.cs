using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
        public static string GetMD5Checksum(string filename)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLower();
                }
            }
        }

        private void btnFullScan_Click(object sender, EventArgs e)
        {
            try
            {
                string[] search = Directory.GetFiles(@"D:\ASP.NET project\testfolder", "*.*", SearchOption.AllDirectories);
                progressBar1.Maximum = search.Length;
                foreach (string item in search)
                {

                    List<string> mdList1 = new List<string>();
                    StreamReader stream = new StreamReader(item);
                    string read = GetMD5Checksum(item);
                    mdList1.Add(read);
                    var virus = File.ReadAllLines("MD5.txt");
                    foreach (string md in mdList1)
                    { 
                        foreach (string st in virus)
                        {
                            if (Regex.IsMatch(md, st))
                            {
                                viruses += 1;
                                label4.Text = "Viruses Detected:" + viruses.ToString();
                                listBox1.Items.Add(item);
                            }
                            progressBar1.Increment(1);

                        }
                    }
                }
                label9.Text = "Scanning Completed.";
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
                string[] search = Directory.GetFiles(folderBrowserDialog1.SelectedPath, "*.*", SearchOption.AllDirectories);
                progressBar2.Maximum = search.Length;
                foreach (string item in search)
                {
                    List<string> mdList = new List<string>();
                    StreamReader stream = new StreamReader(item);
                    string read = GetMD5Checksum(item);
                    mdList.Add(read);

                    var virus = File.ReadAllLines("MD5.txt");

                    foreach (string md in mdList)
                    {
                        foreach (string st in virus)
                        {
                            if (Regex.IsMatch(md, st))
                            {
                                viruses += 1;
                                label8.Text = "Viruses Detected:" + viruses.ToString();
                                listBox1.Items.Add(item);
                            }
                            progressBar2.Increment(1);
                        }
                    }
                }
                label10.Text = "Scanning Completed.";
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

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar2.Value = e.ProgressPercentage;
        }
    }
}

