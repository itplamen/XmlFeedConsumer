namespace XmlFeedConsumer.Services.Data.Contracts
{
    using System.Linq;

    using XmlFeedConsumer.Data.Models;

    public interface IBetsService
    {
        int Add(Bet bet);

        Bet Get(int id);

        IQueryable<Bet> GetAsQueryable(int id);

        IQueryable<Bet> All();

        IQueryable<Bet> AllWithDeleted();

        Bet Update(int id, Bet bet);

        Bet Delete(int id);

        bool HardDelete(int id);
    }
}
