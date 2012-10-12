using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace TradePlannerWcfService
{
    [ServiceContract(CallbackContract = typeof(IBlotterServiceCallback))]
    public interface IBlotterService
    {
        // Subscribe to active orders given optional filters
        [OperationContract]
        void SubscribeSingleOrders(int? deskID, int? traderId, int? sectorId);

        // Subscribe to active baskets given optional filters
        [OperationContract]
        void SubscribeBaskets(int? deskID, int? traderId, int? sectorId);

        // Subscribe to all orders for a given basket
        [OperationContract]
        void SubscribeBasketOrders(string basketID);

        // Subscribe for order updates given specific order IDs (i.e. Watch List)
        [OperationContract]
        void SubscribeOrders(IEnumerable<string> orderIDs);
    }

    public interface IBlotterServiceCallback
    {
        // Callback for when an order update occurs
        void OnOrderUpdate(Order order);

        // Callback when a basket level update occurs
        void OnBasketUpdate(Basket basket);
    }

    [DataContract]
    public class Order
    {
        #region Order level - static
        
        public string OrderID { get; set; }

        // when has it started executing
        public DateTime? Submitted;

        // Time when algo predicted the order will complete
        public DateTime? ScheduledCompletion;

        public Side Side { get; set; }

        public long Size { get; set; }

        public string Desk { get; set; }

        public string PortfolioManager { get; set; }
        
        #endregion

        #region Order level - execution - current state

        public int AggressionLevel { get; set; }

        // current order status
        public OrderStatus Status { get; set; }

        // current filled percentatge
        public decimal PercentFilled { get; set; }

        // current average price paid
        public decimal AvgPrice { get; set; }

        // current slippage
        public decimal Slippage { get; set; }

        // current participation rate
        public decimal Participation { get; set; }

        // current P&L for an order
        public decimal PnL { get; set; }

        // current distribution of fill per pool
        public IDictionary<string, decimal> fillPercentageByPool;

        #endregion

        #region Order level - expected

        // currently predicted order completion time given actual execution and market conditions
        public decimal ExpectedCompletionTime { get; set; }

        // currently predicted participation rate given actual execution and market conditions
        public decimal ExpectedParticipationRate { get; set; }

        #endregion

        #region Order level - relative to the instrumetn (market)

        // percent available liquidity (remaining order size relative to estimated remaining liquidity in the market)
        public decimal PAL { get; set; }

        #endregion

        #region Instrument level - display attributes and dimentions

        public string Symbol { get; set; }

        public string Sector { get; set; }

        #endregion
    }

    [DataContract]
    public class Basket
    {
        public string BasketID { get; set; }

        public int NumberOfOrders { get; set; }

        // most of the same fields that also apply to Order
    }

    public enum Side { Buy, Sell }
    public enum OrderStatus { Unstarted, Running, Paused, Done }
}
