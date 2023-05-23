using Arsha.Models;
using Arsha.ViewModels.AccountVM;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Arsha.Controllers
{
    public class AccountController:Controller
    {

        private readonly UserManager<AppUser> _userManager;
        public AccountController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
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
            AppUser appUser = new AppUser()
            {
                Email = register.Email,
                Name = register.Name,
                Surname = register.Surname,
                UserName = register.UserName,
            };
            IdentityResult result=await _userManager.CreateAsync(appUser);
            if(!result.Succeeded) 
            { 
                foreach(IdentityError? item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }
            return RedirectToAction("Login");
        }

        
    }
}
