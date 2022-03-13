using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetCoreIdentity.Web.Models;
using NetCoreIdentity.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreIdentity.Web.Controllers
{

    // Yazdığınız Attribute lar üzerinden Client Side Validation yapılmasını istiyorsan
    // Client Side Validation için "jquery-validation" ve "jquery-validation-unobtrusive" kütüphanesi eklemelidir.
    // Proje'ye Sağ Tık > Add > Client-Side Library  => "jquery-validate" & "jquery-validation-unobtrusive" 
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UserManager<AppUser> _userManager;                // Identity kütüphanesinden otomatik gelen class
        private SignInManager<AppUser> _signInManager;            // Identity kütüphanesinden otomatik gelen class

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {

            }

            AppUser user = await _userManager.FindByEmailAsync(loginViewModel.Email);

            if (user != null)
            {
                await _signInManager.SignOutAsync();  // Kullanıcı tekrar Login olacağı için Kullanıcı ile ilgili cookie varsa sil demektir. Yani çıkış yapılıyor

               // SignInResult await _signInManager.PasswordSignInAsync(user, loginViewModel.Password,false,false);  // Tekrardan giriş yapılıyor
            } 

            return View();
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }


        // Yazdığınız Attribute lar üzerinden Client Side Validation yapılmasını istiyorsan
        // Client Side Validation için "jquery-validation" ve "jquery-validation-unobtrusive" kütüphanesi eklemelidir.
        // Proje'ye Sağ Tık > Add > Client-Side Library  => "jquery-validate" & "jquery-validation-unobtrusive" 

        [HttpPost]
        public async Task<IActionResult> SignUp(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    UserName = userViewModel.UserName,
                    Email = userViewModel.Email,
                    PhoneNumber = userViewModel.PhoneNumber
                };
                IdentityResult result =  await _userManager.CreateAsync(user, userViewModel.Password);

                if (!result.Succeeded)
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
                else
                {
                    return RedirectToAction("Login");

                }

            }
            return View(userViewModel);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
