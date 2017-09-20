using A4CoreBlog.Data.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace A4CoreBlog.Data.Services.Contracts
{
    public interface ICommentService
    {
        Task<int> Add<T>(T model);
        Task<int> AddPostComment(PostACommentViewModel model);
        Task<int> AddBlogComment(PostACommentViewModel model);
        Task<int> AddPostComment<T>(T model, int blogId);
        Task<int> AddBlogComment<T>(T model, int postId);
        Task<bool> Delete(int id);
        Task<IQueryable<T>> GetAll<T>();
        Task<IQueryable<T>> GetAllForBlog<T>(int blogId);
        Task<IQueryable<T>> GetAllForPost<T>(int postId);
    }
}
