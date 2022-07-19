using Data.Entities;

namespace Data.Repositories.Interfaces
{
    public interface IProfileRepository
    {
        Task<Profile> GetProfileByIdAsync(string userId);
        Task<User> GetUserByIdAsync(string userId);
        Task EditUser(User user);
        Task EditProfile(Profile profile);
    }
}
