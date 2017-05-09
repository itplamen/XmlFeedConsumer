namespace XmlFeedConsumer.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Linq;

    using Common;
    using XmlFeedConsumer.Data.Models;

    public interface IMatchesService
    {
        Match Add(Match match);

        void Add(List<Match> matches, HashSet<int> existMatchXmlIds, int matchesToAdd);

        Match Get(int id);

        IQueryable<Match> GetByXmlId(int xmlId);

        IQueryable<Match> All();

        IQueryable<Match> AllWithDeleted();

        IQueryable<object> GetLatest(int count);

        Match Update(int id, Match match);

        IQueryable<Match> Update(IEnumerable<Match> matches, int matchesToProcessed);

        Match Delete(int id);

        IQueryable<int> DeleteMatches();

        bool HardDelete(int id);

        IQueryable<Match> Search(
            string searchWord,
            string sortBy,
            string sortType,
            int page = Constants.MatchesStartPage,
            int matchesPerPage = Constants.MatchesPerPage);
    }
}
