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

        public async Task<IActionResult> GetCurrentUser()
        {
            // Check if the user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                // Retrieve the username and user ID from the claims
                var username = User.FindFirst(ClaimTypes.Name)?.Value;
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                // Convert the user ID to an integer
                if (int.TryParse(userIdClaim, out int userId))
                {
                    // Use the username and user ID as needed, e.g., return it in a view or JSON
                    return Json(new { Username = username, UserId = userId });
                }
                else
                {
                    // Handle the case where the user ID is not valid
                    return BadRequest("User ID is invalid.");
                }
            }
            else
            {
                // User is not authenticated
                return Unauthorized("User is not logged in.");
            }
        }

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
                        //var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, model.UserName) }, CookieAuthenticationDefaults.AuthenticationScheme);
                        // Add the user ID as a claim
                        var identity = new ClaimsIdentity(new[]
                        {
                            new Claim(ClaimTypes.Name, model.UserName),
                            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) // Add user ID here
                        }, CookieAuthenticationDefaults.AuthenticationScheme);

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
                model = await _accountModelFactory.PrepareSignUpModelAsync(model, null);
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
