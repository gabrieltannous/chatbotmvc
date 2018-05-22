using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ChatBot_MVC.Startup))]
namespace ChatBot_MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
