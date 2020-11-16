using Message.Admin.Models.ApplicaitonUserViewModel;
using Message.Api.Core;
using Message.Data.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using X.PagedList;

namespace Message.Admin.Controllers
{
    [Route("ApplicationUser")]
    [Authorize]
    public class ApplicationUserController : BaseController
    {
        private readonly ILogger<ApplicationUserController> _logger;
        IUploadFile UploadFile;
        public IWebHostEnvironment WebHostEnvironment;
        public ApplicationUserController(ILogger<ApplicationUserController> logger, IUploadFile uploadFile,
            IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            UploadFile = uploadFile;
            WebHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        [Route("Index")]
        public IActionResult Index(int? page)
        {
            var pagenumber = page ?? 1;
            int pagesize = 3;
            var pagelist = UnitOfWork.ApplicationUserRepository.GetQueryable().ToPagedList(pagenumber, pagesize);
            return View(pagelist);
        }

        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult CreateConfirmed(ApplicationUserCEViewModel model)
        {
            if (ModelState.IsValid)
            {
                var applicationUser = new ApplicationUser()
                {
                    Id = Guid.NewGuid(),
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Password = model.Password,
                    UserName = model.UserName,
                    Email = model.Email,
                };
                if (model.Image != null)
                {
                    applicationUser.Image = UploadFile.UploadFileImage(model.Image, applicationUser.Id);
                }
                UnitOfWork.ApplicationUserRepository.Add(applicationUser);
                UnitOfWork.Commit();
                return RedirectToAction("Index", "ApplicationUser");
            }
            return View();
        }

        [Route("Edit")]
        public IActionResult Edit(Guid id)
        {
            var model = UnitOfWork.ApplicationUserRepository.GetById(id);
            var ApplicationUser = new ApplicationUserCEViewModel()
            {
                Id = model.Id,
                UserName = model.UserName,
                LastName = model.LastName,
                FirstName = model.FirstName,
                Password = model.Password,
                Email = model.Email,
            };
            return View(ApplicationUser);
        }
        [HttpPost]
        [Route("Edit")]
        public IActionResult EditConfirmed(ApplicationUserCEViewModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = UnitOfWork.ApplicationUserRepository.GetById(model.Id);
                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;
                entity.UserName = model.UserName;
                entity.Email = model.Email;
                if (model.Image != null)
                {
                    entity.Image = UploadFile.UploadFileImage(model.Image, model.Id);
                }
                UnitOfWork.Commit();
                return RedirectToAction("Index", "ApplicationUser");
            }
            return View();
        }
        [Route("Details")]
        public IActionResult Details(Guid id)
        {
            var entity = UnitOfWork.ApplicationUserRepository.GetById(id);
            return View(entity);
        }
        [Route("ApplicationDelete")]
        public IActionResult ApplicationDelete(Guid id)
        {
            var entity = UnitOfWork.ApplicationUserRepository.GetById(id);
            return View(entity);
        }
        [HttpPost]
        [Route("Delete")]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var entity = UnitOfWork.ApplicationUserRepository.GetById(id);
            var message = UnitOfWork.UserMessageRepository.GetQueryable()
                .Where(p => p.Id == entity.Id).ToList();
            UnitOfWork.ApplicationUserRepository.Delete(entity);
            UnitOfWork.UserMessageRepository.DeleteRange(message);
            UnitOfWork.Commit();
            return RedirectToAction("Index", "ApplicationUser");
        }
    }
}
