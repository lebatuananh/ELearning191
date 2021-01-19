using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Shared.Initialization;

namespace PeaLearning.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().AutoInit().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
