using HealthCheckApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace HealthCheckApp.Data.Context
{
    public class HealthCheckContext : IdentityDbContext<User>
    {
        public HealthCheckContext(DbContextOptions options) : base(options)
        { 
        }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UrlHealthCheck> HealthCheck { get; set; }
        public virtual DbSet<UrlTrack> HealthCheckLog { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}


