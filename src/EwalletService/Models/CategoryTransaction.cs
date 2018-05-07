using EwalletCommon.Enums;

namespace EwalletService.Models
{
    public class CategoryTransaction
    {
        public string CategoryName { get; set; }
        public decimal Price { get; set; }
        public TransactionType Type { get; set; }
    }
}
