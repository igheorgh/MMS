﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;

namespace MMSApi.Tests
{
    public class TestFixture<TStartup> : IDisposable where TStartup : class
    {
        public readonly TestServer Server;
        private readonly HttpClient _client;


        public TestFixture()
        {
            var baseDirectory = AppContext.BaseDirectory;
            var MMSAPIPATH = Path.GetFullPath(Path.Combine(baseDirectory, @"..\..\..\..\..\MMS\MMSAPI"));
            var builder = new WebHostBuilder()
                .UseContentRoot(MMSAPIPATH)
                .UseStartup<TStartup>();

            Server = new TestServer(builder);
            _client = new HttpClient();
        }


        public void Dispose()
        {
            _client.Dispose();
            Server.Dispose();
        }
    }
}
