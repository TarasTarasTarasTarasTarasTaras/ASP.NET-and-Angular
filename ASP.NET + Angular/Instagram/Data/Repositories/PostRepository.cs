using Data.Data;
using Data.Entities;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly InstagramContextDB _context;

        public PostRepository(InstagramContextDB context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            return await _context
                .Posts
                .Include(p => p.User)
                .ThenInclude(u => u.Profile)
                .Include(p => p.Likes)
                .Include(p => p.Saves)
                .Include(p => p.Comments)
                .ToListAsync();
        }

        public async Task<Post> GetPostByIdAsync(int id)
        {
            return await _context.Posts.FindAsync(id);
        }

        public async Task AddPostAsync(Post post)
        {
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePostAsync(Post post)
        {
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePostByIdAsync(int id)
        {
            Post post = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePost(Post entity)
        {
            var local = _context.Set<Post>()
                .Local
                .FirstOrDefault(entry => entry.Id.Equals(entity.Id));

            // check if local is not null 
            if (local != null)
            {
                // detach
                _context.Entry(local).State = EntityState.Detached;
            }

            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
