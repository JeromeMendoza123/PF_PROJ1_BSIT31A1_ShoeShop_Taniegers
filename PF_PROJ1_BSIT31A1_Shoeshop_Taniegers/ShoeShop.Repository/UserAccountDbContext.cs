using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShoeShop.Repository.Entities;

namespace ShoeShop.Repository
{
    public class UserAccountDbContext : IdentityDbContext<ApplicationUser>
    {
        public UserAccountDbContext(DbContextOptions<UserAccountDbContext> options)
            : base(options)
        {
        }
    }
}
