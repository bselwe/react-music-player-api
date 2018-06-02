using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MusicPlayer.Core.Auth;

namespace MusicPlayer.Core.Services
{
    public interface IAccountService
    {
        Task<AuthUser> GetUserInfo(string userId);
        Task SignUp(string firstName, string lastName, string email, string password);
    }

    public class AccountService : IAccountService
    {
        private readonly UserManager<AuthUser> userManager;

        public AccountService(
            UserManager<AuthUser> userManager)
        {
            this.userManager = userManager;
        }

        public Task<AuthUser> GetUserInfo(string userId)
        {
            return userManager.FindByIdAsync(userId);
        }

        public async Task SignUp(string firstName, string lastName, string email, string password)
        {
            var id = Guid.NewGuid();
            var user = new AuthUser
            {
                Id = id,
                Email = email,
                UserName = email,
                FirstName = firstName,
                LastName = lastName
            };
            
            var result = await userManager.CreateAsync(user, password);
            if (!result.Succeeded)
                throw new InvalidOperationException("Cannot create user. ASP.NET Core Identity rejected the request.");
            
            await userManager.AddClaimAsync(user, new Claim(KnownClaims.Role, KnownRoles.User));
        }
    }
}