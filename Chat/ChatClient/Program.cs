using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatClient
{
    class Program
    {
        static string name;
        static int port = 9000;
        static IPAddress ip;
        static Socket soc;
        static Thread rec;


        static string GetIp()
        {
            string hostname = Dns.GetHostName();
            IPHostEntry ipentry = Dns.GetHostEntry(hostname);
            IPAddress[] addr = ipentry.AddressList;
            return addr[addr.Length - 1].ToString();
        }

        static void RecV()
        {
            while (true)
            {
                Thread.Sleep(500);
                byte[] buffer = new byte[300];
                int rece = soc.Receive(buffer, 0, buffer.Length, 0);
                Array.Resize(ref buffer, rece);
                Console.WriteLine(Encoding.Default.GetString(buffer));
            }
        }



        static void Main(string[] args)
        {
            rec = new Thread(RecV);
            Console.WriteLine("Please enter your name");
            name = Console.ReadLine();
            ip = IPAddress.Parse(GetIp());
            Console.WriteLine("Please enter HostPort");
            string prt = Console.ReadLine();

            try
            {
                port = Convert.ToInt32(prt);
            }
            catch
            {
                port = 9000;
            }

            soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            soc.Connect(new IPEndPoint(ip, port));
            rec.Start();
            byte[] data = Encoding.Default.GetBytes("<" + name + "> Connected");
            soc.Send(data, 0, data.Length, 0);

            while (soc.Connected)
            {
                byte[] sdata = Encoding.Default.GetBytes("<" + name + ">" + Console.ReadLine());
                soc.Send(sdata, 0, sdata.Length, 0);

            }

        }
    }
}
