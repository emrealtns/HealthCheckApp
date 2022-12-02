using HealthCheckApp.Models;
using HealthCheckApp.Services.Email;

namespace HealthCheckApp.Services.Notification
{
    public class NotificationSenderFactory : INotificationSenderFactory
    {
        public INotificationService<EmailSenderModel> CreateEmailSender() => new EMailSender();
    }
}
