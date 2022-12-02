using Microsoft.AspNetCore.Identity;

namespace HealthCheckApp.Data.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<UrlHealthCheck> UrlHealthChecks { get; set; }
    }
}