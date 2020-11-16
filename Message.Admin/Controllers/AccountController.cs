using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Message.Admin.Models;
using Microsoft.AspNetCore.Authentication;
using Message.Admin.Models.AccountViewModel;
using System.Security.Claims;

namespace Message.Admin.Controllers
{
    public class AccountController : BaseController
    {

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignIn(AccountCEViewModel model)
        {
            var user = UnitOfWork.ApplicationAdminRepository.GetAll().FirstOrDefault(p => p.Email == model.Email && p.Password == model.Password);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email,model.Email)
                };
                var licensClaims = new List<Claim>() {
                    new Claim(ClaimTypes.Email,"ihsanuguc.33@gmail.com"),
                    new Claim(ClaimTypes.Name,"İhsan Güç")
                };
                var userIdentity = new ClaimsIdentity(claims, "Customer");
                var licensIdentity = new ClaimsIdentity(licensClaims, "İhsan");
                var claimsPrincipal = new ClaimsPrincipal(new[] { userIdentity, licensIdentity });
                HttpContext.SignInAsync(claimsPrincipal);
                return RedirectToAction("Index", "Customer");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("SignIn", "Account");
        }
    }
}
