using Common.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
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

        public async Task<IActionResult> Index()
        {
            var identity = HttpContext.User.Identity;

            var customer = Context.Customers.SingleOrDefault(customer => customer.EmailAddress == identity.Name);

            using var httpClient = new HttpClient();
            var productsList = await httpClient.GetFromJsonAsync<List<Product>>("https://localhost:44392/api/Product");

            var model = new ProductForCustomerViewModel() { CustomerName = customer.Name, Products = productsList };

            return View(model);
        }
    }
}
