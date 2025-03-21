using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EShopApplication.Domain.DomainModels;
using EShopApplication.Domain.DTO;
using EShopApplication.Domain.IdentityModels;

namespace EShopApplication.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<EShopApplicationUser> userManager;
        private readonly SignInManager<EShopApplicationUser> signInManager;
        
        public AccountController(UserManager<EShopApplicationUser> userManager,
            SignInManager<EShopApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet, AllowAnonymous]
        public IActionResult Register()
        {
            return View(new UserRegistrationDto());
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Register(UserRegistrationDto request)
        {
            if (!ModelState.IsValid) return View(request);

            var userCheck = await userManager.FindByEmailAsync(request.Email);
            if (userCheck != null)
            {
                ModelState.AddModelError("message", "Email already exists.");
                return View(request);
            }

            var user = new EShopApplicationUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Address = request.Address,
                UserName = request.Email,
                NormalizedUserName = request.Email.ToUpper(),
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                ShoppingCart = new ShoppingCart()
            };

            var result = await userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Login");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("message", error.Description);
            }

            return View(request);
        }

        [HttpGet, AllowAnonymous]
        public IActionResult Login()
        {
            return View(new UserLoginDto());
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDto model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null || !await userManager.CheckPasswordAsync(user, model.Password))
            {
                ModelState.AddModelError("message", "Invalid credentials");
                return View(model);
            }

            var result = await signInManager.PasswordSignInAsync(user.UserName, model.Password, true, true);
            if (result.Succeeded)
            {
                await userManager.AddClaimAsync(user, new Claim("UserRole", "Admin"));
                return RedirectToAction("Index", "Products");
            }

            ModelState.AddModelError("message", "Invalid login attempt");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
