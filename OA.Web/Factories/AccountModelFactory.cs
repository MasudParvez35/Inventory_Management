using OA.Services;
using OA_WEB.Models;
using OA.Core.Domain;

namespace OA_WEB.Factories
{
    public class AccountModelFactory : IAccountModelFactory
    {
        protected readonly ICityService _cityService;
        protected readonly IStateService _stateService;

        public AccountModelFactory(ICityService cityService, 
            IStateService stateService)
        {
            _cityService = cityService;
            _stateService = stateService;
        }

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
                        StateId = user.StateId,
                        CityId = user.CityId
                    };
                }
            }

            return model;
        }
    }
}
