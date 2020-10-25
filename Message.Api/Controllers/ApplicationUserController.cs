using Message.Api.Models.Request;
using Message.Data.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Message.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : BaseApiController
    {
        [HttpPost]
        [Route("register")]
        public ActionResult Register(RegisterRequest model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    Email = model.Email,
                    UserName = model.UserName,
                    Password = model.Password,
                    Id = Guid.NewGuid()
                };
                MessageContext.ApplicationUsers.Add(user);
                MessageContext.SaveChanges();
            }
            return Ok(ReturnValidationError());
        }
    }
}
