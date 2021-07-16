using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(servicioweb.Startup))]
namespace servicioweb
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
