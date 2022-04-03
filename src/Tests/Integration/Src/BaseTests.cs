using System.Net;
using System.Threading.Tasks;
using Integration.API;
using NUnit.Framework;

namespace Integration
{
    public class BaseTests
    {
        protected APIConnection Client;

        [OneTimeSetUp]
        public async Task Setup()
        {
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
