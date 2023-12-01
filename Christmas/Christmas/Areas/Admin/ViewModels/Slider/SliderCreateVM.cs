using System.ComponentModel.DataAnnotations;

namespace Christmas.Areas.Admin.ViewModels.Slider
{
    public class SliderCreateVM
    {
        [Required]
        public IFormFile Photo { get; set; }
        [Required]
        public string Discount { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Desc { get; set; }
    }
}
