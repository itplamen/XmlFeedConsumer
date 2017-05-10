namespace XmlFeedConsumer.Tests.Common.TestObjects
{
    using System.Collections.Generic;
    using System.Linq;

    using Data.Models;
    using XmlFeedConsumer.Common;

    public static class TestObjectFactoryDataModels
    {
        public static IQueryable<Match> Matches = new List<Match>()
        {
            new Match()
            {
                Id = Constants.EntityValidId,
                Name = Constants.MatchValidName
            }
        }.AsQueryable();

        public static IQueryable<Bet> Bets = new List<Bet>()
        {
            new Bet()
            {
                Id = Constants.EntityValidId,
                Name = Constants.BetValidName
            }
        }.AsQueryable();

        public static IQueryable<Odd> Odds = new List<Odd>()
        {
            new Odd()
            {
                Id = Constants.EntityValidId,
                Name = Constants.OddValidName
            }
        }.AsQueryable();
    }
}
