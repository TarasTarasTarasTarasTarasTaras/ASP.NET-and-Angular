using Business.Models;
using Data.Entities;

namespace Business.Services.Interfaces
{
    public interface IProfileService
    {
        Task<Profile> GetProfileByIdAsync(string userId);
        Task<User> GetUserByIdAsync(string userId);
        Task EditProfile(ProfileViewModel model, string userId);
    }
}
