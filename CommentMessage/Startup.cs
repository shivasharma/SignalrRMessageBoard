using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CommentMessage.Startup))]
namespace CommentMessage
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureAuth(app);
        }
    }
}
