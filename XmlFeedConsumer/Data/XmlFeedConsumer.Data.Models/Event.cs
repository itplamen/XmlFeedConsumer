namespace XmlFeedConsumer.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Common.Models;

    public class Event : BaseModel<int>
    {
        public Event()
        {
            this.Matches = new HashSet<Match>();
        }

        [Required]
        [Index(IsUnique = true)]
        public int XmlId { get; set; }

        [Required]
        public string Name { get; set; }

        public bool IsLive { get; set; }

        [Required]
        public int CategoryID { get; set; }

        public int SportId { get; set; }

        public virtual Sport Sport { get; set; }

        public virtual ICollection<Match> Matches { get; set; }
    }
}
