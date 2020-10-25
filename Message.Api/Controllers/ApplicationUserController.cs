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
                UnitOfWork.ApplicationUserRepository.Add(user);
                UnitOfWork.Commit();
            }
            return Ok(ReturnValidationError());
        }
    }
}
