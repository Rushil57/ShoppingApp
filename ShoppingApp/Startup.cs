using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShoppingDemoApp.Startup))]
namespace ShoppingDemoApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
