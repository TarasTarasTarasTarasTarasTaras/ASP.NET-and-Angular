using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Data.Data
{
    public class UserResolverService
    {
        public readonly IHttpContextAccessor _context;

        public UserResolverService(IHttpContextAccessor context)
        {
            _context = context;
        }

        public string GetGivenName()
        {
            return _context.HttpContext.User.FindFirst(ClaimTypes.GivenName)?.Value;
        }

        public string GetSurname()
        {
            return _context.HttpContext.User.FindFirst(ClaimTypes.Surname)?.Value;
        }

        public string GetNameIdentifier()
        {
            return _context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public string GetEmails()
        {
            return _context.HttpContext.User.FindFirst("emails")?.Value;
        }
    }
}
