using System;

namespace MusicPlayer.Core.Models
{
    public class Track
    {
        public Guid UserId { get; private set; }
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Artist { get; private set; }
        public string Photo { get; private set; }

        private Track() { }

        public static Track Create(Guid userId, int id, string title, string artist, string photo)
        {
            return new Track
            {
                UserId = userId,
                Id = id,
                Title = title,
                Artist = artist,
                Photo = photo
            };
        }
    }
}