using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjectManager.WebApp.Startup))]
namespace ProjectManager.WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}