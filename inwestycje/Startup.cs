using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(inwestycje.Startup))]
namespace inwestycje
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
