using Newtonsoft.Json;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using TestPortal.Domain;
using TestPortal.WebUI.App_Start;
using TestPortal.WebUI.Global.Auth;

namespace TestPortal.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected NinjectControllerFactory factory = new NinjectControllerFactory();
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ControllerBuilder.Current.SetControllerFactory(factory);
        }
        //тут происходит авторизация после аутификации
        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            //получаем файл для авторизации
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            //проверяем успешность получения
            if (authCookie != null)
            {
                //расшифровываем аутификационный билет
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                //десериализуем класс для сериализации пользователя
                CustomPrincipalSerializeModel serializeModel = JsonConvert.DeserializeObject<CustomPrincipalSerializeModel>(authTicket.UserData);
                //получаем репозиторий
                IRepository repository = factory.getRepository();
                //получаем пользователя по десериализованному ид
                var user = repository.Users.Where(u => u.UserId == serializeModel.UserId).FirstOrDefault();
                //проверяем найден ли пользователь и сравниваем соль с билета с солью в базе данных
                if (user != null && serializeModel.Salt == user.Salt)
                {
                    //формируем новую соль
                    user.Salt = Membership.GeneratePassword(6, 2);
                    //сохраняем соль в базе данных
                    repository.SaveUser(user);
                    //создаём новый объект безопасности для пользователя
                    CustomPrincipal newUser = new CustomPrincipal(user);
                    //создёем новый класс для сериализации
                    serializeModel = new CustomPrincipalSerializeModel(newUser);
                    //сериализуем данные пользователя
                    string userData = JsonConvert.SerializeObject(serializeModel);
                    //создаём новый аутификационный билет
                    authTicket = new FormsAuthenticationTicket(
                    authTicket.Version+1,
                    authTicket.Name,
                    DateTime.Now,
                    authTicket.Expiration.AddMinutes(15),
                    authTicket.IsPersistent,
                    userData);
                    //шифруем билет
                    string encTicket = FormsAuthentication.Encrypt(authTicket);
                    //создаём новый файл для ауторизации
                    HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                    //добавляем файл для авторизации в коллекцию кукисов
                    Response.Cookies.Add(faCookie);
                    //авторизируем пользователя
                    HttpContext.Current.User = newUser;
                }
                else
                {
                    FormsAuthentication.SignOut();
                    HttpContext.Current.User = null;
                }
            }
        }
    }
}
