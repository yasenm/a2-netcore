using A4CoreBlog.Data.Models;
using A4CoreBlog.Data.ViewModels;
using AutoMapper;

namespace A4CoreBlog.Data.Infrastructure.Mapping
{
    public static class AutoMapperConfig
    {
        public static void Init()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Blog, BasicBlogViewModel>().ReverseMap();
                cfg.CreateMap<Blog, BlogEditViewModel>().ReverseMap();

                cfg.CreateMap<Post, PostListBasicViewModel>().ReverseMap();
                cfg.CreateMap<Post, PostDetailsViewModel>().ReverseMap();
                cfg.CreateMap<Post, PostEditViewModel>().ReverseMap();

                cfg.CreateMap<User, UserBaseViewModel>()
                    .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName))
                    .ReverseMap();
                cfg.CreateMap<User, UserProfileViewModel>().ReverseMap();

                cfg.CreateMap<SystemImage, SystemImageCreateOrEditViewModel>().ReverseMap();
                cfg.CreateMap<SystemImage, SystemImageContentViewModel>().ReverseMap();
            });
        }
    }
}
