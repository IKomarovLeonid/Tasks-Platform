using Core.API.Src.Startup;
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
            return WebHost.CreateDefaultBuilder(args).UseStartup<Startup>().UseUrls("http://localhost:8080");
        }
    }
}
