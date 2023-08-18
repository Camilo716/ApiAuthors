using System.Text;
using ApiAuthors;
using ApiAuthors.DTOs;
using ApiAuthors.Models;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Test.Helpers;

public static class Utilities
{
    public static async Task CleanDatabase(WebApplicationFactory<Program> factory)
    {
        using var scope = factory.Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var db = scopedServices.GetRequiredService<ApplicationDbContext>();

        db.Authors.RemoveRange(db.Authors);
        db.Books.RemoveRange(db.Books);
        await db.SaveChangesAsync();
    }

    public static HttpContent GetAuthorHttpContent(string name)
    {
        var author = new AuthorRequestDTO{ Name = name };
        var jsonContent = JsonConvert.SerializeObject(author);
        HttpContent httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        return httpContent;
    }
}