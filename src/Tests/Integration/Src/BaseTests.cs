using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Integration
{
    public class BaseTests
    {
        protected HttpClient Client;

        [OneTimeSetUp]
        public async Task Setup()
        {
            Client = new HttpClient();

            Client.BaseAddress = new Uri("http://localhost:8080/api/");

            Client.DefaultRequestHeaders.Add("accept", "application/json");

            var response = await Client.GetAsync("ping");
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Client.Dispose();
        }
    }
}
