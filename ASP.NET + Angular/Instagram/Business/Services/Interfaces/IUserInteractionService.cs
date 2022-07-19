using Data.Entities;

namespace Business.Services.Interfaces
{
    public interface IUserInteractionService
    {
        Task LikePressed(string userId, int postId);
        Task SavePressed(string userId, int postId);
        Task SubscribePressed(string userId, string currentUserId);
        Task<IEnumerable<Subscribe>> GetAllSubscribesByUserId(string userId);
    }
}
