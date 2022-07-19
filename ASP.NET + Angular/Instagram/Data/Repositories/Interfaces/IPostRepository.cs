using Data.Entities;

namespace Data.Repositories.Interfaces
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllPostsAsync();
        Task<Post> GetPostByIdAsync(int id);
        Task AddPostAsync(Post post);
        Task DeletePostAsync(Post post);
        Task DeletePostByIdAsync(int id);
        Task UpdatePost(Post entity);
    }
}
