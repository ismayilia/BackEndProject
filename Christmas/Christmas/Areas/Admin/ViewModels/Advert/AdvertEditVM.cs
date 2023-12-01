﻿using System.ComponentModel.DataAnnotations;

namespace Christmas.Areas.Admin.ViewModels.Advert
{
    public class AdvertEditVM
    {
        public int Id { get; set; }
        [Required]
        public string Desc { get; set; }
        public string Image { get; set; }
        public IFormFile Photo { get; set; }
    }
}
