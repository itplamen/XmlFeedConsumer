namespace XmlFeedConsumer.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using Bytes2you.Validation;

    using Common;
    using Data.Models;
    using Services.Data.Contracts;
    using Services.Utils.Contracts;

    public class HomeController : Controller
    {
        private const int EntitiesToProcessed = 10;

        private readonly IXmlParserService xmlParserService;
        private readonly IMatchesService matchesService;
        private readonly IBetsService betsService;
        private readonly IOddsService oddsService;

        private List<Match> newMatches;
        private HashSet<int> existMatchXmlIds;

        public HomeController(IXmlParserService xmlParserService, IMatchesService matchesService, IBetsService betsService, IOddsService oddsService)
        {
            Guard.WhenArgument(xmlParserService, nameof(xmlParserService)).IsNull().Throw();
            Guard.WhenArgument(matchesService, nameof(matchesService)).IsNull().Throw();
            Guard.WhenArgument(betsService, nameof(betsService)).IsNull().Throw();
            Guard.WhenArgument(oddsService, nameof(oddsService)).IsNull().Throw();

            this.xmlParserService = xmlParserService;
            this.matchesService = matchesService;
            this.betsService = betsService;
            this.oddsService = oddsService;

            this.newMatches = new List<Match>();
            this.existMatchXmlIds = new HashSet<int>();
        }

        public ActionResult Index()
        {
            this.newMatches = this.xmlParserService.GetMatches(Constants.BaseUrl).ToList();
            this.existMatchXmlIds = this.GetExistMatchXmlIds(newMatches);

            this.matchesService.Add(newMatches, existMatchXmlIds, EntitiesToProcessed);
            this.matchesService.Update(newMatches, EntitiesToProcessed);

            var bets = this.xmlParserService.GetBets(Constants.BaseUrl).ToList();
            this.betsService.Update(bets, EntitiesToProcessed);

            var odds = this.xmlParserService.GetOdds(Constants.BaseUrl).ToList();
            this.oddsService.Update(odds, EntitiesToProcessed);

            this.matchesService.DeleteOldMatches();

            return View();
        }

        private HashSet<int> GetExistMatchXmlIds(IEnumerable<Match> xmlMatches)
        {
            HashSet<int> allMatchesIds = this.GetAllMatchXmlIds(xmlMatches);

            return new HashSet<int>(this.matchesService.All()
                .Where(m => allMatchesIds.Contains(m.XmlId))
                .Select(m => m.XmlId));
        }

        private HashSet<int> GetAllMatchXmlIds(IEnumerable<Match> xmlMatches)
        {
            HashSet<int> matchesXmlIds = new HashSet<int>();

            foreach (var match in xmlMatches)
            {
                matchesXmlIds.Add(match.XmlId);
            }

            return matchesXmlIds;
        }
    }
}