using System;
using MusicPlayer.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace MusicPlayer.Core
{
    public class CoreDbContext : DbContext
    {
        public const string Schema = "core";

        public DbSet<Track> Tracks => Set<Track>();
        
        public CoreDbContext(DbContextOptions<CoreDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema);

            modelBuilder.Entity<Track>(cfg =>
            {
                cfg.HasKey(e => e.Id);
            });
        }
    }
}
