using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AniBaseFinal.Startup))]
namespace AniBaseFinal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
