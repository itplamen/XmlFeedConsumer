namespace XmlFeedConsumer.Services.Data.Contracts
{
    using System.Linq;

    using XmlFeedConsumer.Data.Models;

    public interface ISportsService
    {
        int Add(Sport sport);

        Sport Get(int id);

        IQueryable<Sport> GetAsQueryable(int id);

        IQueryable<Sport> All();

        IQueryable<Sport> AllWithDeleted();

        Sport Update(int id, Sport sport);

        Sport Delete(int id);

        bool HardDelete(int id);
    }
}
