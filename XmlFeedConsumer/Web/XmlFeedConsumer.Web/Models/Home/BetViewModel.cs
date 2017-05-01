namespace XmlFeedConsumer.Web.Models.Home
{
    using System.Collections.Generic;

    using Data.Models;
    using Infrastructure.Mapping;

    public class BetViewModel : IMapFrom<Bet> 
    {
        public int XmlId { get; set; }

        public string Name { get; set; }

        public bool IsLive { get; set; }

        public IEnumerable<OddViewModel> Odds { get; set; }
    }
}