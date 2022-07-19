using AutoMapper;
using Business.Models;
using Business.Services.Interfaces;
using Data.Entities;
using Data.Repositories.Interfaces;
using System.Linq;

namespace Business.Services
{
    public class PostService : IPostService
    {
        private readonly IMapper mapper;
        private readonly IPostRepository postRepository;
        private readonly IUserInteractionRepository userInteractionRepository;

        public PostService(IMapper mapper, IPostRepository postRepository, IUserInteractionRepository userInteractionRepository)
        {
            this.mapper = mapper;
            this.postRepository = postRepository;
            this.userInteractionRepository = userInteractionRepository;
        }

        public async Task<IEnumerable<Post>> GetAllExistingPosts(string userId)
        {
            var posts = await postRepository.GetAllPostsAsync();

            return await HelperMethodGetPosts(posts, userId);
        }

        public async Task<Post> GetById(int id)
        {
            return await postRepository.GetPostByIdAsync(id);
        }

        public async Task<IEnumerable<Post>> GetMyAllPosts(string userId)
        {
            var posts = await postRepository.GetAllPostsAsync();
            return posts.Where(p => p.UserId == userId);
        }

        public async Task<IEnumerable<Post>> GetUserExistingPosts(string userId)
        {
            // !!!!!!!!!!!!!!!!!!!!
            var posts = await GetAllExistingPosts(userId);
            return posts.Where(p => p.UserId == userId);
        }

        public async Task<IEnumerable<Post>> GetMyDeletedPosts(string userId)
        {
            var posts = await postRepository.GetAllPostsAsync();
            return posts.Where(p => p.UserId == userId && p.IsDeleted);
        }

        public async Task Create(PostViewModel model, string userId)
        {
            Post post = mapper.Map<Post>(model);
            post.UserId = userId;
            post.Likes = new List<UserLike>();
            post.Comments = new List<Comment>();
            await postRepository.AddPostAsync(post);
        }

        public async Task<bool> Update(int id, PostEditViewModel model, string userId)
        {
            var post = await postRepository.GetPostByIdAsync(id);

            if (post is null || post.UserId != userId || post.IsDeleted)
                return false;

            post.Description = model.Description;
            await postRepository.UpdatePost(post);
            return true;
        }

        public async Task<bool?> Delete(int id, string userId)
        {
            Post post = await postRepository.GetPostByIdAsync(id);
            if (post.IsDeleted) 
                return null;

            if (post.UserId != userId)
                return false;

            await postRepository.DeletePostByIdAsync(id);
            return true;
        }

        public async Task<bool> Restore(int id, string userId)
        {
            Post post = await postRepository.GetPostByIdAsync(id);
            if (post is null || post.UserId != userId || !post.IsDeleted)
                return false;

            post.IsDeleted = false;
            await postRepository.UpdatePost(post);
            return true;
        }

        private async Task<IEnumerable<Post>> HelperMethodGetPosts(IEnumerable<Post> posts, string userId)
        {
            List<Post> result = new List<Post>();

            foreach(Post post in posts)
            {
                if (post.UserId == userId)
                    result.Add(post);
                else if (post.User.Profile?.IsPrivate == null || !post.User.Profile.IsPrivate)
                    result.Add(post);
                else if (await userInteractionRepository.GetSubscribe(post.UserId, userId) != null)
                    result.Add(post);
            }

            return result.Where(p => !p.IsDeleted).Reverse();
        }
    }
}
