using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebApplication_hw.ViewModels;

namespace WebApplication_hw.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthController(UserManager<AppUser> userManager)   //dependency injection
        {
            _userManager = userManager;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registeredVM)
        {
            if(!ModelState.IsValid) return View(registeredVM);

            AppUser appUser = new()
            {
                FullName = registeredVM.FullName,
                UserName = registeredVM.UserName,
                Email = registeredVM.Email,
                IsActive=true,
            };
            var identityResult= await _userManager.CreateAsync(appUser,registeredVM.Password);
            if (!identityResult.Succeeded)
            {
                foreach(var error in identityResult.Errors)
                {
                    ModelState.AddModelError("",error.Description);
                }
                return View(registeredVM);
            }
            return RedirectToAction(nameof(Login));

        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if(!ModelState.IsValid) return View(loginVM);
            var user = await _userManager.FindByEmailAsync(loginVM.UserNameOrEmail);
            if(user == null)
            {
                user=await _userManager.FindByEmailAsync(loginVM.UserNameOrEmail);
                if (user == null)
                {
                    ModelState.AddModelError("", "Username/Email or password incorrect");
                    return View(loginVM);
                }
            }
            var signInResult = await _signInManager.PasswordSignInAsync(user, loginVM.Password, loginVM.RememberMe, true);
            if (signInResult.IsLockedOut)
            {
                ModelState.AddModelError("", "Sonra gel");
                return View(loginVM);
            }
            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError("", "Username/Email or password incorrect");
                return View(loginVM);
            }
            if (!user.IsActive)
            {
                ModelState.AddModelError("", "not found");
                return View(loginVM);
            }
            return RedirectToAction("Index", "Home");
        }


    }
}
