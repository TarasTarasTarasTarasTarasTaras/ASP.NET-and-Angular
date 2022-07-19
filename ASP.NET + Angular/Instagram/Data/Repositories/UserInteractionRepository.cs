using Data.Data;
using Data.Entities;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class UserInteractionRepository : IUserInteractionRepository
    {
        private readonly InstagramContextDB _context;

        public UserInteractionRepository(InstagramContextDB context)
        {
            _context = context;
        }

        public async Task<UserLike> GetUserLikesAsync(string userId, int postId)
        {
            return await _context.UserLikes.FirstOrDefaultAsync(ul => ul.UserId == userId && ul.PostId == postId);
        }

        public async Task<UserSave> GetUserSavesAsync(string userId, int postId)
        {
            return await _context.UserSaves.FirstOrDefaultAsync(us => us.UserId == userId && us.PostId == postId);
        }

        public async Task AddLikeAsync(UserLike like)
        {
            await _context.UserLikes.AddAsync(like);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLikeAsync(UserLike like)
        {
            _context.UserLikes.Remove(like);
            await _context.SaveChangesAsync();
        }

        public async Task AddSaveAsync(UserSave save)
        {
            await _context.UserSaves.AddAsync(save);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSaveAsync(UserSave save)
        {
            _context.UserSaves.Remove(save);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Subscribe>> GetAllSubscribes(string userId)
        {
            return await _context
                .Subscribers
                .Where(s => s.FollowerUserId == userId || s.SubscriberUserId == userId)
                .ToListAsync();
        }

        public async Task<Subscribe> GetSubscribe(string userId, string currentUserId)
        {
            return await _context
                .Subscribers
                .FirstOrDefaultAsync(s => s.FollowerUserId == currentUserId && s.SubscriberUserId == userId);
        }

        public async Task AddSubscribe(Subscribe subscribe)
        {
            await _context.Subscribers.AddAsync(subscribe);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveSubscribe(Subscribe subscribe)
        {
            _context.Subscribers.Remove(subscribe);
            await _context.SaveChangesAsync();
        }
    }
}
