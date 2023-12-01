using System.ComponentModel.DataAnnotations;

namespace Christmas.Areas.Admin.ViewModels.About
{
    public class TeamCreateVM
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Position { get; set; }
        [Required]
        public IFormFile Photo { get; set; }
    }
}
