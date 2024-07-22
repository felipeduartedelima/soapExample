using System.ServiceModel;
using System.Text;
using Polly;
namespace SoapCore_Demo
{
    [ServiceContract]
    public interface IAuthorService
    {
        [OperationContract]
        Author MySoapMethod(Author xml);

        [OperationContract]
        string Example(string phrase);
    }
    public class AuthorService : IAuthorService
    {
        public Author MySoapMethod(Author xml)
        {
            return xml;
        }

        public string Example (string phrase)
        {
            // Create Fallback Policy
            var fallbackPolicy = Policy<string>
            .Handle<DecoderFallbackException>()
            .Fallback(() =>
            {
                Console.WriteLine("Fallback acionado devido a DecoderFallbackException.");
                return "with fallback";
            });
            var result = fallbackPolicy.Execute(() =>
            {
                // Force DecoderExceptionFallback
                Encoding encoding = Encoding.GetEncoding("utf-8", new EncoderExceptionFallback(), new DecoderExceptionFallback());
                byte[] invalidBytes = { 0xFF };
                string result = encoding.GetString(invalidBytes);
                // End Force DecoderExceptionFallback
                return "without fallback";
            });
            return result;

        }
    }
}