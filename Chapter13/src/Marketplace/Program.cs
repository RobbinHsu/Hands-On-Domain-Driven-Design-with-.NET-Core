using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using static System.Environment;

namespace Marketplace
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var configuration = BuildConfiguration(args);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            CreateHostBuilder(args, configuration).Build().Run();
        }

        static IConfiguration BuildConfiguration(string[] args)
            => new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, false)
                .Build();

        static IHostBuilder CreateHostBuilder(
            string[] args,
            IConfiguration configuration)
            => Host.CreateDefaultBuilder(args)
                .UseSerilog() // UseSerilog should be called on IHostBuilder
                .ConfigureWebHostDefaults(
                    webBuilder =>
                    {
                        webBuilder.UseStartup<Startup>()
                            .UseConfiguration(configuration)
                            .UseContentRoot(CurrentDirectory)
                            .UseKestrel();
                    }
                );
    }
}