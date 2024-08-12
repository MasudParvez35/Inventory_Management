using OA.Core.Domain;
using OA.Services;
using OA_WEB.Areas.Admin.Models;

namespace OA_WEB.Areas.Admin.Factories
{
    public class UserModelFactory : IUserModelFactory
    {
        #region Fields

        protected readonly IAccountService _accountService;

        #endregion

        #region Ctor

        public UserModelFactory(IAccountService accountService)
        {
            _accountService = accountService;
        }

        #endregion

        #region Methods

        public async Task<IList<UserModel>> PrepareUserListModelAsync(IEnumerable<User> users)
        {
            var model = new List<UserModel>();

            foreach (var user in users)
                model.Add(await PrepareUserModelAsync(null, user));

            return model;
        }

        public async Task<UserModel> PrepareUserModelAsync(UserModel model, User user)
        {
            if (user != null)
            {
                if (model == null)
                {
                    model = new UserModel
                    {
                        Id = user.Id,
                        UserName = user.Name,
                        Email = user.Email,
                        Mobile = user.Mobile,
                        Address = user.Address
                    };
                }
            }

            return model;
        }

        #endregion
    }
}
