namespace XmlFeedConsumer.Services.Data.Contracts
{
    using System.Linq;

    using XmlFeedConsumer.Data.Models;

    public interface IMatchesService
    {
        int Add(Match match);

        Match Get(int id);

        IQueryable<Match> GetAsQueryable(int id);

        IQueryable<Match> All();

        IQueryable<Match> AllWithDeleted();

        Match Update(int id, Match match);

        Match Delete(int id);

        bool HardDelete(int id);
    }
}
