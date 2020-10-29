using Message.Api.Core;
using Message.Api.Models.Request;
using Message.Api.Models.Response;
using Message.Data.Domain.Entities;
using Microsoft.AspNetCore.Http;
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
        ITokenGenerator TokenGenerator;
        IUploadFile UploadFile;
        public ApplicationUserController(ITokenGenerator tokenGenerator, IUploadFile uploadFile)
        {
            TokenGenerator = tokenGenerator;
            UploadFile = uploadFile;
        }
        /// <summary>
        /// Kullanıcı Kayıt eder
        /// </summary>
        /// <param name="Register"></param>
        /// Post: api/ApplicationUser/register
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
                    TokenString = TokenGenerator.Token(user.UserName, user.Email)
                };
                user.TokenId = token.Id;

                UnitOfWork.ApplicationUserRepository.Add(user);
                UnitOfWork.TokenRepository.Add(token);
                UnitOfWork.Commit();
                return Ok(new RegisterResponse() { IsSuccess = true, Message = "Başarılı", Token = token.TokenString});
            }
            return Ok(ReturnValidationError());
        }
        
        /// <summary>
        /// Kullanıcı Girişi
        /// </summary>
        /// <param name="Login"></param>
        /// Post: api/ApplicationUser/login
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
                token.TokenString = TokenGenerator.Token(user.UserName, user.Email);
                UnitOfWork.Commit();
                return Ok(new LoginResponse() { IsSuccess = true, Token = token.TokenString , Message = "Başarılı",
                Email = user.Email, FirstName = user.FirstName , LastName = user.LastName, UserName = user.UserName});
            }
        }

        /// <summary>
        /// Resim ekler
        /// </summary>
        /// <param name="updateProfilePhotoRequest"></param>
        /// Post: api/ApplicationUser/UploadFileImage
        [HttpPost]
        [Route("UploadFileImage")]
        public ActionResult UploadFileImage([FromForm] UpdateProfilePhotoRequest updateProfilePhotoRequest)
        {
            UploadFile.UploadFileImage(updateProfilePhotoRequest.File,updateProfilePhotoRequest.Id);
            return Ok();
        }

        /// <summary>
        /// Kullanıcı Günceller
        /// </summary>
        /// <param name="ApplicationUserUpdate"></param>
        /// Post: api/ApplicationUser/applicationUserUpdate
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
        /// Kullanıcı Siler
        /// </summary>
        /// <param name="Delete"></param>
        /// Post: api/ApplicationUser/delete
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
        /// <summary>
        /// Kullanıcı Profil
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("getprofile")]
        public ActionResult GetProfileUser(GetProfileRequest getProfileRequest)
        {
            if (ModelState.IsValid)
            {
                var profile = UnitOfWork.ApplicationUserRepository.GetById(getProfileRequest.UserId);
                if (profile != null)
                {
                    var response = new GetProfileResponse()
                    {
                        Email = profile.Email,
                        FirstName = profile.FirstName,
                        LastName = profile.LastName,
                        UserName = profile.UserName,
                        Message = "Başarılı",
                        IsSuccess = true,
                    };
                    return Ok(response);
                }
            }
            return Ok(ReturnValidationError());
        }
    }
}
