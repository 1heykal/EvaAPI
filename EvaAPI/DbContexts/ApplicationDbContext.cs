using EvaAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace EvaLibrary.DbContexts;

public class ApplicationDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Member> Members { get; set; }
    public DbSet<Borrow> Borrows { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicationUser>()
            .Ignore(u => u.ProfilePicture);
        
        
        
        base.OnModelCreating(modelBuilder);
    }
}