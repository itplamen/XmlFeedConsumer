namespace XmlFeedConsumer.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Bytes2you.Validation;

    using EntityFramework.Extensions;

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

        public Odd Add(Odd odd)
        {
            Guard.WhenArgument(odd, nameof(odd)).IsNull().Throw();

            this.oddsRepository.Add(odd);
            this.oddsRepository.SaveChanges(); 

            return odd;
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

                this.oddsRepository.SaveChanges();
            }

            return oddToUpdate;
        }

        public IQueryable<Odd> Update(IEnumerable<Odd> odds, int oddsToProcessed)
        {
            var oddsToUpdate = odds
                .Where(o => this.oddsRepository.All()
                    .Any(x => x.XmlId == o.XmlId && (x.Name != o.Name || x.Value != o.Value || x.SpecialBetValue != o.SpecialBetValue)))
                .Take(oddsToProcessed);

            if (oddsToUpdate.Any())
            {
                foreach (var odd in oddsToUpdate)
                {
                    this.oddsRepository.All()
                        .Where(x => x.XmlId == odd.XmlId)
                        .Update(x => new Odd()
                        {
                            Name = odd.Name,
                            Value = odd.Value,
                            SpecialBetValue = odd.SpecialBetValue
                        });
                }
            }

            return oddsToUpdate.AsQueryable();
        }

        public Odd Delete(int id)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(ValidationConstants.InvalidId).Throw();

            var oddToDelete = this.oddsRepository.GetById(id);

            if (oddToDelete != null)
            {
                this.oddsRepository.Delete(oddToDelete);
                this.oddsRepository.SaveChanges();
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
                this.oddsRepository.SaveChanges();

                return true;
            }

            return false;
        }
    }
}
