using System;
using System.Net;
using System.Threading.Tasks;
using Integration.API;
using Integration.Src.Helpers;
using NUnit.Framework;

namespace Integration
{
    public class BaseTests
    {
        protected APIConnection Client;
        protected Generator Generator;

        [OneTimeSetUp]
        public async Task Setup()
        {
            Generator = new Generator();

            var url = Environment.GetEnvironmentVariable("Url") ?? "http://localhost:8080";

            Client = new APIConnection(url);

            await Client.Healty.PingAsync();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Client.Dispose();
        }
    }
}
