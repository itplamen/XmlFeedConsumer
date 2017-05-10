namespace XmlFeedConsumer.Tests.Common.TestObjects
{
    using Moq;

    using Web.Common.Contracts;

    public static class TestObjectFactoryDataManager
    {
        private const int ValidId = 1;

        public static IDataManager GetDataManager()
        {
            var dataManager = new Mock<IDataManager>();

            dataManager.Setup(x => x.AddMatches(
                    It.Is<int>(i => i < ValidId)))
                .Returns<Data.Models.Match>(null);

            dataManager.Setup(x => x.AddMatches(
                    It.Is<int>(i => i == ValidId)))
                .Returns(TestObjectFactoryDataModels.Matches);

            return dataManager.Object;
        }
    }
}
