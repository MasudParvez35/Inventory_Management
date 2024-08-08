using OA.Core.Domain;

namespace OA.Services
{
    public interface IAccountService
    {
        Task InsertUserAsync(User user);
        Task<User> GetUserByUsernameAsync(string username);
    }
}
