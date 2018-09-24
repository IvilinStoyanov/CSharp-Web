using System;
using System.Net;

namespace UrlDecoder
{
   public class Program
    {
      public static void Main(string[] args)
        {
            // Read input
            var input = Console.ReadLine();

            // Decode URl with WebUtility
            var decodedURL = WebUtility.UrlDecode(input);
            Console.WriteLine(decodedURL);
        }
    }
}
