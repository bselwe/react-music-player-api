using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicPlayer.Core.Models;
using MusicPlayer.Core.Repositories;

namespace MusicPlayer.Core.Services
{
    public interface ITracksService
    {
        Task AddAsync(Guid userId, int id, string title, string artist, string photo);
        Task<bool> RemoveAsync(Guid userId, int id);
        Task<List<Track>> AllFavouritesAsync(Guid userId);
        Task<bool> IsTrackFavouriteAsync(Guid userId, int trackId);
    }

    public class TracksService : ITracksService
    {
        private readonly ITracksRepository tracksRepository;
        private readonly CoreUnitOfWork unitOfWork;

        public TracksService(
            ITracksRepository tracksRepository, 
            CoreUnitOfWork unitOfWork)
        {
            this.tracksRepository = tracksRepository;
            this.unitOfWork = unitOfWork;
        }

        public Task AddAsync(Guid userId, int id, string title, string artist, string photo)
        {
            var track = Track.Create(userId, id, title, artist, photo);
            tracksRepository.AddFavourite(track);
            return unitOfWork.CommitAsync();
        }

        public async Task<bool> RemoveAsync(Guid userId, int id)
        {
            var track = await tracksRepository.FindAsync(userId, id);
            if (track != null) 
            {
                tracksRepository.RemoveFavourite(track);
                await unitOfWork.CommitAsync();
                return true;
            }
            return false;
        }

        public Task<List<Track>> AllFavouritesAsync(Guid userId)
        {
            return tracksRepository.AllFavouritesAsync(userId);
        }

        public async Task<bool> IsTrackFavouriteAsync(Guid userId, int trackId)
        {
            var track = await tracksRepository.FindAsync(userId, trackId);
            return track != null;
        }
    }
}