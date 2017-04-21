namespace XmlFeedConsumer.Web.Models.Home
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    using Data.Models;
    using Infrastructure.Mapping;

    [XmlRoot(ElementName = "Bet")]
    public class BetViewModel : IMapFrom<Bet> 
    {
        [XmlAttribute("ID")]
        public int XmlId { get; set; }

        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlAttribute("IsLive")]
        public bool IsLive { get; set; }

        [XmlElement("Odd")]
        public List<OddViewModel> Odds { get; set; }
    }
}