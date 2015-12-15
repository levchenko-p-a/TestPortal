using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestPortal.WebUI.Startup))]
namespace TestPortal.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
