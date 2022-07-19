using Business.Models;
using Data.Entities;

namespace Business.Services.Interfaces
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetAllExistingPosts(string userId);
        Task<Post> GetById(int id);
        Task<IEnumerable<Post>> GetMyAllPosts(string userId);
        Task<IEnumerable<Post>> GetUserExistingPosts(string userId);
        Task<IEnumerable<Post>> GetMyDeletedPosts(string userId);
        Task Create(PostViewModel model, string userId);
        Task<bool> Update(int id, PostEditViewModel model, string userId);
        Task<bool?> Delete(int id, string userId);
        Task<bool> Restore(int id, string userId);
    }
}
