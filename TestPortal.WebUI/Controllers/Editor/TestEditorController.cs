using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestPortal.Domain;
using TestPortal.Domain.Entities;
using TestPortal.WebUI.Global.Auth;

namespace TestPortal.WebUI.Controllers
{
    [CustomAuthorize(Roles = "Admin,Teacher,Editor")]
    public class TestEditorController : BaseController
    {
        public TestEditorController(IRepository repo) : base(repo) { }

        public ViewResult Index()
        {
            return View();
        }
        
        public PartialViewResult IndexAjax(string SelectTest="All")
        {
            var cookie = new HttpCookie("test_cookie")
            {
                Value = DateTime.Now.ToString("dd.MM.yyyy"),
                Expires = DateTime.Now.AddMinutes(10),
            };
            Response.SetCookie(cookie);
            IEnumerable<Test> tests = repository.Tests;
            if (SelectTest != "All" & SelectTest!="")
            {
                tests = repository.Tests.
                    Where(s => s.Name.ToUpper().StartsWith(SelectTest.ToUpper()));
            }
            return PartialView(tests);
        }
        // GET: Editor/Details/5
        public ViewResult Details(int TestId)
        {
            return View();
        }

        // GET: Editor/Create
        public ViewResult Create()
        {
            return View("Edit", new Test());
        }


        // GET: Editor/Edit/5
        public ViewResult Edit(int TestId)
        {
            Test test =
            repository.Tests.FirstOrDefault(t => t.TestId == TestId);
            return View(test);
        }

        // POST: Editor/Edit/5
        [HttpPost]
        public ActionResult Edit(Test test)
        {
            if(ModelState.IsValid)
            {
                repository.SaveTest(test);
                TempData["message"] = string.Format("{0} был изменён", test.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Delete(int TestId)
        {
            Test test =
                repository.Tests.FirstOrDefault(t => t.TestId == TestId);
            if (test != null)
            {
                repository.DeleteTest(test);
                TempData["message"] = string.Format("{0} был удалён", test.Name);
            }
            return RedirectToAction("Index");
        }
    }
}
