using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using FirmaYonetimWeb.Entities;
using FirmaYonetimWeb.Models;

namespace FirmaYonetimWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }



        [HttpGet]
        public IActionResult SignIn()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }


        [HttpPost]

        public async Task<IActionResult> SignIn(SignInModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.KullaniciAdi);
                var signInResult = await _signInManager.PasswordSignInAsync(model.KullaniciAdi, model.Sifre, model.RememberMe, true);

                if (signInResult.Succeeded)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    if (User.IsInRole("Admin"))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else if (User.IsInRole("User"))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                else if (signInResult.IsLockedOut)
                {
                    var lockoutEnd = await _userManager.GetLockoutEndDateAsync(user);
                    var remainingTime = (lockoutEnd.Value.UtcDateTime - DateTime.UtcNow).Minutes;
                    ModelState.AddModelError("", $" Hesabınız kilitlendi. Lütfen {remainingTime} dakika sonra tekrar deneyin.");
                }

                var message = string.Empty;
                if (user != null)
                {
                    var failedCount = await _userManager.GetAccessFailedCountAsync(user);
                    message = $"{(_userManager.Options.Lockout.MaxFailedAccessAttempts - failedCount)} kez hatalı giriş yapma hakkınız kaldı";
                }
                else
                {
                    message = "Kullanıcı adı veya şifre hatalı.";
                }
                ModelState.AddModelError("", message);

            }
            return View(model);
        }

        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("SignIn", "Account");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}