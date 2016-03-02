using System;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SubscriberHandler subscribeclient1 = new SubscriberHandler(Proxy.NotifyMode.Wicket, "Rajesh", listBox1);
        }

       

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            SubscriberHandler subscribeClient2 = new SubscriberHandler(Proxy.NotifyMode.FiveOverOnce, "suresh", listBox2);
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            SubscriberHandler subscribeClient3 = new SubscriberHandler(Proxy.NotifyMode.FiftyRunsOnce, "Ram", listBox3);

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
