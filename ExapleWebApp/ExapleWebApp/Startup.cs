using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ExapleWebApp.Startup))]
namespace ExapleWebApp
{
    public partial class Startup
    {


        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
