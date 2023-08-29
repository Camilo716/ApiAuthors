
using System.Text;
using ApiAuthors.DTOs;
using Newtonsoft.Json;
using Test.Helpers;

namespace Test;

public class Get_EnpointsTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public Get_EnpointsTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory; 
    }

    [Theory]
    [InlineData("/Api/Author")]
    [InlineData("/Api/Author/first")]
    [InlineData("/Api/Author/Kent")]
    [InlineData("/Api/Book/1/Comment")]
    public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
    {
        await Utilities.CleanDatabase(_factory);
        var client = _factory.CreateClient();
        var kentAuthor = Utilities.GetAuthorHttpContent("Kent");
        await client.PostAsync("/Api/Author", kentAuthor);

        try
        {
            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/json; charset=utf-8", 
                response.Content.Headers.ContentType?.ToString());
        }
        finally
        {
            await Utilities.CleanDatabase(_factory);
        }
    }
}