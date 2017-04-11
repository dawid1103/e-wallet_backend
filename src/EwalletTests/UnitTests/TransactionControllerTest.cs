﻿using EwalletCommon.Endpoints;
using EwalletCommon.Models;
using EwalletTests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EwalletTests.UnitTests
{
    [Collection(nameof(EwalletService))]
    public class TransactionControllerTest : TestBase
    {
        public TransactionControllerTest() : base() { }

        [Fact]
        public async void CreateWithoutCategory()
        {
            TransactionDTO transaction = TestData.GetTransactionData();
            transaction.Id = await _ewalletService.Transaction.CreateAsync(transaction);

            Assert.NotNull(transaction.Id);
        }

        [Fact]
        public async void CreateWithCategory()
        {
            CategoryDTO category = TestData.GetCategoryData();
            int categoryId = await _ewalletService.Category.CreateAsync(category);

            TransactionDTO transaction = TestData.GetTransactionData(categoryId);
            transaction.Id = await _ewalletService.Transaction.CreateAsync(transaction);

            Assert.NotNull(transaction.Id);
            Assert.NotNull(transaction.CategoryId);
        }

        [Fact]
        public async void GetSingle()
        {
            TransactionDTO transaction = TestData.GetTransactionData();
            transaction.Id = await _ewalletService.Transaction.CreateAsync(transaction);

            Assert.NotNull(transaction.Id);

            TransactionDTO fromDatabase = await _ewalletService.Transaction.GetAsync(transaction.Id);

            Assert.NotNull(fromDatabase);
            Assert.Equal(transaction.Id, fromDatabase.Id);
            Assert.Equal(transaction.Title, fromDatabase.Title);
            Assert.Equal(transaction.Description, fromDatabase.Description);
            Assert.Equal(transaction.CategoryId, fromDatabase.CategoryId);
            Assert.True(transaction.AddDate <= fromDatabase.AddDate);
        }

        [Fact]
        public async void GetAll()
        {
            TransactionDTO transaction = TestData.GetTransactionData();

            await _ewalletService.Transaction.CreateAsync(transaction);
            await _ewalletService.Transaction.CreateAsync(transaction);

            IEnumerable<TransactionDTO> transactions = await _ewalletService.Transaction.GetAllAsync();

            Assert.NotEmpty(transactions);
            Assert.True(transactions.Count() > 1);
        }

        [Fact]
        public async void Delete()
        {
            TransactionDTO transaction = TestData.GetTransactionData();
            int id = await _ewalletService.Transaction.CreateAsync(transaction);
            Assert.NotNull(id);

            await _ewalletService.Transaction.DeleteAsync(id);
        }

        [Fact]
        public async void Update()
        {
            CategoryDTO category = TestData.GetCategoryData();
            int categoryId = await _ewalletService.Category.CreateAsync(category);

            TransactionDTO transaction = TestData.GetTransactionData(categoryId);
            transaction.Id = await _ewalletService.Transaction.CreateAsync(transaction);
            TransactionDTO fromDatabase = await _ewalletService.Transaction.GetAsync(transaction.Id);

            string changedTitle = $"Test changed title {DateTime.Now.Ticks}";
            string changedDesc = $"Test changed description {DateTime.Now.Ticks}";
            DateTime time = DateTime.Now;

            fromDatabase.Title = changedTitle;
            fromDatabase.Description = changedDesc;
            fromDatabase.AddDate = time;

            await _ewalletService.Transaction.UpdateAsync(fromDatabase);
            fromDatabase = await _ewalletService.Transaction.GetAsync(transaction.Id);

            Assert.Equal(fromDatabase.Title, changedTitle);
            Assert.Equal(fromDatabase.Description, changedDesc);
        }
    }
}
