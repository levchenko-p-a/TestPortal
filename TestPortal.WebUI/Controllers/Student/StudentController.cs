using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestPortal.Domain;
using TestPortal.WebUI.Global.Auth;

namespace TestPortal.WebUI.Controllers
{
    [CustomAuthorize(Roles = "Admin,Teacher,Editor,Student")]
    public class StudentController : BaseController
    {
        public StudentController(IRepository repo) : base(repo) { }
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }
    }
}