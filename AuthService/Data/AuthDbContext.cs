using AuthService.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AuthService.Data;

public class AuthDbContext(DbContextOptions<AuthDbContext> options) : DbContext(options)
{
    public DbSet<AppUser> Users => Set<AppUser>();
}