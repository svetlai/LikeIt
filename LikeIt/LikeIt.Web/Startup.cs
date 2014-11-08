using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LikeIt.Web.Startup))]
namespace LikeIt.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
