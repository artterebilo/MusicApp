using DataBase.Models;
using Microsoft.AspNetCore.Mvc;
using MusicApp.Contracts;
using MusicApp.Services;

namespace MusicApp.Controllers;

[ApiController]
[Route("artist/{artistId}/albums/{albumId}/[controller]")]
public class SongsController : ControllerBase
{
    [HttpGet]
    public ActionResult<List<SongContract>> GetAllSongsForAlbum(string artistId, string albumId)
    {
        var album = AlbumService.GetAlbumById(albumId);

        if (album == null)
        {
            return NotFound();
        }

        return Ok(SongService.GetAllSongsForAlbum(albumId));
    }

    [HttpGet("{id}")]
    public ActionResult<SongContract> GetSongById(string artistId, string albumId, string id)
    {
        var song = SongService.GetSongById(id);

        if (song is null)
        {
            return NotFound();
        }

        return Ok(song);
    }

    [HttpPost]
    public IActionResult CreateSongs(string artistId, string albumId, List<SongCreateContract> songs)
    {
        var album = AlbumService.GetAlbumById(albumId);

        if (album == null)
        {
            return NotFound();
        }

        var newSongs = SongService.CreateSongs(albumId, songs);

        return Created("Songs", newSongs);
    }

    [HttpPut]
    public IActionResult UpdateSongs(string artistId, string albumId, List<SongUpdateContract> songs)
    {
        var artist = ArtistService.GetArtistById(artistId);
        var album = AlbumService.GetAlbumById(albumId);

        if (artist == null || album == null)
        {
            return NotFound();
        }

        SongService.UpdateSongs(songs);

        return NoContent();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateSong(string artistId, string albumId,  string id, SongUpdateContract song)
    {
        var existingSong = SongService.GetSongById(id);

        if(existingSong is null)
        {
            return NotFound();
        }

        SongService.UpdateSong(id, song);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteSong(string artistId, string albumId, string id)
    {
        var song = SongService.GetSongById(id);

        if (song is null)
        {
            return NotFound();
        }

        SongService.DeleteSong(id);

        return NoContent();
    }
}