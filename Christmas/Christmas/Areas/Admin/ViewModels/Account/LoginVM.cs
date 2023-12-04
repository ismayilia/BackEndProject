using System.ComponentModel.DataAnnotations;

namespace Christmas.Areas.Admin.ViewModels.Account
{
    public class LoginVM
    {
        [Required]
        public string EmailOrUsername { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
