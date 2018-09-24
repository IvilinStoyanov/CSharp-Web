using System;
using System.Net;
using System.Text;

namespace ValidateURL
{
    public class Program
    {
        private const string INVALID_URL_EXCEPTION = "Invalid URL";

        public static void Main(string[] args)
        {
            // Read input
            var input = Console.ReadLine();

            // Decode URl with WebUtility
            var decodedURL = WebUtility.UrlDecode(input);
            Console.WriteLine(decodedURL);

            try
            {
                var parsedURL = new Uri(decodedURL);

                // Required Components
                if (string.IsNullOrWhiteSpace(parsedURL.Scheme) ||
                    string.IsNullOrWhiteSpace(parsedURL.Host) ||
                    string.IsNullOrWhiteSpace(parsedURL.LocalPath) ||
                    !parsedURL.IsDefaultPort)
                {
                    throw new ArgumentException(INVALID_URL_EXCEPTION);
                }

                var builder = new StringBuilder();
                builder
                    .AppendLine($"Protocol: {parsedURL.Scheme}")
                    .AppendLine($"Host: {parsedURL.Host}")
                    .AppendLine($"Port: {parsedURL.Port}")
                    .AppendLine($"Path: {parsedURL.LocalPath}");

                // Optional Components
                if (!string.IsNullOrWhiteSpace(parsedURL.Query))
                {
                    builder.AppendLine($"Query: {parsedURL.Query.Substring(1)}");
                }

                if (!string.IsNullOrWhiteSpace(parsedURL.Fragment))
                {
                    builder.AppendLine($"Fragment: {parsedURL.Fragment.Substring(1)}");
                }

                Console.WriteLine(builder.ToString().Trim());
            }
            catch (Exception)
            {
                Console.WriteLine(INVALID_URL_EXCEPTION);
            }
        }
    }
}
