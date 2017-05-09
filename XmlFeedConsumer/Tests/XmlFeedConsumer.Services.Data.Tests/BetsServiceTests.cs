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
    public class BetsServiceTests
    {
        private IBetsService betsService;

        private InMemoryRepository<Bet> betsRepository;

        private Bet bet;

        [OneTimeSetUp]
        public void TestInitialize()
        {
            this.betsRepository = TestObjectFactoryRepositories.GetBetsRepository();
            this.betsService = new BetsService(this.betsRepository);
            this.bet = new Bet() { Name = "Bet to add" };
        }

        [Test]
        public void AddShouldThrowExceptionWithInvalidBet()
        {
            Assert.Throws(typeof(ArgumentNullException), () => this.betsService.Add(null));
        }

        [Test]
        public void AddShouldInvokeSaveChanges()
        {
            var result = this.betsService.Add(this.bet);
            var expected = 1;

            Assert.AreEqual(expected, this.betsRepository.NumberOfSaves);
        }

        [Test]
        public void AddShouldReturnAddedBet()
        {
            var result = this.betsService.Add(this.bet);

            Assert.IsNotNull(result);
            Assert.AreEqual(this.bet, result);
        }

        [Test]
        public void AddShouldPopulateBetToDatabase()
        {
            var result = this.betsService.Add(this.bet);

            var addedBet = this.betsService.All()
                .FirstOrDefault(b => b.Name == this.bet.Name);

            Assert.IsNotNull(addedBet);
            Assert.AreEqual(this.bet.Name, addedBet.Name);
        }

        [Test]
        public void GetByIdShouldThrowExceptionWithInvalidId()
        {
            var invalidId = -1;
            Assert.Throws(typeof(ArgumentOutOfRangeException), () => this.betsService.Get(invalidId));
        }

        [Test]
        public void GetByIdShouldReturnBet()
        {
            var validId = 1;
            var result = this.betsService.Get(validId);

            Assert.IsNotNull(result);
        }

        [Test]
        public void GetByXmlIdShouldThrowExceptionWithInvalidXmlId()
        {
            var invalidXmlId = -1;
            Assert.Throws(typeof(ArgumentOutOfRangeException), () => this.betsService.GetByXmlId(invalidXmlId));
        }

        [Test]
        public void GetByXmlIdShouldReturnBet()
        {
            var validXmlId = 1;
            var result = this.betsService.GetByXmlId(validXmlId).First();

            Assert.IsNotNull(result);
        }

        [Test]
        public void GetAllShouldReturnAllNotDeletedBetsInDatabase()
        {
            var result = this.betsService.All();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.All(b => !b.IsDeleted && b.DeletedOn == null));
        }

        [Test]
        public void GetAllWithDeletedShouldReturnAllBetsInDatabase()
        {
            var result = this.betsService.AllWithDeleted();

            Assert.IsNotNull(result);
        }

        [Test]
        public void UpdateSingleBetShouldThrowExceptionWithInvalidId()
        {
            var invalidId = -1;

            Assert.Throws(typeof(ArgumentOutOfRangeException), () => this.betsService.Update(invalidId, this.bet));
        }

        [Test]
        public void UpdateSingleBetShouldThrowExceptionWithInvalidBet()
        {
            var validId = 1;

            Assert.Throws(typeof(ArgumentNullException), () => this.betsService.Update(validId, null));
        }

        [Test]
        public void UpdateSingleBetShouldInvokeSaveChanges()
        {
            var saves = this.betsRepository.NumberOfSaves;

            var validId = 1;
            var result = this.betsService.Update(validId, this.bet);

            var expected = saves + 1;

            Assert.AreEqual(expected, this.betsRepository.NumberOfSaves);
        }

        [Test]
        public void UpdateSingleBetShouldReturnEditedBet()
        {
            var validId = 1;
            var result = this.betsService.Update(validId, this.bet);

            Assert.IsNotNull(result);
        }

        [Test]
        public void UpdateSingleBetShouldEditBet()
        {
            var validId = 1;
            var result = this.betsService.Update(validId, this.bet);

            Assert.AreEqual(this.bet.Name, result.Name);
        }

        [Test]
        public void UpdateManyBetsShouldThrowExceptionWithInvalidBets()
        {
            var validBetsToProcessed = 1;

            Assert.Throws(typeof(ArgumentNullException), () => this.betsService.Update(null, validBetsToProcessed));
        }

        [Test]
        public void UpdateManyBetsShouldThrowExceptionWithInvalidBetsToProcessed()
        {
            var invalidBetsToProcessed = -1;

            var validBets = new List<Bet>();
            validBets.Add(this.bet);

            Assert.Throws(typeof(ArgumentOutOfRangeException), () => this.betsService.Update(validBets, invalidBetsToProcessed));
        }

        [Test]
        public void UpdateManyBetsShouldReturnEditedBets()
        {
            var validBetsToProcessed = 1;

            var validBets = new List<Bet>();
            validBets.Add(this.bet);

            var result = this.betsService.Update(validBets, validBetsToProcessed).ToList();

            Assert.IsNotNull(result);
        }

        [Test]
        public void DeleteShouldThrowExceptionWithInvalidId()
        {
            var invalidId = -1;

            Assert.Throws(typeof(ArgumentOutOfRangeException), () => this.betsService.Delete(invalidId));
        }

        [Test]
        public void DeleteShouldInvokeSaveChanges()
        {
            var saves = this.betsRepository.NumberOfSaves;

            var validId = 1;
            var result = this.betsService.Delete(validId);

            var expected = saves + 1;

            Assert.AreEqual(expected, this.betsRepository.NumberOfSaves);
        }

        [Test]
        public void DeleteShouldReturnNotNullBet()
        {
            var validId = 1;
            var result = this.betsService.Delete(validId);

            Assert.IsNotNull(result);
        }

        [Test]
        public void DeleteShouldSetIsDeletedProperyToTrue()
        {
            var validId = 1;
            var result = this.betsService.Delete(validId);

            Assert.IsTrue(result.IsDeleted);
            Assert.IsNotNull(result.DeletedOn);
        }

        [Test]
        public void HardDeleteShouldThrowExceptionWithInvalidId()
        {
            var invalidId = -1;

            Assert.Throws(typeof(ArgumentOutOfRangeException), () => this.betsService.HardDelete(invalidId));
        }

        [Test]
        public void HardDeleteShouldInvokeSaveChanges()
        {
            var saves = this.betsRepository.NumberOfSaves;

            var validId = 1;
            var result = this.betsService.HardDelete(validId);

            var expected = saves + 1;

            Assert.AreEqual(expected, this.betsRepository.NumberOfSaves);
        }

        [Test]
        public void HardDeleteShouldReturnTrueWhenBetIsDeleted()
        {
            var count = this.betsService.All().Count();

            var validId = 1;
            var result = this.betsService.HardDelete(validId);

            Assert.IsTrue(result);
        }
    }
}
