namespace XmlFeedConsumer.Services.Utils
{
    using System.IO;
    using System.Xml.Serialization;

    using Bytes2you.Validation;

    using Contracts;

    public class XmlParserService : IXmlParserService
    {
        public TModel Deserialize<TModel>(string xml)
        {
            Guard.WhenArgument(xml, nameof(xml)).IsNullOrEmpty().Throw();
            
            XmlSerializer serializer = new XmlSerializer(typeof(TModel));
            return (TModel)serializer.Deserialize(new StringReader(xml));
        }
    }
}