namespace XmlFeedConsumer.Web
{
    using System.Web.Mvc;
    using System.Web.Routing;

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "DefaultGetByXmlId",
               url: "{controller}/{xmlId}",
               defaults: new { action = "Get" },
               constraints: new { xmlId = @"\d+" },
               namespaces: new[] { "XmlFeedConsumer.Web.Controllers" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/",
                defaults: new { controller = "Home", action = "Index" },
                namespaces: new[] { "XmlFeedConsumer.Web.Controllers" });
        }
    }
}
