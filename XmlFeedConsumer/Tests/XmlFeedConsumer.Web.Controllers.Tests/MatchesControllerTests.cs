namespace XmlFeedConsumer.Web.Controllers.Tests
{
    using System;

    using NUnit.Framework;

    using TestStack.FluentMVCTesting;

    using Controllers;
    using Infrastructure.Mapping;
    using Models.Home;
    using Models.Search;
    using Services.Data.Contracts;
    using XmlFeedConsumer.Tests.Common.TestObjects;

    [TestFixture]
    public class MatchesControllerTests
    {
        private IMatchesService matchesService;

        private MatchesController matchesController;

        private AutoMapperConfig autoMapperConfig;

        [OneTimeSetUp]
        public void TestInitialize()
        {
            this.matchesService = TestObjectFactoryServices.GetMatchesService();
            this.matchesController = new MatchesController(this.matchesService);
            this.autoMapperConfig = new AutoMapperConfig();
            this.autoMapperConfig.Execute(typeof(MatchesController).Assembly);
        }

        [Test]
        public void IndexShouldThrowExceptionWhenExpectedViewIsInvalid()
        {
            var invalidExpectedView = "Get";

            Assert.Throws(
                typeof(ActionResultAssertionException),
                () => this.matchesController
                        .WithCallTo(x => x.Index())
                        .ShouldRenderView(invalidExpectedView));
        }

        [Test]
        public void IndexShouldRenderCorrectView()
        {
            this.matchesController.WithCallTo(x => x.Index())
                .ShouldRenderDefaultView();
        }

        [Test]
        public void GetShouldThrowExceptionWhenXmlIdIsInvalid()
        {
            var invalidXmlId = -1;
            var validExpectedView = "Get";

            Assert.Throws(
                typeof(ArgumentOutOfRangeException),
                () => this.matchesController
                        .WithCallTo(x => x.Get(invalidXmlId))
                        .ShouldRenderView(validExpectedView));
        }

        [Test]
        public void GetShouldThrowExceptionWhenExpectedViewIsInvalid()
        {
            var validXmlId = 1;
            var invalidExpectedView = "Index";

            Assert.Throws(
                typeof(ActionResultAssertionException),
                () => this.matchesController
                        .WithCallTo(x => x.Get(validXmlId))
                        .ShouldRenderView(invalidExpectedView));
        }

        [Test]
        public void GetShouldThrowExceptionWhenViewModelIsInvalid()
        {
            var validXmlId = 1;
            var validExpectedView = "Get";

            Assert.Throws(
                typeof(ViewResultModelAssertionException),
                () => this.matchesController
                        .WithCallTo(x => x.Get(validXmlId))
                        .ShouldRenderView(validExpectedView)
                        .WithModel<MatchesSearchViewModel>());
        }

        [Test]
        public void GetShouldRenderCorrectView()
        {
            var validExpectedView = "Get";
            var validXmlId = 1;

            this.matchesController
                .WithCallTo(x => x.Get(validXmlId))
                .ShouldRenderView(validExpectedView)
                .WithModel<MatchViewModel>()
                .AndNoModelErrors();
        }

        [Test]
        public void InitialMatchesShouldThrowExceptionWhenExpectsToRenderViewInsteadOfPartialView()
        {
            var invalidExpectedView = "_MatchesPartial";

            Assert.Throws(
                typeof(ActionResultAssertionException),
                () => this.matchesController
                        .WithCallTo(x => x.InitialMatches())
                        .ShouldRenderView(invalidExpectedView)
                        .WithModel<MatchesSearchResultViewModel>()
                        .AndNoModelErrors());
        }

        [Test]
        public void InitialMatchesShouldThrowExceptionWhenExpectedPartialViewIsInvalid()
        {
            var invalidExpectedPartialView = "_Matches";

            Assert.Throws(
                typeof(ActionResultAssertionException),
                () => this.matchesController
                        .WithCallTo(x => x.InitialMatches())
                        .ShouldRenderPartialView(invalidExpectedPartialView)
                        .WithModel<MatchesSearchResultViewModel>()
                        .AndNoModelErrors());
        }

        [Test]
        public void InitialMatchesShouldRenderCorrectPartialView()
        {
            var validPartialView = "_MatchesPartial";

            this.matchesController
                    .WithCallTo(x => x.InitialMatches())
                    .ShouldRenderPartialView(validPartialView)
                    .WithModel<MatchesSearchResultViewModel>()
                    .AndNoModelErrors();
        }

        [Test]
        public void SearchMatchesShouldThrowExceptionWhenMatchesSearchResultViewModelIsNull()
        {
            var validPage = 1;
            var validPartialView = "_MatchesPartial";

            Assert.Throws(
                typeof(ArgumentNullException),
                () => this.matchesController
                        .WithCallTo(x => x.SearchMatches(null, validPage))
                        .ShouldRenderPartialView(validPartialView)
                        .WithModel<MatchesSearchResultViewModel>()
                        .AndNoModelErrors());
        }

        [Test]
        public void SearchMatchesShouldThrowExceptionWhenPageNumberIsInvalid()
        {
            var invalidPage = -1;
            var validPartialView = "_MatchesPartial";

            Assert.Throws(
                typeof(ArgumentOutOfRangeException),
                () => this.matchesController
                        .WithCallTo(x => x.SearchMatches(TestObjectFactoryViewModels.GetValidMatchesSearchViewModel(), invalidPage))
                        .ShouldRenderPartialView(validPartialView)
                        .WithModel<MatchesSearchResultViewModel>()
                        .AndNoModelErrors());
        }

        [Test]
        public void SearchMatchesShouldThrowExceptionWhenExpectsToRenderViewInsteadOfPartialView()
        {
            var validPage = 1;
            var invalidExpectedView = "_MatchesPartial";

            Assert.Throws(
                typeof(ActionResultAssertionException),
                () => this.matchesController
                        .WithCallTo(x => x.SearchMatches(TestObjectFactoryViewModels.GetValidMatchesSearchViewModel(), validPage))
                        .ShouldRenderView(invalidExpectedView)
                        .WithModel<MatchesSearchResultViewModel>()
                        .AndNoModelErrors());
        }

        [Test]
        public void SearchMatchesShouldThrowExceptionWhenExpectedPartialViewIsInvalid()
        {
            var validPage = 1;
            var invalidExpectedPartialView = "_Matches";

            Assert.Throws(
                typeof(ActionResultAssertionException),
                () => this.matchesController
                        .WithCallTo(x => x.SearchMatches(TestObjectFactoryViewModels.GetValidMatchesSearchViewModel(), validPage))
                        .ShouldRenderPartialView(invalidExpectedPartialView)
                        .WithModel<MatchesSearchResultViewModel>()
                        .AndNoModelErrors());
        }

        [Test]
        public void SearchMatchesShouldRenderCorrectPartialView()
        {
            var validPage = 1;
            var validPartialView = "_MatchesPartial";

            this.matchesController
                    .WithCallTo(x => x.SearchMatches(TestObjectFactoryViewModels.GetValidMatchesSearchViewModel(), validPage))
                    .ShouldRenderPartialView(validPartialView)
                    .WithModel<MatchesSearchResultViewModel>()
                    .AndNoModelErrors();
        }

        [Test]
        public void SearchMatchesShouldRenderCorrectPartialViewWhenPageIsNull()
        {
            var validPartialView = "_MatchesPartial";

            this.matchesController
                    .WithCallTo(x => x.SearchMatches(TestObjectFactoryViewModels.GetValidMatchesSearchViewModel(), null))
                    .ShouldRenderPartialView(validPartialView)
                    .WithModel<MatchesSearchResultViewModel>()
                    .AndNoModelErrors();
        }
    }
}