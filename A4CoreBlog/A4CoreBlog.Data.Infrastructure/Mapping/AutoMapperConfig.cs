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
                // Blogs
                cfg.CreateMap<Blog, BasicBlogViewModel>()
                    .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => src.Owner.UserName))
                    .ForMember(dest => dest.From, opt => opt.MapFrom(src => src.CreatedOn))
                    .ReverseMap();
                cfg.CreateMap<Blog, BlogEditViewModel>().ReverseMap();
                
                // Posts
                cfg.CreateMap<Post, PostListBasicViewModel>()
                    .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.UserName))
                    .ForMember(dest => dest.From, opt => opt.MapFrom(src => src.CreatedOn))
                    .ReverseMap();
                cfg.CreateMap<Post, PostDetailsViewModel>().ReverseMap();
                cfg.CreateMap<Post, PostEditViewModel>().ReverseMap();

                // Users
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

                // Images
                cfg.CreateMap<SystemImage, SystemImageCreateOrEditViewModel>().ReverseMap();
                cfg.CreateMap<SystemImage, SystemImageContentViewModel>().ReverseMap();
                cfg.CreateMap<SystemImage, AdminListImageViewModel>().ReverseMap();

                // Comments
                cfg.CreateMap<Comment, PostACommentViewModel>().ReverseMap();
                cfg.CreateMap<BlogComment, BaseCommentViewModel>()
                    .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Comment.Author.UserName))
                    .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.Comment.AuthorId))
                    .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Comment.Content))
                    .ReverseMap();

                cfg.CreateMap<PostComment, BaseCommentViewModel>()
                    .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Comment.Author.UserName))
                    .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.Comment.AuthorId))
                    .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Comment.Content))
                    .ReverseMap();
            });
        }
    }
}
