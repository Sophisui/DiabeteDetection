using Microsoft.EntityFrameworkCore;
using PatientService.Models;

namespace PatientService.Data;

public class PatientDbContext(DbContextOptions<PatientDbContext> options) : DbContext(options)
{
    public DbSet<Patient> Patients => Set<Patient>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Gender).IsRequired().HasMaxLength(10);
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
        });

        // Les 4 patients de test
        modelBuilder.Entity<Patient>().HasData(
            new Patient { Id = 1, FirstName = "Test", LastName = "TestNone", DateOfBirth = new DateOnly(1966, 12, 31), Gender = "F", Address = "1 Brookside St", PhoneNumber = "100-222-3333" },
            new Patient { Id = 2, FirstName = "Test", LastName = "TestBorderline", DateOfBirth = new DateOnly(1945, 6, 24), Gender = "M", Address = "2 High St", PhoneNumber = "200-333-4444" },
            new Patient { Id = 3, FirstName = "Test", LastName = "TestInDanger", DateOfBirth = new DateOnly(2004, 6, 18), Gender = "M", Address = "3 Club Road", PhoneNumber = "300-444-5555" },
            new Patient { Id = 4, FirstName = "Test", LastName = "TestEarlyOnset", DateOfBirth = new DateOnly(2002, 6, 28), Gender = "F", Address = "4 Valley Dr", PhoneNumber = "400-555-6666" }
        );
    }
}