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
    public class ReplyEditorController : BaseController
    {
        public ReplyEditorController(IRepository repo) : base(repo) { }

        public ViewResult Index(int QuestionId)
        {
            var replies =
            repository.Replies.Select(r => r.QuestionId == QuestionId);
            return View(repository.Replies);
        }
        public ViewResult Create(int QuestionId)
        {
            return View("Edit", new Reply() { QuestionId = QuestionId });
        }
        public ViewResult Edit(int ReplyId)
        {
            Reply reply =
            repository.Replies.FirstOrDefault(r => r.ReplyId == ReplyId);
            return View(reply);
        }
        [HttpPost]
        public ActionResult Edit(Reply reply)
        {
            if (ModelState.IsValid)
            {
                repository.SaveReply(reply);
                TempData["message"] = string.Format("{0} был изменён", reply.Text);
                return RedirectToAction("Index", new { reply.QuestionId });
            }
            else
            {
                return View(reply);
            }
        }
        [HttpPost]
        public ActionResult Delete(int ReplyId)
        {
            Reply reply =
                repository.Replies.FirstOrDefault(r => r.ReplyId == ReplyId);
            if (reply != null)
            {
                repository.DeleteReply(reply);
                TempData["message"] = string.Format("{0} был удалён", reply.Text);
            }
            return RedirectToAction("Index", "QuestionEditor", new { reply.QuestionId });
        }
    }
}