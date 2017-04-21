namespace XmlFeedConsumer.Web.Models.Home
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    using Data.Models;
    using Infrastructure.Mapping;

    [XmlRoot(ElementName = "Match")]
    public class MatchViewModel : IMapFrom<Match> 
    {
        [XmlAttribute("ID")]
        public int XmlId { get; set; }

        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlAttribute("StartDate")]
        public DateTime StartDate { get; set; }

        [XmlAttribute("MatchType")]
        public string MatchType { get; set; }

        [XmlElement("Bet")]
        public List<BetViewModel> Bets { get; set; }
    }
}