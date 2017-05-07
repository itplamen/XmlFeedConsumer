namespace XmlFeedConsumer.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Linq;

    using XmlFeedConsumer.Data.Models;

    public interface IBetsService
    {
        Bet Add(Bet bet);

        Bet Get(int id);

        IQueryable<Bet> GetAsQueryable(int id);

        IQueryable<Bet> All();

        IQueryable<Bet> AllWithDeleted();

        Bet Update(int id, Bet bet);

        IQueryable<Bet> Update(IEnumerable<Bet> bets, int betsToProcessed);

        Bet Delete(int id);

        bool HardDelete(int id);
    }
}
