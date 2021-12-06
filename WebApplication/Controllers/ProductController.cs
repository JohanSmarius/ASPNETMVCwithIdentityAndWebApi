using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        public ProductController(CustomerContext context)
        {
            Context = context;
        }

        public CustomerContext Context { get; }

        public IActionResult Index()
        {
            var identity = HttpContext.User.Identity;

            var customer = Context.Customers.SingleOrDefault(customer => customer.EmailAddress == identity.Name);

            var model = new ProductForCustomerViewModel() { CustomerName = customer.Name };

            return View(model);
        }
    }
}
