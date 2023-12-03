using System.ComponentModel.DataAnnotations;

namespace Christmas.Models
{
    public class Blog : BaseEntity
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Desc { get; set; }
        public List<BlogImage> Images { get; set; }
        public List<BlogTag> BlogTags { get; set; }
    }
}
