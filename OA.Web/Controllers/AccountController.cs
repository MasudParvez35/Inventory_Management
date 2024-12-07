using OA.Services;
using OA_WEB.Models;
using OA.Core.Domain;
using OA_WEB.Factories;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace OA_WEB.Controllers;

public class AccountController : Controller
{
    #region Fields

    protected readonly IAreaService _areaService;
    protected readonly ICityService _cityService;
    protected readonly IStateService _stateService;
    protected readonly IAccountService _accountService;
    protected readonly IAccountModelFactory _accountModelFactory;

    #endregion

    #region Ctor

    public AccountController(IAccountService accountService,
        IAccountModelFactory accountModelFactory,
        ICityService cityService,
        IStateService stateService,
        IAreaService areaService)
    {
        _cityService = cityService;
        _stateService = stateService;
        _accountService = accountService;
        _accountModelFactory = accountModelFactory;
        _areaService = areaService;
    }

    #endregion

    #region Methods

    #region User

    [AcceptVerbs("Get", "Post")]
    public async Task<IActionResult> UserNameIsExist(string userName)
    {
        var user = await _accountService.GetUserByUsernameAsync(userName);
        if (user != null)
        {
            return Json($"Username {userName} is already taken.");
        }
        return Json(true);
    }

    public async Task<IActionResult> GetCurrentUser()
    {
        if (User.Identity.IsAuthenticated)
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value;
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (int.TryParse(userIdClaim, out int userId))
            {
                return Json(new { Username = username, UserId = userId });
            }
            else
            {
                return BadRequest("User ID is invalid.");
            }
        }
        else
        {
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
                Phone = model.Mobile,
                StateId = model.StateId,
                CityId = model.CityId,
                AreaId = model.AreaId
            };

            await _accountService.InsertUserAsync(data);
            TempData["successMessage"] = "You are eligible to login, Please fill own credential's then login!";
            
            return RedirectToAction("Login");
        }
        else
        {
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

    #region State-city

    public async Task<IActionResult> GetStates()
    {
        var states = await _stateService.GetAllStatesAsync();

        return new JsonResult(states);
    }

    public async Task<IActionResult> GetCities(int stateId)
    {
        var cities = await _cityService.GetCitiesByStateIdAsync(stateId);

        return new JsonResult(cities);
    }

    public async Task<IActionResult> GetAreas(int cityId)
    {
        var areas = await _areaService.GetAreasByCityIdAsync(cityId);

        return new JsonResult(areas);
    }

    #endregion

    #endregion
}
