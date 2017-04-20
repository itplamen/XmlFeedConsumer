namespace XmlFeedConsumer.Data.Models
{
    using System.Collections.Generic;

    using Common.Models;

    public class XmlSport : BaseModel<int>
    {
        public XmlSport()
        {
            this.Sports = new HashSet<Sport>();
        }

        public virtual ICollection<Sport> Sports { get; set; }
    }
}
