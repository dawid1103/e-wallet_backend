using System;

namespace EwalletCommon.Models
{
    public enum RepeatMode
    {
        Daily,
        Weekly,
        Monthly,
        HalfYearly,
        Yearly
    }

    class ScheduledTransactionDTO : TransactionDTO
    {
        public DateTime StartDay { get; set; }
        public RepeatMode RepeatMode { get; set; }
    }
}
