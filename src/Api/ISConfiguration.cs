using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4.Models;

namespace MusicPlayer.Api
{
    using MusicPlayer.Core.Auth;
    using MusicPlayer.Core.Contracts;
    using static IdentityServer4.IdentityServerConstants;

    static class ISConfiguration
    {
        public static List<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = AuthConsts.MusicPlayerUserId,
                    ClientName = "Music Player User",
                    AllowedGrantTypes = 
                    {
                        GrantType.ResourceOwnerPassword,
                    },
                    AllowOfflineAccess = true,
                    RequireClientSecret = false,

                    AllowedScopes =
                    {
                        StandardScopes.OpenId,
                        StandardScopes.Profile,
                        StandardScopes.Email,
                        AuthConsts.MusicPlayerUserApiScope
                    },
                    
                    // AllowedCorsOrigins = new List<string> { "http://localhost:8080" }
                }
            };
        }

        public static List<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };
        }

        public static List<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource(AuthConsts.MusicPlayerUserApiScope, new string[]
                {
                    KnownClaims.Role
                })
            };
        }
    }
}
