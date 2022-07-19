using Data.Data;
using Data.Entities;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly InstagramContextDB context;

        public ProfileRepository(InstagramContextDB context)
        {
            this.context = context;
        }

        public async Task<Profile> GetProfileByIdAsync(string userId)
        {
            return await context.Profiles.FindAsync(userId);
        }

        public async Task<User> GetUserByIdAsync(string userId)
        {
            User user = await context.Users
                .Include(u => u.Profile)
                .Include(u => u.Posts)
                .Include(u => u.SavedPosts)
                .Include(u => u.UserLikes)
                .FirstOrDefaultAsync(u => u.Id == userId);

            user.Posts = user.Posts.OrderByDescending(p => p.Id).ToList();

            return user;
        }

        public async Task EditUser(User user)
        {
            var local = context.Set<User>()
                .Local
                .FirstOrDefault(entry => entry.Id.Equals(user.Id));

            // check if local is not null 
            if (local != null)
            {
                // detach
                context.Entry(local).State = EntityState.Detached;
            }

            context.Entry(user).State = EntityState.Modified;
            context.SaveChanges();
        }

        public async Task EditProfile(Profile profile)
        {
            var local = context.Set<Profile>()
                .Local
                .FirstOrDefault(entry => entry.UserId.Equals(profile.UserId));

            // check if local is not null 
            if (local != null)
            {
                // detach
                context.Entry(local).State = EntityState.Detached;
            }

            var oldProfile = context.Profiles.FirstOrDefault(p => p.UserId == profile.UserId);
            profile.CreatedOn = oldProfile.CreatedOn;
            profile.CreatedBy = oldProfile.CreatedBy;

            context.Entry(profile).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
