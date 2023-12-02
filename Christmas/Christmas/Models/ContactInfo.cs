using System.ComponentModel.DataAnnotations;

namespace Christmas.Models
{
	public class ContactInfo :BaseEntity
	{
        [Required]
        public string Desc { get; set; }
    }
}
