using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TicketsMVC.Startup))]
namespace TicketsMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
