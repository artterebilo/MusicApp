using Enums.UserRole;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicApp.Contracts;
using MusicApp.Services;
using MusicApp.Validations;
using System.Collections.Specialized;
using System.Security.Claims;
using Utils;

namespace MusicApp.Controllers;

[Authorize]
[ApiController]
[Route("user")]
public class UserLikeSongController : ControllerBase
{
    private readonly IValidator<UserLikeSongContract> validator;
    private readonly IValidator<LikePagination> likeValidator;

    public UserLikeSongController(IValidator<UserLikeSongContract> validator, IValidator<LikePagination> likeValidator)
    {
        this.validator = validator;
        this.likeValidator = likeValidator;
    }

    [HttpPost("favorites/songs/update")]
    public IActionResult UserChooseLikeStatus(UserLikeSongContract user)
    {
        var requestContextUser = User;
        var contextUserClaimId = requestContextUser.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier);
        var contextUserId = contextUserClaimId.Value;
        var contextUserClaimRole = requestContextUser.Claims.First(claim => claim.Type == ClaimTypes.Role);
        var contextUserRole = contextUserClaimRole.Value;

        if (contextUserRole == UserRole.User.ToString() && contextUserId == user.UserId)
        {
            var result = validator.Validate(user);
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
            var existingUser = UserService.GetUserById(user.UserId);
            var existingSong = SongService.GetSongById(user.SongId);

            if (existingUser is null || existingSong is null)
            {
                return NotFound();
            }

            UserLikeSongService.UpdateCreateDeleteLike(user);
            return Ok();
        }

        return Unauthorized();
    }

    [HttpGet("{id}/favorites/songs")]
    public ActionResult<AllLikeSongsUserContract> GetSongsLikedUser(string id, [FromQuery] LikePagination inputParams)
    {
        var requestContextUser = User;
        var contextUserClaimId = requestContextUser.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier);
        var contextUserId = contextUserClaimId.Value;
        var contextUserClaimRole = requestContextUser.Claims.First(claim => claim.Type == ClaimTypes.Role);
        var contextUserRole = contextUserClaimRole.Value;


        if (contextUserRole == UserRole.User.ToString()
            || contextUserRole == UserRole.Admin.ToString()
            && contextUserId == id)
        {
            var result = likeValidator.Validate(inputParams);
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
            var songs = UserLikeSongService.GetAllSongsLikedUser(inputParams, id);

            if (songs is null)
            {
                return NotFound("User or Songs NotFound");
            }

            return Ok(songs);
        }

        return Unauthorized();
    }
}
