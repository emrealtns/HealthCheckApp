namespace HealthCheckApp.Services.Notification
{
    public interface INotificationService<TRequestModel>
    {
        Task Send(TRequestModel model);
    }
}
