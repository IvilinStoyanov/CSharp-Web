﻿using SIS.HTTP.Enums;
using SIS.HTTP.Headers;
using SIS.HTTP.Responses;
using System.Text;
using System;

namespace SIS.WebServer.Results
{
    public class BadRequestResult : HttpResponse
    {
        private const string DefaultErrorHeading = "<h1>Error occured</h1>";

        public BadRequestResult(string content, HttpResponseStatusCode responseStatusCode)
            : base(responseStatusCode)
        {
            content = DefaultErrorHeading + 
                Environment.NewLine +
                content;
            this.Headers.Add(new HttpHeader(HttpHeader.ContentType, "text/html; charset=utf-8"));
            this.Content = Encoding.UTF8.GetBytes(content);
        }
    }
}

