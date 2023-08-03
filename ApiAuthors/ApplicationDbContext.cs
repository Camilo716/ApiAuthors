using ApiAuthors.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiAuthors;

public class ApplicationDbContext : DbContext
{
    public DbSet<AuthorModel> Authors { get; set; }
    public DbSet<BookModel>  Books{ get; set; }
    public ApplicationDbContext(DbContextOptions options) : base(options) { }
}

