using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EwalletService.BackgroundServices
{
    public class ScheduledTransactionService : BackgroundService
    {
        private readonly ILogger<ScheduledTransactionService> logger;

        public ScheduledTransactionService(ILogger<ScheduledTransactionService> logger)
        {
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                //await _randomStringProvider.UpdateString(cancellationToken);
                Console.WriteLine("Usługa w tle");
                logger.LogDebug($"Wykonałem zadanie w tle {nameof(this.GetType)}");
                await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
            }
        }
    }
}
