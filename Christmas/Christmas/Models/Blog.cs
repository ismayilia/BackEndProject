namespace Christmas.Models
{
    public class Blog : BaseEntity
    {
        public string Title { get; set; }
        public string Desc { get; set; }
        public List<BlogImage> Images { get; set; }
        public List<BlogTag> BlogTags { get; set; }
    }
}
