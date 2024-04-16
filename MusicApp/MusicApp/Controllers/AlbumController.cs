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
            return NotFound(Resources.Validations.ArtistNotFoundByThisId);
        }

        return Ok(AlbumService.GetAllAlbumsByArtist(artistId));
    }

    [HttpPost]
    public IActionResult CreateAlbum(string artistId, AlbumCreateAndUpdateContract album)
    {
        var validator = new AlbumCreateAndUpdateValidation();
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
            return NotFound(Resources.Validations.NotPossibleToCreateAlbum);
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
            return NotFound(Resources.Validations.ArtistOrAlbumIsNotFound);
        }

        return Ok(album);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateAlbum(string artistId, string id, AlbumCreateAndUpdateContract updateAlbum)
    {
        var validator = new AlbumCreateAndUpdateValidation();
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
            return NotFound(Resources.Validations.ArtistOrAlbumIsNotFound);
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
            return NotFound(Resources.Validations.ArtistOrAlbumIsNotFound);
        }

        AlbumService.DeleteAlbum(id);

        return NoContent();
    }
}
