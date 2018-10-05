using SIS.HTTP.Session.Contracts;
using System;
using System.Collections.Generic;

namespace SIS.HTTP.Session
{
    public class HttpSessions : IHttpSession
    {
        private readonly IDictionary<string, object> parameters;

        public HttpSessions(string id)
        {
            this.Id = id;
            this.parameters = new Dictionary<string, object>();
        }

        public string Id { get; }

        public object GetParameters(string name)
        {
           if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException();
            }

           if(!this.ContainsParameters(name))
            {
                return null;
            }

            return this.parameters[name];
        }

        public void AddParameter(string name, object parameter)
        {
            if(this.ContainsParameters(name))
            {
                throw new ArgumentException();
            }

            this.parameters[name] = parameters;
        }

        public void ClearParameters()
        {
            this.parameters.Clear();
        }

        public bool ContainsParameters(string name)
        {
            return this.parameters.ContainsKey(name);
        }
    }
}
