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
    public class QuestionController : BaseController
    {
        public QuestionController(IRepository repo) : base(repo) { }
        public int PageSize = 4;
        public ViewResult List(string category,int page = 1)
        {
            QuestionListViewModel viewModel = new QuestionListViewModel
            {
                Questions = repository.Questions
                .Where(q=>category==null || q.Category==category)
                .OrderBy(p => p.QuestionId)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),
                Paginglnfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category==null?
                    repository.Questions.Count() :
                    repository.Questions.Where(e => e.Category == category).Count()
                },
                QuestionCategory = category
            };
            return View(viewModel);
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