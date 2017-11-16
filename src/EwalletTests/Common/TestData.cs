using EwalletCommon.Enums;
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

            var transaction = new TransactionDTO()
            {
                Title = $"Title - {guild}",
                AddDate = DateTime.Now,
                Description = $"Description - {guild}",
            };

            if (categoryId > 0)
            {
                transaction.CategoryId = categoryId;
            }

            return transaction;
        }

        public static UserDTO GetUserData()
        {
            return new UserDTO()
            {
                Email = $"dawid.pfv@gmail.com - {DateTime.Now.Ticks}",
                IsActive = true,
                Password = "testPassword",
                Role = UserRole.Admin
            };
        }
    }
}
