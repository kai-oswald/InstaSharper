using InstaSharper.Classes.Android.DeviceInfo;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace InstaSharper.API.Session
{
    public class SessionData
    {
        public IEnumerable<Cookie> Cookies { get; set; } = new List<Cookie>();

        public AndroidDevice Device { get; set; }
    }
}
