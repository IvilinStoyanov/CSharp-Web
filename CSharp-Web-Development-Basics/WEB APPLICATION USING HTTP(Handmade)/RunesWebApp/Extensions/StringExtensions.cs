using System.Net;

namespace RunesWebApp.Extensions
{
    public static class StringExtensions
    {
        public static string UrlDecode(this string text)
        {
            return WebUtility.UrlDecode(text);
        }
    }
}
