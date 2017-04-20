namespace XmlFeedConsumer.Web.Models.Home
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    using AutoMapper;

    using Data.Models;
    using Infrastructure.Mapping;

    [XmlRoot(ElementName = "XmlSports")]
    public class XmlSportsViewModel : IMapFrom<XmlSport>, IHaveCustomMappings
    {
        [XmlElement("Sport")]
        public List<SportViewModel> Sports { get; set; }

        public void CreateMappings(IConfiguration config)
        {
            config.CreateMap<XmlSport, XmlSportsViewModel>()
                .ForMember(x => x.Sports, opt => opt.MapFrom(x => x.Sports));
        }
    }
}