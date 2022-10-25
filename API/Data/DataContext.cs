using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DataContext: DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<AppUser> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppUser>(model =>
        {
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
        
        
    }
}