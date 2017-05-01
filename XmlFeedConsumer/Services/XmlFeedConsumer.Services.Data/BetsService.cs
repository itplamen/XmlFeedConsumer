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

    public class BetsService : IBetsService
    {
        private readonly IDbRepository<Bet> betsRepository;

        public BetsService(IDbRepository<Bet> betsRepository)
        {
            Guard.WhenArgument(betsRepository, nameof(betsRepository)).IsNull().Throw();

            this.betsRepository = betsRepository;
        }

        public Bet Add(Bet bet)
        {
            Guard.WhenArgument(bet, nameof(bet)).IsNull().Throw();

            this.betsRepository.Add(bet);
            this.betsRepository.SaveChanges();

            return bet;
        }

        public Bet Get(int id)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(ValidationConstants.InvalidId).Throw();

            return this.betsRepository.GetById(id);
        }

        public IQueryable<Bet> GetAsQueryable(int id)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(ValidationConstants.InvalidId).Throw();

            return this.betsRepository.All()
                .Where(b => b.Id == id);
        }

        public IQueryable<Bet> All()
        {
            return this.betsRepository.All();
        }

        public IQueryable<Bet> AllWithDeleted()
        {
            return this.betsRepository.AllWithDeleted();
        }

        public Bet Update(int id, Bet bet)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(ValidationConstants.InvalidId).Throw();
            Guard.WhenArgument(bet, nameof(bet)).IsNull().Throw();

            var betToUpdate = this.betsRepository.GetById(id);

            if (betToUpdate != null)
            {
                betToUpdate.CreatedOn = bet.CreatedOn;
                betToUpdate.IsDeleted = bet.IsDeleted;
                betToUpdate.DeletedOn = bet.DeletedOn;
                betToUpdate.Name = bet.Name;
                betToUpdate.IsLive = bet.IsLive;
                betToUpdate.MatchId = bet.MatchId;

                this.betsRepository.SaveChanges();
            }

            return betToUpdate;
        }

        public void Update(IEnumerable<Bet> bets, int betsToProcessed)
        {
            Guard.WhenArgument(bets, nameof(bets)).IsNullOrEmpty().Throw();
            Guard.WhenArgument(betsToProcessed, nameof(betsToProcessed))
                .IsLessThanOrEqual(ValidationConstants.InvalidEntitiesToProcessed)
                .Throw();

            var betsToUpdate = bets
                .Where(b => this.betsRepository.All()
                    .Any(x => x.XmlId == b.XmlId && (x.Name != b.Name || x.IsLive != b.IsLive)))
                    .Take(betsToProcessed);

            if (betsToUpdate.Any())
            {
                foreach (var bet in betsToUpdate)
                {
                    this.betsRepository.All()
                        .Where(x => x.XmlId == bet.XmlId)
                        .Update(x => new Bet()
                        {
                            Name = bet.Name,
                            IsLive = bet.IsLive
                        });
                }
            }
        }

        public Bet Delete(int id)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(ValidationConstants.InvalidId).Throw();

            var betToDelete = this.betsRepository.GetById(id);

            if (betToDelete != null)
            {
                this.betsRepository.Delete(betToDelete);
                this.betsRepository.SaveChanges();
            }

            return betToDelete;
        }

        public bool HardDelete(int id)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(ValidationConstants.InvalidId).Throw();

            var betToDelete = this.betsRepository.GetById(id);

            if (betToDelete != null)
            {
                this.betsRepository.HardDelete(betToDelete);
                this.betsRepository.SaveChanges();

                return true;
            }

            return false;
        }
    }
}