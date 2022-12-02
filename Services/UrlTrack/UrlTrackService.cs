using HealthCheckApp.Data.Entities;
using HealthCheckApp.Data.Repository;
using System.Linq.Expressions;

namespace HealthCheckApp.Services.UrlTrack
{
    public class UrlTrackService : IUrlTrackService
    {
        protected readonly IRepository<Data.Entities.UrlTrack> _repository;
        public UrlTrackService(IRepository<Data.Entities.UrlTrack> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Data.Entities.UrlTrack>> GetUrlTracksAsync() => await _repository.GetAllAsync();

        public async Task<IEnumerable<Data.Entities.UrlTrack>> GetUrlTracksWithHealthCheckAsync(params Expression<Func<Data.Entities.UrlTrack, object>>[] includes)
        {
            return await _repository.GetAllAsync(x => x.UrlHealthCheck).ConfigureAwait(false);
        }
        public async Task Save(Data.Entities.UrlTrack track) => await _repository.AddAsync(track);

        public IQueryable<Data.Entities.UrlTrack> Where(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
