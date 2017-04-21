namespace XmlFeedConsumer.Web.Models.Home
{
    using System.Xml.Serialization;

    using Data.Models;
    using Infrastructure.Mapping;

    [XmlRoot(ElementName = "Odd")]
    public class OddViewModel : IMapFrom<Odd>
    {
        [XmlAttribute("ID")]
        public int XmlId { get; set; }

        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlAttribute("Value")]
        public double Value { get; set; }

        [XmlAttribute("SpecialBetValue")]
        public string SpecialBetValue { get; set; }
    }
}