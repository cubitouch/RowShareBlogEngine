using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RowShareBlogEngine.Startup))]
namespace RowShareBlogEngine
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
