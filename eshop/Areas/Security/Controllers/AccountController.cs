using eshop.Controllers;
using eshop.Models;
using eshop.Models.ApplicationServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Areas.Security.Controllers
{
    [Area("Security")]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        ISecurityApplicationService iSecure;

        public AccountController(ISecurityApplicationService iSecure)
        {
            this.iSecure = iSecure;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            @ViewData["Failed"] = string.Empty;
            vm.LoginFailed = false;
            if (ModelState.IsValid)
            {
                bool isLogged = await iSecure.Login(vm);
                if (isLogged)
                {
                    return RedirectToAction(nameof(HomeController.Index),nameof(HomeController).Replace("Controller", string.Empty),new { area = ""});
                }
                else
                {
                    vm.LoginFailed = true;
                    @ViewData["Failed"] = "Wrong username and password";
                }
            }
            return View(vm);
        }

        public IActionResult Logout()
        {
            iSecure.Logout();
            return RedirectToAction(nameof(Login));
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            vm.ErrorsDuringRegister = null;
            if (ModelState.IsValid)
            {
                vm.ErrorsDuringRegister = await iSecure.Register(vm, Models.Identity.Roles.Customer);
                if (vm.ErrorsDuringRegister == null)
                {
                    var lvm = new LoginViewModel()
                    {
                        Username = vm.Username,
                        Password = vm.Password,
                        RememberMe = true,
                        LoginFailed = false
                    };

                    return await Login(lvm);

                }
            }
            @ViewData["Error"] = vm.ErrorsDuringRegister[0];
            return View(vm);
        }
    }
}
