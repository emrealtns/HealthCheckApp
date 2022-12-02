using HealthCheckApp.Services.UrlTrackCheck;

namespace HealthCheckApp.Services.Hosted
{
    public class HealthCheckHostedService : BackgroundService, IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<HealthCheckHostedService> _logger;

        public HealthCheckHostedService(IServiceProvider serviceProvider, ILogger<HealthCheckHostedService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(HealthCheckHostedService)} is starting.");
            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"{DateTime.Now:hh:mm:ss} {nameof(HealthCheckHostedService)} is working.");
            using var scope = _serviceProvider.CreateScope();
            var urlHealthCheckService = scope.ServiceProvider.GetRequiredService<IUrlTrackCheckService>();
            await urlHealthCheckService.Run(stoppingToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(HealthCheckHostedService)} is stopping.");
            return base.StopAsync(cancellationToken);
        }
    }
}
