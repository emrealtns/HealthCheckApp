using Hangfire;
using HealthCheckApp.Data.Entities;
using HealthCheckApp.Data.Repository;
using HealthCheckApp.Services.Notification;
using HealthCheckApp.Services.UrlTrack;
using HealthCheckApp.Services.User;

namespace HealthCheckApp.Services.UrlTrackCheck
{
    public class UrlTrackCheckService : IUrlTrackCheckService
    {
        private const string CronMinuteExpression = "*/{0} * * * *";
        private const string EmailAddress = "a@b.com";

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly INotificationSenderFactory _notificationSenderFactory;
        private readonly IRepository<UrlHealthCheck> _healthCheckRepository;
        private readonly ILogger<UrlTrackCheckService> _logger;
        private readonly IRecurringJobManager _recurringJobManager;
        private readonly IUrlTrackService _trackLogService;
        private readonly IUserService _userService;


        public UrlTrackCheckService(IHttpClientFactory httpClientFactory,
                                     INotificationSenderFactory notificationSenderFactory,
                                     IRepository<UrlHealthCheck> healthCheckRepository,
                                     ILogger<UrlTrackCheckService> logger,
                                     IRecurringJobManager recurringJobManager,
                                     IUrlTrackService trackLogService,
                                     IUserService userService)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _notificationSenderFactory = notificationSenderFactory;
            _healthCheckRepository = healthCheckRepository;
            _recurringJobManager = recurringJobManager;
            _trackLogService = trackLogService;
            _userService = userService;
        }
     
        public async Task Run(CancellationToken cancellationToken = default)
        {
            var healthChecks = await _healthCheckRepository.GetAllAsync();

            foreach (var app in healthChecks)
            {
                var appId = app.Id.ToString();
                var expression = string.Format(CronMinuteExpression, app.TimeInterval);
                _recurringJobManager.RemoveIfExists(appId);
                _recurringJobManager.AddOrUpdate(appId, () => Check(app, cancellationToken), expression);
            }
        }

        public async Task Check(UrlHealthCheck urlhealthCheck, CancellationToken cancellationToken = default)
        {

            var track = new Data.Entities.UrlTrack
            {
                UrlHealthCheckId = urlhealthCheck.Id,
            };

            var emailSender = _notificationSenderFactory.CreateEmailSender();
            _logger.LogInformation($"Health Check({urlhealthCheck.Id}) was run again on {DateTime.Now:hh:mm:ss}");

            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var httpResponseMessage = await httpClient.GetAsync(urlhealthCheck.Url, cancellationToken);

                track.Code = (int)httpResponseMessage.StatusCode;
                track.CheckDate = DateTime.Now;

                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    var message = await httpResponseMessage.Content.ReadAsStringAsync(cancellationToken);
                    await emailSender.Send(new()
                    {
                        To = _userService.GetUserEMail(urlhealthCheck.UserId),
                        Body = $"StatusCode={httpResponseMessage.StatusCode}\nResponse:{message}"
                    });
                    _logger.LogError($"Health Check({urlhealthCheck.Id}) application not working.");
                }
            }
            catch (Exception ex)
            {
                track.Message = ex.Message;
                await emailSender.Send(new()
                {
                    To = _userService.GetUserEMail(urlhealthCheck.UserId),
                    Body = ex.Message
                });
                _logger.LogCritical($"Health Check = {urlhealthCheck.Id}.\n{ex.Message}", ex);
            }
            finally
            {
                await _trackLogService.Save(track);
                _logger.LogInformation($"New log added for AppId:{urlhealthCheck.Id}");
            }
        }
    }
}