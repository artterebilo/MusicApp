using Microsoft.AspNetCore.Mvc;
using MusicApp.Contracts;
using MusicApp.Services;

namespace MusicApp.Controllers;

[ApiController]
[Route("[controller]")]

public class AlbumController : ControllerBase
{
    [HttpGet("by-artist/{artistId}")]
    public ActionResult<List<AlbumContract>> GetAllAlbumsByArtist(string artistId)
    {
        var artist = ArtistService.GetArtistById(artistId);

        if (artist == null)
        {
            return NotFound();
        }

        return Ok(AlbumService.GetAllAlbumsByArtist(artistId));
    }

    [HttpPost]
    public IActionResult CreateAlbum(AlbumCreateContract album)
    {
        var artist = ArtistService.GetArtistById(album.ArtistId);

        if (artist is null)
        {
            return NotFound();
        }

        var newAlbum = AlbumService.CreateAlbum(album);

        return Created("Album", newAlbum);
    }

    [HttpGet("{id}")]
    public ActionResult<AlbumContract> GetAlbumById(string id)
    {
        var album = AlbumService.GetAlbumById(id);

        if (album is null)
        {
            return NotFound();
        }

        return Ok(album);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateAlbum(string id, AlbumUpdateContract updateAlbum)
    {
        var album = AlbumService.GetAlbumById(id);
        var artist = ArtistService.GetArtistById(updateAlbum.ArtistId);

        if (artist is null || album is null)
        {
            return NotFound();
        }

        AlbumService.UpdateAlbum(id, updateAlbum);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteAlbum(string id)
    {
        var album = AlbumService.GetAlbumById(id);

        if (album is null)
        {
            return NotFound();
        }

        AlbumService.DeleteAlbum(id);

        return NoContent();
    }
}
