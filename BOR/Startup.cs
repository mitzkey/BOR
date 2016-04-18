using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BOR.Startup))]
namespace BOR
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
