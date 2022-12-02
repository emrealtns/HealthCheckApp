using HealthCheckApp.Data.Entities;

namespace HealthCheckApp.Services.UrlTrackCheck
{
    public interface IUrlTrackCheckService
    {
        Task Run(CancellationToken cancellationToken = default);
        Task Check(UrlHealthCheck healthCheck, CancellationToken cancellationToken = default);
    }
}
