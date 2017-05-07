namespace XmlFeedConsumer.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Bytes2you.Validation;

    using EntityFramework.Extensions;

    using Common;
    using Contracts;
    using XmlFeedConsumer.Data.Common;
    using XmlFeedConsumer.Data.Models;

    public class MatchesService : IMatchesService
    {
        private const int BetsToTake = 1;
        private const int OddsToTake = 2;

        private readonly IDbRepository<Match> matchesRepository;

        public MatchesService(IDbRepository<Match> matchesRepository)
        {
            Guard.WhenArgument(matchesRepository, nameof(matchesRepository)).IsNull().Throw();

            this.matchesRepository = matchesRepository;
        }

        public Match Add(Match match)
        {
            Guard.WhenArgument(match, nameof(match)).IsNull().Throw();

            this.matchesRepository.Add(match);
            this.matchesRepository.SaveChanges();

            return match;
        }

        public void Add(List<Match> matches, HashSet<int> existMatchXmlIds, int matchesToAdd)
        {
            Guard.WhenArgument(matches, nameof(matches)).IsNullOrEmpty().Throw();
            Guard.WhenArgument(existMatchXmlIds, nameof(existMatchXmlIds)).IsNull().Throw();
            Guard.WhenArgument(matchesToAdd, nameof(matchesToAdd)).IsLessThanOrEqual(ValidationConstants.InvalidEntitiesCount).Throw();

            var addedMatches = 0;
            var startIndex = matches.FindIndex(m => !existMatchXmlIds.Contains(m.XmlId));

            if (startIndex == -1)
            {
                return;
            }

            for (int i = startIndex; i < matches.Count; i++)
            {
                if (addedMatches >= matchesToAdd)
                {
                    break;
                }

                if (!existMatchXmlIds.Contains(matches[i].XmlId) && addedMatches < matchesToAdd)
                {
                    this.matchesRepository.Add(matches[i]);
                    addedMatches++;
                }
            }

            this.matchesRepository.SaveChanges();
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

        public IQueryable<object> GetLatest(int count)
        {
            Guard.WhenArgument(count, nameof(count)).IsLessThanOrEqual(ValidationConstants.InvalidEntitiesCount);

            return this.matchesRepository.All()
                .Where(m => m.Bets.Any() && m.Bets.Any(x => x.Odds.Count >= OddsToTake))
                .OrderByDescending(m => m.StartDate)
                .Select(x => new
                {
                    XmlId = x.XmlId,
                    Name = x.Name,
                    MatchType = x.MatchType,
                    StartDate = x.StartDate,
                    Bets = x.Bets
                        .Take(BetsToTake)
                        .Select(b => new
                        {
                            XmlId = b.XmlId,
                            Name = b.Name,
                            IsLive = b.IsLive,
                            Odds = b.Odds.Take(OddsToTake)
                        })
                })
                .Take(count);
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

                this.matchesRepository.SaveChanges();
            }

            return matchToUpdate;
        }

        public IQueryable<Match> Update(IEnumerable<Match> matches, int matchesToProcessed)
        {
            Guard.WhenArgument(matches, nameof(matches)).IsNullOrEmpty().Throw();
            Guard.WhenArgument(matchesToProcessed, nameof(matchesToProcessed))
                .IsLessThanOrEqual(ValidationConstants.InvalidEntitiesCount)
                .Throw();

            var matchesToUpdate = matches
                .Where(m => this.matchesRepository.All()
                    .Any(x => x.XmlId == m.XmlId && (x.Name != m.Name || x.MatchType != m.MatchType || x.StartDate != m.StartDate)))
                .Take(matchesToProcessed);

            if (matchesToUpdate.Any())
            {
                foreach (var match in matchesToUpdate)
                {
                    this.matchesRepository.All()
                        .Where(x => x.XmlId == match.XmlId)
                        .Update(x => new Match()
                        {
                            Name = match.Name,
                            StartDate = match.StartDate,
                            MatchType = match.MatchType
                        });
                }
            }

            return matchesToUpdate.AsQueryable();
        }

        public Match Delete(int id)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(ValidationConstants.InvalidId).Throw();

            var matchToDelete = this.matchesRepository.GetById(id);

            if (matchToDelete != null)
            {
                this.matchesRepository.Delete(matchToDelete);
                this.matchesRepository.SaveChanges();
            }

            return matchToDelete;
        }

        public void DeleteMatches()
        {
            this.matchesRepository.All()
                .Where(m => !m.IsDeleted && m.StartDate.Day < DateTime.Now.Day)
                .Update(m => new Match()
                {
                    IsDeleted = true,
                    DeletedOn = DateTime.UtcNow
                });
        }

        public bool HardDelete(int id)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(ValidationConstants.InvalidId).Throw();

            var matchToDelete = this.matchesRepository.GetById(id);

            if (matchToDelete != null)
            {
                this.matchesRepository.HardDelete(matchToDelete);
                this.matchesRepository.SaveChanges();

                return true;
            }

            return false;
        }
    }
}
