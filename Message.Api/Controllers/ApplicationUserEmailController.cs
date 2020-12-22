using Message.Api.Core;
using Message.Api.Models.Request;
using Message.Api.Models.Response;
using Message.Data.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Message.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserEmailController : BaseApiController
    {
        IChangePasswordCode ChangePasswordCode;
        public ApplicationUserEmailController(IChangePasswordCode _changePasswordCode)
        {
            ChangePasswordCode = _changePasswordCode;
        }
        /// <summary>
        /// Şifre değiştirmek için code gönderir.
        /// </summary>
        /// <param name="ChangePassword"></param>
        /// Post: api/ApplicationUserEmail/changePassword
        [HttpPost]
        [Route("changePassword")]
        public ActionResult ChangePassword(ChangePasswordRequest model)
        {
            if (ModelState.IsValid)
            {
                var entity = new ApplicationUserEmail()
                {
                    Id = Guid.NewGuid(),
                    Email = model.Email,
                    ChangePasswordText = ChangePasswordCode.PasswordCodeCreate(model.Email),
                };
                var user = UnitOfWork.ApplicationUserRepository.GetQueryable().Where(p => p.Email == entity.Email).FirstOrDefault();
                user.Password = entity.ChangePasswordText;
                UnitOfWork.ApplicationUserEmailRepository.Add(entity);
                UnitOfWork.Commit();
                var response = new ChangePasswordResponse();
                response.IsSuccess = true;
                response.Message = "Başarılı";
                if (ChangePasswordCode.SendEmail(entity.Email,entity.ChangePasswordText))
                    response.ChangePasswordMessage = "Şifrenin Email'e Gönderilmedi";
                else
                    response.ChangePasswordMessage = "Email Gönderildi";
                return Ok(response);
            }
            return Ok(ReturnValidationError());
        }
    }
}
