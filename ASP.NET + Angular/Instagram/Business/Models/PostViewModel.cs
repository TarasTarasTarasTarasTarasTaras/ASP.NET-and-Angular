using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class PostViewModel
    {
        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
