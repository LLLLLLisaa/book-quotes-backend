using BookQuotesBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace BookQuotesBackend.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Book> Books { get; set; } = null!;
    public DbSet<Quote> Quotes { get; set; } = null!;
}