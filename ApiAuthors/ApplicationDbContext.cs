using ApiAuthors.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiAuthors;

public class ApplicationDbContext : DbContext
{
    public DbSet<AuthorModel> authors { get; set; }
    public DbSet<BookModel> books { get; set; }
    public ApplicationDbContext(DbContextOptions options) : base(options) { }
}

