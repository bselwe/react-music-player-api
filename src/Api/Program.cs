using System.Net;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace MusicPlayer.Api
{
    public class Program
    {
        private const int Port = 5000;

        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel(cfg => cfg.Listen(IPAddress.Any, Port))
                .UseIISIntegration()
                .UseUrls($"https://*:{Port}")
                .UseStartup<Startup>()
                .Build();
    }
}
