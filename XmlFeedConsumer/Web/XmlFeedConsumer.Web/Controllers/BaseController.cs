namespace XmlFeedConsumer.Web.Controllers
{
    using System.Web.Mvc;

    using Ninject;

    using Services.Utils.Contracts;

    public abstract class BaseController : Controller
    {
        [Inject]
        public ICacheService Cache { protected get; set; }
    }
}