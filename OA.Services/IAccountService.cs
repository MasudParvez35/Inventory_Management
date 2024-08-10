using OA.Core.Domain;

namespace OA.Services
{
    public interface IAccountService
    {
        Task InsertUserAsync(User user);
        Task<User> GetUserByIdAsync(int userId);
        Task<User> GetUserByUsernameAsync(string username);
    }
}
