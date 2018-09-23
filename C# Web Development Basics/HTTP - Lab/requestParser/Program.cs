using System;
using System.Collections.Generic;

namespace requestParser
{
    class Program
    {
        static void Main(string[] args)
        {
            var routes = new Dictionary<string, HashSet<string>>();

            var input = Console.ReadLine();

            while(input != "END")
            {
                var splitInput = input.Split("/", StringSplitOptions.RemoveEmptyEntries);

                var httpMethod = splitInput[1];
                var endPoint = splitInput[0];

                if(!routes.ContainsKey(httpMethod))
                {
                    routes.Add(httpMethod, new HashSet<string>());
                }
                routes[httpMethod].Add(endPoint);

                input = Console.ReadLine();
            }

            var requestString = Console.ReadLine();
        }
    }
}
