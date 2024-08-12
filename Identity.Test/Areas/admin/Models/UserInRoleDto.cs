using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Identity.Test.Areas.admin.Models
{
    public class UserInRoleDto
    {
        public string Id { get; set; }
        public string RoleName { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
