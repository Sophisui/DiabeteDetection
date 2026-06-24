using AuthService.Data;
using AuthService.Models;

namespace AuthService.Data;

public static class SeedData
{
    public static void Initialize(AuthDbContext context)
    {
        if (context.Users.Any()) return;

        context.Users.Add(new AppUser
        {
            Username = "admin",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
            Role = "Admin"
        });

        context.SaveChanges();
    }
}