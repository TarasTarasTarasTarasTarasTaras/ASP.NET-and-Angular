using Business.Services.Interfaces;
using Data.Entities;
using Data.Repositories.Interfaces;

namespace Business.Services
{
    public class UserInteractionService : IUserInteractionService
    {
        private readonly IUserInteractionRepository userInteractionRepository;

        public UserInteractionService(IUserInteractionRepository userLikesRepository)
        {
            this.userInteractionRepository = userLikesRepository;
        }

        public async Task LikePressed(string userId, int postId)
        {
            UserLike like = await userInteractionRepository.GetUserLikesAsync(userId, postId);

            if (like is null)
            {
                like = new UserLike { UserId = userId, PostId = postId };
                await userInteractionRepository.AddLikeAsync(like);
            }
            else
                await userInteractionRepository.DeleteLikeAsync(like);
        }

        public async Task SavePressed(string userId, int postId)
        {
            UserSave save = await userInteractionRepository.GetUserSavesAsync(userId, postId);

            if (save is null)
            {
                save = new UserSave { UserId = userId, PostId = postId };
                await userInteractionRepository.AddSaveAsync(save);
            }
            else
                await userInteractionRepository.DeleteSaveAsync(save);
        }

        public async Task<IEnumerable<Subscribe>> GetAllSubscribesByUserId(string userId)
        {
            return await userInteractionRepository.GetAllSubscribes(userId);
        }

        public async Task SubscribePressed(string userId, string currentUserId)
        {
            Subscribe subscribe = await userInteractionRepository.GetSubscribe(userId, currentUserId);

            if (subscribe != null)
                await userInteractionRepository.RemoveSubscribe(subscribe);
            else
            {
                subscribe = new Subscribe() { FollowerUserId = currentUserId, SubscriberUserId = userId };
                await userInteractionRepository.AddSubscribe(subscribe);
            }
        }
    }
}
