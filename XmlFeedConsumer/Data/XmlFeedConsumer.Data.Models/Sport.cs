namespace XmlFeedConsumer.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Common.Models;
    using System.Xml.Serialization;

    public class Sport : BaseModel<int>
    {
        public Sport()
        {
            this.Events = new HashSet<Event>();
        }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}
