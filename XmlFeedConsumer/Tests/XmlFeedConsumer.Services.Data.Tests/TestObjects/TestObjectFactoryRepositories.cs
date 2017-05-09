namespace XmlFeedConsumer.Services.Data.Tests.TestObjects
{
    using XmlFeedConsumer.Data.Models;

    public static class TestObjectFactoryRepositories
    {
        public static InMemoryRepository<Odd> GetOddsRepository(int numberOfOdds = 25)
        {
            var odds = new InMemoryRepository<Odd>();

            for (int i = 0; i < numberOfOdds; i++)
            {
                odds.Add(new Odd()
                {
                    Id = i,
                    Name = "Test Odd " + i,
                    Value = "Test Value",
                    XmlId = i
                });
            }

            return odds;
        }

        public static InMemoryRepository<Bet> GetBetsRepository(int numberOfBets = 25)
        {
            var bets = new InMemoryRepository<Bet>();

            for (int i = 0; i < numberOfBets; i++)
            {
                bets.Add(new Bet()
                {
                    Id = i,
                    Name = "Test Bet " + i,
                    XmlId = i
                });
            }

            return bets;
        }

        public static InMemoryRepository<Match> GetMatchesRepository(int numberOfMatches = 25)
        {
            var matches = new InMemoryRepository<Match>();

            for (int i = 0; i < numberOfMatches; i++)
            {
                matches.Add(new Match()
                {
                    Id = i,
                    Name = "Test Match " + i,
                    MatchType = "Test Match Type " + i,
                    XmlId = i
                });
            }

            return matches;
        }
    }
}
