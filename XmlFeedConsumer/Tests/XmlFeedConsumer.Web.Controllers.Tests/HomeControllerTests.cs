namespace XmlFeedConsumer.Web.Controllers.Tests
{
    using System;

    using NUnit.Framework;

    using TestStack.FluentMVCTesting;

    using Controllers;
    using Infrastructure.Mapping;
    using Services.Data.Contracts;
    using XmlFeedConsumer.Tests.Common.TestObjects;

    public class HomeControllerTests
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
    }
}
