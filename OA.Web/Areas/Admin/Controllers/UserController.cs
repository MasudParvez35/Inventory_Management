using OA.Services;
using Microsoft.AspNetCore.Mvc;
using OA_WEB.Areas.Admin.Factories;

namespace OA_WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        #region Fields

        protected readonly IAccountService _accountService;
        protected readonly IUserModelFactory _userModelFactory;

        #endregion

        #region Ctor
        public UserController(IAccountService accountService, 
            IUserModelFactory userModelFactory)
        {
            _accountService = accountService;
            _userModelFactory = userModelFactory;
        }

        #endregion

        #region Methods

        public async Task<IActionResult> List()
        {
            var allUser = await _accountService.GetAllUsersAsync();
            var model = await _userModelFactory.PrepareUserListModelAsync(allUser);

            return View(model);
        }

        #endregion
    }
}
