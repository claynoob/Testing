using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCSports.Startup))]
namespace MVCSports
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
