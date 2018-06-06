using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicPlayer.Core.Auth;
using MusicPlayer.Core.Contracts;
using MusicPlayer.Core.Models;
using MusicPlayer.Core.Services;

namespace MusicPlayer.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class TracksController : Controller
    {
        private readonly ITracksService tracksService;

        public TracksController(ITracksService tracksService)
        {
            this.tracksService = tracksService;
        }

        [HttpPost]
        public async Task<IActionResult> AddFavourite([FromBody] AddFavouriteTrackDTO track)
        {
            if (track == null)
                return BadRequest();

            var userId = Guid.Parse(User.FindFirstValue(KnownClaims.UserId));
            
            await tracksService.AddAsync(
                userId,
                track.Id,
                track.Title,
                track.Artist,
                track.Photo
            );

            return new StatusCodeResult((int) HttpStatusCode.Created);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveFavourite([FromBody] RemoveFavouriteTrackDTO track)
        {
            if (track == null)
                return BadRequest();

            var userId = Guid.Parse(User.FindFirstValue(KnownClaims.UserId));
            var result = await tracksService.RemoveAsync(userId, track.Id);
            
            if (!result)
                return NotFound();
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> IsFavourite(int id)
        {
            var userId = Guid.Parse(User.FindFirstValue(KnownClaims.UserId));
            var result = await tracksService.IsTrackFavouriteAsync(userId, id);
            return Ok(new IsFavouriteTrackDTO() { Id = id, IsFavourite = result });
        }

        [HttpGet]
        public async Task<IActionResult> GetFavourites()
        {
            var userId = Guid.Parse(User.FindFirstValue(KnownClaims.UserId));
            if (userId == null)
                return NotFound();

            var tracks = await tracksService.AllFavouritesAsync(userId);
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
