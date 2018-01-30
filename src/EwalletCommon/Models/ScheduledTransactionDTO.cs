using System;
using System.ComponentModel.DataAnnotations;

namespace EwalletCommon.Models
{
    public enum RepeatMode
    {
        [Display(Name = "Codziennie")]
        Daily,

        [Display(Name = "Co tydzień")]
        Weekly,

        [Display(Name = "Co miesiąc")]
        Monthly,

        [Display(Name = "Co pół roku")]
        HalfYearly,

        [Display(Name = "Co rok")]
        Yearly
    }

    public class ScheduledTransactionDTO : TransactionDTO
    {
        /// <summary>
        /// Start day. When to add 1st transaction
        /// </summary>
        public DateTime RepeatDay { get; set; }

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

        public ScheduledTransactionDTO()
        {
        }

        public ScheduledTransactionDTO(TransactionDTO transaction, DateTime startDay, RepeatMode repeatMode, int repeatCount) : base(transaction)
        {
            RepeatDay = startDay.Date;
            RepeatMode = repeatMode;
            RepeatCount = repeatCount;
        }
    }
}
