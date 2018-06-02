
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicPlayer.Core.Auth;
using MusicPlayer.Core.Contracts;
using MusicPlayer.Core.Repositories;

namespace MusicPlayer.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class TracksController : Controller
    {
        private readonly ITracksRepository tracksRepository;

        public TracksController(ITracksRepository tracksRepository)
        {
            this.tracksRepository = tracksRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetFavourites()
        {
            var userId = Guid.Parse(User.FindFirstValue(KnownClaims.UserId));
            if (userId == null)
                return NotFound();

            var tracks = await tracksRepository.AllFavouritesAsync(userId);
            var mappedTracks = tracks.Select(t => new TrackDTO
            {
                Id = t.Id,
                Title = t.Title,
                Artist = t.Title,
                Photo = t.Photo
            });

            return Ok(mappedTracks);
        }
    }
}
