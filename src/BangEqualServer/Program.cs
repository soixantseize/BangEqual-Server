using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace BangEqualServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            var url = $"http://bangequal-server:{Environment.GetEnvironmentVariable("PORT")}/";
            
            var config = new ConfigurationBuilder()
                .AddCommandLine(args)
                .AddEnvironmentVariables(prefix: "ASPNETCORE_")
                .Build();

            var host = new WebHostBuilder()
                //Dev
                .UseConfiguration(config)
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                //Production
                //.UseUrls(url)
                .Build();

            host.Run();
        }
    }
}
