using OA.Core.Domain;
using OA.Services;
using OA_WEB.Areas.Admin.Models;

namespace OA_WEB.Areas.Admin.Factories
{
    public class UserModelFactory : IUserModelFactory
    {
        #region Fields

        protected readonly ICityService _cityService;
        protected readonly IStateService _stateService;
        protected readonly IAccountService _accountService;

        #endregion

        #region Ctor

        public UserModelFactory(IAccountService accountService,
            IStateService stateService,
            ICityService cityService)
        {
            _cityService = cityService;
            _stateService = stateService;
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
                        Mobile = user.Phone,
                        StateId = user.StateId,
                        CityId = user.CityId
                    };
                }

                var state = await _stateService.GetStateByIdAsync(user.StateId);
                var city = await _cityService.GetCityByIdAsync(user.CityId);

                model.StateName = state.Name;
                model.CityName = city.Name;
            }

            return model;
        }

        #endregion
    }
}
