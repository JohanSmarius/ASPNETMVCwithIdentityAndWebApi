using Microsoft.EntityFrameworkCore;

namespace WebApplication.Models
{
    public class CustomerContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(new Customer[] { 
                new Customer() { Id = 1, EmailAddress = "jsmarius@hotmail.com", Name = "Johan" },
                new Customer() { Id = 2, EmailAddress = "jaw.smarius@avans.nl", Name = "JohanWerk"}
            });
        }


    }
}
