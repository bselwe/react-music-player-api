using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MusicPlayer.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MusicPlayer.Core.Repositories
{
    public interface ITracksRepository
    {
        void AddFavourite(Track track);
        Task<List<Track>> AllFavouritesAsync(Guid userId);
    }

    public class TracksRepository : ITracksRepository
    {
        private readonly CoreDbContext dbContext;

        public TracksRepository(CoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        
        public void AddFavourite(Track track)
        {
            dbContext.Tracks.Add(track);
        }

        public Task<List<Track>> AllFavouritesAsync(Guid userId)
        {
            return dbContext.Tracks
                .Where(t => t.UserId == userId)
                .ToListAsync();
        }
    }
}