namespace XmlFeedConsumer.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper;

    using Bytes2you.Validation;

    using Common.Contracts;
    using Models.Home;
    using Services.Data.Contracts;
    using XmlFeedConsumer.Common;

    public class HomeController : BaseController
    {
        private const int MatchesToTake = 30;

        private readonly IManageData manageData;
        private readonly IMatchesService matchesService;

        public HomeController(IManageData manageData, IMatchesService matchesService)
        {
            Guard.WhenArgument(manageData, nameof(manageData)).IsNull().Throw();
            Guard.WhenArgument(matchesService, nameof(matchesService)).IsNull().Throw();

            this.manageData = manageData;
            this.matchesService = matchesService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            this.manageData.AddMatches(Constants.EntitiesToProcessed);

            // The data are cached for 1 minute (1 * 60 seconds)
            var latestMatches = this.Cache.Get(
                "latestMatches",
                () => this.matchesService.GetLatest(MatchesToTake)
                    .Select(Mapper.DynamicMap<MatchViewModel>)
                    .ToList(),
                1 * 60); 

            return this.View(latestMatches);
        }
    }
}