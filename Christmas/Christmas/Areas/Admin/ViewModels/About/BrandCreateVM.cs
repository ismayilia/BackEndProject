using System.ComponentModel.DataAnnotations;

namespace Christmas.Areas.Admin.ViewModels.About
{
    public class BrandCreateVM
    {
        [Required]
        public List<IFormFile> Photos { get; set; }
    }
}
