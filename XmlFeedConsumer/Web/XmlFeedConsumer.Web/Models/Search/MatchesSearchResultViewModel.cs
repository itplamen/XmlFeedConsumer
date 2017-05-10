namespace XmlFeedConsumer.Web.Models.Search
{
    using System.Collections.Generic;

    using Home;
    using Infrastructure.Mapping;

    public class MatchesSearchResultViewModel : IMapFrom<MatchViewModel>
    {
        public IEnumerable<MatchViewModel> Matches { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }
    }
}