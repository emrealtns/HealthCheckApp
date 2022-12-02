using HealthCheckApp.Models;

namespace HealthCheckApp.Services.Notification
{
    public interface INotificationSenderFactory
    {
        INotificationService<EmailSenderModel> CreateEmailSender();
    }
}
