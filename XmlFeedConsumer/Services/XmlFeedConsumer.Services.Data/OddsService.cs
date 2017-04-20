namespace XmlFeedConsumer.Services.Data
{
    using System.Linq;

    using Bytes2you.Validation;

    using Common;
    using Contracts;
    using XmlFeedConsumer.Data.Common;
    using XmlFeedConsumer.Data.Models;

    public class OddsService : IOddsService
    {
        private readonly IDbRepository<Odd> oddsRepository;

        public OddsService(IDbRepository<Odd> oddsRepository)
        {
            Guard.WhenArgument(oddsRepository, nameof(oddsRepository)).IsNull().Throw();

            this.oddsRepository = oddsRepository;
        }

        public int Add(Odd odd)
        {
            Guard.WhenArgument(odd, nameof(odd)).IsNull().Throw();

            this.oddsRepository.Add(odd);
            this.oddsRepository.Save();

            return odd.Id;
        }

        public Odd Get(int id)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(ValidationConstants.InvalidId).Throw();

            return this.oddsRepository.GetById(id);
        }

        public IQueryable<Odd> GetAsQueryable(int id)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(ValidationConstants.InvalidId).Throw();

            return this.oddsRepository.All()
                .Where(o => o.Id == id);
        }

        public IQueryable<Odd> All()
        {
            return this.oddsRepository.All();
        }

        public IQueryable<Odd> AllWithDeleted()
        {
            return this.oddsRepository.AllWithDeleted();
        }

        public Odd Update(int id, Odd odd)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(ValidationConstants.InvalidId).Throw();
            Guard.WhenArgument(odd, nameof(odd)).IsNull().Throw();

            var oddToUpdate = this.oddsRepository.GetById(id);

            if (oddToUpdate != null)
            {
                oddToUpdate.CreatedOn = odd.CreatedOn;
                oddToUpdate.IsDeleted = odd.IsDeleted;
                oddToUpdate.DeletedOn = odd.DeletedOn;
                oddToUpdate.Name = odd.Name;
                oddToUpdate.Value = odd.Value;
                oddToUpdate.SpecialBetValue = odd.SpecialBetValue;
                oddToUpdate.BetId = odd.BetId;

                this.oddsRepository.Save();
            }

            return oddToUpdate;
        }

        public Odd Delete(int id)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(ValidationConstants.InvalidId).Throw();

            var oddToDelete = this.oddsRepository.GetById(id);

            if (oddToDelete != null)
            {
                this.oddsRepository.Delete(oddToDelete);
                this.oddsRepository.Save();
            }

            return oddToDelete;
        }

        public bool HardDelete(int id)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(ValidationConstants.InvalidId).Throw();

            var oddToDelete = this.oddsRepository.GetById(id);

            if (oddToDelete != null)
            {
                this.oddsRepository.HardDelete(oddToDelete);
                this.oddsRepository.Save();

                return true;
            }

            return false;
        }
    }
}
