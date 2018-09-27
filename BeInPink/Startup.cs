using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BeInPink.Startup))]
namespace BeInPink
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
