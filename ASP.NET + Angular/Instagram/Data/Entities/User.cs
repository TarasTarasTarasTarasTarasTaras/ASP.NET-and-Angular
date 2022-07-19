using Microsoft.AspNetCore.Identity;

namespace Data.Entities
{
    public class User : IdentityUser
    {
        public string Name { get; set; }

        public Profile Profile { get; set; } = new Profile
        {
            MainPhotoUrl = "https://instastatistics.com/images/default_avatar.jpg"
        };

        public DateTime CreatedOn { get; set; }

        public List<Post> Posts { get; set; }

        public List<UserSave> SavedPosts { get; set; }

        public List<UserLike> UserLikes { get; set; }
    }
}
