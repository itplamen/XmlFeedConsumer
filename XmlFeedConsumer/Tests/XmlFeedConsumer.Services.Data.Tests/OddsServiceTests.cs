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
    public class OddsServiceTests
    {
        private IOddsService oddsService;

        private InMemoryRepository<Odd> oddsRepository;

        private Odd odd;

        [OneTimeSetUp]
        public void TestInitialize()
        {
            this.oddsRepository = TestObjectFactoryRepositories.GetOddsRepository();
            this.oddsService = new OddsService(this.oddsRepository);
            this.odd = new Odd() { Name = "Odd to add" };
        }

        [Test]
        public void AddShouldThrowExceptionWithInvalidOdd()
        {
            Assert.Throws(typeof(ArgumentNullException), () => this.oddsService.Add(null));
        }

        [Test]
        public void AddShouldInvokeSaveChanges()
        {
            var result = this.oddsService.Add(this.odd);
            var expected = 1;

            Assert.AreEqual(expected, this.oddsRepository.NumberOfSaves);
        }

        [Test]
        public void AddShouldPopulateOddToDatabase()
        {
            var result = this.oddsService.Add(this.odd);

            var addedOdd = this.oddsService.All()
                .FirstOrDefault(o => o.Name == this.odd.Name);

            Assert.IsNotNull(addedOdd);
            Assert.AreEqual(this.odd.Name, addedOdd.Name);
        }

        [Test]
        public void GetByIdShouldThrowExceptionWithInvalidId()
        {
            var invalidId = -1;
            Assert.Throws(typeof(ArgumentOutOfRangeException), () => this.oddsService.Get(invalidId));
        }

        [Test]
        public void GetByIdShouldReturnOdd()
        {
            var validId = 1;
            var result = this.oddsService.Get(validId);

            Assert.IsNotNull(result);
        }

        [Test]
        public void GetByXmlIdShouldThrowExceptionWithInvalidXmlId()
        {
            var invalidXmlId = -1;
            Assert.Throws(typeof(ArgumentOutOfRangeException), () => this.oddsService.GetByXmlId(invalidXmlId));
        }

        [Test]
        public void GetByXmlIdShouldReturnOdd()
        {
            var validXmlId = 1;
            var result = this.oddsService.GetByXmlId(validXmlId).First();

            Assert.IsNotNull(result);
        }

        [Test]
        public void GetAllShouldReturnAllNotDeletedOddsInDatabase()
        {
            var result = this.oddsService.All();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.All(o => !o.IsDeleted && o.DeletedOn == null));
        }

        [Test]
        public void GetAllWithDeletedShouldReturnAllOddsInDatabase()
        {
            var result = this.oddsService.AllWithDeleted();

            Assert.IsNotNull(result);
        }

        [Test]
        public void UpdateSingleOddShouldThrowExceptionWithInvalidId()
        {
            var invalidId = -1;

            Assert.Throws(typeof(ArgumentOutOfRangeException), () => this.oddsService.Update(invalidId, this.odd));
        }

        [Test]
        public void UpdateSingleOddShouldThrowExceptionWithInvalidOdd()
        {
            var validId = 1;

            Assert.Throws(typeof(ArgumentNullException), () => this.oddsService.Update(validId, null));
        }

        [Test]
        public void UpdateSingleOddShouldInvokeSaveChanges()
        {
            var saves = this.oddsRepository.NumberOfSaves;

            var validId = 1;
            var result = this.oddsService.Update(validId, this.odd);

            var expected = saves + 1;

            Assert.AreEqual(expected, this.oddsRepository.NumberOfSaves);
        }

        [Test]
        public void UpdateSingleOddShouldReturnEditedOdd()
        {
            var validId = 1;
            var result = this.oddsService.Update(validId, this.odd);

            Assert.IsNotNull(result);
        }

        [Test]
        public void UpdateSingleOddShouldEditOdd()
        {
            var validId = 1;
            var result = this.oddsService.Update(validId, this.odd);

            Assert.AreEqual(this.odd.Name, result.Name);
        }

        [Test]
        public void UpdateManyOddsShouldThrowExceptionWithInvalidOdds()
        {
            var validOddsToProcessed = 1;

            Assert.Throws(typeof(ArgumentNullException), () => this.oddsService.Update(null, validOddsToProcessed));
        }

        [Test]
        public void UpdateManyOddsShouldThrowExceptionWithInvalidOddsToProcessed()
        {
            var invalidOddsToProcessed = -1;

            var validOdds = new List<Odd>();
            validOdds.Add(this.odd);

            Assert.Throws(typeof(ArgumentOutOfRangeException), () => this.oddsService.Update(validOdds, invalidOddsToProcessed));
        }

        [Test]
        public void UpdateManyOddsShouldReturnEditedOdds()
        {
            var validOddsToProcessed = 1;

            var validOdds = new List<Odd>();
            validOdds.Add(this.odd);

            var result = this.oddsService.Update(validOdds, validOddsToProcessed).ToList();

            Assert.IsNotNull(result);
        }

        [Test]
        public void DeleteShouldThrowExceptionWithInvalidId()
        {
            var invalidId = -1;

            Assert.Throws(typeof(ArgumentOutOfRangeException), () => this.oddsService.Delete(invalidId));
        }

        [Test]
        public void DeleteShouldInvokeSaveChanges()
        {
            var saves = this.oddsRepository.NumberOfSaves;

            var validId = 1;
            var result = this.oddsService.Delete(validId);

            var expected = saves + 1;

            Assert.AreEqual(expected, this.oddsRepository.NumberOfSaves);
        }

        [Test]
        public void DeleteShouldReturnNotNullOdd()
        {
            var validId = 1;
            var result = this.oddsService.Delete(validId);

            Assert.IsNotNull(result);
        }

        [Test]
        public void DeleteShouldSetIsDeletedProperyToTrue()
        {
            var validId = 1;
            var result = this.oddsService.Delete(validId);

            Assert.IsTrue(result.IsDeleted);
            Assert.IsNotNull(result.DeletedOn);
        }

        [Test]
        public void HardDeleteShouldThrowExceptionWithInvalidId()
        {
            var invalidId = -1;

            Assert.Throws(typeof(ArgumentOutOfRangeException), () => this.oddsService.HardDelete(invalidId));
        }

        [Test]
        public void HardDeleteShouldInvokeSaveChanges()
        {
            var saves = this.oddsRepository.NumberOfSaves;

            var validId = 1;
            var result = this.oddsService.HardDelete(validId);

            var expected = saves + 1;

            Assert.AreEqual(expected, this.oddsRepository.NumberOfSaves);
        }

        [Test]
        public void HardDeleteShouldReturnTrueWhenOddIsDeleted()
        {
            var count = this.oddsService.All().Count();

            var validId = 1;
            var result = this.oddsService.HardDelete(validId);

            Assert.IsTrue(result);
        }
    }
}
