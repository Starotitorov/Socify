using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(social_network.Startup))]
namespace social_network
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
