namespace XmlFeedConsumer.Web.Models.Home
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    using AutoMapper;

    using Data.Models;
    using Infrastructure.Mapping;

    [XmlRoot(ElementName = "Sport")]
    public class SportViewModel : IMapFrom<Sport>, IHaveCustomMappings
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlElement("Event")]
        public List<EventViewModel> Events { get; set; }

        public void CreateMappings(IConfiguration config)
        {
            config.CreateMap<Sport, SportViewModel>()
                .ForMember(x => x.Events, opt => opt.MapFrom(x => x.Events));
        }
    }
}