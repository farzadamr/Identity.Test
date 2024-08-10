using Microsoft.AspNetCore.Identity;

namespace Identity.Test.Models.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
