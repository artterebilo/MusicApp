using Microsoft.AspNetCore.Mvc;
using MusicApp.Contracts;
using MusicApp.Services;
using System.Collections.Specialized;
using Utils;

namespace MusicApp.Controllers;

[ApiController]
[Route("user")]
public class UserLikeSongController : ControllerBase
{
    [HttpPost("/favorites/songs/update")]
    public IActionResult UserChooseLikeStatus(UserLikeSongContract user)
    {
        var existingUser = UserService.GetUserById(user.UserId);
        var existingSong = SongService.GetSongById(user.SongId);

        if (existingUser is null || existingSong is null)
        {
            return NotFound(); 
        }


        UserLikeSongService.UpdateCreateDeleteLike(user);
        return Ok();
    }

    [HttpGet("{id}/favorites/songs")]
    public ActionResult<AllSongsLikedUserContract> GetSongsLikedUser(string id, [FromQuery] LikePagination inputParams)
    {
        var songs = UserLikeSongService.GetAllSongsLikedUser(inputParams, id);

        if (songs is null)
        {
            return NotFound("Songs NotFound");
        }

        return Ok(songs);
    }
}
