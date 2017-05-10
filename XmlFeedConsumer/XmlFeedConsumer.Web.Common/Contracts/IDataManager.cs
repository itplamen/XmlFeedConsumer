namespace XmlFeedConsumer.Web.Common.Contracts
{
    using System.Linq;

    using Data.Models;

    public interface IDataManager
    {
        IQueryable<Match> AddMatches(int count);

        IQueryable<Match> UpdateMatches(int count);

        IQueryable<Bet> UpdateBets(int count);

        IQueryable<Odd> UpdateOdds(int count);
    }
}
