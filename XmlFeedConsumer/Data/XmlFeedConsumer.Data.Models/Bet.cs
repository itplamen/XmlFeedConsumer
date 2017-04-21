namespace XmlFeedConsumer.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Common.Models;

    public class Bet : BaseModel<int>
    {
        public Bet()
        {
            this.Odds = new HashSet<Odd>();
        }

        [Required]
        [Index(IsUnique = true)]
        public int XmlId { get; set; }

        [Required]
        public string Name { get; set; }

        public bool IsLive { get; set; }

        public int MatchId { get; set; }

        public virtual Match Match { get; set; }

        public virtual ICollection<Odd> Odds { get; set; }
    }
}
