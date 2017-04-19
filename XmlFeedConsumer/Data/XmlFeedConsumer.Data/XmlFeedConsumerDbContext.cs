namespace XmlFeedConsumer.Data
{
    using System.Data.Entity;

    public class XmlFeedConsumerDbContext : DbContext
    {
        public XmlFeedConsumerDbContext()
            : base("DefaultConnection")
        {
        }

        public static XmlFeedConsumerDbContext Create()
        {
            return new XmlFeedConsumerDbContext();
        }
    }
}
