using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Trimfit.Data
{
    public class QueueService : BackgroundService
    {

        private readonly ILogger<QueueService> _logger;
        private readonly IApiContext _apiContext;

        public QueueService(IApiContext context,
                ILogger<QueueService> logger)
        {
            _logger = logger;
            _apiContext = context;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug($"GracePeriodManagerService is starting.");

            stoppingToken.Register(() =>
                _logger.LogDebug($" GracePeriod background task is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogDebug($"GracePeriod task doing background work.");

                var res = await _apiContext.GetRequest("Queue");


                await Task.Delay(4000, stoppingToken);
            }

            _logger.LogDebug($"GracePeriod background task is stopping.");
        }

    }
}
