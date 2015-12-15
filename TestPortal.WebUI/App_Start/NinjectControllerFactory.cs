using Ninject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TestPortal.Domain;
using TestPortal.WebUI.Global.Auth;

namespace TestPortal.WebUI.App_Start
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;
        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }
        protected override IController GetControllerInstance(
            RequestContext requestContext, Type controllerType)
        {
            return controllerType == null
                ? null : (IController)ninjectKernel.Get(controllerType);
        }
        private void AddBindings()
        {
            //ninjectKernel.Bind<TestContext>().ToMethod(c => new TestContext(ConfigurationManager.ConnectionStrings["TestContext"].ConnectionString));
            ninjectKernel.Bind<IRepository>().To<TestRepository>();
        }
        public IRepository getRepository(){
            return ninjectKernel.Get<IRepository>();
        }
    }
}