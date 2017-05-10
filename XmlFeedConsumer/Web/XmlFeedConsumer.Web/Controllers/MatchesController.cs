namespace XmlFeedConsumer.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper;

    using Bytes2you.Validation;

    using Attributes;
    using Infrastructure.Mapping;
    using Models.Home;
    using Models.Search;
    using Services.Data.Contracts;
    using XmlFeedConsumer.Common;

    public class MatchesController : BaseController
    {
        private readonly IMatchesService matchesService;

        public MatchesController(IMatchesService matchesService)
        {
            Guard.WhenArgument(matchesService, nameof(matchesService)).IsNull().Throw();

            this.matchesService = matchesService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        public ActionResult Get(int xmlId)
        {
            Guard.WhenArgument(xmlId, nameof(xmlId)).IsLessThanOrEqual(ValidationConstants.InvalidId).Throw();

            var match = this.matchesService.GetByXmlId(xmlId)
                .To<MatchViewModel>()
                .FirstOrDefault();

            return this.View(match);
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult InitialMatches()
        {
            return this.Search(new MatchesSearchViewModel(), Constants.MatchesStartPage);
        }

        [HttpPost]
        [AjaxOnly]
        public PartialViewResult SearchMatches(MatchesSearchViewModel searchModel, int? page)
        {
            int actualPage = page ?? Constants.MatchesStartPage;
            Guard.WhenArgument(actualPage, nameof(actualPage)).IsLessThan(Constants.MatchesStartPage).Throw();

            return this.Search(searchModel, actualPage);
        }

        private PartialViewResult Search(MatchesSearchViewModel search, int page)
        {
            var allMatchesCount = this.matchesService.All().Count();
            var totalPages = (int)Math.Ceiling(allMatchesCount / (decimal)Constants.MatchesPerPage);

            var matches = this.matchesService.Search(search.SearchWord, search.SortBy, search.SortType, page)
                .Select(m => new
                {
                    XmlId = m.XmlId,
                    Name = m.Name,
                    StartDate = m.StartDate,
                    MatchType = m.MatchType,
                    IsDeleted = m.IsDeleted
                })
                .Select(Mapper.DynamicMap<MatchViewModel>)
                .ToList();

            var matchesListViewModel = new MatchesSearchResultViewModel()
            {
                Matches = matches,
                CurrentPage = page,
                TotalPages = totalPages
            };

            return this.PartialView("_MatchesPartial", matchesListViewModel);
        }
    }
}