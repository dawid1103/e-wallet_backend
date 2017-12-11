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

    public class ScheduledTransactionDTO : TransactionDTO
    {
        /// <summary>
        /// Start day. When to add 1st transaction
        /// </summary>
        public DateTime StartDay { get; set; }

        /// <summary>
        /// Date of next create
        /// </summary>
        public DateTime NextCreateDay { get; set; }

        /// <summary>
        /// Repeat count
        /// </summary>
        public int RepeatCount { get; set; }

        /// <summary>
        /// Repeat mode
        /// Daily, Weekly, Monthly, HalfYearly, Yearly
        /// </summary>
        public RepeatMode RepeatMode { get; set; }

        /// <summary>
        /// True if transaction is completed
        /// </summary>
        public bool IsCompleted =>
            RepeatCount == 0;
    }
}
