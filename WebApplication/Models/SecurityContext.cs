using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Models
{
    public class SecurityContext : IdentityDbContext
    {
        public SecurityContext(DbContextOptions<SecurityContext> options) : base(options)
        {

        }
    }
}
