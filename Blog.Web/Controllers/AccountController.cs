using Blog.Web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> userManager;
    public AccountController(UserManager<IdentityUser> userManager)
    {
        this.userManager = userManager;
    }

    public UserManager<IdentityUser> Usermanager { get; }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
    {
        var identityUser = new IdentityUser
        {
            UserName = registerViewModel.Username,
            Email = registerViewModel.Email
        };

        var identityResult = await userManager.CreateAsync(identityUser, registerViewModel.Password);

        if (identityResult.Succeeded)
        {
            //relacionar user ao papel user
            var roleIdentityResult = await userManager.AddToRoleAsync(identityUser, "User");

            if (roleIdentityResult.Succeeded)
            {
                Console.WriteLine("login successfull");
                return RedirectToAction("Register");
            }
            
        }
        //acrescentar configuração do identity;;;
        return View();
    }
}
