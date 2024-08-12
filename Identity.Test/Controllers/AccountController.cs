using Identity.Test.Models.Dto;
using Identity.Test.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Test.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountController(UserManager<User> userManager,SignInManager<User> signInManager) 
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register() 
        {

            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterDto register) 
        {
            if(!ModelState.IsValid) 
            {
                return View(register);
            }
            User newUser = new User()
            {
                Email = register.Email,
                UserName = register.UserName,
                LastName = register.LastName,
                FirstName = register.FirstName,
            };
            var result = _userManager.CreateAsync(newUser, register.Password).Result;
            if (result.Succeeded)
            {
                return RedirectToAction("Index","Home");
            }

            string message = "";
            foreach(var item in result.Errors.ToList())
            {
                message += item.Description + Environment.NewLine;
            }

            TempData["Message"] = message;
            return View(register);
        }

        [HttpGet]
        public IActionResult Login(string ReturnUrl ="/")
        {
            return View(new LoginDto()
            {
                ReturnUrl = ReturnUrl,
            });
        }
        [HttpPost]
        public IActionResult Login(LoginDto login)
        {
            if(!ModelState.IsValid)
            {
                return View(login);
            }

            _signInManager.SignOutAsync();
            var user = _userManager.FindByNameAsync(login.UserName).Result;
            if (user != null)
            {
                var Result = _signInManager.PasswordSignInAsync(user, login.Password, login.isPersistent, true).Result;
                if (Result.Succeeded)
                {
                    return Redirect(login.ReturnUrl);
                }
                if (Result.IsLockedOut)
                {
                    //
                }
                if (Result.RequiresTwoFactor)
                {
                    //
                }
            }

            ModelState.AddModelError(string.Empty, "Login Error");


            return View();
        }

        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
