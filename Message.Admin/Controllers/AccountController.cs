using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Message.Admin.Models;
using Microsoft.AspNetCore.Authentication;

namespace Message.Admin.Controllers
{
    public class AccountController : Controller
    {

        [HttpGet]
        public IActionResult SignIn()
        {
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
