using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SummerCamp2017.Startup))]
namespace SummerCamp2017
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
