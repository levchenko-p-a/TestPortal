using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TestPortal.Domain;
using TestPortal.Domain.Entities;
using TestPortal.WebUI.Global.Auth;
using TestPortal.WebUI.Models;

namespace TestPortal.WebUI.Controllers
{
    [AllowAnonymous]
    [CustomAuthorize(Roles = "Admin,Teacher,Editor,Student")]
    public class AccountController : BaseController
    {
        NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public AccountController(IRepository repo):base(repo){}
        //
        // GET: /Account/
        public ActionResult Index()
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            //проверяем успешность получения
            if (authCookie != null)
            {
                //расшифровываем аутификационный билет
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                //десериализуем класс для сериализации пользователя
                CustomPrincipalSerializeModel serializeModel = JsonConvert.DeserializeObject<CustomPrincipalSerializeModel>(authTicket.UserData);
                try
                {
                    var user = repository.Users.Where(u => u.UserId == serializeModel.UserId).FirstOrDefault();
                    if (user != null)
                    {
                        //преобразуем названия ролей в массив
                        var roles = user.Roles.Select(m => m.Title).ToArray();
                        if (roles.Contains("Admin"))
                        {
                            return RedirectToAction("Index", "Admin");
                        }
                        else if (roles.Contains("Student"))
                        {
                            return RedirectToAction("Index", "Student");
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
                    {
                        logger.Error("Object: " + validationError.Entry.Entity.ToString());
                        foreach (DbValidationError err in validationError.ValidationErrors)
                        {
                            logger.Error(err.ErrorMessage + " ");
                        }
                    }
                }
            }
            return View();
        }
        public ActionResult Register()
        {
            return View(new User());
        }
        private void CreateCookie(User user,bool persistent)
        {
            //создаём класс для сериализации
            CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel(user);
            //сериализуем пользователя
            string userData = JsonConvert.SerializeObject(serializeModel);
            DateTime expiration = persistent ? DateTime.Now.AddYears(15) : DateTime.Now.AddMinutes(15);
            //создаём аутификационный билет
            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                     1,
                     user.Login,
                     DateTime.Now,
                     expiration,
                     persistent,
                     userData);
            //шифруем аутификационный билет
            string encTicket = FormsAuthentication.Encrypt(authTicket);
            //создаём файл для ауторизации
            HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            //добавляем файл для авторизации в коллекцию кукисов
            Response.Cookies.Add(faCookie);
        }
        [HttpPost]
        public ActionResult Register(User user)
        {
            if (repository.Users.Any(u => u.Login == user.Login))
            {
                ModelState.AddModelError("", "Логин уже существует");
            }
            if (ModelState.IsValid)
            {
                Role role = repository.Roles.Where(r => r.Title == "Student").FirstOrDefault();
                user.Roles = new List<Role>();
                user.Roles.Add(role);
                user.IsActive = true;
                user.CreateDate = DateTime.Now;
                repository.SaveUser(user);
                TempData["message"] = string.Format("{0} был зарегистрирован", user.Name);
                CreateCookie(user, false);
                return RedirectToAction("Index", "Account", null);
            }
            else
            {
                return View(user);
            }
        }
        [HttpPost]
        //тут происходит аутификация (пользователь предоставляет свои данные)
        public ActionResult Index(LoginViewModel model, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //находим пользователя по логину и паролю
                    var user = repository.Users.Where(u => u.Login == model.Login && u.Password == model.Password).FirstOrDefault();
                    //проверяем найден ли пользователь
                    if (user != null)
                    {
                        //формируем соль
                        user.Salt = Membership.GeneratePassword(6, 2);
                        //сохъраняем соль в базе данных
                        repository.SaveUser(user);
                        CreateCookie(user, model.RememberMe);
                        return RedirectToAction("Index", "Account", null);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Неправильный логин и/или пароль");
                        return View(model);
                    }
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
                    {
                        logger.Error("Object: " + validationError.Entry.Entity.ToString());
                        foreach (DbValidationError err in validationError.ValidationErrors)
                        {
                            logger.Error(err.ErrorMessage + " ");
                        }
                    }
                }
            }
            return View(model);
        }
        public ActionResult LogOut()
        {
            var user = repository.Users.Where(u => u.UserId==User.UserId).FirstOrDefault();
            if (user != null) {
            user.Salt = Membership.GeneratePassword(6, 2);
            repository.SaveUser(user);
            }
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home", null);
        }
    }
}