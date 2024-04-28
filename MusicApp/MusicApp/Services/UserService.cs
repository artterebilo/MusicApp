using DataBase.Models;
using DataBase.Repositories;
using MusicApp.Contracts;
using Utils;

namespace MusicApp.Services;

public class UserService
{
    public static bool IsUniqueLogin(string login)
    {
        return UserRepository.IsUniqueLogin(login);
    }
    public static bool IsUniqueEmail(string email)
    {
        return UserRepository.IsUniqueEmail(email);
    }
    public static List<UserContract> GetAllUsers()
    {
        return UserRepository
            .GetAllUsers()
            .Select(MapToUserContract)
            .ToList();
    }
    public static UserContract CreateUser(UserCreateContract User)
    {
        var newUser = new UserModel
        {
            Id = IdGenerator.Id(),
            Login = User.Login,
            Password = User.Password,
            Email = User.Email,
            FirstName = User.FirstName,
            LastName = User.LastName,
            DateOfBirth = User.DateOfBirth,
        };

        UserRepository.CreateUser(newUser);

        return MapToUserContract(newUser);
    }

    public static UserContract GetUserById(string id)
    {
        return MapToUserContract(UserRepository.GetUserById(id));
            
    }

    public static void UpdateUser(string id, UserUpdateContract user)
    {
        UserRepository.UpdateUser(MapFromUserUpdateContractToUserModel(id, user));
    }

    public static void UpdateUserEmail(string id, UserUpdateEmailContract user)
    {
        UserRepository.UpdateUserEmail(MapFromUserUpdateEmailContractToUserModel(id, user));
    }

    public static void DeleteUser(string id)
    {
        UserRepository.DeleteUser(id);
    }

    private static UserModel MapFromUserUpdateEmailContractToUserModel(string id, UserUpdateEmailContract user)
    {
        if (user is null)
        {
            return null;
        }

        var existingUser = UserService.GetUserById(id);

        return new UserModel
        {
            Id = id,
            Login = existingUser.Login,
            Password = existingUser.Password,
            Email = user.Email,
            FirstName = existingUser.FirstName,
            LastName = existingUser.LastName,
            DateOfBirth = existingUser.DateOfBirth
        };
        
    }
    private static UserModel MapFromUserUpdateContractToUserModel(string id, UserUpdateContract user)
    {
        if (user is null)
        {
            return null;
        }
        var existingUser = UserService.GetUserById(id);
        return new UserModel
        {
            Id = id,
            Login = existingUser.Login,
            Password = user.Password,
            FirstName = user.FirstName,
            LastName = user.LastName,
            DateOfBirth = user.DateOfBirth
        };
    }
    private static UserContract MapToUserContract(UserModel user)
    {
        if (user is null)
        {
            return null;
        }

        return new UserContract
        {
            Id = user.Id,
            Login = user.Login,
            Password = user.Password,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            DateOfBirth = user.DateOfBirth,
            Role = user.Role
        };
    }
}
