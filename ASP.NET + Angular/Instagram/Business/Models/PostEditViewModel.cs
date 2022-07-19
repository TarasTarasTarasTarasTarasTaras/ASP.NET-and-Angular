using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class PostEditViewModel
    {
        [Required]
        public string Description { get; set; }
    }
}
