namespace XmlFeedConsumer.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Linq;

    using XmlFeedConsumer.Data.Models;

    public interface IOddsService
    {
        Odd Add(Odd odd);

        Odd Get(int id);

        IQueryable<Odd> GetAsQueryable(int id);

        IQueryable<Odd> All();

        IQueryable<Odd> AllWithDeleted();

        Odd Update(int id, Odd odd);

        void Update(IEnumerable<Odd> odds, int oddsToProcessed);

        Odd Delete(int id);

        bool HardDelete(int id);
    }
}
