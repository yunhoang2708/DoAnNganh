using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebsiteBanGiayDep23.Startup))]
namespace WebsiteBanGiayDep23
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
