using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfNotification
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class CricketService : ICricketService
    {
        private static int _wicket = 0;
        private static int _run = 0;
        private static double _over = 0;
        static Random rand = new Random();

        static int Wicket
        {
            set
            {
                if (!value.Equals(_wicket))
                {
                    _wicket = value;
                    SendNotifyToClient(NotifyMode.Wicket);
                }
            }
            get { return _wicket; }
        }

        static int Run
        {
            set
            {

                if (!value.Equals(_run))
                {
                    _run = value;
                    if (_run % 50 == 0)
                    {
                        SendNotifyToClient(NotifyMode.FiftyRunsOnce);
                    }

                }
            }
            get { return _run; }
        }

        static double Over
        {
            set
            {
                if (!value.Equals(_over))
                {
                    _over = value;

                    int whole = Convert.ToInt32(Math.Truncate(_over));

                    if (_over - whole >= 0.6)
                    {
                        _over = Math.Ceiling(_over);
                    }

                    if (_over % 5 == 0)
                    {
                        SendNotifyToClient(NotifyMode.FiveOverOnce);
                    }
                }
            }
            get { return _over; }
        }

        public class CricketData
        {
            public OperationContext CricketContext { set; get; }

            public NotifyMode Mode { set; get; }

            public string UserName { set; get; }

        }

        static List<CricketData> notifylist = null;

        public CricketService()
        {
            notifylist = new List<CricketData>();
            System.Threading.Thread th = new System.Threading.Thread(Publisher);
            th.Start();
        }

        public bool GetNotification(NotifyMode mode, string username)
        {
            try
            {
                if (mode == NotifyMode.Wicket || mode == NotifyMode.FiveOverOnce || mode == NotifyMode.FiftyRunsOnce)
                {
                    notifylist.Add(new CricketData()
                    {
                        CricketContext = OperationContext.Current,
                        Mode = mode,
                        UserName = username
                    });
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public void GetNotify(NotifyMode mode, string username)
        {
            try
            {
                if (mode == NotifyMode.Wicket || mode == NotifyMode.FiveOverOnce || mode == NotifyMode.FiftyRunsOnce)
                {
                    notifylist.Add(new CricketData()
                    {
                        CricketContext = OperationContext.Current,
                        Mode = mode,
                        UserName = username
                    });
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        void Publisher()
        {
            DateTime start = DateTime.Now;
            DateTime Modified = DateTime.Now;

            DateTime wt = DateTime.Now.AddMinutes(NotifySecond(NotifyMode.Wicket));

            while (true)
            {
                int now = DateTime.Now.Minute;

                if (Wicket <= 10)
                    if (wt.Minute == now)
                    {
                        Wicket++;
                        wt = wt.AddMinutes(NotifySecond(NotifyMode.Wicket));
                    }
                    else
                    {
                        if ((DateTime.Now - Modified).Seconds > 6)
                        {
                            Over += 0.1;
                            Run += 1;
                            Modified = DateTime.Now;
                        }
                    }

                if (Wicket > 10)
                    break;
            }
        }

        private static void SendNotifyToClient(NotifyMode mode)
        {
            for (int i = 0; i < notifylist.Count; i++)
            {
                CricketData data = notifylist[i];
                if (data.Mode == mode)
                {
                    Send(data);
                }
            }
        }

        private static void Send(CricketData data)
        {
            int f = NotifySecond(data.Mode);
            IClientCallback client =
                       data.CricketContext.GetCallbackChannel<IClientCallback>();
            String message = string.Format("{1}/{0} : Runs/Wickets {2} Overs", Wicket,
                       Run, Over);
            try
            {
                client.AsyncNotifyMessage(message);
            }
            catch
            {

            }
        }

        static int NotifySecond(NotifyMode mode)
        {
            if (mode == NotifyMode.Wicket)
            {
                return 1;
            }
            else if (mode == NotifyMode.FiveOverOnce)
            {
                return 2;
            }
            else if (mode == NotifyMode.FiftyRunsOnce)
            {
                return 3;
            }
            return 0;
        }

    }
  
}
