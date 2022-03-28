using Core.API.Configuration;
using Core.API.Startup;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Tasks_Platform
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var baseUrl = ConfigurationReader.ReadConfig<ApplicationConfiguration>().Url;
            return WebHost.CreateDefaultBuilder(args).UseStartup<Startup>().UseUrls(baseUrl);
        }
    }
}
