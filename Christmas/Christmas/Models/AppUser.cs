using Microsoft.AspNetCore.Identity;

namespace Christmas.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
