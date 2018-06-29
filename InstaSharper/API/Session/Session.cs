using InstaSharper.Classes;
using InstaSharper.Classes.Android.DeviceInfo;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace InstaSharper.API.Session
{
    public class Session
    {
        public ISessionStorage Storage { get; set; }

        public IEnumerable<Cookie> Cookies { get; private set; }

        private IHttpRequestProcessor _httpRequestProcessor;

        public bool Authenticated { get; private set; }

        string UserName { get; set; }

        string Password { get; set; }

        AndroidDevice Device { get; set; }

        protected Session(string userName, string password, AndroidDevice device, IHttpRequestProcessor httpRequestProcessor)
        {
            UserName = userName;
            Password = password;
            Device = device;
            Storage = new FileSessionStorage(userName);
            _httpRequestProcessor = httpRequestProcessor;
        }
    }
}
