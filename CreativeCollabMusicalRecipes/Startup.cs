using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CreativeCollabMusicalRecipes.Startup))]
namespace CreativeCollabMusicalRecipes
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
