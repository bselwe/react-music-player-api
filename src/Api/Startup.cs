using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MusicPlayer.Core;
using MusicPlayer.Core.Auth;
using MusicPlayer.Core.Repositories;
using MusicPlayer.Core.Services;
using Newtonsoft.Json.Serialization;

namespace MusicPlayer.Api
{
    public class Startup
    {
        private readonly string databaseConnStr;
        
        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true);
            Configuration = builder.Build();

            this.databaseConnStr = Configuration.GetConnectionString("Database");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            
            services
                .AddMvc()
                .AddJsonOptions(o => o.SerializerSettings.ContractResolver = new DefaultContractResolver());

             services.AddDbContext<CoreDbContext>(opts =>
                opts.UseSqlServer(databaseConnStr, cfg =>
                    cfg.MigrationsAssembly("MusicPlayer.Migrations"))
            );

            services.AddIdentity<AuthUser, AuthRole>()
                .AddEntityFrameworkStores<CoreDbContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<CoreUnitOfWork, CoreUnitOfWork>();
            services.AddScoped<ITracksRepository, TracksRepository>();
            services.AddScoped<ITracksService, TracksService>();
            services.AddScoped<IAccountService, AccountService>();

            ConfigureIdentityServer(services);
            ConfigureAuthentication(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseMvc();
        }

        private void ConfigureIdentityServer(IServiceCollection services)
        {
            services.AddIdentityServer()
                .AddInMemoryApiResources(ISConfiguration.GetApiResources())
                .AddInMemoryIdentityResources(ISConfiguration.GetIdentityResources())
                .AddInMemoryClients(ISConfiguration.GetClients())
                .AddDeveloperSigningCredential()
                .AddAspNetIdentity<AuthUser>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 1;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            });
        }

        public void ConfigureAuthentication(IServiceCollection services)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(
                options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.Authority = Configuration["AppBase"];
                    options.TokenValidationParameters.ValidateAudience = false;
                    options.TokenValidationParameters.ValidateIssuer = false;
                    
                    options.RequireHttpsMetadata = false;

                    options.TokenValidationParameters.RoleClaimType = KnownClaims.Role;
                });
        }
    }
}
