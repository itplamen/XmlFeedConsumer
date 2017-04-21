namespace XmlFeedConsumer.Web.Models.Home
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    using Data.Models;
    using Infrastructure.Mapping;

    [XmlRoot(ElementName = "XmlSports")]
    public class XmlSportsViewModel : IMapFrom<XmlSport> 
    {
        [XmlElement("Sport")]
        public List<SportViewModel> Sports { get; set; }
    }
}