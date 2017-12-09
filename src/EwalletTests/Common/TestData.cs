using EwalletCommon.Models;
using System;

namespace EwalletTests.Common
{
    public static class TestData
    {
        public static CategoryDTO GetCategoryData()
        {
            var category = new CategoryDTO()
            {
                Name = $"Category - {Guid.NewGuid()}"
            };

            return category;
        }

        public static TransactionDTO GetTransactionData(int categoryId = 0)
        {
            Guid guild = Guid.NewGuid();
            Random rnd = new Random();

            var transaction = new TransactionDTO()
            {
                Title = $"Title - {guild}",
                AddDate = DateTime.Now,
                Description = $"Description - {guild}",
                Price = decimal.Round((decimal)(rnd.Next(1, 999999999) + rnd.NextDouble()), 2, MidpointRounding.AwayFromZero)
            };

            if (categoryId > 0)
            {
                transaction.CategoryId = categoryId;
            }

            return transaction;
        }

        public static UserRegistrationDataDTO GetUserRegistrationData()
        {
            return new UserRegistrationDataDTO()
            {
                Email = $"dawid.pfv@gmail.com - {DateTime.Now.Ticks}",
                Password = "testPassword",
            };
        }
    }
}
