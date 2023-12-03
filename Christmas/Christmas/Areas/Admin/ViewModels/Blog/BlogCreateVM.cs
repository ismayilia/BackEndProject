using System.ComponentModel.DataAnnotations;

namespace Christmas.Areas.Admin.ViewModels.Blog
{
    public class BlogCreateVM
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Desc { get; set; }
        
        [Required]
        public List<IFormFile> Photos { get; set; }
    }
}
