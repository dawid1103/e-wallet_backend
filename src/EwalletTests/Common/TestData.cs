using EwalletCommon.Endpoints;
using EwalletCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EwalletTests.Common
{
    public static class TestData
    {
        public static CategoryDTO GetCategoryData()
        {
            var category = new CategoryDTO()
            {
                Name = $"Category{DateTime.Now}"
            };

            return category;
        }

        public static TransactionDTO GetTransactionData(int categoryId = 0)
        {
            DateTime time = DateTime.Now;

            var transaction = new TransactionDTO()
            {
                Title = $"Title{time}",
                AddDate = time,
                Description = $"Description{time}",
            };

            if (categoryId > 0)
            {
                transaction.CategoryId = categoryId;
            }

            return transaction;
        }
    }
}
