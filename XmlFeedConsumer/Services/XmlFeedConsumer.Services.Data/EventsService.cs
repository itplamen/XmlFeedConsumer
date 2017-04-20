namespace XmlFeedConsumer.Services.Data
{
    using System.Linq;

    using Bytes2you.Validation;

    using Common;
    using Contracts;
    using XmlFeedConsumer.Data.Common;
    using XmlFeedConsumer.Data.Models;

    public class EventsService : IEventsService
    {
        private readonly IDbRepository<Event> eventsRepository;

        public EventsService(IDbRepository<Event> eventsRepository)
        {
            Guard.WhenArgument(eventsRepository, nameof(eventsRepository)).IsNull().Throw();

            this.eventsRepository = eventsRepository;
        }

        public int Add(Event sportEvent)
        {
            Guard.WhenArgument(sportEvent, nameof(sportEvent)).IsNull().Throw();

            this.eventsRepository.Add(sportEvent);
            this.eventsRepository.Save();

            return sportEvent.Id;
        }

        public Event Get(int id)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(ValidationConstants.InvalidId).Throw();

            return this.eventsRepository.GetById(id);
        }

        public IQueryable<Event> GetAsQueryable(int id)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(ValidationConstants.InvalidId).Throw();

            return this.eventsRepository.All()
                .Where(e => e.Id == id);
        }

        public IQueryable<Event> All()
        {
            return this.eventsRepository.All();
        }

        public IQueryable<Event> AllWithDeleted()
        {
            return this.eventsRepository.AllWithDeleted();
        }

        public Event Update(int id, Event sportEvent)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(ValidationConstants.InvalidId).Throw();
            Guard.WhenArgument(sportEvent, nameof(sportEvent)).IsNull().Throw();

            var eventToUpdate = this.eventsRepository.GetById(id);

            if (eventToUpdate != null)
            {
                eventToUpdate.CreatedOn = sportEvent.CreatedOn;
                eventToUpdate.IsDeleted = sportEvent.IsDeleted;
                eventToUpdate.DeletedOn = sportEvent.DeletedOn;
                eventToUpdate.Name = sportEvent.Name;
                eventToUpdate.IsLive = sportEvent.IsLive;
                eventToUpdate.CategoryID = sportEvent.CategoryID;
                eventToUpdate.SportId = sportEvent.SportId;

                this.eventsRepository.Save();
            }

            return eventToUpdate;
        }

        public Event Delete(int id)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(ValidationConstants.InvalidId).Throw();

            var eventToDelete = this.eventsRepository.GetById(id);

            if (eventToDelete != null)
            {
                this.eventsRepository.Delete(eventToDelete);
                this.eventsRepository.Save();
            }

            return eventToDelete;
        }

        public bool HardDelete(int id)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(ValidationConstants.InvalidId).Throw();

            var eventToDelete = this.eventsRepository.GetById(id);

            if (eventToDelete != null)
            {
                this.eventsRepository.HardDelete(eventToDelete);
                this.eventsRepository.Save();

                return true;
            }

            return false;
        }
    }
}