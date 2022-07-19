namespace Data.Entities
{
    public class Subscribe
    {
        public int Id { get; set; }

        public string FollowerUserId { get; set; }

        public User FollowerUser { get; set; }

        public string SubscriberUserId { get; set; }

        public User SubscriberUser { get; set; }
    }
}
