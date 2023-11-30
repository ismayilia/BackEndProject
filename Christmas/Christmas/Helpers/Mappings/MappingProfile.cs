using AutoMapper;
using Christmas.Models;
using Christmas.ViewModels.About;
using Christmas.ViewModels.Advert;
using Christmas.ViewModels.Blog;
using Christmas.ViewModels.Product;
using Christmas.ViewModels.Review;
using Christmas.ViewModels.Tag;

namespace Christmas.Helpers.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Advert, AdvertVM>();
            CreateMap<Review, ReviewVM>().ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Customer.FullName))
                                          .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Customer.Image));
            CreateMap<Blog, BlogVM>();/*.ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Images.FirstOrDefault(m => m.IsMain).Image));*/
            CreateMap<Tag, TagVM>();

            CreateMap<Product, ProductVM>().ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                                           .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Images.FirstOrDefault(m => m.IsMain).Image));
            CreateMap<About, AboutVM>();
            CreateMap<Team, TeamVM>();
            CreateMap<Brand, BrandVM>();

        }
    }
}
