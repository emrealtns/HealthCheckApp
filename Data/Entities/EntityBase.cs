namespace HealthCheckApp.Data.Entities
{
    public class EntityBase : IEntityBase
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
