using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace MusicPlayer.Migrations
{
    static class MigrationsConfig
    {
        private const string EnvironmentVariableName = "ASPNETCORE_ENVIRONMENT";
        private const string DefaultConnectionStringName = "Database";

        public static readonly string EnvironmentName;

        static MigrationsConfig()
        {
            EnvironmentName = System.Environment.GetEnvironmentVariable(EnvironmentVariableName);
        }

        public static string LoadConnectionString()
        {
            var dir = Path.Combine(Directory.GetCurrentDirectory(), $"../Api");
            var builder = new ConfigurationBuilder()
                .SetBasePath(dir)
                .AddJsonFile($"appsettings.{EnvironmentName}.json", true);
            var configuration = builder.Build();
            return configuration.GetConnectionString(DefaultConnectionStringName);
        }
    }
}
