namespace XmlFeedConsumer.Web.Models.Search
{
    using XmlFeedConsumer.Common;

    public class MatchesSearchViewModel
    {
        public MatchesSearchViewModel()
        {
            if (string.IsNullOrEmpty(this.SortBy))
            {
                this.SortBy = Constants.MatchesInitialOrderBy;
            }

            if (string.IsNullOrEmpty(this.SortType))
            {
                this.SortType = Constants.AscendingSortMatches;
            }
        }

        public string SearchWord { get; set; }

        public string SortBy { get; set; }

        public string SortType { get; set; }
    }
}