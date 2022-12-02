using System.ComponentModel.DataAnnotations.Schema;

namespace HealthCheckApp.Data.Entities
{
    public class UrlTrack : EntityBase
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public DateTime CheckDate { get; set; }   

        [ForeignKey("UrlHealthCheckId")]
        public int UrlHealthCheckId { get; set; }
        public virtual UrlHealthCheck UrlHealthCheck { get; set; }
    }
}
