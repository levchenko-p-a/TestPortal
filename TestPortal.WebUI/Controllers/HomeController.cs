using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TestPortal.Domain;
using TestPortal.WebUI.Global.Auth;

namespace TestPortal.WebUI.Controllers
{
    [AllowAnonymous]
    [CustomAuthorize(Roles = "Admin,Teacher,Editor,Student")]
    public class HomeController : BaseController
    {
        public HomeController(IRepository repo) : base(repo) { }
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}