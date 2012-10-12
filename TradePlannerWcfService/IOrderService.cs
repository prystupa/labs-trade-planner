using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace TradePlannerWcfService
{
    [ServiceContract(CallbackContract = typeof(IOrderServiceCallback))]
    interface IOrderService
    {
        void StartOrder(string orderID);

        void SuspendOrder(string orderID);

        void ResumeOrder(string orderID);

        // Get the snapshot of all the fills from the beginning of the order and continue updating as new fills occur
        void SubscribeFills(string orderID);
    }

    interface IOrderServiceCallback
    {
        void OnOrderFill(OrderFill fill);
    }

    [DataContract]
    public class OrderFill
    {
        public string FillID { get; set; }

        public DateTime Timestamp { get; set; }

        public decimal Price { get; set; }

        public decimal Quantity { get; set; }
    }
}
