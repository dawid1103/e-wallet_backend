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

        public ScheduledTransactionDTO(TransactionDTO transaction, DateTime startDay, RepeatMode repeatMode, int repeatCount)
        {
            Title = transaction.Title;
            Price = transaction.Price;
            Description = transaction.Description;
            AddDate = transaction.AddDate;
            CategoryId = transaction.CategoryId;
            UserId = transaction.UserId;
            RepeatDay = startDay.Date;
            RepeatMode = repeatMode;
            RepeatCount = repeatCount;
        }
    }
}
