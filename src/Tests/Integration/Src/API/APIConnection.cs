using System;
using System.Net.Http;
using TasksPlatform.Shared.API;

namespace Integration.API
{
    public class APIConnection : IDisposable
    {
        private readonly HttpClient _client;

        public APIConnection(string address)
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(address);

            Settings = new SettingsRoute(_client);
            Tasks = new TasksRoute(_client);
            Healty = new HealtyRoute(_client);
        }

        // routes
        public ISettingsRoute Settings;

        public ITasksRoute Tasks;

        public IHealtyRoute Healty;

        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}
