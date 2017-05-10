namespace XmlFeedConsumer.Web.Routes.Tests
{
    using System.Web.Routing;

    using MvcRouteTester;

    using Newtonsoft.Json;

    using NUnit.Framework;

    using Controllers;
    using TestObjects;

    [TestFixture]
    public class MatchesRouteTests
    {
        private const string ValidUrl = "/Matches/";

        private RouteCollection routeCollection;

        [OneTimeSetUp]
        public void TestInitialize()
        {
            this.routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(this.routeCollection);
        }

        [Test]
        public void IndexShouldThrowExceptionWhenControllersDoNotMatch()
        {
            Assert.Throws(
                typeof(MvcRouteTester.Assertions.AssertionException),
                () => routeCollection
                        .ShouldMap(ValidUrl)
                        .To<HomeController>(x => x.Index()));
        }

        [Test]
        public void IndexShouldMapCorrectAction()
        {
            routeCollection
                .ShouldMap(ValidUrl)
                .To<MatchesController>(x => x.Index());
        }

        [Test]
        public void GetShouldThrowExceptionWhenIdIsNegative()
        {
            var invalidMatchId = -1;

            Assert.Throws(
                typeof(MvcRouteTester.Assertions.AssertionException), 
                () => routeCollection
                        .ShouldMap(ValidUrl + invalidMatchId)
                        .To<MatchesController>(x => x.Get(invalidMatchId)));
        }

        [Test]
        public void GetShouldThrowExceptionWhenIdsDoNotMatch()
        {
            var firstId = 1;
            var secondId = 2;

            Assert.Throws(
                typeof(MvcRouteTester.Assertions.AssertionException),
                () => routeCollection
                        .ShouldMap(ValidUrl + firstId)
                        .To<MatchesController>(x => x.Get(secondId)));
        }

        [Test]
        public void GetShouldMapCorrectAction()
        {
            var validMatchId = 1;

            routeCollection.ShouldMap(ValidUrl + validMatchId)
                .To<MatchesController>(x => x.Get(validMatchId));
        }

        [Test]
        public void InitialMatchesShoudMapCorrectAction()
        {
            var actionName = "InitialMatches";

            routeCollection.ShouldMap(ValidUrl + actionName)
                .To<MatchesController>(x => x.InitialMatches());
        }

        [Test]
        public void SearchMatchesShouldThrowExceptionWhenPageNumbersDoNotMatch()
        {
            var actionName = "SearchMatches";
            var matchesSearchViewModel = TestObjectFactoryViewModels.GetValidMatchesSearchViewModel();
            var firstPageNumber = 1;
            var secondPageNumber = 2;
            string jsonContent = JsonConvert.SerializeObject(matchesSearchViewModel);

            Assert.Throws(
                typeof(MvcRouteTester.Assertions.AssertionException),
                () => routeCollection.ShouldMap(ValidUrl + actionName + "?page=" + firstPageNumber)
                        .WithJsonBody(jsonContent)
                        .To<MatchesController>(x => x.SearchMatches(matchesSearchViewModel, secondPageNumber)));
        }

        [Test]
        public void SearchMatchesShouldThrowExceptionWhenPageNumberIsNegative()
        {
            var actionName = "SearchMatches";
            var matchesSearchViewModel = TestObjectFactoryViewModels.GetValidMatchesSearchViewModel();
            var negativePageNumber = -1;
            string jsonContent = JsonConvert.SerializeObject(matchesSearchViewModel);

            routeCollection.ShouldMap(ValidUrl + actionName + "?page=" + negativePageNumber)
                            .WithJsonBody(jsonContent)
                            .To<MatchesController>(x => x.SearchMatches(matchesSearchViewModel, negativePageNumber));
        }

        [Test]
        public void SearchMatchesShouldMapCorrectActionWhenPageIsNull()
        {
            var actionName = "SearchMatches";
            var matchesSearchViewModel = TestObjectFactoryViewModels.GetValidMatchesSearchViewModel();
            string jsonContent = JsonConvert.SerializeObject(matchesSearchViewModel);

            routeCollection.ShouldMap(ValidUrl + actionName)
                .WithJsonBody(jsonContent)
                .To<MatchesController>(x => x.SearchMatches(matchesSearchViewModel, null));
        }
    }
}
