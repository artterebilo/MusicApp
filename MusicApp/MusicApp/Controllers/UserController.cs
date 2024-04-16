using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MusicApp.Contracts;
using MusicApp.Services;
using MusicApp.Validations;

namespace MusicApp.Controllers;

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
        var user = UserService.GetUserById(id);

        if (user is null)
        {
            return NotFound("User Not Found");
        }

        return Ok(user);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateUser(string id, UserUpdateContract user)
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

    [HttpPut("{id}/updateEmail")]
    public IActionResult UpdateUserEmail(string id, UserUpdateEmailContract email)
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
