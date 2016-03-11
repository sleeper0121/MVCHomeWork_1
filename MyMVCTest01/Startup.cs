using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyMVCTest01.Startup))]
namespace MyMVCTest01
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
