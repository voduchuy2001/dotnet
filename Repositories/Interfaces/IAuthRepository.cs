using Api.Models;

namespace Api.Repositories.Interfaces;

public interface IAuthRepository
{
    Task<User?> FindUserByEmail(string email);
    Task<bool> RegisterUser(User user);
}