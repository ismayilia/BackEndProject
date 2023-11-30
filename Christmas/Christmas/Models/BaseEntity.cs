namespace Christmas.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public bool SoftDeleted { get; set; } = false;
        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}
