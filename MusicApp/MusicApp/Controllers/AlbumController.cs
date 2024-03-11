using Microsoft.AspNetCore.Mvc;
using MusicApp.Contracts;
using MusicApp.Services;

namespace MusicApp.Controllers;

[ApiController]
[Route("artist/{artistId}/[controller]")]
public class AlbumController : ControllerBase
{
    [HttpGet]
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
    public IActionResult CreateAlbum(string artistId, AlbumCreateContract album)
    {
        var artist = ArtistService.GetArtistById(artistId);

        if (artist is null)
        {
            return NotFound();
        }

        var newAlbum = AlbumService.CreateAlbum(artistId, album);

        return Created("Album", newAlbum);
    }

    [HttpGet("{id}")]
    public ActionResult<AlbumContract> GetAlbumById(string artistId, string id)
    {
        var album = AlbumService.GetAlbumById(id);

        if (album is null || album.ArtistId != artistId)
        {
            return NotFound();
        }

        return Ok(album);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateAlbum(string artistId, string id, AlbumUpdateContract updateAlbum)
    {
        var album = AlbumService.GetAlbumById(id);
        var artist = ArtistService.GetArtistById(artistId);

        if (artist is null || album is null)
        {
            return NotFound();
        }

        AlbumService.UpdateAlbum(id, updateAlbum);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteAlbum(string artistId, string id)
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
