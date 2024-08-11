using Identity.Test.Areas.admin.Models;
using Identity.Test.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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
        [HttpGet]
        public IActionResult Edit(string Id)
        {
            var user = _userManager.FindByIdAsync(Id).Result;
            UserEditDto userEdit = new UserEditDto()
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
            };
            return View(userEdit);
        }
        [HttpPost]
        public IActionResult Edit(UserEditDto userEdit)
        {
            var user = _userManager.FindByIdAsync(userEdit.Id).Result;  
            user.UserName = userEdit.UserName;
            user.FirstName = userEdit.FirstName;
            user.LastName = userEdit.LastName;
            user.Email = userEdit.Email;

            var result = _userManager.UpdateAsync(user).Result;
            if(result.Succeeded)
            {
                return RedirectToAction("index", "users", new { area = "admin" });
            }
            string Message = "";
            foreach(var item in result.Errors.ToList())
            {
                Message += item.Description + Environment.NewLine;
            }
            TempData["Message"] = Message;
            return View(userEdit);
        }
        public IActionResult Delete(string Id)
        {
            var user = _userManager.FindByIdAsync(Id).Result;
            UserDeleteDto userDelete = new UserDeleteDto()
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
            };
            return View(userDelete);
        }
        [HttpPost]
        public IActionResult Delete(UserDeleteDto userDelete)
        {
            var user = _userManager.FindByIdAsync(userDelete.Id).Result;
            var result = _userManager.DeleteAsync(user).Result;
            if (result.Succeeded)
            {
                return RedirectToAction("Index","Users",new {area="admin" });
            }
            string Message = "";
            foreach(var item in result.Errors.ToList())
            {
                Message += item.Description + Environment.NewLine;
            }
            TempData["Message"] = Message;
            return View(userDelete);
        }

    }
}
