using Microsoft.AspNetCore.Mvc;
using MusicApp.Contracts;
using MusicApp.Services;
using MusicApp.Validations;

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
            return NotFound("No artist has been found for this ID");
        }

        return Ok(AlbumService.GetAllAlbumsByArtist(artistId));
    }

    [HttpPost]
    public IActionResult CreateAlbum(string artistId, AlbumCreateContract album)
    {
        var validator = new AlbumCreateValidation();
        var result = validator.Validate(album);
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

        if (artist is null)
        {
            return NotFound("It was not possible to create an album of this artist, so the id was not found.");
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
            return NotFound("ArtistId or AlbumId not found");
        }

        return Ok(album);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateAlbum(string artistId, string id, AlbumUpdateContract updateAlbum)
    {
        var validator = new AlbumUpdateValidation();
        var result = validator.Validate(updateAlbum);
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

        var album = AlbumService.GetAlbumById(id);
        var artist = ArtistService.GetArtistById(artistId);

        if (artist is null || album is null)
        {
            return NotFound("ArtistId or AlbumId not found");
        }

        AlbumService.UpdateAlbum(id, updateAlbum);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteAlbum(string artistId, string id)
    {
        var album = AlbumService.GetAlbumById(id);
        var artist = ArtistService.GetArtistById(artistId);

        if (artist is null || album is null)
        {
            return NotFound("ArtistId or AlbumId not found");
        }

        AlbumService.DeleteAlbum(id);

        return NoContent();
    }
}
