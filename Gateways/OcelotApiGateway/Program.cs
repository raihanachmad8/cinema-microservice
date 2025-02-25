using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using DotNetEnv;

namespace OcelotApiGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Load environment variables from Config/global.env
            Env.Load("../../Configs/global.env"); // or specify the path where your global.env file is located

            // Start the application
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    var environmentUrls = Environment.GetEnvironmentVariable("API_GATEWAY_HOST") ?? "http://localhost:5000";
                    webBuilder.UseUrls(environmentUrls);
                });
    }

}
