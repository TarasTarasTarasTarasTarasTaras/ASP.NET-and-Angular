namespace Data.Entities
{
    public class UserLike
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public int PostId { get; set; }

        public Post Post { get; set; }
    }
}
