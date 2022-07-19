using Data.Entities;

namespace Data.Repositories.Interfaces
{
    public interface IUserInteractionRepository
    {
        Task<UserLike> GetUserLikesAsync(string userId, int postId);
        Task<UserSave> GetUserSavesAsync(string userId, int postId);
        Task AddLikeAsync(UserLike like);
        Task DeleteLikeAsync(UserLike like);
        Task AddSaveAsync(UserSave save);
        Task DeleteSaveAsync(UserSave save);
        Task<IEnumerable<Subscribe>> GetAllSubscribes(string userId);
        Task<Subscribe> GetSubscribe(string userId, string currentUserId);
        Task AddSubscribe(Subscribe subscribe);
        Task RemoveSubscribe(Subscribe subscribe);
    }
}
