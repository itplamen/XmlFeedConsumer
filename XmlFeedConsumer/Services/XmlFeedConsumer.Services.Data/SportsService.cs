namespace XmlFeedConsumer.Services.Data
{
    using System.Linq;

    using Bytes2you.Validation;

    using Common;
    using Contracts;
    using XmlFeedConsumer.Data.Common;
    using XmlFeedConsumer.Data.Models;

    public class SportsService : ISportsService
    {
        private readonly IDbRepository<Sport> sportsRepository;

        public SportsService(IDbRepository<Sport> sportsRepository)
        {
            Guard.WhenArgument(sportsRepository, nameof(sportsRepository)).IsNull().Throw();

            this.sportsRepository = sportsRepository;
        }

        public int Add(Sport sport)
        {
            Guard.WhenArgument(sport, nameof(sport)).IsNull().Throw();

            this.sportsRepository.Add(sport);
            this.sportsRepository.Save();

            return sport.Id;
        }

        public Sport Get(int id)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(ValidationConstants.InvalidId).Throw();

            return this.sportsRepository.GetById(id);
        }

        public IQueryable<Sport> GetAsQueryable(int id)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(ValidationConstants.InvalidId).Throw();

            return this.sportsRepository.All()
                .Where(s => s.Id == id);
        }

        public IQueryable<Sport> All()
        {
            return this.sportsRepository.All();
        }

        public IQueryable<Sport> AllWithDeleted()
        {
            return this.sportsRepository.AllWithDeleted();
        }

        public Sport Update(int id, Sport sport)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(ValidationConstants.InvalidId).Throw();
            Guard.WhenArgument(sport, nameof(sport)).IsNull().Throw();

            var sportToUpdate = this.sportsRepository.GetById(id);

            if (sportToUpdate != null)
            {
                sportToUpdate.CreatedOn = sport.CreatedOn;
                sportToUpdate.IsDeleted = sport.IsDeleted;
                sportToUpdate.DeletedOn = sport.DeletedOn;
                sportToUpdate.Name = sport.Name;
      
                this.sportsRepository.Save();
            }

            return sportToUpdate;
        }

        public Sport Delete(int id)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(ValidationConstants.InvalidId).Throw();

            var sportToDelete = this.sportsRepository.GetById(id);

            if (sportToDelete != null)
            {
                this.sportsRepository.Delete(sportToDelete);
                this.sportsRepository.Save();
            }

            return sportToDelete;
        }

        public bool HardDelete(int id)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(ValidationConstants.InvalidId).Throw();

            var sportToDelete = this.sportsRepository.GetById(id);

            if (sportToDelete != null)
            {
                this.sportsRepository.HardDelete(sportToDelete);
                this.sportsRepository.Save();

                return true;
            }

            return false;
        }
    }
}
