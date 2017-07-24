using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CDIPAlumniAssociation.Startup))]
namespace CDIPAlumniAssociation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
