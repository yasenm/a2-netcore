﻿using System.Linq;
using System.Threading.Tasks;

namespace A4CoreBlog.Data.Services.Contracts
{
    public interface ICommentService
    {
        Task<int> Add<T>(T model);
        Task<int> AddPostComment<T>(T model, int blogId);
        Task<int> AddBlogComment<T>(T model, int postId);
        Task<IQueryable<T>> GetAll<T>();
        Task<IQueryable<T>> GetAllForBlog<T>(int blogId);
        Task<IQueryable<T>> GetAllForPost<T>(int postId);
    }
}
