using Christmas.Models;
using System.ComponentModel.DataAnnotations;

namespace Christmas.Areas.Admin.ViewModels.Blog
{
    public class BlogEditVM
    {
		public int Id { get; set; }
		[Required]
		public string Title { get; set; }
		[Required]
		public string Desc { get; set; }
		public List<BlogImage> Images { get; set; }
		public List<IFormFile> Photos { get; set; }
	}
}
