using Microsoft.AspNetCore.Http;
using System;

namespace EwalletCommon.Models
{
    public class TransactionDTO
    {
        /// <summary>
        /// Transaction id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Transaction title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Transaction price
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Transaction date
        /// </summary>
        public DateTime AddDate { get; set; }

        /// <summary>
        /// Transaction description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Transaction category id
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// User id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Path to image
        /// </summary>
        public string FilePath { get; set; }
    }
}
