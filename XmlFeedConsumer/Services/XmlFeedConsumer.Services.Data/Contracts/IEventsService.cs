namespace XmlFeedConsumer.Services.Data.Contracts
{
    using System.Linq;

    using XmlFeedConsumer.Data.Models;

    public interface IEventsService
    {
        int Add(Event sportEvent);

        Event Get(int id);

        IQueryable<Event> GetAsQueryable(int id);

        IQueryable<Event> All();

        IQueryable<Event> AllWithDeleted();

        Event Update(int id, Event sportEvent);

        Event Delete(int id);

        bool HardDelete(int id);
    }
}
