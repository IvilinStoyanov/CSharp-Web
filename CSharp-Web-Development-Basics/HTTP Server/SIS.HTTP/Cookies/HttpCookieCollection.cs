using SIS.HTTP.Cookies.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SIS.HTTP.Cookies
{
    public class HttpCookieCollection : IHttpCookieCollection
    {
        private readonly IDictionary<string, HttpCookie> cookies;

        public HttpCookieCollection()
        {
            this.cookies = new Dictionary<string, HttpCookie>();
        }

        public void Add(HttpCookie cookie)
        {
            if(cookie == null)
            {
                throw new ArgumentNullException();
            }

            if (this.ContainsCookie(cookie.Key))
            {
                // TODO: More specific
                throw new Exception();
            }

            this.cookies[cookie.Key] = cookie;
            // this.cookies.Add(cookie.Key, cookie);
        }

        public bool ContainsCookie(string key)
        {
            return cookies.ContainsKey(key);
        }

        public HttpCookie GetCookie(string key)
        {
            if(!this.ContainsCookie(key))
            {
                return null;
            }
           return this.cookies[key];
        }

        public bool HasCookie()
         => this.cookies.Any();

        public override string ToString()
        {
            return string.Join("; ", this.cookies.Values);
        }

    }
}
