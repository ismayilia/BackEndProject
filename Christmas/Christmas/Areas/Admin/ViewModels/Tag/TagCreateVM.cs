using System.ComponentModel.DataAnnotations;

namespace Christmas.Areas.Admin.ViewModels.Tag
{
    public class TagCreateVM
    {
        [Required]
        public string Name { get; set; }
    }
}
