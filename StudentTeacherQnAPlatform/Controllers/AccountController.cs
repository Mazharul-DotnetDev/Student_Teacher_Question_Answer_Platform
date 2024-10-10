using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentTeacherQnAPlatform.Entities.Security;
using StudentTeacherQnAPlatform.Repositories.IRepository;
using StudentTeacherQnAPlatform.ViewModels;

namespace StudentTeacherQnAPlatform.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Create a new User object
                var user = new User
                {
                    Name = model.Name,
                    Email = model.Email,
                    RoleId = model.RoleId
                };                
                await _userService.RegisterUserAsync(user, model.Password);
                return RedirectToAction("Login");
            }
            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.GetUserByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, model.Password);
                    if (result == PasswordVerificationResult.Success)
                    {
                        HttpContext.Session.SetString("UserId", user.Id.ToString());
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("", "Invalid login attempt");
            }
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();            
            return RedirectToAction("Login", "Account");
        }

    }
}
