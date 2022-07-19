namespace Data.Data
{
    public class ContextManagerDB
    {
        public InstagramContextDB _context;
        public UserResolverService _userService;

        public ContextManagerDB(InstagramContextDB context, UserResolverService userService)
        {
            _context = context;
            _userService = userService;
            _context._currentUserExternalId = _userService.GetNameIdentifier();
        }
    }
}
