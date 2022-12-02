using HealthCheckApp.Models;

namespace HealthCheckApp.Services.Email
{
    public class EMailSender : IEMailSender
    {
        public Task Send(EmailSenderModel model)
        {
            return Task.CompletedTask;
        }

    }
}
