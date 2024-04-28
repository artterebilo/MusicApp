using Microsoft.AspNetCore.Mvc;
using MusicApp.Services;

namespace MusicApp.Controllers;

[ApiController]
[Route("[controller]")]
public class AutentificationController : ControllerBase
{
    [HttpGet]
    public ActionResult<string> Login(string userId, string password)
    {
        var user = UserService.GetUserById(userId);

        if (user is null || user.Password != password)
        {
            return NotFound("This user not found or invalid password");
        }

        var jwtService = new JwtService(Constants.Issuer, Constants.Audience, Constants.SecretKey);

        var token = jwtService.GenerateToken(userId, user.Role.ToString());
        Console.WriteLine(user.Role);
        return Ok(token);
    }
}