using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AttendenceSystem.Startup))]
namespace AttendenceSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
