using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AntiVirus_Scanner
{
    public partial class StatusForm : Form
    {
        private static System.Timers.Timer _timer;
        public StatusForm()
        {
            InitializeComponent();
            _timer = new System.Timers.Timer();
            _timer.Interval = 3000;
            _timer.Elapsed += OntimedEvent;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }
        private  void OntimedEvent(object source, System.Timers.ElapsedEventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;

            int cpuValue = GetCpuValue();
            int memValue = GetMemValue();
           
            if (prCPU.InvokeRequired)
            {
                prCPU.Invoke(new Action(() => prCPU.Value = cpuValue));
            }
            else
            {
                prCPU.Value = cpuValue;
            }
            
            if (lblCPUp.InvokeRequired)
            {
                lblCPUp.Invoke(new Action(() => lblCPUp.Text = cpuValue.ToString() + "%"  ));
            }
            else
            {
                lblCPUp.Text = cpuValue.ToString() + "%";
            }

            if (prRam.InvokeRequired)
            {
                prRam.Invoke(new Action(() => prRam.Value = memValue));
            }
            else
            {
                prRam.Value = memValue ;
            }
           
            if (lblRamp.InvokeRequired)
            {
                lblRamp.Invoke(new Action(() => lblRamp.Text = memValue.ToString() + "%"));
            }
            else
            {
                lblRamp.Text = memValue.ToString() + "%";
            }

            prCPU.Value = cpuValue;
            prRam.Value = memValue;
        }
        private  int GetCpuValue()
        {
            var CpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            CpuCounter.NextValue();
            System.Threading.Thread.Sleep(1000);
            int returnvalue = (int)CpuCounter.NextValue();
            return returnvalue;
        }
        private  int GetMemValue()
        {
            var MemCounter = new PerformanceCounter("Memory", "% Committed Bytes in Use");
            int returnvalue = (int)MemCounter.NextValue();
            return returnvalue;
        }

        private void StatusForm_Load(object sender, EventArgs e)
        {

        }

        private void lblCPUp_Click(object sender, EventArgs e)
        {

        }
    }
}
