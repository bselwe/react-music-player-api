using System;

namespace MusicPlayer.Core.Contracts
{
    public class UserInfoDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; }
    }
}
