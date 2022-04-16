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
            Client = new APIConnection("http://localhost:8080");
            
            await Client.Healty.PingAsync();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Client.Dispose();
        }
    }
}
