using Common.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using WebApplication.Helpers;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Authorize(Policy = "CustomerOnly")]
    public class ProductController : Controller
    {
        private readonly IConfiguration _configuration;

        public ProductController(CustomerContext context, IConfiguration configuration)
        {
            Context = context;
            _configuration = configuration;
        }

        public CustomerContext Context { get; }

        public async Task<IActionResult> Index()
        {
            var identity = HttpContext.User.Identity;

            var customer = Context.Customers.SingleOrDefault(customer => customer.EmailAddress == identity.Name);

            using var httpClient = new HttpClient();

            // Fetch the login token. It is not recommended to pass the username and password in code, but just for demo purposes in this code and this is not a real system and a real password.
            var signInResponse = await httpClient.PostAsJsonAsync<SignInRequest>("https://localhost:44392/api/signin", new SignInRequest { Email = _configuration.GetValue<string>("ApiCredentials:UserName"), 
                Password = _configuration.GetValue<string>("ApiCredentials:Password") });

            // Read the response string as string
            var responseRaw = await signInResponse.Content.ReadAsStringAsync();

            // And deserialize the string into the typed object.
            var typedResponse = JsonSerializer.Deserialize<SignInResponse>(responseRaw);

            if (signInResponse.IsSuccessStatusCode)
            {
                // Set the bearer token on the request. 
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", typedResponse.token);

                // We can now request the response from the json.
                var productsList = await httpClient.GetFromJsonAsync<List<Product>>("https://localhost:44392/api/Product");
                var model = new ProductForCustomerViewModel() { CustomerName = customer.Name, Products = productsList };

                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
