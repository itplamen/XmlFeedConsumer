namespace XmlFeedConsumer.Web.Models.Home
{
    using System;
    using System.Collections.Generic;

    using Data.Models;
    using Infrastructure.Mapping;

    public class MatchViewModel : IMapFrom<Match> 
    {
        public int XmlId { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public string MatchType { get; set; }

        public IEnumerable<BetViewModel> Bets { get; set; }
    }
}