using A4CoreBlog.Data.Models;
using A4CoreBlog.Data.Services.Contracts;
using A4CoreBlog.Data.UnitOfWork;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A4CoreBlog.Data.ViewModels;

namespace A4CoreBlog.Data.Services.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly IBlogSystemData _data;

        public CommentService(IBlogSystemData data)
        {
            _data = data;
        }

        public async Task<int> Add<T>(T model)
        {
            var comment = Mapper.Map<Comment>(model);
            _data.Comments.Add(comment);
            await _data.SaveChangesAsync();
            return comment.Id;
        }

        public async Task<int> AddBlogComment<T>(T model, int blogId)
        {
            var commentId = await Add(model);
            var dbBlogComment = new BlogComment
            {
                BlogId = blogId,
                CommentId = commentId
            };
            _data.BlogComments.Add(dbBlogComment);
            _data.SaveChanges();
            return dbBlogComment.Id;
        }

        public async Task<int> AddBlogComment(PostACommentViewModel model)
        {
            var user = _data.Users.All().FirstOrDefault(u => u.UserName == model.AuthorName);
            if (user != null)
            {
                model.AuthorId = user.Id;
                return await AddBlogComment(model, model.ForId);
            }

            return 0;
        }

        public async Task<int> AddPostComment<T>(T model, int postId)
        {
            var commentId = await Add(model);
            var dbPostComment = new PostComment
            {
                PostId = postId,
                CommentId = commentId
            };
            _data.PostComments.Add(dbPostComment);
            await _data.SaveChangesAsync();
            return dbPostComment.Id;
        }

        public async Task<int> AddPostComment(PostACommentViewModel model)
        {
            var user = _data.Users.All().FirstOrDefault(u => u.UserName == model.AuthorName);
            if (user !=null)
            {
                model.AuthorId = user.Id;
                return await AddPostComment(model, model.ForId);
            }

            return 0;
        }

        public async Task<IQueryable<T>> GetAll<T>()
        {
            return _data.Comments.All().ProjectTo<T>();
        }

        public async Task<IQueryable<T>> GetAllForBlog<T>(int blogId)
        {
            var result = _data.BlogComments.All()
                .Where(bc => bc.BlogId == blogId)
                .ProjectTo<T>();

            return result;
        }

        public async Task<IQueryable<T>> GetAllForPost<T>(int postId)
        {
            var result = _data.PostComments.All()
                .Where(pc => pc.PostId == postId)
                .ProjectTo<T>();

            return result;
        }
    }
}
