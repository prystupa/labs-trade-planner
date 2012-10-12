using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace TradePlannerWcfService
{
    [ServiceContract(CallbackContract = typeof(IMarketDataServiceCallback))]
    interface IMarketDataService
    {
        void SubscribeMarketUpdates(string symbol, TimeSpan intervalSize);
    }


    interface IMarketDataServiceCallback
    {
        void OnMarketUpdate(MarketUpdate marketUpdate);
    }

    [DataContract]
    public class MarketUpdate
    {
        [DataMember]
        public DateTime Start { get; set; }

        [DataMember]
        public DateTime End { get; set; }

        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public decimal Volume { get; set; }
    }
}
