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
    public class QuestionEditorController : BaseController
    {
        public QuestionEditorController(IRepository repo) : base(repo) { }

        // GET: Editor
        public ViewResult Index()
        {
            return View(repository.Questions);
        }
        public ViewResult Create()
        {
            return View("Edit", new Question());
        }
        public ViewResult Edit(int QuestionId)
        {
            Question quest =
            repository.Questions.FirstOrDefault(q => q.QuestionId == QuestionId);
            return View(quest);
        }
        [HttpPost]
        public ActionResult Edit(Question quest, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    quest.ImageMimeType = image.ContentType;
                    quest.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(quest.ImageData, 0, image.ContentLength);
                }
                repository.SaveQuestion(quest);
                TempData["message"] = string.Format("{0} был изменён", quest.Text);
                return RedirectToAction("Index");
            }
            else
            {
                return View(quest);
            }
        }
        [HttpPost]
        public ActionResult Delete(int QuestionId)
        {
            Question quest =
                repository.Questions.FirstOrDefault(q => q.QuestionId == QuestionId);
            if (quest != null)
            {
                repository.DeleteQuestion(quest);
                TempData["message"] = string.Format("{0} был удалён", quest.Text);
            }
            return RedirectToAction("Index");
        }
    }
}