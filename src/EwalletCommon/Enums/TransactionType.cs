using System.ComponentModel.DataAnnotations;

namespace EwalletCommon.Enums
{
    public enum TransactionType
    {
        [Display(Name = "Wydatek")]
        Expense,

        [Display(Name = "Przychód")]
        Income
    }
}