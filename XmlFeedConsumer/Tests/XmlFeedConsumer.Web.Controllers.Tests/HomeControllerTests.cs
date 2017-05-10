namespace XmlFeedConsumer.Web.Controllers.Tests
{
    using System.Collections.Generic;

    using NUnit.Framework;

    using TestStack.FluentMVCTesting;

    using Common.Contracts;
    using Controllers;
    using Data.Models;
    using Infrastructure.Mapping;
    using Models.Home;
    using Services.Data.Contracts;
    using XmlFeedConsumer.Tests.Common.TestObjects;

    public class HomeControllerTests
    {
        private IDataManager dataManager;

        private IMatchesService matchesService;

        private HomeController homeController;

        private AutoMapperConfig autoMapperConfig;

        [OneTimeSetUp]
        public void TestInitialize()
        {
            this.matchesService = TestObjectFactoryServices.GetMatchesService();
            this.dataManager = TestObjectFactoryDataManager.GetDataManager();
            this.homeController = new HomeController(this.dataManager, this.matchesService);
            this.homeController.Cache = TestObjectFactoryServices.GetCacheService<Match>();
            this.autoMapperConfig = new AutoMapperConfig();
            this.autoMapperConfig.Execute(typeof(HomeController).Assembly);
        }

        [Test]
        public void IndexShouldThrowExceptionWhenExpectedViewIsInvalid()
        {
            var invalidExpectedView = "Get";

            Assert.Throws(
                typeof(ActionResultAssertionException),
                () => this.homeController
                        .WithCallTo(x => x.Index())
                        .ShouldRenderView(invalidExpectedView)
                        .WithModel<IEnumerable<MatchViewModel>>()
                        .AndNoModelErrors());
        }

        [Test]
        public void IndexShouldRenderCorrectView()
        {
            this.homeController
                .WithCallTo(x => x.Index())
                .ShouldRenderDefaultView();
        }
    }
}
