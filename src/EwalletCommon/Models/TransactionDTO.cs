using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        /// Transaction date
        /// </summary>
        public string AddDate { get; set; }

        /// <summary>
        /// Transaction description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Transaction category
        /// </summary>
        public CategoryDTO Category { get; set; }
    }
}
