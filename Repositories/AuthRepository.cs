using Api.Config;
using Api.Dtos;
using Api.Models;
using Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories;

public class AuthRepository(AppDbContext context) : IAuthRepository
{
    private readonly AppDbContext _context = context;

    public async Task<User?> FindUserByEmail(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(user => user.Email == email);
    }

    public async Task<bool> RegisterUser(User user)
    {
        var existingUser = await FindUserByEmail(user.Email);
        if (existingUser != null)
        {
            throw new Exception($"Email {user.Email} already exists");
        }

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<User?> GetAuthenticatedUser(string email)
    {
        return await _context.Users
            .Include(user => user.Roles)
            .ThenInclude(role => role.Permissions)
            .FirstOrDefaultAsync(user => user.Email == email);
    }
}