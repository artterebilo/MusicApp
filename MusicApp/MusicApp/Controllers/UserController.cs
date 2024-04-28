using Enums.UserRole;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MusicApp.Contracts;
using MusicApp.Services;
using MusicApp.Validations;
using System.Security.Claims;

namespace MusicApp.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IValidator<UserCreateContract> validator;
    private readonly IValidator<UserUpdateContract> updateValidator;

    public UsersController(IValidator<UserCreateContract> userCreateValidation, IValidator<UserUpdateContract> userUpdateValidation)
    {
        validator = userCreateValidation;
        updateValidator = userUpdateValidation;
    }


    [HttpGet]
    public ActionResult<List<UserContract>> GetAllUsers()
    {
        return Ok(UserService.GetAllUsers());
    }

    [HttpPost]
    public IActionResult CreateUser(UserCreateContract user)
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
        var newUser = UserService.CreateUser(user);

        return Created("users", newUser);
    }

    
    [HttpGet("{id}")]
    public ActionResult<UserContract> GetUserById(string id)
    {
        var requestContextUser = User;
        var contextUserClaimId = requestContextUser.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier);
        var contextUserId = contextUserClaimId.Value;
        var contextUserClaimRole = requestContextUser.Claims.First(claim => claim.Type == ClaimTypes.Role);
        var contextUserRole = contextUserClaimRole.Value;

        if (contextUserRole == UserRole.Admin.ToString()
            || contextUserRole == UserRole.User.ToString()
            || contextUserRole == UserRole.Artist.ToString()
            && id == contextUserId)
        {
            var user = UserService.GetUserById(id);
            if (user is null)
            {
                return NotFound("User Not Found");
            }

            return Ok(user);
        }

        return Unauthorized();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateUser(string id, UserUpdateContract user)
    {
        var requestContextUser = User;
        var contextUserClaimId = requestContextUser.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier);
        var contextUserId = contextUserClaimId.Value;
        var contextUserClaimRole = requestContextUser.Claims.First(claim => claim.Type == ClaimTypes.Role);
        var contextUserRole = contextUserClaimRole.Value;

        if (contextUserRole == UserRole.Admin.ToString()
            || contextUserRole == UserRole.User.ToString()
            || contextUserRole == UserRole.Artist.ToString()
            && id == contextUserId)
        {
            var result = updateValidator.Validate(user);
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

            var existingUser = UserService.GetUserById(id);

            if (existingUser is null)
            {
                return NotFound();
            }

            UserService.UpdateUser(id, user);

            return NoContent();
        }

        return Unauthorized();
    }

    [HttpPut("{id}/updateEmail")]
    public IActionResult UpdateUserEmail(string id, UserUpdateEmailContract email)
    {
        var requestContextUser = User;
        var contextUserClaimId = requestContextUser.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier);
        var contextUserId = contextUserClaimId.Value;
        var contextUserClaimRole = requestContextUser.Claims.First(claim => claim.Type == ClaimTypes.Role);
        var contextUserRole = contextUserClaimRole.Value;

        if (contextUserRole == UserRole.Admin.ToString()
            || contextUserRole == UserRole.User.ToString()
            || contextUserRole == UserRole.Artist.ToString()
            && id == contextUserId)
        {
            var emailValidator = new UserUpdateEmailValidation();
            var result = emailValidator.Validate(email);
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
            var existingUser = UserService.GetUserById(id);

            if (existingUser is null)
            {
                return NotFound();
            }

            UserService.UpdateUserEmail(id, email);

            return NoContent();
        }

        return Unauthorized();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(string id)
    {
        var existingUser = UserService.GetUserById(id);

        if (existingUser is null)
        {
            return NotFound();
        }

        UserService.DeleteUser(id);

        return NoContent();
    }
}
