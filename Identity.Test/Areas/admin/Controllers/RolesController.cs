using Identity.Test.Areas.admin.Models;
using Identity.Test.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Test.Areas.admin.Controllers
{
    [Area("admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        public RolesController(RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;

        }
        public IActionResult Index()
        {
            var roles = _roleManager.Roles
                .Select(p=>new RoleListDto()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Describtion = p.Describtion
                }).ToList();
            
            return View(roles);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateRoleDto createRole)
        {

            Role newRole = new Role();
            newRole.Name = createRole.Name;
            newRole.Describtion = createRole.Describtion;

            var result = _roleManager.CreateAsync(newRole).Result;

            if(result.Succeeded)
            {
                return RedirectToAction("Index", "Roles", new { area = "admin" });
            }
            string message= "";
            foreach (var item in result.Errors) 
            {
                message += item.Description + Environment.NewLine;
            }
            TempData["Errors"] = message;
            return View(createRole);

        }
        [HttpGet]
        public IActionResult UserInRole(string Id,string RoleName)
        {
            var role = _roleManager.FindByIdAsync(Id).Result;
            var users = _userManager.GetUsersInRoleAsync(role.Name).Result;
            ViewBag.RoleName = RoleName;
            return View(users.Select(p => new UserInRoleDto()
            {
                Id = p.Id,
                FullName = $"{p.FirstName} {p.LastName}",
                UserName = p.UserName,
                Email = p.Email,
            }).ToList());
            
        }
    }
}
