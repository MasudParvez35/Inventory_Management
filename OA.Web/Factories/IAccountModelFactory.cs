using OA.Core.Domain;
using OA_WEB.Models;

namespace OA_WEB.Factories
{
    public interface IAccountModelFactory
    {
        Task<SignUpModel> PrepareSignUpModelAsync(SignUpModel model, User user, bool excludeProperties = false);
        Task<LoginModel> PrepareLoginModelModelAsync(LoginModel model, User user, bool excludeProperties = false);
    }
}
