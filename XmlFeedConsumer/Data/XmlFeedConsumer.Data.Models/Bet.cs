namespace XmlFeedConsumer.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Common.Models;

    public class Bet : BaseModel<int>
    {
        public Bet()
        {
            this.Odds = new HashSet<Odd>();
        }

        [Required]
        public string Name { get; set; }

        public bool IsLive { get; set; }

        public int MatchId { get; set; }

        public virtual Match Match { get; set; }

        public virtual ICollection<Odd> Odds { get; set; }
    }
}
