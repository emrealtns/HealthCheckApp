using HealthCheckApp.Models;
using HealthCheckApp.Services.Notification;

namespace HealthCheckApp.Services.Email
{
    public interface IEMailSender : INotificationService<EmailSenderModel> { }
}
