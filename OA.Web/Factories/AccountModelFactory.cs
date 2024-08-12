using OA.Core.Domain;
using OA_WEB.Models;

namespace OA_WEB.Factories
{
    public class AccountModelFactory : IAccountModelFactory
    {
        public async Task<LoginModel> PrepareLoginModelModelAsync(LoginModel model, User user, bool excludeProperties = false)
        {
            if (user != null)
            {
                if (model == null)
                {
                    model = new LoginModel
                    {
                        Id = user.Id,
                        UserName = user.Name,
                        Password = user.Password
                    };
                }
            }

            return model;
        }

        public async Task<SignUpModel> PrepareSignUpModelAsync(SignUpModel model, User user, bool excludeProperties = false)
        {
            if (user != null)
            {
                if (model == null)
                {
                    model = new SignUpModel
                    {
                        UserName = user.Name,
                        Email = user.Email,
                        Password = user.Password,
                        Mobile = user.Mobile,
                        Address = user.Address
                    };
                }
                else
                {
                    model.UserName = user.Name;
                    model.Email = user.Email;
                    model.Password = user.Password;
                    model.Mobile = user.Mobile;
                    model.Address = user.Address;
                }
            }

            return model;
        }
    }
}
