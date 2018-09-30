using SIS.HTTP.Common;
using SIS.HTTP.Enums;
using SIS.HTTP.Exceptions;
using SIS.HTTP.Headers;
using SIS.HTTP.Headers.Contracts;
using SIS.HTTP.Requests.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SIS.HTTP.Requests
{
    public class HttpRequest : IHttpRequest
    {
        public HttpRequest(string requestString)
        {
            this.FormData = new Dictionary<string, object>();
            this.QueryData = new Dictionary<string, object>();
            this.Headers = new HttpHeaderCollection();

            this.ParseRequest(requestString);
        }

        public string Path { get; private set; }

        public string Url { get; private set; }

        public Dictionary<string, object> FormData { get; }

        public Dictionary<string, object> QueryData { get; }

        public IHttpHeaderCollection Headers { get; }

        public HttpRequestMethod RequestMethod { get; private set; }

        private void ParseRequest(string requestString)
        {
            var splitRequestContent = requestString
                .Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            var requestLine = splitRequestContent[0]
                .Trim()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (!this.IsValidRequestLine(requestLine))
            {
                throw new BadRequestException();
            }

            // Parsing Methods
            this.ParseRequestMethod(requestLine);

            this.ParseRequestUrl(requestLine);

            this.ParseRequestPath();

            this.ParseHeaders(splitRequestContent.Skip(1).ToArray());

            var requestBody = false;
            if(splitRequestContent.Length > 1)
            {
                requestBody = true;
            }

            this.ParseRequestParameters(splitRequestContent[splitRequestContent.Length - 1], requestBody);
        }

        private void ParseRequestParameters(string bodyParameters, bool requestBody)
        {
            ParseQueryParameters(this.Url);
            if (requestBody)
            {
                this.ParseFormDataParameters(bodyParameters);
            }
        }

        private void ParseFormDataParameters(string bodyParameters)
        {
            throw new NotImplementedException();
        }

        private void ParseQueryParameters(string url)
        {
            var queryParameters = this.Url
                .Split(new[] {'?' , '#' })
                .Skip(1)
                .ToArray()[0];

            if(!string.IsNullOrEmpty(queryParameters))
            {
                throw new BadRequestException();
            }

            var queryKeyValuePairs = queryParameters
                .Split('&', StringSplitOptions.RemoveEmptyEntries);

            foreach (var queryKeyValuePair in queryKeyValuePairs)
            {
                var keyValuePair = queryKeyValuePair
                    .Split('=', StringSplitOptions.RemoveEmptyEntries);

                var queryString = keyValuePair[0];
                var queryValue = keyValuePair[1];
            }
        }

        private void ParseHeaders(string[] requestHeaders)
        {
            if(!requestHeaders.Any())
            {
                throw new BadRequestException();
            }

            foreach (var requestHeader in requestHeaders)
            {
                if(string.IsNullOrEmpty(requestHeader))
                {
                    return;
                }

                var splitRequestHeader = requestHeader
                    .Split(": ", StringSplitOptions.RemoveEmptyEntries);

                var requestHeaderKey = splitRequestHeader[0];
                var requestHeaderValue = splitRequestHeader[1];

                this.Headers.Add(new HttpHeader(requestHeaderKey, requestHeaderValue));
            }
        }

        private void ParseRequestPath()
        {
            var path = this.Url
                .Split('?')
                .FirstOrDefault();

            if(string.IsNullOrEmpty(path))
            {
                throw new BadRequestException();
            }

             this.Path = path;
        }

        private void ParseRequestUrl(string[] requestLine)
        {
            if (string.IsNullOrEmpty(requestLine[1]))
            {
                throw new BadRequestException();
            }
        }

        private void ParseRequestMethod(string[] requestLine)
        {
            var requestMethod = Enum
                .TryParse<HttpRequestMethod>(requestLine[0], out var parseMethod);         
        }

        private bool IsValidRequestLine(string[] requestLine)
        {
            if (!requestLine.Any())
            {
                throw new BadRequestException();
            }

            if (requestLine.Length == 3 && 
                requestLine[2] == GlobalConstants.HttpOneProtocolFragment)
            {
                return true;
            }
            return false;
        }      
    }
}
