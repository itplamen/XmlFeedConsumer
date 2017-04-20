namespace XmlFeedConsumer.Web.Models.Home
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    using AutoMapper;

    using Data.Models;
    using Infrastructure.Mapping;

    [XmlRoot(ElementName = "Bet")]
    public class BetViewModel : IMapFrom<Bet>, IHaveCustomMappings
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlAttribute("IsLive")]
        public bool IsLive { get; set; }

        [XmlElement("Odd")]
        public List<OddViewModel> Odds { get; set; }

        public void CreateMappings(IConfiguration config)
        {
            config.CreateMap<Bet, BetViewModel>()
                .ForMember(x => x.Odds, opt => opt.MapFrom(x => x.Odds));
        }
    }
}