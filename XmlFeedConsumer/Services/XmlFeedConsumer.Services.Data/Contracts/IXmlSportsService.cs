namespace XmlFeedConsumer.Services.Data.Contracts
{
    using System.Linq;

    using XmlFeedConsumer.Data.Models;

    public interface IXmlSportsService
    {
        int Add(XmlSport xmlSport);

        XmlSport Get(int id);

        IQueryable<XmlSport> GetAsQueryable(int id);

        IQueryable<XmlSport> All();

        IQueryable<XmlSport> AllWithDeleted();

        XmlSport Update(int id, XmlSport xmlSport);

        XmlSport Delete(int id);

        bool HardDelete(int id);
    }
}
