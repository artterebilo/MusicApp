using DataBase.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MusicApp.Contracts;
using MusicApp.Services;
using MusicApp.Validations;

namespace MusicApp.Controllers;

[ApiController]
[Route("artist/{artistId}/albums/{albumId}/[controller]")]
public class SongsController : ControllerBase
{
    [HttpGet]
    public ActionResult<List<SongContract>> GetAllSongsForAlbum(string artistId, string albumId)
    {
        var album = AlbumService.GetAlbumById(albumId);
        var artist = ArtistService.GetArtistById(artistId);

        if (artist is null || album is null)
        {
            return NotFound("ArtistId or AlbumId not found");
        } else
        {
            return Ok(SongService.GetAllSongsForAlbum(albumId));
        }        
    }

    [HttpGet("{id}")]
    public ActionResult<SongContract> GetSongById(string artistId, string albumId, string id)
    {
        var song = SongService.GetSongById(id);
        var artist = ArtistService.GetArtistById(artistId);
        var album = AlbumService.GetAlbumById(albumId);

        if (artist is null || album is null ||song is null)
        {
            return NotFound("Artist or Album or Song is not found");
        }

        return Ok(song);
    }

    [HttpPost]
    public IActionResult CreateSongs(string artistId, string albumId, List<SongCreateAndUpdateContract> songs)
    {
        var validator = new SongsCreateAndUpdateValidation();
        var result = validator.Validate(songs);
        if (!result.IsValid)
        {
            var messages = result.Errors.Select((e) =>
            {
                return new Error
                {
                    Message = e.ErrorMessage,
                    Field = e.PropertyName
                };
            });

            var errors = new Errors
            {
                ErrorsMessages = messages.ToList(),
            };
            return BadRequest(errors);
        }

        var album = AlbumService.GetAlbumById(albumId);

        if (album == null)
        {
            return NotFound();
        }

        var newSongs = SongService.CreateSongs(albumId, songs);

        return Created("Songs", newSongs);
    }

    [HttpPut]
    public IActionResult UpdateSongs(string artistId, string albumId, List<SongCreateAndUpdateContract> songs)
    {
        var validator = new SongsCreateAndUpdateValidation();
        var result = validator.Validate(songs);
        if (!result.IsValid)
        {
            var messages = result.Errors.Select((e) =>
            {
                return new Error
                {
                    Message = e.ErrorMessage,
                    Field = e.PropertyName
                };
            });

            var errors = new Errors
            {
                ErrorsMessages = messages.ToList(),
            };
            return BadRequest(errors);
        }

        var artist = ArtistService.GetArtistById(artistId);
        var album = AlbumService.GetAlbumById(albumId);

        if (artist == null || album == null)
        {
            return NotFound("Artist or Album is not found");
        }

        SongService.UpdateSongs(songs);

        return NoContent();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateSong(string artistId, string albumId,  string id, SongCreateAndUpdateContract song)
    {
        var validator = new SongUpdateValidation();
        var result = validator.Validate(song);
        if (!result.IsValid)
        {
            var messages = result.Errors.Select((e) =>
            {
                return new Error
                {
                    Message = e.ErrorMessage,
                    Field = e.PropertyName
                };
            });

            var errors = new Errors
            {
                ErrorsMessages = messages.ToList(),
            };
            return BadRequest(errors);
        }

        var artist = ArtistService.GetArtistById(artistId);
        var album = AlbumService.GetAlbumById(albumId);
        var existingSong = SongService.GetSongById(id);

        if(artist is null || album is null || existingSong is null)
        {
            return NotFound("Artist or Album or Song is not found");
        }

        SongService.UpdateSong(id, song);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteSong(string artistId, string albumId, string id)
    {
        var artist = ArtistService.GetArtistById(artistId);
        var album = AlbumService.GetAlbumById(albumId);
        var song = SongService.GetSongById(id);

        if (artist is null || album is null || song is null)
        {
            return NotFound("Artist or Album or Song is not found");
        }

        SongService.DeleteSong(id);

        return NoContent();
    }
}