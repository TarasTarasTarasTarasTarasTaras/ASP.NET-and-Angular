namespace Business.Models
{
    public class ProfileViewModel
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Country { get; set; }

        public string? City { get; set; }

        public string? MainPhotoUrl { get; set; }

        public string? Biography { get; set; }

        public bool IsPrivate { get; set; }
    }
}
