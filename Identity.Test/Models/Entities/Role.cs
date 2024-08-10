using Microsoft.AspNetCore.Identity;

namespace Identity.Test.Models.Entities
{
    public class Role:IdentityRole
    {
        public string? Describtion { get; set; }
    }
}

