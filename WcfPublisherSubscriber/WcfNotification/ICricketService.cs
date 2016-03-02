using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfNotification
{
    public enum NotifyMode
    {
        Wicket,
        Over,
        FiveOverOnce,
        Runs,
        FiftyRunsOnce
    }

    [ServiceContract(CallbackContract = typeof(IClientCallback))]
    interface ICricketService
    {
        [OperationContract]
        bool GetNotification(NotifyMode mode, string username);

        [OperationContract(IsOneWay = true)]
        void GetNotify(NotifyMode mode, string username);
    }

    public interface IClientCallback
    {
        [OperationContract(IsOneWay = true)]
        void AsyncNotifyMessage(string message);
    }
}
