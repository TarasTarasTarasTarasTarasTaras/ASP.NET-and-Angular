using Data.Entities.Base;

namespace Data.Entities
{
    public class Comment : Entity
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public int PostId { get; set; }

        public Post Post { get; set; }

        public List<User> CommentLikes { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
