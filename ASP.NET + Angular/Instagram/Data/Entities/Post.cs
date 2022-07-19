using Data.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class Post : DeletableEntity
    {
        public int Id { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

        public List<UserLike> Likes { get; set; }

        public List<UserSave> Saves { get; set; }

        public List<Comment> Comments { get; set; }
    }
}
