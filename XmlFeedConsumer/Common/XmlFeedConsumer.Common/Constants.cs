namespace XmlFeedConsumer.Common
{
    public static class Constants
    {
        public const string BaseUrl = "http://vitalbet.net/sportxml";
        public const int EntitiesToProcessed = 10;
        public const int EntityValidId = 1;
        public const string MatchValidName = "Match Test Name";
        public const string BetValidName = "Bet Test Name";
        public const string OddValidName = "Odd Test Name";

        // Views
        public const int MatchesStartPage = 1;
        public const int MatchesPerPage = 30;
        public const string MatchesInitialOrderBy = "StartDate";
        public const string AscendingSortMatches = "Ascending";
        public const string DescendingSortMatches = "Descending";
        public const int MatchNameMaxLength = 40;
        public const int MatchTypeMaxLength = 19;
        public const int BetNameMaxLength = 19;
    }
}
