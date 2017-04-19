namespace XmlFeedConsumer.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Common.Models;

    public class Match : BaseModel<int>
    {
        public Match()
        {
            this.Bets = new HashSet<Bet>();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public string MatchType { get; set; }

        public int EventId { get; set; }

        public virtual Event Event { get; set; }

        public virtual ICollection<Bet> Bets { get; set; }
    }
}
