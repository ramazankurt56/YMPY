using HospitalServer.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace HospitalServer.DataAccess.Context;
public class ApplicationDbContext : IdentityDbContext<AppUser,AppRole,Guid>
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Examination> Examination { get; set; }
    public DbSet<Medication> Medication { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>().HasQueryFilter(filter => !filter.IsDeleted);
        modelBuilder.Entity<Doctor>().HasQueryFilter(filter => !filter.IsDeleted);
        modelBuilder.Entity<Examination>().HasQueryFilter(filter => !filter.IsDeleted);
        modelBuilder.Entity<Medication>().HasQueryFilter(filter => !filter.IsDeleted);
        modelBuilder.Entity<Patient>().HasQueryFilter(filter => !filter.IsDeleted);
        modelBuilder.Entity<Prescription>().HasQueryFilter(filter => !filter.IsDeleted);
        modelBuilder.Ignore<IdentityUserRole<Guid>>();
        modelBuilder.Ignore<IdentityRoleClaim<Guid>>();
        modelBuilder.Ignore<IdentityUserClaim<Guid>>();
        modelBuilder.Ignore<IdentityUserLogin<Guid>>();
        modelBuilder.Ignore<IdentityUserToken<Guid>>();
    }
}
