using Business.Validators;

namespace Instagram.Responses
{
    public class CustomResponse
    {
        public string? Status { get; set; }
        public string? Message { get; set; }
        public List<ErrorModel> SpecificErrors { get; set; } = new List<ErrorModel>();
    }
}
