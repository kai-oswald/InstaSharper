using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using InstaSharper.API.Session;
using InstaSharper.Classes.Android.DeviceInfo;

namespace InstaSharper.Classes
{
    public interface IHttpRequestProcessor
    {
        HttpClientHandler HttpHandler { get; }
        ApiRequestMessage RequestMessage { get; }
        HttpClient Client { get; }

        CookieContainer CookieContainer { get; set; }

        DateTime LastUpdated { get; }

        DateTime LastLoaded { get; }

        ISessionStorage Storage { get; }
        void SaveCookieJar();
        void LoadCookieJar();

        Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage);
        Task<HttpResponseMessage> GetAsync(Uri requestUri);
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage, HttpCompletionOption completionOption);
        Task<string> SendAndGetJsonAsync(HttpRequestMessage requestMessage, HttpCompletionOption completionOption);
        Task<string> GetJsonAsync(Uri requestUri);
    }
}