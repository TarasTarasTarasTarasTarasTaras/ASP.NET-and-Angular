using Business.Models;
using Business.Services.Interfaces;
using Instagram.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Instagram.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : Controller
    {
        private readonly IPostService postService;
        private readonly IUserInteractionService userInteractionService;

        public PostController(IPostService postService, IUserInteractionService userInteractionService)
        {
            this.postService = postService;
            this.userInteractionService = userInteractionService;
        }

        [HttpGet]
        [Authorize]
        [Route("AllExistingPosts")]
        public async Task<IActionResult> GetAllExistingPosts()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return Ok(await postService.GetAllExistingPosts(userId));
        }

        [HttpGet]
        [Authorize]
        [Route("Read/{id}")]
        public async Task<IActionResult> GetPostById(int id)
        {
            var post = await postService.GetById(id);

            if (post is null || post.IsDeleted)
                return NotFound();

            return Ok(post);
        }

        [HttpGet]
        [Authorize]
        [Route("MyAllPosts")]
        public async Task<IActionResult> GetMyAllPosts()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return Ok(await postService.GetMyAllPosts(userId));
        }

        [HttpGet]
        [Authorize]
        [Route("MyExistingPosts")]
        public async Task<IActionResult> GetMyExistingPosts()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return Ok(await postService.GetUserExistingPosts(userId));
        }

        [HttpGet]
        [Authorize]
        [Route("MyDeletedPosts")]
        public async Task<IActionResult> GetMyDeletedPosts()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return Ok(await postService.GetMyDeletedPosts(userId));
        }

        [HttpPost]
        [Authorize]
        [Route("Create")]
        public async Task<IActionResult> Create(PostViewModel post)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await postService.Create(post, userId);
            return Ok(new CustomResponse { Status = "Success", Message = "Post created successfully" });
        }

        [HttpPut]
        [Authorize]
        [Route("Update/{id}")]
        public async Task<IActionResult> Update(int id, PostEditViewModel post)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var wasUpdated = await postService.Update(id, post, userId);

            if (wasUpdated) 
                return Ok(new CustomResponse { Status = "Success", Message = "The post was successfully modified" });

            return NotFound();
        }

        [HttpDelete]
        [Authorize]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var post = await postService.GetById(id);
            if (post is null)
                return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var wasDeleted = await postService.Delete(id, userId);

            if (wasDeleted is null)
                return BadRequest(new CustomResponse { Status = "Error", Message = "This post has already been deleted" });

            if (wasDeleted == false)
                return BadRequest(new CustomResponse { Status = "Error", Message = "You do not have permission to delete this post" });

            return Ok();
        }

        [HttpPost]
        [Authorize]
        [Route("Restore/{id}")]
        public async Task<IActionResult> Restore(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            bool wasRestored = await postService.Restore(id, userId);

            if(wasRestored)
                return Ok(new CustomResponse { Status = "Success", Message = "The post was restored successfully" });

            return NotFound();
        }

        [HttpPost]
        [Authorize]
        [Route("Like/{id}")]
        public async Task<IActionResult> Like(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await userInteractionService.LikePressed(userId, id);

            return Ok();
        }

        [HttpPost]
        [Authorize]
        [Route("Save/{id}")]
        public async Task<IActionResult> Save(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await userInteractionService.SavePressed(userId, id);

            return Ok();
        }
    }
}
