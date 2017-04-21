namespace XmlFeedConsumer.Web.Models.Home
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    using Data.Models;
    using Infrastructure.Mapping;
 
    [XmlRoot(ElementName = "Event")]
    public class EventViewModel : IMapFrom<Event> 
    {
        [XmlAttribute("ID")]
        public int XmlId { get; set; }

        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlAttribute("IsLive")]
        public bool IsLive { get; set; }

        [XmlAttribute("CategoryID")]
        public int CategoryID { get; set; }

        [XmlElement("Match")]
        public List<MatchViewModel> Matches { get; set; }
    }
}