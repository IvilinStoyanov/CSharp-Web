﻿using SIS.HTTP.Headers.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.HTTP.Headers
{
    public class HttpHeaderCollection : IHttpHeaderCollection
    {
        private readonly Dictionary<string, HttpHeader> headers;

        public HttpHeaderCollection()
        {
            this.headers = new Dictionary<string, HttpHeader>();
        }

        public void Add(HttpHeader header)
        {
            if (header == null ||
               string.IsNullOrEmpty(header.Key) ||
               string.IsNullOrEmpty(header.Value) ||
               this.ContainsHeader(header.Key))
            {
                throw new ArgumentException();
            }

            this.headers.Add(header.Key, header);
        }

        public bool ContainsHeader(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException($"{nameof(key)} cannot be null");
            }

            return this.headers.ContainsKey(key);
        }

        public HttpHeader GetHeader(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException($"{nameof(key)} cannot be null");
            }

            return this.headers[key];
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, this.headers.Values);
        }
    }
}
