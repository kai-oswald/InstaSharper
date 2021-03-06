﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace InstaSharper.API.Session
{
    public class FileSessionStorage : ISessionStorage
    {
        protected string Id { get; }
        protected string Path => $@"session_{Id}.json";

        public FileSessionStorage(string id)
        {
            this.Id = id;
        }

        public bool Exists => File.Exists(Path);

        public SessionData Get()
        {
            return JsonConvert.DeserializeObject<SessionData>(File.ReadAllText(Path));
        }

        public void Persist(SessionData sessionData)
        {
            File.WriteAllText(Path, JsonConvert.SerializeObject(sessionData));
        }
    }
}
