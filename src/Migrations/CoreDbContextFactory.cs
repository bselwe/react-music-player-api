using MusicPlayer.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MusicPlayer.Migrations
{
    public class CoreDbContextFactory : IDesignTimeDbContextFactory<CoreDbContext>
    {
        public CoreDbContext CreateDbContext(string[] args)
        {
            var opts = new DbContextOptionsBuilder<CoreDbContext>()
                .UseSqlServer(
                    MigrationsConfig.LoadConnectionString(),
                    cfg => cfg.MigrationsAssembly("MusicPlayer.Migrations"))
                .Options;
            return new CoreDbContext(opts);
        }
    }
}
