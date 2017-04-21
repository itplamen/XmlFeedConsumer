namespace XmlFeedConsumer.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Common.Models;

    public class Sport : BaseModel<int>
    {
        public Sport()
        {
            this.Events = new HashSet<Event>();
        }

        [Required]
        [Index(IsUnique = true)]
        public int XmlId { get; set; }

        [Required]
        public string Name { get; set; }

        public int XmlSportId { get; set; }

        public virtual XmlSport XmlSport { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}
