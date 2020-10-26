using Message.Api.Models.Request;
using Message.Api.Models.Response;
using Message.Data.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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
                    TokenString = Token(user.UserName, user.Email)
                };
                user.TokenId = token.Id;

                UnitOfWork.ApplicationUserRepository.Add(user);
                UnitOfWork.TokenRepository.Add(token);
                UnitOfWork.Commit();
                return Ok(new RegisterResponse() { IsSuccess = true, Message = "Başarılı", Token = token.TokenString});
            }
            return Ok(ReturnValidationError());
        }

        public string Token(string userName, string email)
        {
            var token = new JwtSecurityToken(
                issuer: "Message Test",
                audience: userName,
                expires: DateTime.Now,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(email + "Test Verisidir")),
                    SecurityAlgorithms.HmacSha256Signature)
                );
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        /// <summary>
        /// İstek için api/ApplicationUser/Login
        /// </summary>
        /// <param name="Login"></param>
        [HttpPost]
        [Route("login")]
        public ActionResult<LoginResponse> Login(LoginRequest model)
        {
            var user = UnitOfWork.ApplicationUserRepository.GetQueryable().Where(p => p.Email == model.Email && p.Password == model.Password).FirstOrDefault();
            if (user == null)
                return Ok(ReturnValidationError());
            else
            {
                var token = UnitOfWork.TokenRepository.GetById(user.TokenId);
                token.TokenString = Token(user.UserName, user.Email);
                UnitOfWork.Commit();
                return Ok(new LoginResponse() { IsSuccess = true, Token = token.TokenString , Message = "Başarılı",
                Email = user.Email, FirstName = user.FirstName , LastName = user.LastName, UserName = user.UserName});
            }
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
                return Ok(new BaseResponse() { IsSuccess = true });
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
                return Ok(new BaseResponse() { IsSuccess = true ,Message = "Başarılı"});
            }
            return Ok(ReturnValidationError());
        }
    }
}
