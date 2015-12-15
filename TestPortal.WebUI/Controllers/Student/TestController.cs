using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestPortal.Domain;
using TestPortal.Domain.Entities;
using TestPortal.WebUI.Global.Auth;
using TestPortal.WebUI.Models;

namespace TestPortal.WebUI.Controllers
{
    [CustomAuthorize(Roles = "Admin,Teacher,Editor,Student")]
    public class TestController : BaseController
    {
        public TestController(IRepository repo) : base(repo) { }
        public ViewResult Index()
        {

            return View(repository.Tests);
        }
        public FileContentResult GetImage(int QuestionId)
        {
            Question quest =
                repository.Questions.FirstOrDefault(q => q.QuestionId == QuestionId);
            if (quest != null)
            {
                return File(quest.ImageData, quest.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}