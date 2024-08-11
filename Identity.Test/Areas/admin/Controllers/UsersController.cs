using Identity.Test.Areas.admin.Models;
using Identity.Test.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Test.Areas.admin.Controllers
{
    [Area("admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<User> _userManager;
        public UsersController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var Users = _userManager.Users.Select(p => new UserListDto()
            {
                Id = p.Id,
                UserName = p.UserName,
                Email = p.Email,
                FirstName = p.FirstName,
                LastName = p.LastName,
                AccessFailedCount = p.AccessFailedCount
            }).ToList();
            

            return View(Users);
        }
        [HttpGet]
        public IActionResult Create() 
        { 

            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateUserDto register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }
            User newUser = new User()
            {    
                UserName = register.UserName,   
                FirstName = register.FirstName,
                LastName = register.LastName,
            };
            var result = _userManager.CreateAsync(newUser, register.Password).Result;
            if (result.Succeeded)
            {
                return RedirectToAction("index", "users");
            }

            string message = "";
            foreach (var item in result.Errors.ToList())
            {
                message += item.Description + Environment.NewLine;
            }

            TempData["Message"] = message;
            return View(register);


        }
    }
}
