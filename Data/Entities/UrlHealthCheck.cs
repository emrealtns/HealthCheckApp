using System.ComponentModel.DataAnnotations.Schema;

namespace HealthCheckApp.Data.Entities
{
    public class UrlHealthCheck : EntityBase
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public int TimeInterval { get; set; }

        [ForeignKey("UserId")]
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
