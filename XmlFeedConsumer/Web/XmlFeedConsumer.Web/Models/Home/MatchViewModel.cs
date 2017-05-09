namespace XmlFeedConsumer.Web.Models.Home
{
    using System;
    using System.Collections.Generic;

    using AutoMapper;

    using Data.Models;
    using Infrastructure.Mapping;

    public class MatchViewModel : IMapFrom<Match>, IHaveCustomMappings
    {
        public int XmlId { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public string MatchType { get; set; }

        public bool IsDeleted { get; set; }

        public IEnumerable<BetViewModel> Bets { get; set; }

        public void CreateMappings(IConfiguration config)
        {
            config.CreateMap<Match, MatchViewModel>()
                .ForMember(x => x.IsDeleted, opt => opt.MapFrom(x => x.IsDeleted));
        }
    }
}