using DataBase.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using MusicApp.Contracts;
using MusicApp.Services;
using MusicApp.Validations;
using System.Diagnostics.Contracts;
using Utils;

namespace MusicApp.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class ArtistController : ControllerBase
{
    static ArtistPaginationContract MapToPaginationContract(ArtistContract artists)
    {
        return new ArtistPaginationContract
        {
            Id = artists.Id,
            Name = artists.Name,
            Description = artists.Description,
            Genres = artists.Genres
        };
    }

    [HttpGet]
    public ActionResult<ArtistsPaginationContract> GetAllArtistWithPagination([FromQuery] AlbumPagination inputParams)
    {
        var validator = new AtristsPaginationValidation();
        var result = validator.Validate(inputParams);
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

        var artists = ArtistService.GetAllArtists(inputParams);

        var paginationsArtist = new ArtistsPaginationContract
        {
            TotalCount = artists.Count,
            PageNumber = inputParams.PageNumber,
            PageSize = inputParams.PageSize,
            PageCount = (int)Math.Ceiling((double)artists.Count / (double)inputParams.PageSize),
            Artists = artists.Select(MapToPaginationContract).ToList()
        };

        return paginationsArtist;
    }

    [HttpPost]
    public IActionResult CreateArtist(ArtistCreateAndUpdateContract artist)
    {
        var validator = new ArtistCreateAndUpdateValidation();
        var result = validator.Validate(artist);
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

        var newArtist = ArtistService.CreateArtist(artist);

        return Created("artist", newArtist);
    }

    [HttpGet("{id}")]
    public ActionResult<ArtistContract> GetArtistById(string id)
    {
        var artist = ArtistService.GetArtistById(id);

        if (artist is null)
        {
            return NotFound(Resources.Validations.ArtistNotFoundByThisId);
        }

        return Ok(artist);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateArtist(string id, ArtistCreateAndUpdateContract updateArtist)
    {
        var validator = new ArtistCreateAndUpdateValidation();
        var result = validator.Validate(updateArtist);
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

        var existingArtist = ArtistService.GetArtistById(id);

        if (existingArtist is null)
        {
            return NotFound(Resources.Validations.ArtistNotFoundByThisId);
        }

        ArtistService.UpdateArtist(id, updateArtist);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteArtist(string id)
    {
        var artist = ArtistService.GetArtistById(id);

        if (artist is null)
        {
            return NotFound(Resources.Validations.ArtistNotFoundByThisId);
        }

        ArtistService.DeleteArtist(id);

        return NoContent();
    }
}
