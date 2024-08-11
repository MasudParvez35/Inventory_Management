using OA.Core.Domain;

namespace OA.Services
{
    public interface IAccountService
    {
        Task InsertUserAsync(User user);
        Task<User> GetUserByIdAsync(int userId);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByUsernameAsync(string username);
        Task<int> GetTotalUsersAsync();
    }
}
