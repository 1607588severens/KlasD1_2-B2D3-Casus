using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(D2_B2D3_Toetsgenerator.Startup))]
namespace D2_B2D3_Toetsgenerator
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
