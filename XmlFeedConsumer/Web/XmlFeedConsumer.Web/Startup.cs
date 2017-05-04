using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(XmlFeedConsumer.Web.Startup))]
namespace XmlFeedConsumer.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        { 
            app.MapSignalR();
        }
    }
}