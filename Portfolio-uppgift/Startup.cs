using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Portfolio_uppgift.Startup))]
namespace Portfolio_uppgift
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
