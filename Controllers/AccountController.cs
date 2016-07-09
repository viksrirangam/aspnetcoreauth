using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using aspnetcoreauth.Core.Security;
using aspnetcoreauth.ViewModel;

namespace aspnetcoreauth.Controllers
{
    public class AccountController : Controller
    {
        private readonly IIdentityHelper _identityHelper;

        public AccountController(IIdentityHelper identityHelper)
        {
            _identityHelper = identityHelper;
        }

        public IActionResult Login([FromQuery]string ReturnUrl)
        {
            var loginInfo = new LoginInfo(){ReturnUrl = "/"};

            if(!string.IsNullOrEmpty(ReturnUrl)){
                loginInfo.ReturnUrl = ReturnUrl;
            }

            return View(loginInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginInfo loginInfo)
        {
            if(ModelState.IsValid){
                //assuming user name and password is validated. Now create identity.
                var Claims = new Dictionary<string, string>(){
                        ["EmployeeNumber"] = "1221"
                    };

                var principal = _identityHelper.CreateIdentity(loginInfo.UserName, "", Claims);

                await HttpContext.Authentication.SignInAsync("aspnetcoreauth", principal);
            }

            return RedirectToAction("Update", "Home");
        }

        public async Task<IActionResult> LogOff()
        {
            await HttpContext.Authentication.SignOutAsync("aspnetcoreauth");

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Forbidden()
        {
            return View();
        }
    }
}
