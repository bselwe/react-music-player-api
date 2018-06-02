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
        Task CreateAsync(Guid userId, int id, string title, string artist, string photo);
        Task<List<Track>> AllFavouritesAsync(Guid userId);
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

        public Task CreateAsync(Guid userId, int id, string title, string artist, string photo)
        {
            var track = Track.Create(userId, id, title, artist, photo);
            tracksRepository.AddFavourite(track);
            return unitOfWork.CommitAsync();
        }

        public Task<List<Track>> AllFavouritesAsync(Guid userId)
        {
            return tracksRepository.AllFavouritesAsync(userId);
        }
    }
}