using OA.Core.Domain;
using OA_WEB.Areas.Admin.Models;

namespace OA_WEB.Areas.Admin.Factories
{
    public interface IUserModelFactory
    {
        Task<IList<UserModel>> PrepareUserListModelAsync(IEnumerable<User> users);
        Task<UserModel> PrepareUserModelAsync(UserModel model, User user);
    }
}
