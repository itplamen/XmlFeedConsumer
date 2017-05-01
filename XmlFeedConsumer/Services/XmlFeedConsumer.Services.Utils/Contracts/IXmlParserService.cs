namespace XmlFeedConsumer.Services.Utils.Contracts
{
    using System.Collections.Generic;

    using Data.Models;

    public interface IXmlParserService
    {
        IEnumerable<Match> GetMatches(string uri);

        IEnumerable<Bet> GetBets(string uri);

        IEnumerable<Odd> GetOdds(string uri);
    }
}
