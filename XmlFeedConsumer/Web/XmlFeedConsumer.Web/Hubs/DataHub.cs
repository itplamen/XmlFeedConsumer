namespace XmlFeedConsumer.Web.Hubs
{
    using System.Linq;

    using Bytes2you.Validation;

    using Microsoft.AspNet.SignalR;

    using Common.Contracts;
    using Infrastructure.Mapping;
    using Models.Home;
    using XmlFeedConsumer.Common;
    using Services.Data.Contracts;

    public class DataHub : Hub
    {
        private readonly IDataManager dataManager;
        private readonly IMatchesService matchesService;

        public DataHub(IDataManager manageData, IMatchesService matchesService)
        {
            Guard.WhenArgument(manageData, nameof(manageData)).IsNull().Throw();
            Guard.WhenArgument(matchesService, nameof(matchesService)).IsNull().Throw();

            this.dataManager = manageData;
            this.matchesService = matchesService;
        }

        public void AddMatches()
        {
            var addedMatches = this.dataManager.AddMatches(Constants.EntitiesToProcessed)
                .To<MatchViewModel>()
                .ToList();

            Clients.All.addMatches(addedMatches);
        }

        public void UpdateMatches()
        {
            var updatedMatches = this.dataManager.UpdateMatches(Constants.EntitiesToProcessed)
                .To<MatchViewModel>()
                .ToList();

            Clients.All.updateMatches(updatedMatches);
        }

        public void DeleteMatches()
        {
            var matchesToDeleteXmlIds = this.matchesService.DeleteMatches().ToList();

            Clients.All.deleteMatches(matchesToDeleteXmlIds);
        }

        public void UpdateBets()
        {
            var updatedBets = this.dataManager.UpdateBets(Constants.EntitiesToProcessed)
                .To<BetViewModel>()
                .ToList();

            Clients.All.updateBets(updatedBets);
        }

        public void UpdateOdds()
        {
            var updatedOdds = this.dataManager.UpdateOdds(Constants.EntitiesToProcessed)
                .To<OddViewModel>()
                .ToList();

            Clients.All.updateOdds(updatedOdds);
        }
    }
}