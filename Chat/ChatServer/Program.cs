using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatServer
{
    class Program
    {
        static Socket soc;
        static Socket acc;
        static int port = 9000;
        static IPAddress ip;
        static Thread rec;
        static string name;

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
                int rece = acc.Receive(buffer, 0, buffer.Length, 0);
                Array.Resize(ref buffer, rece);
                Console.WriteLine(Encoding.Default.GetString(buffer));
            }
        }



        static void Main(string[] args)
        {
            rec = new Thread(RecV);
            Console.WriteLine("Your Local Ip is " + GetIp());
            Console.WriteLine("Please enter your name");
            name = Console.ReadLine();
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

            ip = IPAddress.Parse(GetIp());
            soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            soc.Bind(new IPEndPoint(ip, port));
            soc.Listen(0);
            acc = soc.Accept();
            rec.Start();
            while (true)
            {
                byte[] sdata = Encoding.Default.GetBytes("<" + name + ">" + Console.ReadLine());
                acc.Send(sdata, 0, sdata.Length, 0);
            }


        }
    }
}
