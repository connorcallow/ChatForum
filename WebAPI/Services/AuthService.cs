using System.ComponentModel.DataAnnotations;
using Application.DaoInterfaces;
using Domain.Models;

namespace WebAPI.Services;

public class AuthService : IAuthService
{
    private readonly IUserDao userDao; // Inject your user data access class

    public AuthService(IUserDao userDao)
    {
        this.userDao = userDao;
    }

    public async Task<User> GetUser(string username, string password)
    {
        User? existingUser = await userDao.GetByUsernameAsync(username);

        if (existingUser == null)
        {
            throw new Exception("User not found");
        }

        if (!existingUser.Password.Equals(password))
        {
            throw new Exception("Password mismatch");
        }

        return existingUser;
    }
}