namespace XmlFeedConsumer.Web.Routes.Tests
{
    using System.Web.Routing;

    using MvcRouteTester;

    using NUnit.Framework;

    using Controllers;

    [TestFixture]
    public class HomeRouteTests
    {
        private const string ValidUrl = "/Home/";

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
                        .To<MatchesController>(x => x.Index()));
        }

        [Test]
        public void IndexShouldMapCorrectAction()
        {
            routeCollection
                .ShouldMap(ValidUrl)
                .To<HomeController>(x => x.Index());
        }
    }
}
