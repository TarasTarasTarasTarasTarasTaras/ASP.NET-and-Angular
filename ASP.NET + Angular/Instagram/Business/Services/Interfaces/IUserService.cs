namespace Business.Services.Interfaces
{
    public interface IUserService
    {
        bool IsEmailUnique(string email);
        bool IsUserNameUnique(string userName);
    }
}
