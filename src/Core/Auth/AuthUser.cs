using System;
using Microsoft.AspNetCore.Identity;

namespace MusicPlayer.Core.Auth
{
    public class AuthUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
