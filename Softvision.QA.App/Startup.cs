using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Softvision.QA.App.Startup))]
namespace Softvision.QA.App
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
