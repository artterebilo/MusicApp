using Enums.UserRole;
using System.ComponentModel.DataAnnotations;

namespace MusicApp.Contracts;

public class UserContract
{
    public string Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public UserRole Role { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
}

public interface IBaseUserModel
{
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
}
public class UserCreateContract : IBaseUserModel
{
    public string Login { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
}

public class UserUpdateContract : IBaseUserModel
{
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
}

public class UserUpdateEmailContract
{
    public string Email { get; set; }
}