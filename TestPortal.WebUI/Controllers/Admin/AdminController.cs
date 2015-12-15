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
    [CustomAuthorize(Roles = "Admin")]
    public class AdminController : BaseController
    {
        public AdminController(IRepository repo) : base(repo) { }
        //
        // GET: /Admin/
        public ActionResult Index()
        {
            return View();
        }

        public int PageSize = 2;

        public ViewResult UserList(String role, int page = 1)
        {
            UserListViewModel viewModel = new UserListViewModel
            {
                Users = repository.Users
                .Where(u => role == null || u.Roles.Any(r=>r.Title==role))
                .OrderBy(p => p.CreateDate)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),
                Paginglnfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = role == null ?
                    repository.Users.Count() :
                    repository.Users.Where(u => u.Roles.Any(r => r.Title == role)).Count()
                },
                Role = role
            };
            return View(viewModel);
        }
    }
}