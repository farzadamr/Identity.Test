using Identity.Test.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Test.Data
{
    public class DatabaseContext : IdentityDbContext<User,Role,string>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) 
        {
        }




    }
}
