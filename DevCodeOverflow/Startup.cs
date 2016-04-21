using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DevCodeOverflow.Startup))]
namespace DevCodeOverflow
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
