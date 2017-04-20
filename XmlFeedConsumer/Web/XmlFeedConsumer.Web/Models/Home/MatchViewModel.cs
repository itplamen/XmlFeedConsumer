namespace XmlFeedConsumer.Web.Models.Home
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    using AutoMapper;

    using Data.Models;
    using Infrastructure.Mapping;

    [XmlRoot(ElementName = "Match")]
    public class MatchViewModel : IMapFrom<Match>, IHaveCustomMappings
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlAttribute("StartDate")]
        public DateTime StartDate { get; set; }

        [XmlAttribute("MatchType")]
        public string MatchType { get; set; }

        [XmlElement("Bet")]
        public List<BetViewModel> Bets { get; set; }

        public void CreateMappings(IConfiguration config)
        {
            config.CreateMap<Match, MatchViewModel>()
                .ForMember(x => x.Bets, opt => opt.MapFrom(x => x.Bets));
        }
    }
}