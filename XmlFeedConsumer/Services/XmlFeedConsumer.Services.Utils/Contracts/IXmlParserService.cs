namespace XmlFeedConsumer.Services.Utils.Contracts
{
    public interface IXmlParserService
    {
        TModel Deserialize<TModel>(string xml);
    }
}
