namespace Christmas.Models
{
    public class Customer : BaseEntity
    {
        public string FullName { get; set; }
        public string Image { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
