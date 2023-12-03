using System.ComponentModel.DataAnnotations;

namespace Christmas.Models
{
    public class Tag : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public List<BlogTag> BlogTags { get; set; }
    }
}
