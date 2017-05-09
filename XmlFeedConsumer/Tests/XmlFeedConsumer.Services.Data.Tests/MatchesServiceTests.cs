namespace XmlFeedConsumer.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using NUnit.Framework;

    using Contracts;
    using TestObjects;
    using XmlFeedConsumer.Data.Models;

    [TestFixture]
    public class MatchesServiceTests
    {
        private IMatchesService matchesService;

        private InMemoryRepository<Match> matchesRepository;

        private Match match;

        [OneTimeSetUp]
        public void TestInitialize()
        {
            this.matchesRepository = TestObjectFactoryRepositories.GetMatchesRepository();
            this.matchesService = new MatchesService(this.matchesRepository);
            this.match = new Match() { Name = "Match to add" };
        }

        [Test]
        public void AddSingleMatchShouldThrowExceptionWithInvalidMatch()
        {
            Assert.Throws(typeof(ArgumentNullException), () => this.matchesService.Add(null));
        }

        [Test]
        public void AddSingleMatchShouldInvokeSaveChanges()
        {
            var numberOfSaves = this.matchesRepository.NumberOfSaves;

            var result = this.matchesService.Add(this.match);
            var expected = numberOfSaves + 1;

            Assert.AreEqual(expected, this.matchesRepository.NumberOfSaves);
        }

        [Test]
        public void AddSingleMatchShouldPopulateMatchToDatabase()
        {
            var result = this.matchesService.Add(this.match);

            var addedMatches = this.matchesService.All()
                .FirstOrDefault(m => m.Name == this.match.Name);

            Assert.IsNotNull(addedMatches);
            Assert.AreEqual(this.match.Name, addedMatches.Name);
        }

        [Test]
        public void AddManyMatchesShouldThrowExceptionWithInvalidMatches()
        {
            var validMatchXmlIds = new HashSet<int>();
            var validMatchesToAdd = 1;

            Assert.Throws(typeof(ArgumentNullException), () => this.matchesService.Add(null, validMatchXmlIds, validMatchesToAdd));
        }

        [Test]
        public void AddManyMatchesShouldThrowExceptionWithInvalidMatchXmlIds()
        {
            var validMatches = new List<Match>();
            var validMatchesToAdd = 1;

            Assert.Throws(typeof(ArgumentNullException), () => this.matchesService.Add(validMatches, null, validMatchesToAdd));
        }

        [Test]
        public void AddManyMatchesShouldThrowExceptionWithInvalidMatchToAdd()
        {
            var validMatches = new List<Match>();
            var validMatchXmlIds = new HashSet<int>();
            var intvalidMatchesToAdd = -1;

            Assert.Throws(typeof(ArgumentOutOfRangeException), () => this.matchesService.Add(validMatches, validMatchXmlIds, intvalidMatchesToAdd));
        }

        [Test]
        public void AddManyMatchesShouldInvokeSaveChanges()
        {
            var validMatches = new List<Match>();
            validMatches.Add(this.match);

            var validMatchXmlIds = new HashSet<int>();
            var validMatchesToAdd = 1;

            this.matchesService.Add(validMatches, validMatchXmlIds, validMatchesToAdd);
            var expected = 1;

            Assert.AreEqual(expected, this.matchesRepository.NumberOfSaves);
        }

        [Test]
        public void AddManyMatchesShouldPopulateMatchesToDatabase()
        {
            var count = this.matchesService.All().Count();

            var validMatches = new List<Match>();
            validMatches.Add(this.match);

            var validMatchXmlIds = new HashSet<int>();
            var validMatchesToAdd = 1;

            this.matchesService.Add(validMatches, validMatchXmlIds, validMatchesToAdd);

            var expected = count + 1;

            Assert.AreEqual(expected, this.matchesService.All().Count());
        }

        [Test]
        public void GetByIdShouldThrowExceptionWithInvalidId()
        {
            var invalidId = -1;
            Assert.Throws(typeof(ArgumentOutOfRangeException), () => this.matchesService.Get(invalidId));
        }

        [Test]
        public void GetByIdShouldReturnMatch()
        {
            var validId = 1;
            var result = this.matchesService.Get(validId);

            Assert.IsNotNull(result);
        }

        [Test]
        public void GetByXmlIdShouldThrowExceptionWithInvalidXmlId()
        {
            var invalidXmlId = -1;
            Assert.Throws(typeof(ArgumentOutOfRangeException), () => this.matchesService.GetByXmlId(invalidXmlId));
        }

        [Test]
        public void GetByXmlIdShouldReturnMatch()
        {
            var validXmlId = 1;
            var result = this.matchesService.GetByXmlId(validXmlId).First();

            Assert.IsNotNull(result);
        }

        [Test]
        public void GetAllShouldReturnAllNotDeletedMatchesInDatabase()
        {
            var result = this.matchesService.All();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.All(m => !m.IsDeleted && m.DeletedOn == null));
        }

        [Test]
        public void GetAllWithDeletedShouldReturnAllMatchesInDatabase()
        {
            var result = this.matchesService.AllWithDeleted();

            Assert.IsNotNull(result);
        }

        [Test]
        public void GetLatestShouldThrowExceptionWithInvalidCount()
        {
            var invalidCount = -1;

            Assert.Throws(typeof(ArgumentOutOfRangeException), () => this.matchesService.GetLatest(invalidCount));
        }

        [Test]
        public void GetLatestShouldReturnData()
        {
            var validCount = 1;

            var result = this.matchesService.GetLatest(validCount).ToList();

            Assert.IsNotNull(result);
        }

        [Test]
        public void UpdateSingleMatchShouldThrowExceptionWithInvalidId()
        {
            var invalidId = -1;

            Assert.Throws(typeof(ArgumentOutOfRangeException), () => this.matchesService.Update(invalidId, this.match));
        }

        [Test]
        public void UpdateSingleMatchShouldThrowExceptionWithInvalidMatch()
        {
            var validId = 1;

            Assert.Throws(typeof(ArgumentNullException), () => this.matchesService.Update(validId, null));
        }

        [Test]
        public void UpdateSingleMatchShouldInvokeSaveChanges()
        {
            var saves = this.matchesRepository.NumberOfSaves;

            var validId = 1;
            var result = this.matchesService.Update(validId, this.match);

            var expected = saves + 1;

            Assert.AreEqual(expected, this.matchesRepository.NumberOfSaves);
        }

        [Test]
        public void UpdateSingleMatchShouldReturnEditedMatch()
        {
            var validId = 1;
            var result = this.matchesService.Update(validId, this.match);

            Assert.IsNotNull(result);
        }

        [Test]
        public void UpdateSingleMatchShouldEditMatch()
        {
            var validId = 1;
            var result = this.matchesService.Update(validId, this.match);

            Assert.AreEqual(this.match.Name, result.Name);
        }

        [Test]
        public void UpdateManyMatchesShouldThrowExceptionWithInvalidMatches()
        {
            var validMatchesToProcessed = 1;

            Assert.Throws(typeof(ArgumentNullException), () => this.matchesService.Update(null, validMatchesToProcessed));
        }

        [Test]
        public void UpdateManyMatchesShouldThrowExceptionWithInvalidMatchesToProcessed()
        {
            var invalidMatchesToProcessed = -1;

            var validMatches = new List<Match>();
            validMatches.Add(this.match);

            Assert.Throws(typeof(ArgumentOutOfRangeException), () => this.matchesService.Update(validMatches, invalidMatchesToProcessed));
        }

        [Test]
        public void UpdateManyMatchesShouldReturnEditedMatches()
        {
            var validMatchesToProcessed = 1;

            var validMatches = new List<Match>();
            validMatches.Add(this.match);

            var result = this.matchesService.Update(validMatches, validMatchesToProcessed).ToList();

            Assert.IsNotNull(result);
        }

        [Test]
        public void DeleteShouldThrowExceptionWithInvalidId()
        {
            var invalidId = -1;

            Assert.Throws(typeof(ArgumentOutOfRangeException), () => this.matchesService.Delete(invalidId));
        }

        [Test]
        public void DeleteShouldInvokeSaveChanges()
        {
            var saves = this.matchesRepository.NumberOfSaves;

            var validId = 1;
            var result = this.matchesService.Delete(validId);

            var expected = saves + 1;

            Assert.AreEqual(expected, this.matchesRepository.NumberOfSaves);
        }

        [Test]
        public void DeleteShouldReturnNotNullMatch()
        {
            var validId = 1;
            var result = this.matchesService.Delete(validId);

            Assert.IsNotNull(result);
        }

        [Test]
        public void DeleteShouldSetIsDeletedProperyToTrue()
        {
            var validId = 1;
            var result = this.matchesService.Delete(validId);

            Assert.IsTrue(result.IsDeleted);
            Assert.IsNotNull(result.DeletedOn);
        }

        [Test]
        public void HardDeleteShouldThrowExceptionWithInvalidId()
        {
            var invalidId = -1;

            Assert.Throws(typeof(ArgumentOutOfRangeException), () => this.matchesService.HardDelete(invalidId));
        }

        [Test]
        public void HardDeleteShouldInvokeSaveChanges()
        {
            var saves = this.matchesRepository.NumberOfSaves;

            var validId = 1;
            var result = this.matchesService.HardDelete(validId);

            var expected = saves + 1;

            Assert.AreEqual(expected, this.matchesRepository.NumberOfSaves);
        }

        [Test]
        public void HardDeleteShouldReturnTrueWhenMatchIsDeleted()
        {
            var count = this.matchesService.All().Count();

            var validId = 1;
            var result = this.matchesService.HardDelete(validId);

            Assert.IsTrue(result);
        }
    }
}
