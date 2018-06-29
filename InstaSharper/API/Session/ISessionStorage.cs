using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace InstaSharper.API.Session
{
    public interface ISessionStorage
    {
        IEnumerable<Cookie> Get();
        void Persist(IEnumerable<Cookie> cookies);
        bool Exists { get; }

    }
}
