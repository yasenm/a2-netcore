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
                cfg.CreateMap<Blog, BasicBlogViewModel>()
                    .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => src.Owner.UserName))
                    .ForMember(dest => dest.From, opt => opt.MapFrom(src => src.CreatedOn))
                    .ReverseMap();
                cfg.CreateMap<Blog, BlogEditViewModel>().ReverseMap();

                cfg.CreateMap<Post, PostListBasicViewModel>()
                    .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.UserName))
                    .ForMember(dest => dest.From, opt => opt.MapFrom(src => src.CreatedOn))
                    .ReverseMap();
                cfg.CreateMap<Post, PostDetailsViewModel>().ReverseMap();
                cfg.CreateMap<Post, PostEditViewModel>().ReverseMap();

                cfg.CreateMap<User, UserBaseViewModel>()
                    .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName))
                    .ReverseMap();
                cfg.CreateMap<User, UserProfileViewModel>().ReverseMap();
                cfg.CreateMap<RegistrationViewModel, User>()
                    .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                    .ReverseMap();
                cfg.CreateMap<User, LoginViewModel>().ReverseMap();
                cfg.CreateMap<LoginViewModel, RegistrationViewModel>().ReverseMap();

                cfg.CreateMap<SystemImage, SystemImageCreateOrEditViewModel>().ReverseMap();
                cfg.CreateMap<SystemImage, SystemImageContentViewModel>().ReverseMap();
            });
        }
    }
}
