namespace XmlFeedConsumer.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Common.Models;

    public class Match : BaseModel<int>
    {
        public Match()
        {
            this.Bets = new HashSet<Bet>();
        }

        [Required]
        [Index(IsUnique = true)]
        public int XmlId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public string MatchType { get; set; }

        public virtual ICollection<Bet> Bets { get; set; }
    }
}
