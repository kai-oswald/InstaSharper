using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace InstaSharper.API.Session
{
    public interface ISessionStorage
    {
        SessionData Get();
        void Persist(SessionData sessionData);
        bool Exists { get; }

    }
}
