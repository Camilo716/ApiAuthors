using ApiAuthors.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ApiAuthors;

public class ApplicationDbContext : IdentityDbContext    
{
    public DbSet<AuthorModel> Authors { get; set; }
    public DbSet<BookModel>  Books{ get; set; }
    public DbSet<CommentModel>  Comments{ get; set; }
    
    public ApplicationDbContext(DbContextOptions options) : base(options) { }

    protected override void  OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}

