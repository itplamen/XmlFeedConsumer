namespace XmlFeedConsumer.Services.Data
{
    using System.Linq;

    using Bytes2you.Validation;

    using Common;
    using Contracts;
    using XmlFeedConsumer.Data.Common;
    using XmlFeedConsumer.Data.Models;

    public class XmlSportsService : IXmlSportsService
    {
        private readonly IDbRepository<XmlSport> xmlSportsRepository;

        public XmlSportsService(IDbRepository<XmlSport> xmlSportsRepository)
        {
            Guard.WhenArgument(xmlSportsRepository, nameof(xmlSportsRepository)).IsNull().Throw();

            this.xmlSportsRepository = xmlSportsRepository;
        }

        public int Add(XmlSport xmlSport)
        {
            Guard.WhenArgument(xmlSport, nameof(xmlSport)).IsNull().Throw();

            this.xmlSportsRepository.Add(xmlSport);
            this.xmlSportsRepository.Save();

            return xmlSport.Id;
        }

        public XmlSport Get(int id)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(ValidationConstants.InvalidId).Throw();

            return this.xmlSportsRepository.GetById(id);
        }

        public IQueryable<XmlSport> GetAsQueryable(int id)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(ValidationConstants.InvalidId).Throw();

            return this.xmlSportsRepository.All()
                .Where(x => x.Id == id);
        }

        public IQueryable<XmlSport> All()
        {
            return this.xmlSportsRepository.All();
        }

        public IQueryable<XmlSport> AllWithDeleted()
        {
            return this.xmlSportsRepository.AllWithDeleted();
        }

        public XmlSport Update(int id, XmlSport xmlSport)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(ValidationConstants.InvalidId).Throw();
            Guard.WhenArgument(xmlSport, nameof(xmlSport)).IsNull().Throw();

            var xmlSportToUpdate = this.xmlSportsRepository.GetById(id);

            if (xmlSportToUpdate != null)
            {
                xmlSportToUpdate.CreatedOn = xmlSport.CreatedOn;
                xmlSportToUpdate.IsDeleted = xmlSport.IsDeleted;
                xmlSportToUpdate.DeletedOn = xmlSport.DeletedOn;

                this.xmlSportsRepository.Save();
            }

            return xmlSportToUpdate;
        }

        public XmlSport Delete(int id)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(ValidationConstants.InvalidId).Throw();

            var xmlSportToDelete = this.xmlSportsRepository.GetById(id);

            if (xmlSportToDelete != null)
            {
                this.xmlSportsRepository.Delete(xmlSportToDelete);
                this.xmlSportsRepository.Save();
            }

            return xmlSportToDelete;
        }

        public bool HardDelete(int id)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(ValidationConstants.InvalidId).Throw();

            var sportToDelete = this.xmlSportsRepository.GetById(id);

            if (sportToDelete != null)
            {
                this.xmlSportsRepository.HardDelete(sportToDelete);
                this.xmlSportsRepository.Save();

                return true;
            }

            return false;
        }
    }
}
