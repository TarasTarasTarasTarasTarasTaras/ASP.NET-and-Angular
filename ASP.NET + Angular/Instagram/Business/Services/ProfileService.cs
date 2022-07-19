using AutoMapper;
using Business.Models;
using Business.Services.Interfaces;
using Data.Entities;
using Data.Repositories.Interfaces;

namespace Business.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IMapper mapper;
        private readonly IProfileRepository profileRepository;

        public ProfileService(IMapper mapper, IProfileRepository profileRepository)
        {
            this.mapper = mapper;
            this.profileRepository = profileRepository;
        }

        public async Task<Data.Entities.Profile> GetProfileByIdAsync(string userId)
        {
            return await profileRepository.GetProfileByIdAsync(userId);
        }

        public async Task<User> GetUserByIdAsync(string userId)
        {
            return await profileRepository.GetUserByIdAsync(userId);
        }

        public async Task EditProfile(ProfileViewModel model, string userId)
        {
            var profile = mapper.Map<Data.Entities.Profile>(model);
            profile.UserId = userId;

            await profileRepository.EditProfile(profile);
        }
    }
}
