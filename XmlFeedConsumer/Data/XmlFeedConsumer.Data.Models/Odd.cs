namespace XmlFeedConsumer.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Common.Models;

    public class Odd : BaseModel<int>
    {
        [Required]
        [Index(IsUnique = true)]
        public int XmlId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Value { get; set; }

        public string SpecialBetValue { get; set; }

        public int BetId { get; set; }

        public virtual Bet Bet { get; set; }
    }
}
