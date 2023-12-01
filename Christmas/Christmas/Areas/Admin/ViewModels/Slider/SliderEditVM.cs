using System.ComponentModel.DataAnnotations;

namespace Christmas.Areas.Admin.ViewModels.Slider
{
    public class SliderEditVM
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Discount { get; set; }
        [Required]
        public string Desc { get; set; }
        public string Image { get; set; }
        public IFormFile Photo { get; set; }
    }
}
