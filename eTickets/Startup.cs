using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(eTickets.Startup))]
namespace eTickets
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
