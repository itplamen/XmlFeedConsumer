namespace XmlFeedConsumer.Tests.Common.TestObjects
{
    using System;
    using System.Collections.Generic;

    using Moq;

    using Services.Data.Contracts;
    using Services.Utils.Contracts;

    public static class TestObjectFactoryServices
    {
        private const int ValidId = 1;
        private const int MatchesToAdd = 1;

        public static IMatchesService GetMatchesService()
        {
            var matchesService = new Mock<IMatchesService>();

            matchesService.Setup(m => m.Add(
                    It.IsAny<Data.Models.Match>()))
                .Returns(new Data.Models.Match());

            matchesService.Setup(m => m.Add(
                    It.IsAny<List<Data.Models.Match>>(),
                    It.IsAny<HashSet<int>>(),
                    It.Is<int>(i => i == MatchesToAdd)));

            matchesService.Setup(m => m.All())
                .Returns(TestObjectFactoryDataModels.Matches);

            matchesService.Setup(m => m.Get(
                    It.Is<int>(i => i < ValidId)))
                .Returns<Data.Models.Match>(null);

            matchesService.Setup(m => m.Get(
                    It.Is<int>(i => i >= ValidId)))
                .Returns(new Data.Models.Match() { Id = ValidId });

            matchesService.Setup(m => m.GetByXmlId(
                    It.Is<int>(i => i >= ValidId)))
                .Returns(TestObjectFactoryDataModels.Matches);

            matchesService.Setup(m => m.GetLatest(
                    It.IsAny<int>()))
                .Returns(TestObjectFactoryDataModels.Matches);

            matchesService.Setup(m => m.Search(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<int>(),
                    It.IsAny<int>()))
                .Returns(TestObjectFactoryDataModels.Matches);

            return matchesService.Object;
        }

        public static ICacheService GetCacheService<T>()
            where T : class
        {
            var cacheService = new Mock<ICacheService>();
            
            cacheService.Setup(c => c.Get(
                    It.IsAny<string>(),
                    It.IsAny<Func<T>>(),
                    It.IsAny<int>()))
                .Returns(new Mock<T>().Object);

            return cacheService.Object;
        }
    }
}
