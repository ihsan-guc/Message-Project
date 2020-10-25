using Message.Api.Models.Request;
using Message.Data.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Message.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : BaseApiController
    {
        /// <summary>
        /// İstek için api/ApplicationUser/Register
        /// </summary>
        /// <param name="Register"></param>
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
                
                var token = new Token()
                {
                    Id = Guid.NewGuid(),
                    ApplicationUserId = user.Id,
                    TokenString = Token(user.UserName,user.Email)
                };
                user.TokenId = token.Id;

                UnitOfWork.ApplicationUserRepository.Add(user);
                UnitOfWork.TokenRepository.Add(token);
                UnitOfWork.Commit();
            }
            return Ok(ReturnValidationError());
        }

        public string Token(string userName, string email)
        {
            var token = new JwtSecurityToken(
                issuer: "Test",
                audience: userName,
                expires: DateTime.Now,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(email)),
                    SecurityAlgorithms.HmacSha256Signature)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// İstek için api/ApplicationUser/ApplicationUserUpdate
        /// </summary>
        /// <param name="ApplicationUserUpdate"></param>
        [HttpPost]
        [Route("applicationUserUpdate")]
        public ActionResult ApplicationUserUpdate(ApplicaitionUserUpdateRequest model)
        {
            if (ModelState.IsValid)
            {
                var user = UnitOfWork.ApplicationUserRepository.GetById(model.Id);
                user.UserName = model.UserName;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Password = model.Password;
                user.Email = model.Email;
                UnitOfWork.Commit();
            }
            return Ok(ReturnValidationError());
        }

        /// <summary>
        /// İstek için api/ApplicationUser/delete
        /// </summary>
        /// <param name="Delete"></param>
        [HttpDelete]
        [Route("delete")]
        public ActionResult Delete(Guid id)
        {
            if (ModelState.IsValid)
            {
                var user = UnitOfWork.ApplicationUserRepository.GetById(id);
                var token = UnitOfWork.TokenRepository.GetById(id);
                UnitOfWork.ApplicationUserRepository.Delete(user);
                UnitOfWork.TokenRepository.Delete(token);
                UnitOfWork.Commit();
            }
            return Ok(ReturnValidationError());
        }
    }
}
