namespace XmlFeedConsumer.Web.Common
{
    using System.Collections.Generic;
    using System.Linq;

    using Bytes2you.Validation;

    using Contracts;
    using Data.Models;
    using Services.Data.Contracts;
    using Services.Utils.Contracts;
    using XmlFeedConsumer.Common;

    public class DataManager : IDataManager
    {
        private readonly IXmlParserService xmlParserService;
        private readonly IMatchesService matchesService;
        private readonly IBetsService betsService;
        private readonly IOddsService oddsService;

        private readonly List<Match> xmlMatches;
        private readonly HashSet<int> existMatchXmlIds;

        public DataManager(IXmlParserService xmlParserService, IMatchesService matchesService, IBetsService betsService, IOddsService oddsService)
        {
            Guard.WhenArgument(xmlParserService, nameof(xmlParserService)).IsNull().Throw();
            Guard.WhenArgument(matchesService, nameof(matchesService)).IsNull().Throw();
            Guard.WhenArgument(betsService, nameof(betsService)).IsNull().Throw();
            Guard.WhenArgument(oddsService, nameof(oddsService)).IsNull().Throw();

            this.xmlParserService = xmlParserService;
            this.matchesService = matchesService;
            this.betsService = betsService;
            this.oddsService = oddsService;

            this.xmlMatches = this.xmlParserService.GetMatches(Constants.BaseUrl).ToList();
            this.existMatchXmlIds = this.GetExistMatchXmlIds(this.xmlMatches);
        }

        /// <summary>
        /// Adds matches and their bets and odds, by calling matches service.
        /// </summary>
        /// <param name="count">The number of matches to add. For best performance, add 10 matches.</param>
        /// <returns>Added matches like IQueryable, ordered by match start date.</returns>
        public IQueryable<Match> AddMatches(int count)
        {
            this.matchesService.Add(this.xmlMatches, this.existMatchXmlIds, count);

            return this.xmlMatches
                .AsQueryable()
                .Where(m => m.Id != ValidationConstants.InvalidId)
                .OrderByDescending(m => m.StartDate);
        }

        /// <summary>
        /// Updates only matches without their bets and odds, by calling matches service.
        /// </summary>
        /// <param name="count">The number of matches to update. For best performance, update 10 matches.</param>
        /// <returns>Updated matches, if there are any, like IQueryable.</returns>
        public IQueryable<Match> UpdateMatches(int count)
        {
            return this.matchesService.Update(this.xmlMatches, count);
        }

        /// <summary>
        /// Updates bets by calling bets service.
        /// </summary>
        /// <param name="count">The number of bets to update.</param>
        /// <returns>Updated bets, if there are any, like IQueryable.</returns>
        public IQueryable<Bet> UpdateBets(int count)
        {
            var bets = this.xmlParserService.GetBets(Constants.BaseUrl).ToList();

            return this.betsService.Update(bets, count);
        }

        /// <summary>
        /// Updates odds by calling odds service.
        /// </summary>
        /// <param name="count">The number of odds to update.</param>
        /// <returns>Updated odds, if there are any, like IQueryable.</returns>
        public IQueryable<Odd> UpdateOdds(int count)
        {
            var odds = this.xmlParserService.GetOdds(Constants.BaseUrl).ToList();

            return this.oddsService.Update(odds, count);
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
