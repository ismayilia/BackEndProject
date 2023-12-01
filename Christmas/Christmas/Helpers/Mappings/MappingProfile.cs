using AutoMapper;
using Christmas.Areas.Admin.ViewModels.About;
using Christmas.Areas.Admin.ViewModels.Advert;
using Christmas.Areas.Admin.ViewModels.Blog;
using Christmas.Areas.Admin.ViewModels.Contact;
using Christmas.Areas.Admin.ViewModels.Product;
using Christmas.Areas.Admin.ViewModels.Review;
using Christmas.Areas.Admin.ViewModels.Slider;
using Christmas.Areas.Admin.ViewModels.Tag;
using Christmas.Models;

namespace Christmas.Helpers.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Advert, AdvertVM>().ReverseMap();
            CreateMap<Review, ReviewVM>().ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Customer.FullName))
                                          .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Customer.Image));
            CreateMap<Blog, BlogVM>();/*.ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Images.FirstOrDefault(m => m.IsMain).Image));*/
            CreateMap<Tag, TagVM>();

            CreateMap<Product, ProductVM>().ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                                           .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Images.FirstOrDefault(m => m.IsMain).Image));
            CreateMap<About, AboutVM>();
            CreateMap<Team, TeamVM>();
            CreateMap<Brand, BrandVM>();
			CreateMap<ContactInfo, ContactVM>();
            CreateMap<Slider, SliderVM>();
            CreateMap<Slider, SliderCreateVM>().ReverseMap();
            CreateMap<Slider, SliderEditVM>().ReverseMap();
            CreateMap<Advert, AdvertCreateVM>().ReverseMap();
            CreateMap<Advert, AdvertEditVM>().ReverseMap();

        }
    }
}
