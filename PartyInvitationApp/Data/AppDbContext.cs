using Microsoft.EntityFrameworkCore;
using PartyInvitationApp.Models;  

namespace PartyInvitationApp.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Party> Parties { get; set; }
    public DbSet<Invitation> Invitations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Invitation>()
            .Property(i => i.Status)
            .HasConversion<string>()  // Store enum as string
            .HasMaxLength(64);
    }
}
