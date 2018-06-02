using System;
using System.Collections.Generic;

namespace MusicPlayer.Core.Models
{
    public class Track
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Artist { get; private set; }
        public string Photo { get; private set; }

        private Track() { }

        public static Track Create(int id, string title, string artist, string photo)
        {
            return new Track
            {
                Id = id,
                Title = title,
                Artist = artist,
                Photo = photo
            };
        }
    }
}