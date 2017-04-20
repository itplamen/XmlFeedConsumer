namespace XmlFeedConsumer.Services.Data
{
    using System.Linq;

    using Bytes2you.Validation;

    using Common;
    using Contracts;
    using XmlFeedConsumer.Data.Common;
    using XmlFeedConsumer.Data.Models;

    public class MatchesService : IMatchesService
    {
        private readonly IDbRepository<Match> matchesRepository;

        public MatchesService(IDbRepository<Match> matchesRepository)
        {
            Guard.WhenArgument(matchesRepository, nameof(matchesRepository)).IsNull().Throw();

            this.matchesRepository = matchesRepository;
        }

        public int Add(Match match)
        {
            Guard.WhenArgument(match, nameof(match)).IsNull().Throw();

            this.matchesRepository.Add(match);
            this.matchesRepository.Save();

            return match.Id;
        }

        public Match Get(int id)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(ValidationConstants.InvalidId).Throw();

            return this.matchesRepository.GetById(id);
        }

        public IQueryable<Match> GetAsQueryable(int id)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(ValidationConstants.InvalidId).Throw();

            return this.matchesRepository.All()
                .Where(m => m.Id == id);
        }

        public IQueryable<Match> All()
        {
            return this.matchesRepository.All();
        }

        public IQueryable<Match> AllWithDeleted()
        {
            return this.matchesRepository.AllWithDeleted();
        }

        public Match Update(int id, Match match)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(ValidationConstants.InvalidId).Throw();
            Guard.WhenArgument(match, nameof(match)).IsNull().Throw();

            var matchToUpdate = this.matchesRepository.GetById(id);

            if (matchToUpdate != null)
            {
                matchToUpdate.CreatedOn = match.CreatedOn;
                matchToUpdate.IsDeleted = match.IsDeleted;
                matchToUpdate.DeletedOn = match.DeletedOn;
                matchToUpdate.Name = match.Name;
                matchToUpdate.StartDate = match.StartDate;
                matchToUpdate.MatchType = match.MatchType;
                matchToUpdate.EventId = match.EventId;

                this.matchesRepository.Save();
            }

            return matchToUpdate;
        }

        public Match Delete(int id)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(ValidationConstants.InvalidId).Throw();

            var matchToDelete = this.matchesRepository.GetById(id);

            if (matchToDelete != null)
            {
                this.matchesRepository.Delete(matchToDelete);
                this.matchesRepository.Save();
            }

            return matchToDelete;
        }

        public bool HardDelete(int id)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(ValidationConstants.InvalidId).Throw();

            var matchToDelete = this.matchesRepository.GetById(id);

            if (matchToDelete != null)
            {
                this.matchesRepository.HardDelete(matchToDelete);
                this.matchesRepository.Save();

                return true;
            }

            return false;
        }
    }
}
