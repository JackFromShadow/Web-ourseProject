using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebСourseProject.Startup))]
namespace WebСourseProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
