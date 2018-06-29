using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using InstaSharper.API.Session;
using InstaSharper.Classes.Android.DeviceInfo;
using InstaSharper.Logger;

namespace InstaSharper.Classes
{
    internal class HttpRequestProcessor : IHttpRequestProcessor
    {
        private readonly IRequestDelay _delay;
        private readonly IInstaLogger _logger;

        public HttpRequestProcessor(IRequestDelay delay, HttpClient httpClient, HttpClientHandler httpHandler,
            ApiRequestMessage requestMessage, IInstaLogger logger, ISessionStorage storage)
        {
            _delay = delay;
            Client = httpClient;
            HttpHandler = httpHandler;
            RequestMessage = requestMessage;
            _logger = logger;
            CookieContainer = new CookieContainer();
            HttpHandler.CookieContainer = CookieContainer;
            Storage = storage;
            LoadCookieJar();
        }
        public ISessionStorage Storage { get; }

        public HttpClientHandler HttpHandler { get; }
        public ApiRequestMessage RequestMessage { get; }
        public HttpClient Client { get; }

        public CookieContainer CookieContainer { get; set; }

        public DateTime LastUpdated { get; private set; }

        public DateTime LastLoaded { get; private set; }

        public void SaveCookieJar()
        {
            var sessionCookies = CookieContainer.GetCookies(Client.BaseAddress);
            Storage.Persist(sessionCookies.Cast<Cookie>());

            LastUpdated = DateTime.Now;
        }

        public void LoadCookieJar()
        {
            CookieContainer = new CookieContainer();

            var cookies = Storage.Get();

            cookies
                .ToList()
                .ForEach(c =>
                {
                    CookieContainer.Add(Client.BaseAddress, c);
                });

            LastLoaded = DateTime.Now;
        }

        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage)
        {
            LogHttpRequest(requestMessage);
            if (_delay.Exist)
                await Task.Delay(_delay.Value);
            var response = await Client.SendAsync(requestMessage);
            LogHttpResponse(response);
            return response;
        }

        public async Task<HttpResponseMessage> GetAsync(Uri requestUri)
        {
            _logger?.LogRequest(requestUri);
            if (_delay.Exist)
                await Task.Delay(_delay.Value);
            var response = await Client.GetAsync(requestUri);
            LogHttpResponse(response);
            return response;
        }

        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage,
            HttpCompletionOption completionOption)
        {
            LogHttpRequest(requestMessage);
            if (_delay.Exist)
                await Task.Delay(_delay.Value);
            var response = await Client.SendAsync(requestMessage, completionOption);
            LogHttpResponse(response);
            return response;
        }

        public async Task<string> SendAndGetJsonAsync(HttpRequestMessage requestMessage,
            HttpCompletionOption completionOption)
        {
            LogHttpRequest(requestMessage);
            if (_delay.Exist)
                await Task.Delay(_delay.Value);
            var response = await Client.SendAsync(requestMessage, completionOption);
            LogHttpResponse(response);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetJsonAsync(Uri requestUri)
        {
            _logger?.LogRequest(requestUri);
            if (_delay.Exist)
                await Task.Delay(_delay.Value);
            var response = await Client.GetAsync(requestUri);
            LogHttpResponse(response);
            return await response.Content.ReadAsStringAsync();
        }

        private void LogHttpRequest(HttpRequestMessage request)
        {
            _logger?.LogRequest(request);
        }

        private void LogHttpResponse(HttpResponseMessage request)
        {
            _logger?.LogResponse(request);
        }
    }
}