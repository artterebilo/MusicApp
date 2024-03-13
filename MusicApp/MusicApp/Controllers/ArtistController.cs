using DataBase.Models;
using Microsoft.AspNetCore.Mvc;
using MusicApp.Contracts;
using MusicApp.Services;
using Utils;

namespace MusicApp.Controllers;

[ApiController]
[Route("[controller]")]


public class ArtistController : ControllerBase
{
    [HttpGet]
    public ActionResult<List<ArtistContract>> GetAllArtists([FromQuery] PaginationParams @params)
    {
        return ArtistService.GetAllArtists(@params);
    }

    [HttpPost]
    public IActionResult CreateArtist(ArtistCreateContract artist)
    {
        var newArtist = ArtistService.CreateArtist(artist);

        return Created("artist", newArtist);
    }

    [HttpGet("{id}")]
    public ActionResult<ArtistContract> GetArtistById(string id)
    {
        var artist = ArtistService.GetArtistById(id);

        if (artist is null)
        {
            return NotFound();
        }

        return Ok(artist);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateArtist(string id, ArtistUpdateContract updateArtist)
    {
        var existingArtist = ArtistService.GetArtistById(id);

        if (existingArtist is null)
        {
            return NotFound();
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
            return NotFound();
        }

        ArtistService.DeleteArtist(id);

        return NoContent();
    }
}
