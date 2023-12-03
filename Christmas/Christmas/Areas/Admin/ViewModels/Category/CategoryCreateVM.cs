using System.ComponentModel.DataAnnotations;

namespace Christmas.Areas.Admin.ViewModels.Category
{
    public class CategoryCreateVM
    {
        [Required]
        public string Name { get; set; }
    }
}
