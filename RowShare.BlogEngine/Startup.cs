using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RowShare.BlogEngine.Startup))]
namespace RowShare.BlogEngine
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
