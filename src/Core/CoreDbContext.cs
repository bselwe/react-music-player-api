using System;
using MusicPlayer.Core.Models;
using Microsoft.EntityFrameworkCore;
using MusicPlayer.Core.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MusicPlayer.Core
{
    public class CoreDbContext : IdentityDbContext<AuthUser, AuthRole, Guid>
    {
        public const string Schema = "core";

        public DbSet<Track> Tracks => Set<Track>();
        
        public CoreDbContext(DbContextOptions<CoreDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.HasDefaultSchema(Schema);

            builder.Entity<Track>(cfg =>
            {
                cfg.HasKey(e => e.Id);
            });

            builder.Entity<AuthUser>(b => b.ToTable("AspNetUsers"));
            builder.Entity<AuthRole>(b => b.ToTable("AspNetRoles"));
            builder.Entity<IdentityUserClaim<Guid>>(b => b.ToTable("AspNetUserClaims"));
            builder.Entity<IdentityRoleClaim<Guid>>(b => b.ToTable("AspNetRoleClaims"));
            builder.Entity<IdentityUserRole<Guid>>(b => b.ToTable("AspNetUserRoles"));
            builder.Entity<IdentityUserLogin<Guid>>(b => b.ToTable("AspNetUserLogins"));
            builder.Entity<IdentityUserToken<Guid>>(b => b.ToTable("AspNetUserTokens"));

            builder.Entity<AuthUser>(b =>
            {
                b.Property(u => u.FirstName)
                    .HasMaxLength(100);
                b.Property(u => u.LastName)
                    .HasMaxLength(100);
            });
        }
    }
}
