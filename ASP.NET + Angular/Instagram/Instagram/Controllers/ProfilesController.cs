using Business.Models;
using Business.Services.Interfaces;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Instagram.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfilesController : Controller
    {
        private readonly IProfileService profileService;
        private readonly IUserInteractionService userInteractionService;

        public ProfilesController(IProfileService profileService, IUserInteractionService userInteractionService)
        {
            this.profileService = profileService;
            this.userInteractionService = userInteractionService;
        }

        [HttpGet]
        [Authorize]
        [Route("profile/{userId}")]
        public async Task<IActionResult> GetProfile(string userId)
        {
            Profile profile = await profileService.GetProfileByIdAsync(userId);

            if(profile is null)
                return NotFound();

            return Ok(profile);
        }

        [HttpPut]
        [Authorize]
        [Route("edit")]
        public async Task<IActionResult> EditProfile(ProfileViewModel profile)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            profileService.EditProfile(profile, userId);

            return Ok();
        }

        [HttpGet]
        [Authorize]
        [Route("subscribes/{userId}")]
        public async Task<IEnumerable<Subscribe>> GetSubscribesByUserId(string userId)
        {
            return await userInteractionService.GetAllSubscribesByUserId(userId);
        }

        [HttpPost]
        [Authorize]
        [Route("subscribe/{userId}")]
        public async Task<IActionResult> SubscribeToTheProfile(string userId)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(currentUserId != userId)
                await userInteractionService.SubscribePressed(userId, currentUserId);

            return Ok();
        }
    }
}
