using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(D1_2_B2D3_Casus_Toetsgenerator.Startup))]
namespace D1_2_B2D3_Casus_Toetsgenerator
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
