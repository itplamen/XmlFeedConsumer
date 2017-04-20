namespace XmlFeedConsumer.Services.Data.Contracts
{
    using System.Linq;

    using XmlFeedConsumer.Data.Models;

    public interface IOddsService
    {
        int Add(Odd odd);

        Odd Get(int id);

        IQueryable<Odd> GetAsQueryable(int id);

        IQueryable<Odd> All();

        IQueryable<Odd> AllWithDeleted();

        Odd Update(int id, Odd odd);

        Odd Delete(int id);

        bool HardDelete(int id);
    }
}
