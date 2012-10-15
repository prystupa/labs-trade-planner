using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TradePlannerWcfService
{
    interface ITradePlannerService
    {
        // given the current market conditions (netrual, momentum, or reversal)
        // generate trade profile curves, considering given filters
        // Used by: Trade Profile view
        TradeProfiles GetTradeProfiles(string instrumentID, string portfolioManagerID, Range averageDailyVolumeRange, string sector);

        // For a given order, given target completion time and aggression level, return trade schedule:
        // Trade schedule contains time series of percent competed AND expected final participation rate and P&L
        // Used by: Trade Projection view
        TradeSchedules GetTradeScedule(string orderID, DateTime targetCompletionTime, int aggressionLevel);
    }

    public class Range
    {
        public decimal start { get; set; }
        public decimal end { get; set; }
    }

    public class TradeProfile
    {
        public decimal price1D { get; set; }
        public decimal price2D { get; set; }
        public decimal price1W { get; set; }
        public decimal price1M { get; set; }
    }

    public class TradeProfiles
    {
        public TradeProfile inTheMoney { get; set; }
        public TradeProfile atTheMoney { get; set; }
        public TradeProfile awayFromTheMoney { get; set; }
    }

    public class TradeSchedules
    {
        public TradeSchedule normalLess1StdDev { get; set; }
        public TradeSchedule normal { get; set; }
        public TradeSchedule normalPlus1StdDev { get; set; }
    }

    public class TradeScheduleItem
    {
        public DateTime timestamp { get; set; }
        public decimal expectedPercentComplete { get; set; }
    }

    public class TradeSchedule
    {
        public List<TradeScheduleItem> timeSeries { get; set; }
        public decimal expectedParticipationRate { get; set; }
        public decimal expectedPnL { get; set; }
    }
}
