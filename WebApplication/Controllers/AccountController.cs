using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Models;
using WebApplication.Helpers;

namespace WebApplication.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;
        
        public AccountController(UserManager<IdentityUser> userMgr,
            SignInManager<IdentityUser> signInMgr, IConfiguration configuration)
        {
            _userManager = userMgr;
            _signInManager = signInMgr;
            _configuration = configuration;
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginModel
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var user =
                    await _userManager.FindByNameAsync(loginModel.Name);
                if (user != null)
                {
                    await _signInManager.SignOutAsync();
                    if ((await _signInManager.PasswordSignInAsync(user,
                        loginModel.Password, false, false)).Succeeded)
                    {
                        return Redirect(loginModel?.ReturnUrl ?? "/");
                    }
                }
            }

            ModelState.AddModelError("", "Invalid name or password");
            return View(loginModel);
        }

        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await _signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }

        public IActionResult AccessDenied(string returnUrl)
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View(new UserRegisterModel());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserRegisterModel userModel)
        {
            if (!ModelState.IsValid)
            { 
                return View(userModel); 
            }

            // Create new IdentityUser
            var user = new IdentityUser
            {
                UserName = userModel.Name,
                Email = userModel.Name,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);
            if (result.Succeeded)
            {
                var addClaimResult = await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("Customer", "Customer"));

                if (addClaimResult.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            return View();
        }
    }
}
