using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Christmas.Areas.Admin.ViewModels.Advert
{
    public class AdvertCreateVM
    {
        
        [Required]
        public string Desc { get; set; }
        [Required]
        public IFormFile Photo { get; set; }
    }
}
