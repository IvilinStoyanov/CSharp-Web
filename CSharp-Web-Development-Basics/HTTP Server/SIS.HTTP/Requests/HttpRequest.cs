﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using SIS.HTTP.Common;
using SIS.HTTP.Cookies;
using SIS.HTTP.Cookies.Contracts;
using SIS.HTTP.Enums;
using SIS.HTTP.Exceptions;
using SIS.HTTP.Headers;
using SIS.HTTP.Headers.Contracts;
using SIS.HTTP.Requests.Contracts;
using SIS.HTTP.Session.Contracts;

public class HttpRequest : IHttpRequest
{
    public HttpRequest(string requestString)
    {
        this.FormData = new Dictionary<string, object>();
        this.QueryData = new Dictionary<string, object>();
        this.Headers = new HttpHeaderCollection();
        this.Cookies = new HttpCookieCollection();

        this.ParseRequest(requestString);
    }

    public string Url { get; private set; }

    public string Path { get; private set; }

    public Dictionary<string, object> FormData { get; }

    public Dictionary<string, object> QueryData { get; }

    public IHttpHeaderCollection Headers { get; }

    public HttpRequestMethod RequestMethod { get; private set; }

    public IHttpCookieCollection Cookies { get; }

    public IHttpSession Session { get; set; }

    private void ParseRequest(string requestString)
    {

        string[] splitRequestContent = requestString
            .Split(new[] { Environment.NewLine }, StringSplitOptions.None);


        string[] requestLine = splitRequestContent[0].Trim()
            .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        if (!this.IsValidRequestLine(requestLine))
        {
            throw new BadRequestException();
        }

        this.ParseRequestMethod(requestLine);

        this.ParseRequestUrl(requestLine);

        this.ParseRequestPath(this.Url);

        this.ParseHeaders(splitRequestContent.Skip(1).ToArray());

        this.ParseCookies();

        string queryParams = splitRequestContent[splitRequestContent.Length - 1];

        this.ParseRequestParameters(queryParams);
    }

    private void ParseCookies()
    {
        if (!this.Headers.ContainsHeader(GlobalConstants.CookieRequestHeaderName))
        {
            return;
        }

        var cookiesRaw = this.Headers
            .GetHeader(GlobalConstants.CookieRequestHeaderName)
            .Value;

        var cookies = cookiesRaw
            .Split("; ", StringSplitOptions.RemoveEmptyEntries);

        foreach (var rawCookie in cookies)
        {
            var cookieKeyValuePair = rawCookie.Split("=", 2);

            if (cookieKeyValuePair.Length !=
                GlobalConstants.NumberOfParametersInRequestOfKeyValuePair)
            {
                throw new BadRequestException();
            }

            var cookieName = cookieKeyValuePair[0];
            var cookieValue = cookieKeyValuePair[1];
            this.Cookies.Add(new HttpCookie(cookieName, cookieValue));
        }
    }

    private void ParseRequestParameters(string bodyParams)
    {
        this.ParseQueryParameters();


        if (this.RequestMethod == HttpRequestMethod.Get)
        {
            return;
        }

        this.ParseFormDataParams(bodyParams);



    }

    private void ParseQueryParameters()
    {
        if (!this.Url.Contains("?"))
        {
            return;
        }

        var queryParams = this.Url.Split(new[] { '?', '#' })
            .Skip(1)
            .Take(1)
            .ToArray()[0];

        var queryKeyValuePairs = queryParams.Split('&', StringSplitOptions.RemoveEmptyEntries);

        FillData(queryKeyValuePairs, this.QueryData);

    }

    private void ParseFormDataParams(string bodyParams)
    {
        var dataParams = bodyParams.Split('&', StringSplitOptions.RemoveEmptyEntries);

        FillData(dataParams, this.FormData);
    }

    private void FillData(IEnumerable<string> dataParams, IDictionary<string, object> data)
    {
        foreach (var queryPair in dataParams)
        {
            var queryKeyValuePair = queryPair.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);

            if (queryKeyValuePair.Length != GlobalConstants.NumberOfParametersInRequestOfKeyValuePair)
            {
                throw new BadRequestException();
            }

            var dataFormKey = WebUtility.UrlDecode(queryKeyValuePair[0]);
            var dataFormValue = WebUtility.UrlDecode(queryKeyValuePair[1]);

            data[dataFormKey] = dataFormValue;
        }
    }

    private void ParseHeaders(string[] headers)
    {
        var emptyLineAfterHeadersIndex = Array.IndexOf(headers, string.Empty);

        for (int i = 0; i < emptyLineAfterHeadersIndex; i++)
        {
            var currentLine = headers[i];
            var headerParts = currentLine.Split(new[] { ": " }, StringSplitOptions.RemoveEmptyEntries);

            if (headerParts.Length != 2)
            {
                throw new BadRequestException();
            }

            var headerKey = headerParts[0];
            var headerValue = headerParts[1].Trim();

            var header = new HttpHeader(headerKey, headerValue);

            this.Headers.Add(header);
        }

        if (!this.Headers.ContainsHeader(GlobalConstants.HostHeaderKey))
        {
            throw new BadRequestException();
        }
    }

    private void ParseRequestPath(string url)
    {

        this.Path = url.Split(new[] { '?', '#' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();

    }

    private void ParseRequestUrl(string[] requestLine)
    {
        string url = requestLine[1];
        if (string.IsNullOrEmpty(url))
        {
            throw new BadRequestException();
        }

        this.Url = url;

    }

    private void ParseRequestMethod(string[] requestLine)
    {
        if (!requestLine.Any())
        {
            throw new BadRequestException();
        }

        string reqMethod = requestLine[0];
        bool tryParseReqMethod = Enum.TryParse(reqMethod, true, out HttpRequestMethod method);

        if (!tryParseReqMethod)
        {
            throw new BadRequestException();
        }

        this.RequestMethod = method;
    }

    private bool IsValidRequestLine(string[] requestLine)
    {
        bool validRequest = requestLine.Length == 3 && requestLine[2] == GlobalConstants.HttpOneProtocolFragment;

        return validRequest;
    }
}
