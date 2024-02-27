using Microsoft.Owin;
using Owin;
using ShoppingDemoApp.DAL.Service;
using ShoppingDemoApp.DAL.Abstract;
using Autofac;
using ShoppingDemoApp.Web.AppSystem;

[assembly: OwinStartupAttribute(typeof(ShoppingDemoApp.Web.Startup))]
namespace ShoppingDemoApp.Web
{
    public partial class Startup
    {
        public IContainer ApplicationContainer { get; private set; }
        
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            AutofacConfig.ConfigureContainer();

        }
    }
}
