using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BridgingTheGap.WebMVC.Startup))]
namespace BridgingTheGap.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
