using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DB.Startup))]
namespace DB
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
