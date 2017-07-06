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
                //// Custom field mapping tested and working !! :)
                //    .ForMember(dest => dest.Description123, opt => opt.MapFrom(src => src.Description))
                //    .ReverseMap();

                //cfg.CreateMap<BlogPost, BlogPostDetailsViewModel>()
                //    .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.UserName))
                //    .ReverseMap();

                //cfg.CreateMap<BlogPost, BlogSmallListViewModel>()
                //    .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.UserName))
                //    .ReverseMap();


                //cfg.CreateMap<User, UserListViewModel>().ReverseMap();
                //cfg.CreateMap<User, UserShortInfoViewModel>().ReverseMap();
            });
        }
    }
}
