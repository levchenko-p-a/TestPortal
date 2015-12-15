using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestPortal.Domain;
using TestPortal.WebUI.Global.Auth;

namespace TestPortal.WebUI.Controllers
{
    [CustomAuthorize(Roles = "root")]
    public class BaseController : Controller
    {
        protected IRepository repository;
        protected BaseController(IRepository repo)
        {
            repository = repo;
        }
        protected virtual new CustomPrincipal User
        {
            get { return HttpContext.User as CustomPrincipal; }
        }
    }
}