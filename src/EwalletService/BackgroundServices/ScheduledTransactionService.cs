using EwalletCommon.Models;
using EwalletService.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EwalletService.BackgroundServices
{
    public class ScheduledTransactionService : BackgroundService
    {
        private readonly ILogger<ScheduledTransactionService> logger;
        private readonly IScheduledTransactionRepository scheduledTransactionRepository;
        private readonly ITransactionRepository transactionRepository;
        private readonly int serviceInterval = 0;

        public ScheduledTransactionService(IServiceProvider provider, IConfigurationSection settings)
        {
            this.logger = provider.GetRequiredService<ILogger<ScheduledTransactionService>>(); ;
            this.scheduledTransactionRepository = provider.GetRequiredService<IScheduledTransactionRepository>(); ;
            this.transactionRepository = provider.GetRequiredService<ITransactionRepository>();
            this.serviceInterval = int.Parse(settings["Interval"]);

            this.logger.LogInformation("Service interval: every {0} hours.", this.serviceInterval);
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await ManageTransactions();
                await Task.Delay(TimeSpan.FromHours(serviceInterval), cancellationToken);
            }
        }

        private async Task ManageTransactions()
        {
            IEnumerable<ScheduledTransactionDTO> transactions = await scheduledTransactionRepository.GetAllIncomingAsync(DateTime.Now.Date);
            logger.LogInformation($"Scheduled transactions count to process: {transactions.Count()}");

            foreach (var transaction in transactions)
            {
                await transactionRepository.CreateAsync(transaction);
                await ScheduleNextInterval(transaction.Id, transaction.RepeatCount - 1, transaction.RepeatMode);
            }
        }

        private async Task ScheduleNextInterval(int id, int repeatCount, RepeatMode repeatMode)
        {
            DateTime nextCreationDay = DateTime.Now.Date;

            switch (repeatMode)
            {
                case RepeatMode.Daily:
                    nextCreationDay = DateTime.Now.Date.AddDays(1);
                    break;
                case RepeatMode.Weekly:
                    nextCreationDay = DateTime.Now.Date.AddDays(7);
                    break;
                case RepeatMode.Monthly:
                    nextCreationDay = DateTime.Now.Date.AddMonths(1);
                    break;
                case RepeatMode.HalfYearly:
                    nextCreationDay = DateTime.Now.Date.AddMonths(6);
                    break;
                case RepeatMode.Yearly:
                    nextCreationDay = DateTime.Now.Date.AddYears(1);
                    break;
            }

            await scheduledTransactionRepository.SetNextIntervalAsync(id, nextCreationDay, repeatCount);
            logger.LogInformation($"Transaction from scheduled transaction (id: {id}) created.");

            if (repeatCount <= 0)
            {
                logger.LogInformation($"Scheduled transaction finished.");
            }
            else
            {
                logger.LogInformation($"Next creation date: {nextCreationDay}. Repeat count left: {repeatCount}");
            }
        }
    }
}
