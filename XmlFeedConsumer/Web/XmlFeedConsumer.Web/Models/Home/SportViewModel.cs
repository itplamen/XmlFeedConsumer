namespace XmlFeedConsumer.Web.Models.Home
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    using Data.Models;
    using Infrastructure.Mapping;

    [XmlRoot(ElementName = "Sport")]
    public class SportViewModel : IMapFrom<Sport> 
    {
        [XmlAttribute("ID")]
        public int XmlId { get; set; }

        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlElement("Event")]
        public List<EventViewModel> Events { get; set; }
    }
}