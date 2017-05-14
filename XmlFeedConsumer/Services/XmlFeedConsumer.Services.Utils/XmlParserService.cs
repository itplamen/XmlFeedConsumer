namespace XmlFeedConsumer.Services.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    using Bytes2you.Validation;

    using Contracts;
    using Data.Models;

    public class XmlParserService : IXmlParserService
    {
        private const int TwentyFourHours = 24;

        public IEnumerable<Match> GetMatches(string uri)
        {
            Guard.WhenArgument(uri, nameof(uri)).IsNullOrEmpty().Throw();

            var validMatches = this.GetValidMatches(uri);

            return validMatches
                .Select(m => new Match
                {
                    XmlId = int.Parse(m.Attribute("ID").Value),
                    Name = m.Attribute("Name").Value,
                    StartDate = Convert.ToDateTime(m.Attribute("StartDate").Value),
                    MatchType = m.Attribute("MatchType").Value,
                    Bets = m.Elements("Bet").Where(b => b.Elements("Odd").Any()).Select(b => new Bet
                    {
                        XmlId = int.Parse(b.Attribute("ID").Value),
                        Name = b.Attribute("Name").Value,
                        IsLive = Convert.ToBoolean(b.Attribute("IsLive").Value),
                        Odds = b.Elements("Odd").Select(o => new Odd
                        {
                            XmlId = int.Parse(o.Attribute("ID").Value),
                            Name = o.Attribute("Name").Value,
                            Value = o.Attribute("Value").Value,
                            SpecialBetValue = o.Attribute("Attribute") != null ? o.Attribute("Attribute").Value : null
                        }).ToList()
                    }).ToList()
                });
        }

        public IEnumerable<Bet> GetBets(string uri)
        {
            Guard.WhenArgument(uri, nameof(uri)).IsNullOrEmpty().Throw();

            var validMatches = this.GetValidMatches(uri);
                
            return validMatches
                .Select(b => new Bet()
                {
                    XmlId =  int.Parse(b.Element("Bet").Attribute("ID").Value),
                    Name = b.Element("Bet").Attribute("Name").Value,
                    IsLive = Convert.ToBoolean(b.Element("Bet").Attribute("IsLive").Value)
                });
        }

        public IEnumerable<Odd> GetOdds(string uri)
        {
            Guard.WhenArgument(uri, nameof(uri)).IsNullOrEmpty().Throw();

            var validMatches = this.GetValidMatches(uri);

            return validMatches
                .Where(b => b.Element("Bet").Descendants().Any())
                .Select(o => new Odd()
                {
                    XmlId = int.Parse(o.Element("Bet").Element("Odd").Attribute("ID").Value),
                    Name = o.Element("Bet").Element("Odd").Attribute("Name").Value,
                    Value = o.Element("Bet").Element("Odd").Attribute("Value").Value,
                    SpecialBetValue = o.Element("Bet").Element("Odd").Attribute("SpecialBetValue") != null ?
                        o.Element("Bet").Element("Odd").Attribute("SpecialBetValue").Value : null
                });
        }

        /// <summary>
        /// Returns all matches which have odds and will start in the next 24 hours.
        /// </summary>
        /// <param name="uri">Path to the xml file or feed.</param>
        private IEnumerable<XElement> GetValidMatches(string uri)
        {
            XDocument xmlDoc = XDocument.Load(uri);

            return xmlDoc.Descendants("Match")
                .Where(m => ((Convert.ToDateTime(m.Attribute("StartDate").Value)) - DateTime.Now).TotalHours >= 0 &&
                    ((Convert.ToDateTime(m.Attribute("StartDate").Value)) - DateTime.Now).TotalHours <= TwentyFourHours &&
                    m.Elements("Bet").Elements("Odd").Any());
        }
    }
}