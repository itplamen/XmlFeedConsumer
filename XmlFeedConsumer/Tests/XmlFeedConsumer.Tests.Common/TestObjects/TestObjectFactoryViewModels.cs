namespace XmlFeedConsumer.Tests.Common.TestObjects
{
    using Web.Models.Search;

    public static class TestObjectFactoryViewModels
    {
        public static MatchesSearchViewModel GetValidMatchesSearchViewModel()
        {
            return new MatchesSearchViewModel()
            {
                SearchWord = "Test Search Word",
                SortBy = "Test Sort By",
                SortType = "Test Sort Type"
            };
        }
    }
}
