using HealthCheckApp.Data.Entities;
using System.Linq.Expressions;

namespace HealthCheckApp.Services.UrlTrack
{
    public interface IUrlTrackService
    {
        Task<IEnumerable<Data.Entities.UrlTrack>> GetUrlTracksAsync();
        Task Save(Data.Entities.UrlTrack track);
        IQueryable<Data.Entities.UrlTrack> Where(string userId);
        Task<IEnumerable<Data.Entities.UrlTrack>> GetUrlTracksWithHealthCheckAsync(params Expression<Func<Data.Entities.UrlTrack, object>>[] includes);
    }
}
