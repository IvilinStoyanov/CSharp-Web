using SIS.HTTP.Session.Contracts;
using System.Collections.Concurrent;

namespace SIS.HTTP.Session
{
    public class HttpSessionStorage
    {
        public const string SeesionCookieKey = "SIS_ID";

        private static readonly ConcurrentDictionary<string, IHttpSession> sessions
            = new ConcurrentDictionary<string, IHttpSession>();

        public static IHttpSession GetSession(string id)
        {
            return sessions.GetOrAdd(id, _ => new HttpSessions(id));
        }


    }
}
