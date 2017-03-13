using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC5WebWork.Startup))]
namespace MVC5WebWork
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
