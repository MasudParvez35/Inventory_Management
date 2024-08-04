using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using OA.Services;
using OA.Core.Domain;
using OA_WEB.Models;
using System.Security.Claims;
using OA_WEB.Factories;

namespace OA_WEB.Controllers
{
    public class AccountController : Controller
    {
        #region Fields

        protected readonly IAccountService _accountService;
        protected readonly IAccountModelFactory _accountModelFactory;

        #endregion

        #region Ctor

        public AccountController(IAccountService accountService, 
            IAccountModelFactory accountModelFactory)
        {
            _accountService = accountService;
            _accountModelFactory = accountModelFactory;
        }

        #endregion

        #region Methods

        public async Task<IActionResult> Login()
        {
            var model = await _accountModelFactory.PrepareLoginModelModelAsync(new LoginModel(), null);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _accountService.GetUserByUsernameAsync(model.UserName);
                if (user != null)
                {
                    bool isValid = (user.Name == model.UserName && (user.Password) == model.Password);
                    if (isValid)
                    {
                        var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, model.UserName) }, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                        HttpContext.Session.SetString("Username", model.UserName);

                        return RedirectToAction("List", "Product");

                    }
                    else
                    {
                        TempData["errorPassword"] = "Invalid password!";
                        return View(model);
                    }
                }
                else
                {
                    TempData["errorUsername"] = "Username not found!";
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        public async Task<IActionResult> SignUp()
        {
            var model = await _accountModelFactory.PrepareSignUpModelAsync(new SignUpModel(), null);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpModel model)
        {
            if (ModelState.IsValid)
            {
                var data = new User()
                {
                    Name = model.UserName,
                    Email = model.Email,
                    Password = model.Password,
                    Mobile = model.Mobile,
                    Address = model.Address
                };

                await _accountService.InsertUserAsync(data);
                TempData["successMessage"] = "You are eligible to login, Please fill own credential's then login!";
                return RedirectToAction("Login");
            }
            else
            {
                TempData["errorMessage"] = "Empty form can't be submitted!";
                return View(model);
            }
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var storedCookies = Request.Cookies.Keys;
            foreach (var cookies in storedCookies)
            {
                Response.Cookies.Delete(cookies);
            }

            return RedirectToAction("Login");
        }

        #endregion
    }
}
