using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Magazine.Startup))]
namespace Magazine
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
