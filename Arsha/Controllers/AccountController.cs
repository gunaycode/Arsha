using Arsha.Models;
using Arsha.ViewModels.AccountVM;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Arsha.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Register(RegisterVM register)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser user = new AppUser()
            {
                Email = register.Email,
                Name = register.Name,
                Surname = register.Surname,
                UserName = register.UserName,
            };
            IdentityResult result = await _userManager.CreateAsync(user, register.Password);
            if (!result.Succeeded)
            {
                foreach (IdentityError? item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }
            IdentityResult resultrole = await _userManager.AddToRoleAsync(user, "SuperAdmin");
            
            if (!resultrole.Succeeded)
            {
                foreach (IdentityError? item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }
            
            return RedirectToAction("Login");
        }
        
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM login)
        {
            if (!ModelState.IsValid) { return View(); }
            AppUser user = await _userManager.FindByEmailAsync(login.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Incorrect");
                return View();
            }
          Microsoft.AspNetCore.Identity.SignInResult singInResult= await _signInManager.PasswordSignInAsync(user, login.Password, true, false);
            if(!singInResult.Succeeded)
            {
                ModelState.AddModelError("", "User or password incorrect");
                return View();
            } 
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        //public async Task<IActionResult> AddRole()
        //{
        //    IdentityRole role=new IdentityRole("SuperAdmin");
        //    await _roleManager.CreateAsync(role);
        //    return Json("ok");

        //}
    }
}
