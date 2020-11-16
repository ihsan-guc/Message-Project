using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Message.Admin.Models;
using Microsoft.AspNetCore.Authentication;
using Message.Data.DAL.Repository.Core;

namespace Message.Admin.Controllers
{
    public class BaseController : Controller
    {
        public IUnitOfWork _UnitOfWork { get; set; }
        public IUnitOfWork UnitOfWork
        {
            get
            {
                if (_UnitOfWork == null)
                    _UnitOfWork = (IUnitOfWork)HttpContext.RequestServices.GetService(typeof(IUnitOfWork));
                return _UnitOfWork;
            }
        }
    }
}
