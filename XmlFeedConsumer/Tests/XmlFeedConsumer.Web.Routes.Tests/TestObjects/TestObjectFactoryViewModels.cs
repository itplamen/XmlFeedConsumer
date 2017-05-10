namespace XmlFeedConsumer.Web.Routes.Tests.TestObjects
{
    using Models.Search;

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
