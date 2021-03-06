﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WcfClient
{
    
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            SubscriberHandler subscribeClient2 = new SubscriberHandler(Proxy.NotifyMode.FiveOverOnce, "suresh", listBox2);
        }

        private void btnOver_Click(object sender, EventArgs e)
        {
            SubscriberHandler subscribeClient3 = new SubscriberHandler(Proxy.NotifyMode.FiftyRunsOnce, "Ram", listBox3);

        }

        private void btnWicket_Click(object sender, EventArgs e)
        {
            SubscriberHandler subscribeclient1 = new SubscriberHandler(Proxy.NotifyMode.Wicket, "Rajesh", listBox1);
        }
    }

    class SubscriberHandler : Proxy.ICricketServiceCallback
    {
        ListBox lstobj = null;

        public SubscriberHandler(Proxy.NotifyMode mode, string name, ListBox obj)
        {
            Proxy.CricketServiceClient client = new Proxy.CricketServiceClient(new System.ServiceModel.InstanceContext(this));
            lstobj = obj;
            bool status = client.GetNotification(mode, name);
            MessageBox.Show("Cricket Notification Activated in " + mode.ToString() + " Mode : " + status);
        }

        public void AsyncNotifyMessage(string message)
        {
            lstobj.Items.Add(message);
        }
    }
}
