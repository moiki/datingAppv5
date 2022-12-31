using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DataContext :  IdentityDbContext<AppUser, AppRole, int, 
    IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>, 
    IdentityRoleClaim<int>, IdentityUserToken<int>>
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<AppUserRole>(userRole =>
        {
            userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

            userRole.HasOne(ur => ur.Role)
                .WithMany(r => r.AppUserRoles)
                .HasForeignKey(ur => ur.RoleId).IsRequired();

            userRole.HasOne(ur => ur.User)
                .WithMany(r => r.AppUserRoles)
                .HasForeignKey(ur => ur.UserId).IsRequired();
        });
        modelBuilder.Entity<AppUser>(model =>
        {
            model.HasMany(prop => prop.AppUserRoles)
                .WithOne(prop => prop.User)
                .HasForeignKey(prop => prop.UserId).IsRequired();
            model.Property(p => p.BirthDate).HasConversion(
                src => src.Kind == DateTimeKind.Utc ? src : DateTime.SpecifyKind(src, DateTimeKind.Utc),
                dst => dst.Kind == DateTimeKind.Utc ? dst : DateTime.SpecifyKind(dst, DateTimeKind.Utc)
            );
            model.Property(p => p.Created).HasConversion(
                src => src.Kind == DateTimeKind.Utc ? src : DateTime.SpecifyKind(src, DateTimeKind.Utc),
                dst => dst.Kind == DateTimeKind.Utc ? dst : DateTime.SpecifyKind(dst, DateTimeKind.Utc)
            );
            model.Property(p => p.LastActive).HasConversion(
                src => src.Kind == DateTimeKind.Utc ? src : DateTime.SpecifyKind(src, DateTimeKind.Utc),
                dst => dst.Kind == DateTimeKind.Utc ? dst : DateTime.SpecifyKind(dst, DateTimeKind.Utc)
            );
        });

        modelBuilder.Entity<AppRole>(model =>
        {
            model.HasMany(prop => prop.AppUserRoles)
                .WithOne(prop => prop.Role)
                .HasForeignKey(prop => prop.RoleId).IsRequired();
        });
    }
}