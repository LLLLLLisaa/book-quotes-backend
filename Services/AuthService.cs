using BookQuotesBackend.DTOs;
using BookQuotesBackend.Data;
using BookQuotesBackend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookQuotesBackend.Services;

public class AuthService
{
    private readonly AppDbContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;

    public AuthService(AppDbContext context,IPasswordHasher<User> passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

   public async Task<bool> Register(RegisterRequest request)
{
    var existingUser = await _context.Users
        .FirstOrDefaultAsync(u => u.Email == request.Email);

    if (existingUser != null)
    {
        return false;
    }

    var user = new User
    {
        FullName = request.FullName,
        Email = request.Email
    };

    user.PasswordHash =
        _passwordHasher.HashPassword(user, request.Password);

    _context.Users.Add(user);

    var savedRows = await _context.SaveChangesAsync();

    return true;
}

    internal async Task<bool> Login(LoginRequest request)
    {
         var user = await _context.Users
        .FirstOrDefaultAsync(u => u.Email == request.Email);

        if (user == null)
        {
            return false;
        }

        var result = _passwordHasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            request.Password
        );

        return result == PasswordVerificationResult.Success;
    }
}