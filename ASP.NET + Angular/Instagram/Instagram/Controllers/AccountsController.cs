using Business.Models;
using Business.Services.Interfaces;
using Business.Validators;
using Data.Entities;
using Instagram.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Instagram.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountsController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IProfileService _profileService;

        public AccountsController(UserManager<User> userManager, IConfiguration configuration, IProfileService profileService)
        {
            _userManager = userManager;
            _configuration = configuration;
            _profileService = profileService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized("Incorrect login or password");
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.UserName);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new CustomResponse
                    {
                        Status = "Error",
                        Message = "Invalid data",
                        SpecificErrors = new List<ErrorModel> {
                            new ErrorModel
                            {
                                FieldName = "Username",
                                Message = "User already exists!"
                            }}
                    });

            User user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                CreatedOn = DateTime.Now,
                Name = model.Name
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new CustomResponse { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            return Ok(new CustomResponse { Status = "Success", Message = "User created successfully!" });
        }

        [HttpGet]
        [Authorize]
        [Route("currentUser")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            User user = await _profileService.GetUserByIdAsync(userId);

            if (user is null)
                return NotFound();

            return Ok(user);
        }

        [HttpGet]
        [Authorize]
        [Route("user/{userId}")]
        public async Task<IActionResult> GetUserById(string userId)
        {
            User user = await _profileService.GetUserByIdAsync(userId);

            if (user is null)
                return NotFound();

            return Ok(user);
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
