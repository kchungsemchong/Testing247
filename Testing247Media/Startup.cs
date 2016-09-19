using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Testing247Media.Startup))]
namespace Testing247Media
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
