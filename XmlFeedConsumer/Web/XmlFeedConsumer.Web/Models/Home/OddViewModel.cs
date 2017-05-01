namespace XmlFeedConsumer.Web.Models.Home
{
    using Data.Models;
    using Infrastructure.Mapping;

    public class OddViewModel : IMapFrom<Odd>
    {
        public int XmlId { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public string SpecialBetValue { get; set; }
    }
}