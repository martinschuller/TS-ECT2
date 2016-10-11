using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TS_ECT2
{
    public partial class CheckListWindow : Form
    {
        Timer timer = new Timer();
        int test = 0;

        public CheckListWindow()
        {
            InitializeComponent();
            timer.Interval = 1000;
            timer.Start();
            timer.Tick += new EventHandler(TimerEventProcessor);
        }

        private void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            test += 1;
            label1.Text = test.ToString();
        }

        private void CheckListWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer.Stop();
        }
    }
}
